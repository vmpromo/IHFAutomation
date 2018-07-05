//
// Name: PackingDAO.cs
// Type: ADO class
// Description: contains the class variables, methods and pl/sql calls
//              associated with the Pack entities
//
//$Revision:   1.83$
//
// Version   Date        Author    Reason
//  1.0      11/07/11    MSalman   Initial Release
//  1.1      14/07/11    MSalman   New Function is added.
//  1.2      14/07/11    MSalman   function updated. 
//  1.3      14/07/11    MSalman   function updated. 
//  1.4      20/07/11    MSalman   Exception updated. 
//  1.5      20/07/11    MSalman   Print function enabled.
//  1.6      21/07/11    MSalman   Oracle temp fix Blob saving.
//  1.7      22/07/11    MSalman   PackageId updated.
//  1.8      22/07/11    MSalman   Product Value Updated.             
//  1.9      22/07/11    MSalman   Move to next order fix.                
//  1.10     28/07/11    MSalman   Enable Printing call.  
//  1.11     29/07/11    MSalman   New function added.
//  1.12     01/08/11    MSalman   Parameter updated.                             
//  1.13     01/08/11    MSalman   Parameter updated.                             
//  1.14     01/08/11    MSalman   Namespaces updated.
//  1.15     04/08/11    MSalman   MetaPack Cancel Function added. 
//  1.16     08/08/11    MSalman   New function added.
//  1.17     10/08/11    MSalman   Consignment date is updated.
//  1.18     10/08/11    MSalman   Meta pack filter updated. 
//  1.19     10/08/11    MSalman   Meta pack filter updated. 
//  1.20     12/08/11    MSalman   Fail Tote report added.
//  1.21     12/08/11    MSalman   Meta pack error handling added.    
//  1.22     18/08/11    MSalman   CarrierCode Check is added.                           
//  1.23     18/08/11    MSalman   Code Test.
//  1.24     18/08/11    MSalman   Service tries function is added.
//  1.25     22/08/11    MSalman   Fabric composition removed xml.  
//  1.26     24/08/11    MSalman   Repack to FT single.  
//  1.27     24/08/11    MSalman   Set Tote Id to Null to handle MissingItem 
//                                 FT scan 
//  1.28     25/08/11    MSalman   Metapack fields are added. 
//  1.29     01/09/11    MSalman   Excess item for repack and fail order
//  1.30     02/09/11    MSalman   Item Weight updated.
//  1.31     05/09/11    MSalman   new Function added.      
//  1.32     05/09/11    MSalman   New Field added.
//  1.33     07/09/11    MSalman   Parcel bag id index chagned.
//  1.34     09/09/11    MSalman   New Fields are added.
//  1.35     13/09/11    MSalman   User Named passed in for markedPacked.            
//  1.36     14/09/11    MSalman   New field added. 
//  1.37     19/09/11    MSalman   Missing Item Validation is added.  
//  1.38     20/09/11    MSalman   Cancellation tries are added for metapack.
//  1.39     21/09/11    MSalman   Recipient Phone is added.  
//  1.40     26/09/11    MSalman   clean up the code. 
//  1.41     03/09/11    MSalman   changed default recipient name in case of null  
//  1.42     19/10/11    MSalman   Validtion for select open order screen is added   
//  1.43     20/10/11    MSalman   Changed the Reprint call INT / DOM Mantis 1598   
//  1.44     21/10/11    MSalman   Delivery Instructions field added.
//  1.45     10/11/11    MSalman   County is added for sender / recipient address.   
//  1.46     16/11/11    MSalman   Recipient Mobile phone is set for sms alerts.       
//  1.47     29/11/11    MSalman   New function for trolley view added.   
//  1.48     05/12/11    MSalman   Sorting added function.   
//  1.49     14/02/12    M Khan    Added Metapack performance logging.
//  1.50     03/04/12    M Khan    Added sender and recipient email address to metapack call
//                                 and FLOW related functions.
//  1.51     24/09/12    J Watt    Logging.. capturing order number and consignment code in failures.
//  1.52     24/09/12    J Watt    Exception when failing to print
//  1.53     04/10/12    J Watt    Fix for try and allocate
//  1.55     08/10/12    J Watt    Error handling change
//  1.56     12/10/12    J Watt    Additional log when not carrier service found.
//  1.57     28/11/12    J Watt    I-Order changes
//  1.58     25/01/13    J Watt    Multi Currency
//  1.59     16/05/13    J Watt    Click and collect
//  1.60     13/06/13    J Watt    Click and collect intermediate check in
//  1.61     13/06/13    J Watt    Click and collect intermediate check in
//  1.62     19/06/13    J Watt    Collect+ store id
//  1.67     16/10/13    J Watt    Special instructions with control chars removed.
//  1.68     28/10/13    J Watt    click and collect or store
//  1.69     28/11/13    AJP       changed the no-of-retries parameter for getting parcel label and custom document
//  1.70     02/12/13    AJP       swapped the MP re-try paramerters for consignment creation and document retrival from MP
//  1.71     14/05/14    J Watt    N/A
//  1.72     04/08/14    S Green   changes for Customer Services address labels
//  1.73     04/08/14    S Green   regress JW changes (1.71) then reapply changes for Customer Services address labels
//  1.74     28/10/14    J Watt    Add booking code to consignment to help with MetaPack investigation
//  1.75     05/02/15    M Cackett Changes for Hermes ParcelStore.
//                                 Made booking code functions more generic to allow for future enhancements in this area.
//  1.76     16/02/15    M Cackett Make sure we don't exit create consignment loop early if carrier is null
//  1.77     23/04/15    S Green   Temporary change to add extra logging for printing
//  1.78     28/04/15    S Green   Another temporary change to add further logging for printing
//  1.79     02/08/17    M Smart   Cater for new Nominated Day Delivery (NDD) services. Also removed reference to 'e' variable that was giving a compile time warning.
//  1.80     31/08/17    M Smart   NDD changes to ProcessPackConsignment, to remove time element from carrier collection date
//  1.81     05/09/17    M Smart   NDD change to GetServiceForOrder, to remove calling GetWorkingDeliveryDate as Sunday deliveries were being bumped to a NextDay
//                                 (Monday) service. Looks like this was a live issue in any case irrespective of NDD
//  1.82     12/10/17    APetrescu Generating 1d, 2d barcodes
//  1.83     12/10/17    APetrescu Added activity log when generating 1d, 2d barcode
//  1.84     13/10/17    APetrescu Fixed header version mismatch


using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml;
using System.Text.RegularExpressions;
using System.Net;
using System.Configuration;
using IHF.EnterpriseLibrary.Data;
using IHF.BusinessLayer.BusinessClasses.Packing;
using IHF.BusinessLayer.BusinessClasses.ActivityLog;
using IHF.BusinessLayer.DataAccessObjects;
using IHF.BusinessLayer.DataAccessObjects.ActivityLog;
using IHF.BusinessLayer.Util;
using Com.MetaPack.DeliveryManager;
using System.Data;
using Oracle.DataAccess.Client;
using IHF.BusinessLayer.BusinessClasses;
using IHF.BusinessLayer.BusinessClasses.Stack;
using IHF.EnterpriseLibrary.DataServices;


namespace IHF.BusinessLayer.DataAccessObjects.Packing
{
    public class PackingDAO
    {
        private readonly ActivityLogDAO _activityLogDao;
        private readonly DataManager _dal;
        private readonly PrintLabelDAO _printLabelDAO;

        public PackingDAO()
        {
            _dal = new DataManager(Util.DBInstanceEnum.Ora);
            _activityLogDao = new ActivityLogDAO();
            _printLabelDAO = new PrintLabelDAO(_dal);
        }


        #region Private Memebers

        protected DateTime? timeBeforeCall = null;
        protected DateTime? timeAfterCall = null;


        protected PackResponse _response = new PackResponse();
        protected PackConsignment _consignment = new PackConsignment();

        protected PackOrder _packOrder = new PackOrder();

        protected TrolleyView _trolleyview = new TrolleyView();

        protected int METAPACK_SERVICE_TRIES = GetServiceTries();

        protected int METAPACK_ALLOCATION_TRIES = GetAllocationTries();

        protected int METAPACK_DELETE_TRIES = GetServiceTries();

        //for Packstation confguration functions
        private WorkstationLookup _workstationLookup = new WorkstationLookup();
        private StackDetails _stackDetails = new StackDetails();

        protected const int FILTER_GROUP_SERVICE = 2;
        protected const int SORT_COST_CHEAPEST = 6;
        protected const int EARLIEST_COLLECTION_DAY_ASCENDING = 8;
        protected const int SORT_LATEST_DELIVERY_TIME_ASCENDING = 32;
        protected const int SORT_EARLIEST_COLLECTION_ASCENDING = 2;

        Regex regex = new Regex(@"^07\d??");


        DeliveryManagerClient client = null;

        private const string PRINT_DOCS_STRING_INT = "DP,L,D";

        private const string PRINT_DOCS_STRING_DOM = "DP,L";

        private const string SAVE_CONSIGNMENT_PDF = "OMS_PACK.P_STORE_DOCUMENT";

        private bool INTERNATIONAL_ORDER = false;

        private string[] DAYS_TO_PASS = { "Sunday" };

        //Really diapointed by this implementation.But oracle has put me down again.
        private const string SAVE_CONSIGNMENT_PDF_SQL = @"insert into oms_package_document(package_doc_id,
                                                                                            package_id,
                                                                                            document_type_id,
                                                                                            document_contents,
                                                                                            created_dtm, 
                                                                                            created_by, 
                                                                                            last_changed_dtm,
                                                                                            last_changed_by) values(oms_package_document_seq.NEXTVAL,{0},{1},:BlobParameter,SYSDATE,'{2}',SYSDATE,'{3}')";

        private const string UNDOCK_CONTAINER = "OMS_PACK.P_UNDOCK_TROLLEY";

        private const string END_PACK_PROCESS = "OMS_PACK.P_END_SESSION";

        private const string OPEN_ORDER_WORKSTATION = "OMS_PACK.F_REOPEN_ORDER";

        private const string MARK_CONSIGNMENT_CANCEL = "OMS_PACK.P_CANCEL_METAPACK_CONSIGNMENT";

        private const string MARK_ORDER_PACKED = "OMS_PACK.P_UPDATE_ORDER_PACKED";

        private const string ASSIGN_CHUTE_TO_PACKSTATION = "OMS_PACK_UTIL.P_ASSIGN_CHUTE_TO_WORKSTATION";

        private const string REMOVE_CHUTE_MAPPING = "OMS_PACK_UTIL.P_REMOVE_CHUTE_MAPPING";

        private const string UPDATE_STACK_SELECTION = "OMS_PACK_UTIL.P_UPDATE_STACK_SELECTION";

        private const string CLONE_PACKAGE = "OMS_PACK.P_CLONE_PACKAGE";

        #endregion

        #region Local Functions

        public PackResponse PackScanRequest(PackRequest req)
        {
            PackResponse res = null;

            string errorMsg = string.Empty;


            if (req.ActionId.ToUpper() == Enumerations.Action.CV.ToString())
                res = ValidateContainer(req);
            else

                res = PackScanItem(req);




            // 3- Before replying update the PackingState the Object so that it will be persisted.
            // This function implementation.

            if (req.ActionId == Enumerations.Action.RP.ToString() && res.OrderNo != "" && res.ItemInd == "" && res.SuccessInd == "T")
            {
                try
                {
                    DeleteMetaPackConsignment(res.OrderNo);

                    //res.ActionId = Enumerations.Action.RP.ToString();
                }
                catch (Exception e)
                {

                    errorMsg = ErrorManager.GetExceptionMessage(e);

                }

            }


            if (!string.IsNullOrEmpty(errorMsg) && req.ActionId == Enumerations.Action.RP.ToString())
            {
                res.ErrorCode = "MT002";
                res.ErrorMessage = errorMsg;
                res.SuccessInd = "F";
            }


            res.PackingState = UpdateRequestState(req, res);

            return res;

        }

        public PackResponse ValidateContainer(PackRequest req)
        {

            PackResponse presponse = (PackResponse)_dal.Get(PackResponse.ClassMethods.ValidateContainer.ToString()
                                                                    , this._response
                                                                    , new object[] { req.Barcode, Shared.CurrentUser, Shared.UserHostName })[0];


            presponse.PackingState = UpdateRequestState(req, presponse);

            return presponse;
        }

        public PackResponse PackScanItem(PackRequest req)
        {

            PackResponse response = (PackResponse)_dal.Get(PackResponse.ClassMethods.PackScanItem.ToString()
                                                                , this._response
                                                                , new object[] { req.Barcode, 
                                                                                 Shared.CurrentUser, 
                                                                                 Shared.UserHostName,
                                                                                 Convert.ToInt32(req.TrolleyId==string.Empty?null:req.TrolleyId),
                                                                                 Convert.ToInt32(req.ToteId==string.Empty?null:req.ToteId),       
                                                                                 Convert.ToInt32(req.OrderNo==string.Empty?null:req.OrderNo),
                                                                                 req.ActionId,
                                                                                 req.PackMode,
                                                                                 req.DestinationType,
                                                                                 req.ExcessItemInd,
                                                                                 req.ReasonCode,
                                                                                 req.PreviousActionId,
                                                                                 req.PreviousToteId,
                                                                                 req.PreviousOrderNo,
                                                                                 req.PreviousCurrentItem,
                                                                                 req.PreviousTotalItem,
                                                                                 req.PreviousContainerLabel,
                                                                                 req.PreviousPackMode,
                                                                                 req.PreviousLocation,
                                                                                 req.PreviousTotalParcelBag,
                                                                                 req.PreviousOrderCount,
                                                                                 req.MissingItemToteId
                                                                               })[0];


            return response;
        }

        private PackRequest UpdateRequestState(PackRequest req, PackResponse res)
        {
            //Trolley No and ToteNo is required.
            req.OrderNo = res.OrderNo ?? req.OrderNo;
            req.PreviousOrderNo = res.PreviousOrderNo ?? req.PreviousOrderNo;

            req.ToteId = res.ToteId ?? req.ToteId;
            req.PreviousToteId = res.PreviousToteId ?? req.PreviousToteId;

            req.TrolleyId = res.TrolleyId ?? res.TrolleyId;

            req.ActionId = res.ActionId ?? req.ActionId;
            req.PreviousActionId = res.PreviousActionId ?? req.PreviousActionId;
            req.PackMode = res.PackMode ?? req.PackMode;
            req.DestinationType = res.DestinationType ?? req.DestinationType;
            req.ExcessItemInd = res.ExcessItemInd ?? req.ExcessItemInd;
            req.InOrder = IsInOrder(res) ? "T" : "F";
            req.EndOfTrolley = req.EndOfTrolley;
            req.PreviousCurrentItem = res.PreviousCurrentItem ?? req.PreviousCurrentItem;
            req.PreviousTotalItem = res.PreviousTotalItem ?? req.PreviousTotalItem;
            req.PreviousContainerLabel = res.PreviousContainerLabel ?? req.PreviousContainerLabel;
            req.PreviousPackMode = res.PreviousPackMode ?? req.PreviousPackMode;
            req.MissingItemToteId = res.MissingItemToteId ?? req.MissingItemToteId;


            return req;
        }

        private PackResponse UpdateResponseState(PackRequest req)
        {
            PackResponse res = new PackResponse();

            res.OrderNo = req.OrderNo;
            res.ToteId = req.ToteId;
            res.TrolleyId = req.TrolleyId;
            res.ActionId = req.ActionId;
            res.PackMode = req.PackMode;
            res.DestinationType = req.DestinationType;
            res.ExcessItemInd = req.ExcessItemInd;

            return res;
        }

        public PackResponse GenAndPrintDocs(PackRequest req)
        {

            PackResponse res = null;
            string errorMsg = string.Empty;
            string retVal = string.Empty;

            RecordActivity(new UserActivity()
            {
                AppSystem = (int)ActivityLogEnum.AppSystem.IHF,
                ApplicationId = (int)ActivityLogEnum.ApplicationID.Pack,
                EventDateTime = DateTime.Now,
                EventType = 510,
                ModuleId = (int)ActivityLogEnum.ModuleID.Pack,
                OrderNumber = Convert.ToInt32(req.OrderNo),
                TerminalId = Shared.UserHostName,
                UserId = Shared.CurrentUser
            });

            if (req.ActionId != Enumerations.Action.FT.ToString())
            {
                try
                {
                    RecordActivity(new UserActivity()
                    {
                        AppSystem = (int)ActivityLogEnum.AppSystem.IHF,
                        ApplicationId = (int)ActivityLogEnum.ApplicationID.Pack,
                        EventDateTime = DateTime.Now,
                        EventType = 520,
                        ModuleId = (int)ActivityLogEnum.ModuleID.Pack,
                        OrderNumber = Convert.ToInt32(req.OrderNo),
                        TerminalId = Shared.UserHostName,
                        UserId = Shared.CurrentUser
                    });

                    List<PackConsignment> lst = GetPackOrderConsignment(req.OrderNo, null);

                    RecordActivity(new UserActivity()
                    {
                        AppSystem = (int)ActivityLogEnum.AppSystem.IHF,
                        ApplicationId = (int)ActivityLogEnum.ApplicationID.Pack,
                        EventDateTime = DateTime.Now,
                        EventType = 530,
                        ModuleId = (int)ActivityLogEnum.ModuleID.Pack,
                        OrderNumber = Convert.ToInt32(req.OrderNo),
                        TerminalId = Shared.UserHostName,
                        UserId = Shared.CurrentUser
                    });

                    retVal = GetOrderPackDocumentation(req.OrderNo, lst);
                    // retVal = Enumerations.MetaPackCallErrorStatus.MetaPackCallSuccessul.ToString();

                    RecordActivity(new UserActivity()
                    {
                        AppSystem = (int)ActivityLogEnum.AppSystem.IHF,
                        ApplicationId = (int)ActivityLogEnum.ApplicationID.Pack,
                        EventDateTime = DateTime.Now,
                        EventType = 560,
                        ModuleId = (int)ActivityLogEnum.ModuleID.Pack,
                        OrderNumber = Convert.ToInt32(req.OrderNo),
                        TerminalId = Shared.UserHostName,
                        UserId = Shared.CurrentUser
                    });

                    if (retVal == Enumerations.MetaPackCallErrorStatus.MetaPackCallSuccessul.ToString())
                    {
                        RecordActivity(new UserActivity()
                        {
                            AppSystem = (int)ActivityLogEnum.AppSystem.IHF,
                            ApplicationId = (int)ActivityLogEnum.ApplicationID.Pack,
                            EventDateTime = DateTime.Now,
                            EventType = 570,
                            ModuleId = (int)ActivityLogEnum.ModuleID.Pack,
                            OrderNumber = Convert.ToInt32(req.OrderNo),
                            TerminalId = Shared.UserHostName,
                            UserId = Shared.CurrentUser
                        });

                        PrintDocs(req.OrderNo);
                    }
                    else
                    {
                        RecordActivity(new UserActivity()
                        {
                            AppSystem = (int)ActivityLogEnum.AppSystem.IHF,
                            ApplicationId = (int)ActivityLogEnum.ApplicationID.Pack,
                            EventDateTime = DateTime.Now,
                            EventType = 580,
                            ModuleId = (int)ActivityLogEnum.ModuleID.Pack,
                            OrderNumber = Convert.ToInt32(req.OrderNo),
                            TerminalId = Shared.UserHostName,
                            UserId = Shared.CurrentUser
                        });

                    }
                }
                catch (Exception e)
                {
                    errorMsg = ErrorManager.GetExceptionMessage(e);

                }

            }

            if ((Enumerations
                      .MetaPackCallErrorStatus
                      .MetaPackCallSuccessul.ToString()
                      .Equals(retVal) && !string.IsNullOrEmpty(retVal)) || (req.ActionId == Enumerations.Action.FT.ToString()))
            {


                if (req.ActionId != Enumerations.Action.FT.ToString())
                    MarkOrderPacked(req.OrderNo);
                else
                    req.ToteId = "";

                //This needs to set to null for missing item mode.
                if (req.ActionId == Enumerations.Action.OI.ToString())
                    req.ToteId = "";

                if (req.ToteId == "" && req.TrolleyId != "")
                    res = InitiateNextOrder(req);


            }

            //Fail in to Tote.
            if (req.ActionId == Enumerations.Action.FT.ToString())
            {

                if (!string.IsNullOrEmpty(req.OrderNo))
                {

                    req.ActionId = Enumerations.Action.OI.ToString();

                    PrintService print = new PrintService();

                    print.PrintFailedTote(Convert.ToDecimal(req.OrderNo), true, Shared.UserHostName, Shared.CurrentUser);
                }

                if (res == null)
                {
                    res = new PackResponse();

                    res.SuccessInd = "T";
                    res.ErrorCode = "TT000";
                }

            }


            if (req.ActionId != Enumerations.Action.OI.ToString()
                && req.ActionId != Enumerations.Action.FT.ToString()
                && (!(req.ActionId == Enumerations.Action.MI.ToString() && req.PreviousActionId == Enumerations.Action.OI.ToString())))
            {
                res = new PackResponse();

                res.ErrorCode = "TT000";

                res.SuccessInd = "T";


            }


            if (res == null)
                res = UpdateResponseState(req);

            if (!string.IsNullOrEmpty(errorMsg) && req.ActionId == Enumerations.Action.RP.ToString())
            {
                res.ErrorCode = "MT001";
                res.ErrorMessage = errorMsg;
                res.SuccessInd = "F";
            }
            else if (!string.IsNullOrEmpty(errorMsg))
            {
                res.ErrorCode = "MT000";
                res.ErrorMessage = errorMsg;
                res.SuccessInd = "F";
            }



            res.PackingState = UpdateRequestState(req, res);



            return res;
        }

        private bool IsOrderComplete(PackResponse res)
        {
            bool retVal = false;

            if (!string.IsNullOrEmpty(res.CurrentItem) && !string.IsNullOrEmpty(res.TotalItem) && (res.TotalItem != "0" || res.CurrentItem != "0"))
            {

                if (Convert.ToInt32(res.CurrentItem) == Convert.ToInt32(res.TotalItem))
                {

                    retVal = true;
                    return retVal;
                }

            }


            return retVal;

        }

        private bool IsInOrder(PackResponse res)
        {

            bool retVal = false;

            if (!string.IsNullOrEmpty(res.CurrentItem) && !string.IsNullOrEmpty(res.TotalItem) && (res.TotalItem != "0" || res.CurrentItem != "0"))
            {

                if (Convert.ToInt32(res.CurrentItem) > 0 && (Convert.ToInt32(res.CurrentItem) < Convert.ToInt32(res.TotalItem)))
                {

                    retVal = true;
                    return retVal;
                }



            }

            return false;

        }

        private PackResponse InitiateNextOrder(PackRequest req)
        {

            PackResponse response = (PackResponse)_dal.Get(PackResponse.ClassMethods.InitializeNextOrder.ToString()
                                                              , this._response
                                                              , new object[] {  Convert.ToInt32(req.TrolleyId==string.Empty?"0":req.TrolleyId),
                                                                                Convert.ToInt32(req.ToteId==string.Empty?"0":req.ToteId),
                                                                                Shared.CurrentUser,
                                                                                Shared.UserHostName,
                                                                                req.DestinationType,
                                                                                req.PackMode,
                                                                                req.OrderNo
                                                                               })[0];


            return response;

        }

        private void MarkOrderPacked(string orderNo)
        {

            try
            {
                this._dal.ExecuteNonQuery(MARK_ORDER_PACKED, new object[] { 
                                                                            orderNo,
                                                                            Shared.CurrentUser
                                                                          });

            }
            catch
            {


            }


        }

        public string UnDockContainer(PackRequest req)
        {
            string retVal = "T";

            try
            {
                this._dal.ExecuteNonQuery(UNDOCK_CONTAINER,
                                                    new object[] { 
                                                Convert.ToInt32(req.TrolleyId==string.Empty?"0":req.TrolleyId),
                                                Shared.CurrentUser,
                                                Shared.UserHostName
                                                });

            }
            catch
            {

                retVal = "F";
            }
            return retVal;

        }

        public string EndPackProcess(PackRequest req)
        {
            string retVal = "T";

            try
            {
                this._dal.ExecuteNonQuery(END_PACK_PROCESS,
                                                    new object[] { 
                                                Convert.ToInt32(req.TrolleyId),
                                                Shared.CurrentUser,
                                                Shared.UserHostName
                                                });

            }
            catch
            {

                retVal = "F";
            }
            return retVal;

        }


        #region Pack Order

        public List<PackOrder> GetPackOrder(string orderNo)
        {


            List<PackOrder> _list = ((PackOrder)_dal.Get(PackOrder.ClassMethods.GetPackOrder.ToString()
                                                                          , this._packOrder
                                                                          , new object[] {
                                                                                            Shared.CurrentUser,
                                                                                            Shared.UserHostName,
                                                                                            orderNo
                                                                                              })[0]).PackOrderInfo;


            return _list;


        }

        public bool OpenForRePack(string orderNo)
        {
            return this._dal.CheckBooleanValue(OPEN_ORDER_WORKSTATION,
                                            new object[] { 
                                            Shared.CurrentUser,
                                            Shared.UserHostName,
                                            orderNo});

        }


        public List<PackOrder> OpenOrderForPrint(string orderNo, string parcelNo)
        {
            List<PackOrder> _list = ((PackOrder)_dal.Get(PackOrder.ClassMethods.GetOrderForPrint.ToString()
                                                                            , this._packOrder
                                                                            , new object[] {
                                                                                            orderNo,
                                                                                            parcelNo
                                                                                              })[0]).PackOrderInfo;


            return _list;
        }

        public TrolleyView GetTrolleyView()
        {

            IList<TrolleyView> _list = ((TrolleyView)_dal.Get(TrolleyView.ClassMethods.GetTrolleyView.ToString()
                                                                                , this._trolleyview
                                                                                , new object[] { })[0]).TrolleyViewInfo;


            return new TrolleyView
            {
                TrolleyViewInfo = _list
                                    .OrderBy(x => x.StatusDescription)
                                    .ToList<TrolleyView>()
            };

        }

        #endregion

        #region Label and Document Generation


        // 
        // Create a consignment within MetaPack for using the consignment data and supplied booking code
        // 
        private Consignment CreateMetaPackConsignment(string bookingCode, Consignment consignment)
        {
            Consignment createdConsignment = null;

            // Add the book code to the custom 8 field to help with MetaPack investigation
            consignment.custom8 = bookingCode;

            NetworkCredential credentials = GetCredentials();



            client = new DeliveryManagerClient(GetServiceAddress(), credentials);

            try
            {

                if (createdConsignment == null)
                    createdConsignment = AllocateConsignment(client, consignment, bookingCode);


                if (string.IsNullOrEmpty(createdConsignment.carrierCode))
                {

                    createdConsignment = TryAllocateConsignment(client, createdConsignment, bookingCode);
                }


            }
            catch (Exception e)
            {
                if (createdConsignment == null)
                {

                    if (e is MetaPackFailure)
                    {
                        ErrorManager.ThrowMetaPackFailure(e.Message);
                    }
                    else
                    {
                        ErrorManager.ThrowFailToCreateAndAllocateConsignment(e.Message);
                    }
                }
            }
            return createdConsignment;
        }


        private Enumerations.MetaPackCallErrorStatus GetServiceForOrder(string orderNo, ref Consignment createConsignment, List<PackConsignment> lst)
        {

            UserActivity ua = new UserActivity();
            ActivityLogDAO al = new ActivityLogDAO();

            ua.AppSystem = (int)ActivityLogEnum.AppSystem.IHF;
            ua.ApplicationId = (int)ActivityLogEnum.ApplicationID.Pack;
            ua.TerminalId = Shared.UserHostName;
            ua.UserId = Shared.CurrentUser;
            ua.OrderNumber = int.Parse(orderNo);
            ua.EventType = 1000;
            al.SaveUserActivity(ua);

            Enumerations.MetaPackCallErrorStatus retVal = Enumerations.MetaPackCallErrorStatus.MetaPackCallSuccessul;

            string bookingcode = "";

            Consignment createdConsignment = null;

            ua.AppSystem = (int)ActivityLogEnum.AppSystem.IHF;
            ua.ApplicationId = (int)ActivityLogEnum.ApplicationID.Pack;
            ua.TerminalId = Shared.UserHostName;
            ua.UserId = Shared.CurrentUser;
            ua.OrderNumber = int.Parse(orderNo);
            ua.EventType = 1010;
            al.SaveUserActivity(ua);

            // Fetch consignment details from database ref cursor to populate consignment class
            Consignment consignment = ProcessPackConsignment(lst, orderNo);

            ua.AppSystem = (int)ActivityLogEnum.AppSystem.IHF;
            ua.ApplicationId = (int)ActivityLogEnum.ApplicationID.Pack;
            ua.TerminalId = Shared.UserHostName;
            ua.UserId = Shared.CurrentUser;
            ua.OrderNumber = int.Parse(orderNo);
            ua.EventType = 1020;
            al.SaveUserActivity(ua);

            // Removed call to GetWorkingDeliveryDate function
            DateTime deliverByDate = lst[0].DeliverByDate;

            ua.AppSystem = (int)ActivityLogEnum.AppSystem.IHF;
            ua.ApplicationId = (int)ActivityLogEnum.ApplicationID.Pack;
            ua.TerminalId = Shared.UserHostName;
            ua.UserId = Shared.CurrentUser;
            ua.OrderNumber = int.Parse(orderNo);
            ua.EventType = 1030;
            al.SaveUserActivity(ua);

            BookingCodeParamsDAO bookingCodeDAO = new BookingCodeParamsDAO();

            IList<BookingCodeParams> lstBookingCodeParams = bookingCodeDAO.GetBookingCodeParams(lst[0].ServiceGroup);

            ua.AppSystem = (int)ActivityLogEnum.AppSystem.IHF;
            ua.ApplicationId = (int)ActivityLogEnum.ApplicationID.Pack;
            ua.TerminalId = Shared.UserHostName;
            ua.UserId = Shared.CurrentUser;
            ua.OrderNumber = int.Parse(orderNo);
            ua.EventType = 1040;
            al.SaveUserActivity(ua);

            if (lstBookingCodeParams.Count != 0)  // We have found some parameters for service group
            {

                ua.AppSystem = (int)ActivityLogEnum.AppSystem.IHF;
                ua.ApplicationId = (int)ActivityLogEnum.ApplicationID.Pack;
                ua.TerminalId = Shared.UserHostName;
                ua.UserId = Shared.CurrentUser;
                ua.OrderNumber = int.Parse(orderNo);
                ua.EventType = 1050;
                al.SaveUserActivity(ua);

                /*  New Processing goes here */
                for (int i = 0; i < lstBookingCodeParams.Count; i++)
                {

                    ua.AppSystem = (int)ActivityLogEnum.AppSystem.IHF;
                    ua.ApplicationId = (int)ActivityLogEnum.ApplicationID.Pack;
                    ua.TerminalId = Shared.UserHostName;
                    ua.UserId = Shared.CurrentUser;
                    ua.OrderNumber = int.Parse(orderNo);
                    ua.EventType = 1060;
                    al.SaveUserActivity(ua);

                    bookingcode = lstBookingCodeParams[i].CarrierServiceId + "_";

                    for (int storeCnt = 0; storeCnt < lstBookingCodeParams[i].StoreCount; storeCnt++)
                    {
                        bookingcode += lst[0].CollectPlusStore + "_";
                    }
                    bookingcode += lstBookingCodeParams[i].ServiceCodeSuffix;

                    if (lstBookingCodeParams[i].DeliverByDateInd == "T")
                    {
                        bookingcode += "/*/*-*/" + deliverByDate.ToString("yyyy-MM-dd") + "/*-23:59";
                    }

                    ua.AppSystem = (int)ActivityLogEnum.AppSystem.IHF;
                    ua.ApplicationId = (int)ActivityLogEnum.ApplicationID.Pack;
                    ua.TerminalId = Shared.UserHostName;
                    ua.UserId = Shared.CurrentUser;
                    ua.OrderNumber = int.Parse(orderNo);
                    ua.EventType = 1070;
                    al.SaveUserActivity(ua);

                    createdConsignment = CreateMetaPackConsignment(bookingcode, consignment);

                    ua.AppSystem = (int)ActivityLogEnum.AppSystem.IHF;
                    ua.ApplicationId = (int)ActivityLogEnum.ApplicationID.Pack;
                    ua.TerminalId = Shared.UserHostName;
                    ua.UserId = Shared.CurrentUser;
                    ua.OrderNumber = int.Parse(orderNo);
                    ua.EventType = 1080;
                    al.SaveUserActivity(ua);

                    if (createdConsignment != null && createdConsignment.carrierCode != null)
                    {
                        break;
                    }
                }
            }
            else  // Nothing in the booking prarm list, so must be store orders, web orders or NDD type orders
            {

                ua.AppSystem = (int)ActivityLogEnum.AppSystem.IHF;
                ua.ApplicationId = (int)ActivityLogEnum.ApplicationID.Pack;
                ua.TerminalId = Shared.UserHostName;
                ua.UserId = Shared.CurrentUser;
                ua.OrderNumber = int.Parse(orderNo);
                ua.EventType = 1090;
                al.SaveUserActivity(ua);

                int callAttempts = 1;

                while ((createdConsignment == null || createdConsignment.carrierCode == null) && (callAttempts <= METAPACK_ALLOCATION_TRIES))
                {
                    callAttempts += 1;
                    // New construct for NDD (Nominated Day Delivery) service groups; 03-Day,04-Evening,05-DPD Precise
                    // CarrierCollectionDate equates to the latest despatch by date
                    if (lst[0].ServiceGroup == "03" || lst[0].ServiceGroup == "04" || lst[0].ServiceGroup == "05")
                    {
                        bookingcode = "@" + lst[0].ServiceGroup + "/" + lst[0].CarrierCollectionDate.ToString("yyyy-MM-dd") + "/*-*/" + deliverByDate.ToString("yyyy-MM-dd") + "/*-23:59";
                    }
                    else
                    {
                        bookingcode = "@" + lst[0].ServiceGroup + "/*/*-*/" + deliverByDate.ToString("yyyy-MM-dd") + "/*-23:59";
                    }

                    ua.AppSystem = (int)ActivityLogEnum.AppSystem.IHF;
                    ua.ApplicationId = (int)ActivityLogEnum.ApplicationID.Pack;
                    ua.TerminalId = Shared.UserHostName;
                    ua.UserId = Shared.CurrentUser;
                    ua.OrderNumber = int.Parse(orderNo);
                    ua.EventType = 1100;
                    al.SaveUserActivity(ua);

                    createdConsignment = CreateMetaPackConsignment(bookingcode, consignment);

                    ua.AppSystem = (int)ActivityLogEnum.AppSystem.IHF;
                    ua.ApplicationId = (int)ActivityLogEnum.ApplicationID.Pack;
                    ua.TerminalId = Shared.UserHostName;
                    ua.UserId = Shared.CurrentUser;
                    ua.OrderNumber = int.Parse(orderNo);
                    ua.EventType = 1110;
                    al.SaveUserActivity(ua);

                    // If the consignment allocation fails then we clock on the deliver by date
                    if (createdConsignment == null || string.IsNullOrEmpty(createdConsignment.carrierCode))
                    {

                        ua.AppSystem = (int)ActivityLogEnum.AppSystem.IHF;
                        ua.ApplicationId = (int)ActivityLogEnum.ApplicationID.Pack;
                        ua.TerminalId = Shared.UserHostName;
                        ua.UserId = Shared.CurrentUser;
                        ua.OrderNumber = int.Parse(orderNo);
                        ua.EventType = 1120;
                        al.SaveUserActivity(ua);

                        deliverByDate = GetWorkingDeliveryDate(deliverByDate.AddDays(1));

                        ua.AppSystem = (int)ActivityLogEnum.AppSystem.IHF;
                        ua.ApplicationId = (int)ActivityLogEnum.ApplicationID.Pack;
                        ua.TerminalId = Shared.UserHostName;
                        ua.UserId = Shared.CurrentUser;
                        ua.OrderNumber = int.Parse(orderNo);
                        ua.EventType = 1130;
                        al.SaveUserActivity(ua);

                    }
                }
            }

            ua.AppSystem = (int)ActivityLogEnum.AppSystem.IHF;
            ua.ApplicationId = (int)ActivityLogEnum.ApplicationID.Pack;
            ua.TerminalId = Shared.UserHostName;
            ua.UserId = Shared.CurrentUser;
            ua.OrderNumber = int.Parse(orderNo);
            ua.EventType = 1140;
            al.SaveUserActivity(ua);

            if (createdConsignment == null)
            {
                retVal = Enumerations.MetaPackCallErrorStatus.MetaPackCallFailed;

                //Make a log when no MetaPack error but failure to find available service
                UserActivity useractivity = new UserActivity();
                useractivity.AppSystem = (int)ActivityLogEnum.AppSystem.IHF;
                useractivity.ApplicationId = (int)ActivityLogEnum.ApplicationID.Pack;
                useractivity.TerminalId = Shared.UserHostName;
                useractivity.UserId = Shared.CurrentUser;
                useractivity.OrderNumber = int.Parse(orderNo);
                useractivity.ReasonCode = (int)Enumerations.EventReason.CreateAndAllocateConsignment;
                useractivity.EventType = (int)EventType.MetapPackAllocationFail;
                useractivity.Value2 = "Failed to find available service";

                ActivityLogDAO activityLog = new ActivityLogDAO();
                activityLog.SaveUserActivity(useractivity);

                ErrorManager.ThrowFailToCreateAndAllocateConsignment("Failed to find available service");
            }

            ua.AppSystem = (int)ActivityLogEnum.AppSystem.IHF;
            ua.ApplicationId = (int)ActivityLogEnum.ApplicationID.Pack;
            ua.TerminalId = Shared.UserHostName;
            ua.UserId = Shared.CurrentUser;
            ua.OrderNumber = int.Parse(orderNo);
            ua.EventType = 1150;
            al.SaveUserActivity(ua);

            // Populate functions ref cursor with consignment details
            createConsignment = createdConsignment;

            ua.AppSystem = (int)ActivityLogEnum.AppSystem.IHF;
            ua.ApplicationId = (int)ActivityLogEnum.ApplicationID.Pack;
            ua.TerminalId = Shared.UserHostName;
            ua.UserId = Shared.CurrentUser;
            ua.OrderNumber = int.Parse(orderNo);
            ua.EventType = 1160;
            al.SaveUserActivity(ua);

            return retVal;
        }

        private void SaveUserActivity(string orderNo, int? eventType, int? reasonCode = null, string value2 = null)
        {
            UserActivity ua = new UserActivity();

            ua.AppSystem = (int)ActivityLogEnum.AppSystem.IHF;
            ua.ApplicationId = (int)ActivityLogEnum.ApplicationID.Pack;
            ua.TerminalId = Shared.UserHostName;
            ua.UserId = Shared.CurrentUser;
            ua.OrderNumber = int.Parse(orderNo);
            ua.EventType = eventType;
            ua.ReasonCode = reasonCode;
            ua.Value2 = value2;

            _activityLogDao.SaveUserActivity(ua);
        }

        private void CreateDespatchOrderBarcodes(string orderNo)
        {
            var despatchNoteType = _printLabelDAO.GetDespatchNoteType(orderNo);

            SaveUserActivity(orderNo, 815);

            //2 - DomesticSimplex, 5 - DomesticFrenchSimplex
            if (!new[] { 2, 5 }.Contains(despatchNoteType))
            {
                return;
            }

            _printLabelDAO.GenerateDespatchNoteBarcodes(orderNo);
        }

        private string GetOrderPackDocumentation(string orderNo, List<PackConsignment> lst)
        {
            Enumerations.MetaPackCallErrorStatus retVal = Enumerations.MetaPackCallErrorStatus.MetaPackCallSuccessul;

            Consignment createdConsignment = null;

            SaveUserActivity(orderNo, 800);

            retVal = GetServiceForOrder(orderNo, ref createdConsignment, lst);

            SaveUserActivity(orderNo, 810);

            if (createdConsignment != null && createdConsignment.carrierCode != null)
            {
                CreateDespatchOrderBarcodes(orderNo);

                SaveUserActivity(orderNo, 820);

                string labelPdf = string.Empty;
                string docPdf = string.Empty;

                int printLabelAttempts = 1;
                int printDocAttempts = 1;

                while ((string.IsNullOrEmpty(labelPdf)) && (printLabelAttempts <= METAPACK_SERVICE_TRIES))
                {
                    SaveUserActivity(orderNo, 830);

                    printLabelAttempts += 1;

                    try
                    {
                        SaveUserActivity(orderNo, 840);

                        labelPdf = GetPdfAndDocuments(client, createdConsignment, Enumerations.PdfType.L);

                        SaveUserActivity(orderNo, 850);
                    }
                    catch (Exception e)
                    {
                        if ((printLabelAttempts > METAPACK_SERVICE_TRIES) && (string.IsNullOrEmpty(labelPdf)))
                        {
                            SaveUserActivity(orderNo, 860);

                            CancelConsignment(client, createdConsignment);

                            SaveUserActivity(orderNo, 870);

                            retVal = Enumerations.MetaPackCallErrorStatus.MetaPackCallFailed;

                            ErrorManager.ThrowFailToGetTheLabel(e.Message);
                        }

                    }
                }

                SaveUserActivity(orderNo, 880);

                try
                {

                    if (!string.IsNullOrEmpty(labelPdf))
                    {
                        SaveUserActivity(orderNo, 890);

                        SaveConsignmentDocumentation(orderNo,
                                           createdConsignment, Enumerations.PdfType.L.ToString(), labelPdf, lst);

                        SaveUserActivity(orderNo, 900);

                    }

                }
                catch (Exception e)
                {
                    ErrorManager.ThrowSaveLabelFailure(e.Message);

                }

                SaveUserActivity(orderNo, 910);


                // If it is an international order and not a 'cloned' consignment as result of store order relabelling
                //  try to print of customs documentation
                if (lst[0].DestinationCode == Enumerations.DType.INT.ToString() && lst[0].CloneInd == "F")
                {

                    SaveUserActivity(orderNo, 920);

                    INTERNATIONAL_ORDER = true;

                    while ((string.IsNullOrEmpty(docPdf)) && (printDocAttempts <= METAPACK_SERVICE_TRIES))
                    {

                        SaveUserActivity(orderNo, 930);

                        printDocAttempts += 1;

                        try
                        {

                            SaveUserActivity(orderNo, 940);

                            docPdf = GetPdfAndDocuments(client, createdConsignment, Enumerations.PdfType.D);

                            SaveUserActivity(orderNo, 950);

                        }
                        catch (Exception e)
                        {
                            if ((printDocAttempts > METAPACK_SERVICE_TRIES) && (string.IsNullOrEmpty(docPdf)))
                            {

                                SaveUserActivity(orderNo, 960);

                                CancelConsignment(client, createdConsignment);

                                SaveUserActivity(orderNo, 970);

                                retVal = Enumerations.MetaPackCallErrorStatus.MetaPackCallFailed;

                                ErrorManager.ThrowFailToGetTheDocument(e.Message);
                            }

                        }
                    }

                }

                SaveUserActivity(orderNo, 980);

                try
                {
                    if (!string.IsNullOrEmpty(docPdf))
                    {

                        SaveUserActivity(orderNo, 985);

                        SaveConsignmentDocumentation(orderNo, createdConsignment, Enumerations.PdfType.D.ToString(), docPdf, lst);

                        SaveUserActivity(orderNo, 990);

                    }
                }
                catch (Exception e)
                {
                    ErrorManager.ThrowSaveDocumentFailure(e.Message);

                }

                SaveUserActivity(orderNo, 995);

            }//if
            else
            {
                //Make a log when no MetaPack error but failure to find available service
                SaveUserActivity(orderNo, (int)EventType.MetapPackAllocationFail, (int)Enumerations.EventReason.CreateAndAllocateConsignment, "Failed to find available service");

                retVal = Enumerations.MetaPackCallErrorStatus.MetaPackCallFailed;

                ErrorManager.ThrowFailToCreateAndAllocateConsignment("Fail to create or allocate the consignment");

            }


            if (retVal == Enumerations.MetaPackCallErrorStatus.MetaPackCallSuccessul)
            {
                // No need to call it.
                //  MarkConsignmentPrinted(client, createdConsignment);
            }

            SaveUserActivity(orderNo, 999);

            return retVal.ToString();

        }

        private void DeleteMetaPackConsignment(string orderNo)
        {

            DeliveryManagerClient client = new DeliveryManagerClient(GetServiceAddress(), GetCredentials());

            bool isCancelled = false;

            try
            {

                string code = GetPreviousConsignmentCode(orderNo);

                if (!string.IsNullOrEmpty(code))
                {
                    if (CancelConsignment(client, code, int.Parse(orderNo)))
                        isCancelled = true;
                    else
                        ErrorManager.ThrowCancelAllocationFailure("Fail to cancel the consignment.");

                }
                else
                    ErrorManager.ThrowCancelAllocationFailure("Fail Invalid consignment.");


            }
            catch (Exception e)
            {
                ErrorManager.ThrowCancelAllocationFailure(e.Message);
            }
            finally
            {

                if (isCancelled)
                    MarkConsignmentCancel(orderNo);

            }



        }

        private DateTime GetWorkingDeliveryDate(DateTime dateTime)
        {

            DateTime retVal = dateTime;

            DateTime dDateTime = dateTime;

            while (DAYS_TO_PASS.Contains(dDateTime.DayOfWeek.ToString()))

                dDateTime = dDateTime.AddDays(1);


            retVal = dDateTime;

            return retVal;



        }

        private bool CancelConsignment(DeliveryManagerClient c, string code, int ordernumber)
        {
            bool retVal = false;

            string[] consignmentCodes = { code };
            int callAttempts = 1;

            while (callAttempts <= METAPACK_DELETE_TRIES && retVal == false)
            {
                callAttempts += 1;

                timeBeforeCall = DateTime.Now;
                try
                {

                    c.AllocationService.deallocate(consignmentCodes);

                    c.ConsignmentService.deleteConsignment(code);

                    retVal = true;

                }
                catch (Exception e)
                {
                    //Log Metapack Failure
                    UserActivity useractivity = new UserActivity();
                    useractivity.Ref1 = code;
                    useractivity.OrderNumber = ordernumber;
                    useractivity.ReasonCode = (int)Enumerations.EventReason.DeAllocate;
                    ErrorManager.GetMessageAndLogException(e, useractivity);

                    if (callAttempts > METAPACK_DELETE_TRIES && retVal == false)
                    {

                        throw e;
                    }
                }
                finally
                {
                    timeAfterCall = DateTime.Now;
                    LogMetapackActivity((int)Enumerations.EventReason.DeAllocate, ordernumber, code);
                }

            }


            return retVal;
        }

        private bool CancelConsignment(DeliveryManagerClient c, Consignment consignment)
        {
            bool retVal = false;
            try
            {
                CancelConsignment(c, consignment.consignmentCode, int.Parse(consignment.orderNumber));

                retVal = true;
            }
            catch (Exception e)
            {

                throw e;
            }

            return retVal;
        }

        private void MarkConsignmentCancel(string orderNo)
        {

            try
            {
                this._dal.ExecuteNonQuery(MARK_CONSIGNMENT_CANCEL,
                                                    new object[] { 
                                                                    orderNo,
                                                                    Shared.CurrentUser,
                                                
                                                                   });

            }
            catch
            {


            }

        }

        private void MarkConsignmentPrinted(DeliveryManagerClient c, Consignment createdConsignment)
        {
            string[] consignmentCodes = { createdConsignment.consignmentCode };
            timeBeforeCall = DateTime.Now;

            bool marked = c.ConsignmentService.markConsignmentsAsPrinted(consignmentCodes);

            timeAfterCall = DateTime.Now;

            LogMetapackActivity((int)Enumerations.EventReason.MarkConsignmentAsPrinted, int.Parse(createdConsignment.orderNumber), createdConsignment.consignmentCode);
        }

        private string GetPreviousConsignmentCode(string orderNo)
        {
            string code = string.Empty;


            PackConsignment consign = (PackConsignment)_dal.Get(PackConsignment.ClassMethods.GetConsignmentCode.ToString()
                                                               , this._consignment
                                                               , new object[] { orderNo })[0];


            if (consign != null)
                code = consign.ConsignmentCode;


            return code;

        }

        private Consignment AllocateConsignment(DeliveryManagerClient c, Consignment consignment, string bookingCode)
        {

            Consignment[] createdConsignments = null;


            try
            {
                timeBeforeCall = DateTime.Now;
                createdConsignments = c.AllocationService.createAndAllocateConsignmentsWithBookingCode(new Consignment[] { consignment },
                                                                                               bookingCode, false);
            }
            catch (Exception e)
            {
                //Log Metapack Failure
                UserActivity useractivity = new UserActivity();
                useractivity.TerminalId = Shared.UserHostName;
                useractivity.UserId = Shared.CurrentUser;
                useractivity.OrderNumber = int.Parse(consignment.orderNumber);
                useractivity.ReasonCode = (int)Enumerations.EventReason.CreateAndAllocateConsignment;
                ErrorManager.GetMessageAndLogException(e, useractivity);

                if (useractivity.EventType == (int)EventType.E20)
                {
                    ErrorManager.ThrowFailToCreateAndAllocateConsignment(e.Message);
                }
                else
                {
                    ErrorManager.ThrowMetaPackFailure(e.Message);
                }

            }
            finally
            {
                timeAfterCall = DateTime.Now;
                LogMetapackActivity((int)Enumerations.EventReason.CreateAndAllocateConsignment, int.Parse(consignment.orderNumber), string.Empty);
            }

            return createdConsignments[0];

        }

        private void LogMetapackActivity(int metapackCallCode, int ordernumber, string consignmentcode)
        {
            if (metapackCallCode > 0)
            {
                UserActivity activity = new UserActivity();
                activity.AppSystem = (int)ActivityLogEnum.AppSystem.IHF;
                activity.ApplicationId = (int)ActivityLogEnum.ApplicationID.Pack;
                activity.EventType = (int)EventType.MetapackPerformance;
                activity.OrderNumber = ordernumber;
                activity.Ref1 = consignmentcode;
                activity.ReasonCode = metapackCallCode;
                activity.SessionStartDateTime = timeBeforeCall;
                activity.SessionEndDateTime = timeAfterCall;
                activity.TerminalId = Shared.UserHostName;
                activity.UserId = Shared.CurrentUser;

                try
                {
                    new ActivityLogDAO().SaveUserActivity(activity);
                }
                catch (Exception)
                {
                    // dont know what to do but is better than keeping it unhandled and crashing
                    // will come back later
                }
                finally
                {
                    timeBeforeCall = null;
                    timeAfterCall = null;
                }
            }

        }

        private Consignment TryAllocateConsignment(DeliveryManagerClient c, Consignment consignment, string bookingCode)
        {

            Consignment[] allocatedConsignments = { new Consignment() };
            Consignment[] consignments = { consignment };
            String[] consignmentCodes = { consignment.consignmentCode };

            try
            {
                timeBeforeCall = DateTime.Now;
                allocatedConsignments = c.AllocationService.allocateConsignmentsWithBookingCode(consignmentCodes, bookingCode, false);
            }
            catch (Exception e)
            {
                //Log Metapack Failure
                UserActivity useractivity = new UserActivity();
                useractivity.OrderNumber = int.Parse(consignment.orderNumber);
                useractivity.ReasonCode = (int)Enumerations.EventReason.AllocateConsignment;
                ErrorManager.GetMessageAndLogException(e, useractivity);

                if (useractivity.EventType == (int)EventType.E20)
                {
                    ErrorManager.ThrowFailToCreateAndAllocateConsignment(e.Message);
                }
                else
                {
                    ErrorManager.ThrowMetaPackFailure(e.Message);
                }
            }
            finally
            {
                timeAfterCall = DateTime.Now;
                LogMetapackActivity((int)Enumerations.EventReason.AllocateConsignment, int.Parse(consignment.orderNumber), string.Empty);
            }


            return allocatedConsignments[0];
        }

        private AllocationFilter GetConsignmentFilter(string serviceGroup, DateTime deliverByDate)
        {

            AllocationFilter af = new AllocationFilter();


            af.preFilterSortOrder1 = SORT_EARLIEST_COLLECTION_ASCENDING;

            af.acceptableCarrierServiceGroupCodes = new string[] { serviceGroup };

            af.filterGroup1 = FILTER_GROUP_SERVICE;

            af.firstCollectionOnly = true;

            af.sortOrder1 = SORT_COST_CHEAPEST;

            af.sortOrder2 = EARLIEST_COLLECTION_DAY_ASCENDING;

            af.sortOrder3 = SORT_LATEST_DELIVERY_TIME_ASCENDING;


            af.acceptableDeliverySlots = new DateRange[]{new DateRange{ from = null,
                                                                         to= Convert.ToDateTime(string.Format("{0} 23:59:00",deliverByDate.ToShortDateString()))}};


            return af;
        }

        private string GetPdfAndDocuments(DeliveryManagerClient c, Consignment consignment, Enumerations.PdfType type)
        {
            string strPdf = string.Empty;
            timeBeforeCall = DateTime.Now;
            try
            {
                if (type == Enumerations.PdfType.L)
                    strPdf = c.ConsignmentService.createLabelsAsPdf(consignment.consignmentCode);
                else if (type == Enumerations.PdfType.D)
                    strPdf = c.ConsignmentService.createDocumentationAsPdf(consignment.consignmentCode);
            }
            catch (Exception e)
            {
                //Log Metapack Failure
                UserActivity useractivity = new UserActivity();
                useractivity.OrderNumber = int.Parse(consignment.orderNumber);
                useractivity.Ref1 = consignment.consignmentCode;
                if (type == Enumerations.PdfType.L)
                    useractivity.ReasonCode = (int)Enumerations.EventReason.CreateLabelsAsPdf;
                else if (type == Enumerations.PdfType.D)
                    useractivity.ReasonCode = (int)Enumerations.EventReason.CreateDocumentationAsPdf;
                ErrorManager.GetMessageAndLogException(e, useractivity);

                throw e;
            }
            finally
            {
                timeAfterCall = DateTime.Now;
                int metapackCallCode = 0;

                if (type == Enumerations.PdfType.L)
                    metapackCallCode = (int)Enumerations.EventReason.CreateLabelsAsPdf;
                else if (type == Enumerations.PdfType.D)
                    metapackCallCode = (int)Enumerations.EventReason.CreateDocumentationAsPdf;

                LogMetapackActivity(metapackCallCode, int.Parse(consignment.orderNumber), consignment.consignmentCode);
            }


            return strPdf;
        }

        //Need to change this function for barcode against each Parcel

        private bool SaveConsignmentDocumentation(string orderNo, Consignment consignment, string docType, string strPdf, List<PackConsignment> lst)
        {
            bool retVal = false;
            int returnResult = 0;
            decimal warehouseId = lst[0].WarehouseId;
            OracleParameter param = null;

            var parcels = lst.GroupBy(u => u.ParcelBagID)
                                         .Select(grp => grp.ToList())
                                         .ToList();
            int i = 0;

            foreach (Parcel p in consignment.parcels)
            {

                //This is very silly implementation. 
                //Have to clean this up don't know how.
                //Will think about it later.
                param = new OracleParameter();
                param.OracleDbType = OracleDbType.Blob;

                string packageId = parcels[i][0].ParcelBagID;
                //               
                string sql = string.Format(SAVE_CONSIGNMENT_PDF_SQL, packageId, docType == Enumerations.PdfType.L.ToString() ? "0" : "1", Shared.CurrentUser, Shared.CurrentUser);


                OracleCommand command = new OracleCommand(sql);
                command.CommandType = CommandType.Text;
                OracleParameter parameter = new OracleParameter();
                parameter.OracleDbType = OracleDbType.Blob;
                parameter.ParameterName = "BlobParameter";
                parameter.Value = Convert.FromBase64String(strPdf);
                command.Parameters.Add(parameter);

                this._dal.ExecuteSQL(command);

                //insert query for saving the BLOB
                // and logging the time for saving it
                this.timeBeforeCall = DateTime.Now;

                this._dal.ExecuteNonQuery(SAVE_CONSIGNMENT_PDF,
                                                                new object[] { 
                                                                orderNo,
                                                                Shared.CurrentUser,
                                                                packageId,
                                                                consignment.consignmentCode,
                                                                p.code,
                                                                docType==Enumerations.PdfType.L.ToString()?"0":"1",
                                                                consignment.carrierServiceCode,
                                                                consignment.carrierServiceName,
                                                                consignment.despatchDate,
                                                                consignment.guaranteedDeliveryDate,
                                                                consignment.carrierConsignmentCode,
                                                                warehouseId
                                                                });

                this.timeAfterCall = DateTime.Now;

                RecordActivity(
                    new UserActivity
                    {
                        AppSystem = (int)ActivityLogEnum.AppSystem.IHF,
                        ApplicationId = (int)ActivityLogEnum.ApplicationID.Pack,
                        EventDateTime = DateTime.Now,
                        EventType = (int)EventType.SavePackDocuments,
                        ModuleId = (int)ActivityLogEnum.ModuleID.Pack,
                        OrderNumber = Convert.ToInt32(orderNo),
                        TerminalId = Shared.UserHostName,
                        UserId = Shared.CurrentUser,
                        SessionStartDateTime = this.timeBeforeCall,
                        SessionEndDateTime = this.timeAfterCall
                    }
                );
                //
                i += 1;

            }

            return retVal = returnResult != 0;

        }

        public void RelabelParcel(string barcode, string userId, string machineName)
        {
            decimal consignmentId = 0;
            consignmentId = _dal.ExecuteReturnMethodDecimal(CLONE_PACKAGE, new object[] { consignmentId, barcode, userId, machineName });

            List<PackConsignment> lst = GetPackOrderConsignment("", consignmentId);
            GetOrderPackDocumentation(lst[0].OrderNo, lst);

            try
            {
                PrintService printservice = new PrintService();
                string retval = printservice.PrintPackageDocument(Shared.UserHostName, consignmentId, "L");
            }
            catch (Exception ex)
            {
                ErrorManager.ThrowFailToPrint(ex.Message);
            }
        }

        public List<PackConsignment> GetPackOrderConsignment(String OrderNumber, decimal? consignmentId)
        {
            List<PackConsignment> _list = ((PackConsignment)_dal.Get(PackConsignment.ClassMethods.GetPackConsignment.ToString()
                                                                        , this._consignment
                                                                        , new object[] { OrderNumber, consignmentId })[0]).PackConsignmentInfo;

            return _list;
        }

        private Consignment ProcessPackConsignment(List<PackConsignment> lstPackConsigment, string OrderNo)
        {

            var orderBags = lstPackConsigment.GroupBy(u => u.ParcelBagID)
                                             .Select(grp => grp.ToList())
                                             .ToList();

            Consignment consignment = new Consignment();


            consignment.orderNumber = OrderNo;

            // put that in config file. 
            consignment.senderCode = lstPackConsigment[0].SenderCode;

            consignment.senderName = lstPackConsigment[0].SenderName;
            if (string.IsNullOrEmpty(consignment.senderName) == true) consignment.senderName = "River Island";

            consignment.senderAddress = GetAddress(lstPackConsigment[0].SenderAddress);

            consignment.senderPhone = lstPackConsigment[0].SenderPhone;

            consignment.recipientName = lstPackConsigment[0].CustomerName;

            consignment.recipientAddress = GetAddress(lstPackConsigment[0].RecipientAddress);

            consignment.recipientPhone = lstPackConsigment[0].RecipientPhone;

            if (!string.IsNullOrEmpty(lstPackConsigment[0].RecipientPhone))
                if (IsMobile(lstPackConsigment[0].RecipientPhone))
                    consignment.recipientMobilePhone = lstPackConsigment[0].RecipientPhone;

            consignment.parcels = GetConsignmentParcel(orderBags).ToArray();

            consignment.parcelCount = consignment.parcels.Count();

            // Remove control characters from special instructions
            string specialinstructions = new string(lstPackConsigment[0].DeliveryInstructions.Where(c => !char.IsControl(c)).ToArray());
            consignment.specialInstructions1 = specialinstructions;

            consignment.senderEmail = lstPackConsigment[0].SenderEmailAdderss;

            consignment.recipientEmail = lstPackConsigment[0].RecipientEmailAddress;

            // IF the currency is not Sterling then need to pass the code and rate
            //   before multi currency the currency code for Sterling was GB but once the project started became GBP


            //Now ignoring this...  back end is adjusting the values on each of the lines
            //if (lstPackConsigment[0].CurrencyCode.Substring(0, 2).ToUpper() != "GB")
            //{
            //    consignment.consignmentValueCurrencyCode = lstPackConsigment[0].CurrencyCode;
            //    consignment.consignmentValueCurrencyRate = lstPackConsigment[0].CurrencyRate;
            //}

            consignment.custom6 = "Parcel Ref:";

            // If this a store order destined for a store
            if (lstPackConsigment[0].DestType == "01")
            {
                //Append indication to name as to whether this is a store order or click and collect

                consignment.custom1 = lstPackConsigment[0].CustomerName + " - " + lstPackConsigment[0].StoreDelivOrdTypeTag;
                consignment.custom2 = "Delivery:^" + lstPackConsigment[0].StoreName;
                consignment.custom3 = "Order Ref:^" + lstPackConsigment[0].OrderNo;
                consignment.custom4 = "Order Date:^" + lstPackConsigment[0].OrderDate.ToString("ddd dd MMM") + " - Collect: " + lstPackConsigment[0].CustCollectionDate.ToString("ddd dd MMM");
                //consignment.custom4 = "Order Date:^" + "WED 25th NOV - Collect: SUN 30th NOV";
                consignment.custom5 = "";// For now a blank line

                //Also make sure the telephone numbers don't get passed over 
                consignment.recipientPhone = "";
                consignment.recipientMobilePhone = "";
                consignment.senderPhone = "";
            }

            // Cater for Nominated Day Delivery (NDD) service groups
            // 03-Day, 04-Evening, 05-Hour Slot
            if (lstPackConsigment[0].ServiceGroup == "03" ||
                lstPackConsigment[0].ServiceGroup == "04" ||
                lstPackConsigment[0].ServiceGroup == "05")
            {
                consignment.custom2 = "Order Ref:^" + lstPackConsigment[0].OrderNo;
                consignment.custom3 = "Cust Deliv:^" + lstPackConsigment[0].DeliverByDate.ToString("ddd dd MMM");
                consignment.custom4 = "Carrier Col:^" + lstPackConsigment[0].CarrierCollectionDate.ToString("ddd dd MMM");
                // for precise time slot need to populate properties element with slot token id
                if (lstPackConsigment[0].ServiceGroup == "05")
                {
                    List<Property> NDDPrecise = new List<Property>();
                    NDDPrecise.Add(new Property
                    {
                        propertyName = "carrier.dpd.precise.pr.reference",
                        propertyValue = lstPackConsigment[0].NddSlotTokenId
                    });
                    Property[] NDDProperty = NDDPrecise.ToArray();
                    consignment.properties = NDDProperty;
                }
            }

            consignment.custom7 = lstPackConsigment[0].StoreNum;

            return consignment;
        }


        private Address GetAddress(string address)
        {
            Address add = new Address();

            string[] strAddress = null;

            strAddress = address.Split('|');

            add.line1 = strAddress[0].ToString();
            add.line2 = strAddress[1].ToString();

            if (string.IsNullOrEmpty(add.line2) == true)
                add.line2 = ".";

            add.line3 = strAddress[2].ToString();
            add.line4 = strAddress[3].ToString() + " " + strAddress[4].ToString();

            add.postCode = strAddress[5].ToString();
            add.countryCode = strAddress[6].ToString();

            return add;
        }

        private Product[] GetProduct(List<PackConsignment> consignments)
        {

            List<Product> products = new List<Product>();

            foreach (PackConsignment con in consignments)
            {

                products.Add(new Product
                {

                    fabricContent = GetFabricCompositionString(con.FabricComposition),
                    unitProductWeight = con.ItemWeight != string.Empty ? Convert.ToDouble(con.ItemWeight) : 0,
                    totalProductValue = con.ProductValue,
                    productCode = con.Sku,
                    productDescription = con.ProductDescription,
                    countryOfOrigin = con.CountryCode,
                    productQuantity = con.ProductQuantity,
                    productTypeDescription = con.ProductTypeDescription,
                    harmonisedProductCode = con.TariffCode

                });

            }


            return products.ToArray();
        }

        private List<Parcel> GetConsignmentParcel(List<List<PackConsignment>> orderBags)
        {

            List<Parcel> lstParcel = new List<Parcel>();
            int count = orderBags.Count;
            int i = 0;
            foreach (List<PackConsignment> conParcel in orderBags)
            {
                i += 1;

                Parcel parcel = new Parcel();

                var parcelWeight = conParcel.Sum(x => Convert.ToDouble(x.ItemWeight));

                Product[] products = GetProduct(conParcel);

                parcel.number = i;

                parcel.products = products;

                parcel.parcelWeight = parcelWeight;

                parcel.parcelHeight = conParcel[0].ItemHeight != string.Empty ? Convert.ToDouble(conParcel[0].ItemHeight) : 0;

                parcel.parcelWidth = conParcel[0].ItemWidth != string.Empty ? Convert.ToDouble(conParcel[0].ItemWidth) : 0;

                parcel.parcelDepth = conParcel[0].ItemLength != string.Empty ? Convert.ToDouble(conParcel[0].ItemLength) : 0;

                parcel.number = Convert.ToInt32(conParcel[0].ParcelBagID);

                parcel.parcelValue = conParcel.Sum(x => x.ProductValue);

                parcel.cartonId = conParcel[0].Gs1Barcode;

                lstParcel.Add(parcel);

            }


            return lstParcel;
        }

        private NetworkCredential GetCredentials()
        {
            string[] pc = ConfigurationManager.AppSettings["MPackCredential"].Split(';');

            if (pc.Length == 0)
                throw new Exception("Meta Pack configuration missing for label printing.");

            return new NetworkCredential(pc[0], pc[1]);
        }

        private string GetFabricCompositionString(string fcomposition)
        {
            string retVal = string.Empty;

            string Cstr = string.Empty;

            if (!string.IsNullOrEmpty(fcomposition))
            {
                var val = XElement.Parse(fcomposition);

                IEnumerable<string> q =
                    from str in val.Elements()
                    select str.Value;

                foreach (string n in q)
                    Cstr += n + ", ";

            }

            if (!string.IsNullOrEmpty(Cstr))
                retVal = Cstr.Substring(0, Cstr.LastIndexOf(','));


            return retVal;
        }

        private static int GetServiceTries()
        {

            string servicetries = ConfigurationManager.AppSettings["MPackServiceTries"].ToString();

            if (string.IsNullOrEmpty(servicetries))
                throw new Exception("Meta Pack configuration missing service tries.");

            return Convert.ToInt32(servicetries);

        }

        private static int GetAllocationTries()
        {
            string servicetries = ConfigurationManager.AppSettings["MPackAllocationTries"].ToString();

            if (string.IsNullOrEmpty(servicetries))
                throw new Exception("Meta Pack configuration missing allocation tries.");

            return Convert.ToInt32(servicetries);
        }

        private string GetServiceAddress()
        {

            string url = ConfigurationManager.AppSettings["ServiceAddress"];

            if (url.Length == 0)
                throw new Exception("Meta Pack configuration missing for label printing.");


            return url;

        }

        private void PrintDocs(string orderno)
        {

            RecordActivity(new UserActivity()
            {
                AppSystem = (int)ActivityLogEnum.AppSystem.IHF,
                ApplicationId = (int)ActivityLogEnum.ApplicationID.Pack,
                EventDateTime = DateTime.Now,
                EventType = 600,
                ModuleId = (int)ActivityLogEnum.ModuleID.Pack,
                OrderNumber = Convert.ToInt32(orderno),
                TerminalId = Shared.UserHostName,
                UserId = Shared.CurrentUser,
                InternationalInd = INTERNATIONAL_ORDER ? "T" : "F"
            });

            RePrintDocs(orderno, INTERNATIONAL_ORDER ? PRINT_DOCS_STRING_INT : PRINT_DOCS_STRING_DOM);

            RecordActivity(new UserActivity()
            {
                AppSystem = (int)ActivityLogEnum.AppSystem.IHF,
                ApplicationId = (int)ActivityLogEnum.ApplicationID.Pack,
                EventDateTime = DateTime.Now,
                EventType = 610,
                ModuleId = (int)ActivityLogEnum.ModuleID.Pack,
                OrderNumber = Convert.ToInt32(orderno),
                TerminalId = Shared.UserHostName,
                UserId = Shared.CurrentUser,
                InternationalInd = INTERNATIONAL_ORDER ? "T" : "F"
            });

        }

        public void RePrintDocs(string orderno, string docs)
        {

            PrintService prints = new PrintService();

            string strDocs = docs.EndsWith(",") ? docs.Substring(0, docs.LastIndexOf(',')) : docs;

            try
            {

                RecordActivity(new UserActivity()
                {
                    AppSystem = (int)ActivityLogEnum.AppSystem.IHF,
                    ApplicationId = (int)ActivityLogEnum.ApplicationID.Pack,
                    EventDateTime = DateTime.Now,
                    EventType = 700,
                    ModuleId = (int)ActivityLogEnum.ModuleID.Pack,
                    OrderNumber = Convert.ToInt32(orderno),
                    TerminalId = Shared.UserHostName,
                    UserId = Shared.CurrentUser,
                    InternationalInd = INTERNATIONAL_ORDER ? "T" : "F"
                });

                prints.PrintPackDocuments(Convert.ToDecimal(orderno), true, Shared.UserHostName, strDocs, Shared.CurrentUser);

                #region ActivityRecord

                //To Record the User Activity. 

                if (strDocs.Contains("L"))
                {
                    RecordActivity(new UserActivity()
                    {
                        AppSystem = (int)ActivityLogEnum.AppSystem.IHF,
                        ApplicationId = (int)ActivityLogEnum.ApplicationID.Pack,
                        EventDateTime = DateTime.Now,
                        EventType = (int)EventType.PackLabelsPrint,
                        ModuleId = (int)ActivityLogEnum.ModuleID.Pack,
                        OrderNumber = Convert.ToInt32(orderno),
                        ReasonCode = (int)ActivityLogEnum.ResultCd.Success,
                        TerminalId = Shared.UserHostName,
                        UserId = Shared.CurrentUser,
                        InternationalInd = INTERNATIONAL_ORDER ? "T" : "F"
                    });
                }

                if (strDocs.Contains("D"))
                {
                    RecordActivity(new UserActivity()
                    {
                        AppSystem = (int)ActivityLogEnum.AppSystem.IHF,
                        ApplicationId = (int)ActivityLogEnum.ApplicationID.Pack,
                        EventDateTime = DateTime.Now,
                        EventType = (int)EventType.PackDocumentationPrint,
                        ModuleId = (int)ActivityLogEnum.ModuleID.Pack,
                        OrderNumber = Convert.ToInt32(orderno),
                        ReasonCode = (int)ActivityLogEnum.ResultCd.Success,
                        TerminalId = Shared.UserHostName,
                        UserId = Shared.CurrentUser,
                        InternationalInd = INTERNATIONAL_ORDER ? "T" : "F"
                    });
                }
                else if (strDocs.Contains("DP"))
                {
                    RecordActivity(new UserActivity()
                    {
                        AppSystem = (int)ActivityLogEnum.AppSystem.IHF,
                        ApplicationId = (int)ActivityLogEnum.ApplicationID.Pack,
                        EventDateTime = DateTime.Now,
                        EventType = (int)EventType.DespatchNotePrint,
                        ModuleId = (int)ActivityLogEnum.ModuleID.Pack,
                        OrderNumber = Convert.ToInt32(orderno),
                        ReasonCode = (int)ActivityLogEnum.ResultCd.Success,
                        TerminalId = Shared.UserHostName,
                        UserId = Shared.CurrentUser,
                        InternationalInd = INTERNATIONAL_ORDER ? "T" : "F"
                    });

                }

                #endregion
            }
            catch (Exception ex)
            {
                ErrorManager.ThrowFailToPrint(ex.Message);
            }


        }

        private void RecordActivity(UserActivity ulog)
        {
            ActivityLogDAO ac = new ActivityLogDAO();

            ac.SaveUserActivity(ulog);
        }

        private bool IsMobile(string phone)
        {
            bool retVal = false;

            if (regex.IsMatch(phone))
                retVal = true;

            return retVal;
        }




        #endregion

        #region "Functions for Packstation configuration"
        public List<WorkstationLookup> GetPackstations()
        {
            List<WorkstationLookup> list = new List<WorkstationLookup>();
            WorkstationLookup lookup = new WorkstationLookup();

            List<IDataService> list1;

            list1 = _dal.Get(WorkstationLookup.ClassMethods.ListPackstations.ToString(), this._workstationLookup);

            for (int i = 0; i < list1.Count; i++)
            {
                lookup = (WorkstationLookup)list1[i];
                list.Add(lookup);
            }


            return list;
        }

        public List<StackDetails> GetAvailableStacks(string packstationId, string areaId)
        {
            List<StackDetails> list = new List<StackDetails>();
            StackDetails stackDetails = new StackDetails();

            List<IDataService> list1;

            list1 = _dal.Get(StackDetails.ClassMethods.ListAvailableStack.ToString(), this._stackDetails, new object[] { int.Parse(packstationId), areaId });

            for (int i = 0; i < list1.Count; i++)
            {
                stackDetails = (StackDetails)list1[i];
                list.Add(stackDetails);
            }

            return list;
        }

        public List<StackDetails> GetAssignedStacks(string packstationId, string areaId)
        {
            List<StackDetails> list = new List<StackDetails>();
            StackDetails stackDetails = new StackDetails();

            List<IDataService> list1;

            list1 = _dal.Get(StackDetails.ClassMethods.ListAssignedStack.ToString(), this._stackDetails, new object[] { int.Parse(packstationId), areaId });

            for (int i = 0; i < list1.Count; i++)
            {
                stackDetails = (StackDetails)list1[i];
                list.Add(stackDetails);
            }

            return list;
        }

        public List<StackDetails> GetAssignedStacks()
        {
            List<StackDetails> list = new List<StackDetails>();
            StackDetails stackDetails = new StackDetails();

            List<IDataService> list1;

            list1 = _dal.Get(StackDetails.ClassMethods.ListStackInfo.ToString(), this._stackDetails, new object[] { Shared.UserHostName });

            for (int i = 0; i < list1.Count; i++)
            {
                stackDetails = (StackDetails)list1[i];
                list.Add(stackDetails);
            }

            return list;
        }


        public void AssignStack(int chuteId, string packstationId)
        {
            _dal.ExecuteNonQuery(ASSIGN_CHUTE_TO_PACKSTATION, new object[] { chuteId, int.Parse(packstationId), Shared.CurrentUser });
        }

        public void RemoveStack(int chuteId, string packstationId)
        {
            _dal.ExecuteNonQuery(REMOVE_CHUTE_MAPPING, new object[] { chuteId, int.Parse(packstationId), Shared.CurrentUser });
        }

        public void UpdateStackSelection(int chuteId, string packstationId, string preConfigured)
        {
            _dal.ExecuteNonQuery(UPDATE_STACK_SELECTION, new object[] { chuteId, int.Parse(packstationId), preConfigured, Shared.CurrentUser });
        }

        #endregion

        #endregion
    }
}
