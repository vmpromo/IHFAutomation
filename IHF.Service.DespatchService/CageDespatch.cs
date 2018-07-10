// Name       : CageDespatch.cs
// Type       : Class file 
// Description: Class file for Cage despatch in Despatch windows service
//
//$Revision:   1.11  $
//-------------------------------------------------------------------------------------------------
// Version  | Date        | Author       | Reason
//-------------------------------------------------------------------------------------------------
// 1.9      | 20/09/11    | M. Khan      | Version in production
// 1.10     | 03/10/11    | M. Khan      | getting all consignments now from database
//          |             |              | and splitting them in batches of 100 before 
//          |             |              | sending to metapack
// 1.11     | 16/10/11    | M. Khan      | Reference to new proxy class as metapack changed WSDLs
//-------------------------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Net;
using System.ServiceProcess;
using System.Threading;
using Com.MetaPack.DeliveryManager;
using IHF.BusinessLayer.DataAccessObjects.Despatch;
using System.Collections.Generic;
using System.Linq;

namespace IHF.Service.DespatchService
{
    public partial class CageDespatch : ServiceBase
    {
        Despatch.Com.MetaPack.DeliveryManager.DeliveryManagerClient dm = null;

        private DespatchServiceDAO     despatchServiceDAO;
        private System.Threading.Timer timer;
        private TimerCallback          timeCallback;
        EventLog                       eventlog;
        private string                 diagnosticMode         = String.Empty;



        private const string serviceName       = "DESPATCH SERVICE";
        private const string shortErrorMessage = "Despatch service executed with errors";

        public CageDespatch()
        {
            InitializeComponent();
            SetEventLogInstance();
        }

        private void UpdateStatus(string user)
        {
            try
            {
                despatchServiceDAO.UpdateServiceStatus(user);
            }
            catch (Exception exception)
            {
                LogError(shortErrorMessage,
                             "Failed to update service status. " + exception.Message);
                eventlog.WriteEntry(shortErrorMessage + ": " + "Failed to update service status. " + exception.Message);
            }
        }


        private string[] GetconsignmentArray(IDataReader consignments)
        {
            ArrayList consignmentArray = new ArrayList();
            while (consignments.Read())
            {
                if (consignments[2].ToString() == "170")
                    consignmentArray.Add(consignments[0].ToString());
            }

            return consignmentArray.ToArray(typeof(string)) as string[];
        }

        private ICredentials GetMetapackCredentials()
        {
            string[] credentials = ConfigurationManager.AppSettings["MPackCredential"]
                                                       .Split(';');
            return new NetworkCredential(credentials[0],
                                         credentials[1]);
        }

        private void SetEventLogInstance()
        {
            eventlog = new EventLog();

            if (!System.Diagnostics.EventLog.SourceExists("IHF Automation Despatch service"))
                System.Diagnostics
                      .EventLog
                      .CreateEventSource("IHF Automation Despatch service",
                                         "Application");

            eventlog.Source = "IHF Automation Despatch service";
        }

        private void LogError(string shortMessage, string longMessage)
        {
            longMessage = longMessage.Length > 255 ? longMessage.Substring(0, 254) : longMessage;
            despatchServiceDAO.WriteToErrorLog(1,
                                               serviceName,
                                               shortMessage,
                                               longMessage);
        }

        private void InitialiseMetapack()
        {
            string metapackUrl = ConfigurationManager.AppSettings["MPackUrl"].ToString();

            dm = new Despatch.Com.MetaPack.DeliveryManager.DeliveryManagerClient(metapackUrl, GetMetapackCredentials());
        }

        /// <summary>
        /// Writes to event log only when diagnostic mode is switched ON
        /// </summary>
        /// <param name="text">Text to write to event log</param>
        private void WriteToLog(string text)
        {
            if (diagnosticMode.ToLower() == "true")
            {
                eventlog.WriteEntry(text);
            }
        }


        private void StartDespatchProcess(object stateObject)
        {

            IDataReader carriers = null;
            string user = String.Empty;
            string warehouseCode = String.Empty;
            string transactionType = String.Empty;
            int batchSize = 0;


            try
            {
                despatchServiceDAO = new DespatchServiceDAO();
                user = ConfigurationManager.AppSettings["UserLogin"];
                warehouseCode = ConfigurationManager.AppSettings["WarehouseCode"];
                transactionType = ConfigurationManager.AppSettings["TransactionType"];
                diagnosticMode = ConfigurationManager.AppSettings["DiagnosticMode"];
                batchSize = int.Parse(ConfigurationManager.AppSettings["BatchSize"].ToString());


                carriers = despatchServiceDAO.GetCarriersForDespatch();
            }
            catch (ConfigurationErrorsException exception)
            {
                LogError(shortErrorMessage,
                         exception.Message);
                eventlog.WriteEntry(shortErrorMessage + ": " + exception.Message);
                return;
            }
            catch (Exception exception)
            {
                eventlog.WriteEntry(shortErrorMessage + ": " + exception.Message + "  " + exception.StackTrace);
                return;
            }

            try
            {
                InitialiseMetapack();
            }
            catch (ConfigurationErrorsException exception)
            {
                LogError(shortErrorMessage,
                         exception.Message);
                eventlog.WriteEntry(shortErrorMessage + ": " + exception.Message);
                return;
            }

            WriteToLog("Despatch Service Executed");

            while (carriers.Read())
            {
                WriteToLog("Carrier - " + carriers[0].ToString());
                string[] consignmentArray = null;

                IDataReader consignments = null;
                try
                {
                    consignments = despatchServiceDAO.GetConsignments(carriers[0].ToString());
                    consignmentArray = GetconsignmentArray(consignments);
                    WriteToLog("Total Consignments fetched - " + consignmentArray.Length);

                }
                catch (Exception exception)
                {
                    eventlog.WriteEntry(carriers[0].ToString() + "    " + shortErrorMessage + ": " + exception.Message + "  " + exception.StackTrace);
                    continue;
                }

                try
                {
                    if (consignmentArray.Length > 0)
                    {
                        //continue;

                        //
                        //splitting array in to batches of size defined in app.config
                        List<string> consignmentList = consignmentArray.ToList<string>();

                        for (int i = 0; i < consignmentList.Count; i += batchSize)
                        {
                            IEnumerable<string> currentBatch = consignmentList.Skip(i).Take(batchSize);
                            string[] consignmentBatch = currentBatch.ToArray();
                            WriteToLog("No. of consignments in batch selected - " + consignmentBatch.Length);
                            WriteToLog("Batch of Consignments - " + string.Join(",", consignmentBatch));


                            dm.ConsignmentService.markConsignmentsAsReadyToManifest(consignmentBatch);
                            //update consgmt in database as mark for manifest - 180

                            foreach (string consignment in consignmentBatch)
                            {

                                despatchServiceDAO.UpdateConsignmentstatus(consignment.Trim(), 180, user);
                            }

                            WriteToLog("Marked for manifest");

                        }

                        //


                    }
                }
                catch (Exception exception)
                {
                    HandleError(carriers[0].ToString(), exception.Message, true, true, true, user);
                    if (exception.Message.Contains("E20094") == false)
                        continue;
                }

                try
                {

                    decimal futureOffSet = despatchServiceDAO.GetMaxDespatchOffSet();
                    WriteToLog("off set");
                    
                    string[] manifestCodes = dm.ManifestService.createManifestForFutureDespatch(carriers[0].ToString(),
                                                                          warehouseCode,
                                                                          transactionType,
                                                                          DateTime.Today.AddDays((double)futureOffSet),
                                                                          false);

                    if (manifestCodes != null && manifestCodes.Length > 0)
                    {
                        WriteToLog("Manifested - " + string.Join(",", manifestCodes));

                        string joinedCodes = String.Join(",", manifestCodes);

                        WriteToLog("Carrier code passed to add manifest codes - " + carriers[0].ToString());
                        despatchServiceDAO.AddManifestCodes(carriers[0].ToString(), joinedCodes, user);
                        WriteToLog("Manifest codes added...");

                    }
                    //else
                    //{
                    //    LogError(shortErrorMessage,
                    //        "Manifest codes not returned by Metapack");
                    //    eventlog.WriteEntry(carriers[0].ToString() + "    " + shortErrorMessage + ": " + "Manifest codes not returned by Metapack");
                    //}

                    despatchServiceDAO.Despatch(carriers[0].ToString(), user);
                    WriteToLog("Despatched");



                    //if (manifestCodes!= null && manifestCodes.Length > 0)
                    //{
                    //    eventlog.WriteEntry("Carrier code passed to add manifest codes - " + carriers[0].ToString());
                    //    despatchServiceDAO.AddManifestCodes(carriers[0].ToString(), joinedCodes, user);
                    //    eventlog.WriteEntry("Manifest codes added...");

                    //}
                    //else {
                    //    LogError(shortErrorMessage,
                    //        "Manifest codes not returned by Metapack");
                    //    eventlog.WriteEntry(carriers[0].ToString() + "    " + shortErrorMessage + ": " + "Manifest codes not returned by Metapack");
                    //}

                }
                catch (Exception exception)
                {
                    LogError(shortErrorMessage,
                             exception.Message);
                    eventlog.WriteEntry(carriers[0].ToString() + "    " + shortErrorMessage + ": " + exception.Message);
                    continue;
                }

            }

            UpdateStatus(user);
        }

        private void HandleError(string carrier, string message, bool HandleMetapackError, bool logDBError, bool writeToEventLog, string user)
        {
            try
            {
                if (HandleMetapackError)
                {
                    int len = 0;
                    string consignmentNumber = string.Empty;

                    if (message.Contains("E20094"))
                    {
                        len = message.IndexOf("as") - 24;
                        consignmentNumber = message.Substring(24, len);
                        eventlog.WriteEntry("Befor update to failed");
                        despatchServiceDAO.UpdateConsignmentstatus(consignmentNumber.Trim(), 180, user);
                        eventlog.WriteEntry("after update to failed");
                    }

                    if (message.Contains("E20010"))
                    {
                        len = message.IndexOf("cannot") - 19;
                        consignmentNumber = message.Substring(19, len);
                        eventlog.WriteEntry("Befor update to failed");
                        despatchServiceDAO.UpdateConsignmentstatus(consignmentNumber.Trim(), 210, user);
                        eventlog.WriteEntry("after update to failed");
                    }

                    /*if (consignmentNumber != string.Empty)
                    {
                        eventlog.WriteEntry("Befor update to failed");
                        despatchServiceDAO.UpdateConsignmentstatus(consignmentNumber.Trim(),210, user);
                        eventlog.WriteEntry("after update to failed");
                    }*/
                }

                if (logDBError)
                {
                    LogError(shortErrorMessage,
                             message);
                }

                if (writeToEventLog)
                {
                    eventlog.WriteEntry(carrier + "    " + shortErrorMessage + ": " + message);
                }
            }
            catch (Exception exception)
            {
                eventlog.WriteEntry(carrier + "    " + shortErrorMessage + ": " + exception.Message);
            }
        }

        protected override void OnStart(string[] args)
        {
            int timeInterval = Convert.ToInt32(ConfigurationManager.AppSettings["TimeInterval"]);
            timeCallback = new TimerCallback(StartDespatchProcess);
            timer = new System.Threading.Timer(timeCallback,
                                                            null,
                                                            1000,
                                                            timeInterval);
        }

        protected override void OnStop()
        {
            timer.Dispose();
        }
    }
}
