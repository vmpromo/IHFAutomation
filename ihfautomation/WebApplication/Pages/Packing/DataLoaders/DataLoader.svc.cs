//
// Name: DataLoaders.cs
// Type: Interface class
// Description: contains the class variables, and Inerface methods
//              associated with the Pack entities
//
//$Revision:   1.10$
//
// Version   Date        Author    Reason
//  1.0      12/07/11    MSalman   Released version
//  1.1      14/07/11    MSalman   function updated. 
//  1.2      19/07/11    MSalman   new function added. 
//  1.3      20/07/11    MSalman   new function added. 
//  1.4      28/07/11    MSalman   new function added. 
//  1.5      05/09/11    MSalman   new function added. 
//  1.6      01/12/11    MSalman   Change Request New functions added for trolley view.     
//  1.10     12/04/12    M Khan    Servive calls for Packstation configuration
//  1.11     08/10/14    S Green   New function to determine master packer from user barcode

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;
using System.ServiceModel.Activation;
using IHF.BusinessLayer.BusinessClasses.Packing;
using IHF.BusinessLayer.DataAccessObjects.Packing;
using IHF.Security.UserManagement;
using IHF.BusinessLayer.DataAccessObjects;
using IHF.BusinessLayer.BusinessClasses;
using IHF.BusinessLayer.BusinessClasses.Stack;
using System.Data;
using System.Configuration;

namespace IHF.ApplicationLayer.Web.Pages.Packing.DataLoaders
{

    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class DataLoader
    {

        #region private members

        //TODO:Change the name of the this object it is not correct.
        PackingDAO _packingDAO = new PackingDAO();
        LookupDAO _lookup = new LookupDAO();
        UserDAO _userDAO = new UserDAO();


        #endregion

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Bare,
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public PackResponse ValidateRequest(PackRequest request)
        {

            PackResponse res = _packingDAO.ValidateContainer(request);

            return res;

        }

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Bare,
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public PackResponse PackScanRequest(PackRequest req)
        {

            PackResponse res = _packingDAO.PackScanRequest(req);

            return res;
        }



        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Bare,
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public List<KeyValuePair<string, string>> ReasonCode()
        {
            return _lookup.GetReasonCode();
        }



        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Bare,
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public string UnDockContainer(PackRequest req)
        {
            return _packingDAO.UnDockContainer(req);

        }



        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Bare,
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public void RePrintDoc(PackRequest req)
        {
            _packingDAO.RePrintDocs(req.OrderNo, req.Docs);

        }

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Bare,
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public List<KeyValuePair<string, string>> GetMissingItem(string orderNo)
        {
            return _lookup.GetMissingItem(orderNo);

        }


        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Bare,
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public PackResponse PrintDocs(PackRequest req)
        {

            return _packingDAO.GenAndPrintDocs(req);

        }

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Bare,
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public string EndPackProcess(PackRequest req)
        {

            return _packingDAO.EndPackProcess(req);

        }


        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Bare,
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public TrolleyView GetTrolleyView()
        {
            return _packingDAO.GetTrolleyView();

        }

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Bare,
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public string MasterPacker(string userBarcode)
        {
            
            string isMasterPacker = "F";
            string userLogin = "";

            // try to find the userlogin for the user barcode
            DataSet userDetailsDs = _userDAO.GeUserByBarcode(userBarcode);
            DataTable userDetailsDt = userDetailsDs.Tables[0];
            
            foreach (DataRow row in userDetailsDt.Rows)
            {
                userLogin = row["userlogin"].ToString();
            }


            // if the user name is empty do not do any further validation - not a master packer
            if (userLogin != string.Empty)
            {
                // Get the list of user groups which have access to the advanced packing funcionality
                string[] advancedPackingGroups = ConfigurationManager.AppSettings["AdvancedPackingGroups"].Split(',');

                IHFRoleProvider roleprovider = new IHFRoleProvider();

                // Now determine if one of these groups is 
                foreach (string grp in advancedPackingGroups)
                {
                    if (roleprovider.IsUserInRole(userLogin, grp))
                    {
                        isMasterPacker = "T";
                    }
                }

            }
            
            return isMasterPacker;

        }


        #region "Packstation confguration service calls"

        [OperationContract]
        [WebInvoke(Method="POST", BodyStyle= WebMessageBodyStyle.Bare,
                   RequestFormat=WebMessageFormat.Json, ResponseFormat=WebMessageFormat.Json)]
        public List<WorkstationLookup> Packstations()
        {
            return _packingDAO.GetPackstations();
        }


        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Bare,
                   RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public List<KeyValuePair<string, string>> Areas()
        {
            return _lookup.GetSortArea();
        }

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Bare,
                   RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public List<StackDetails> AvailableStacks(WorkstationLookup packstation)
        {
            return _packingDAO.GetAvailableStacks(packstation.value, packstation.areaid);
        }

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Bare,
                   RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public List<StackDetails> AssignedStacks(WorkstationLookup packstation)
        {
            return _packingDAO.GetAssignedStacks(packstation.value, packstation.areaid);
        }

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Bare,
                   RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public StackDetails StackView()
        {
            return _packingDAO.GetAssignedStacks()[0];
        }


        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Bare,
                   RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public void AssignStack(StackDetails stack)
        {
            _packingDAO.AssignStack(stack.ChuteId, stack.PackstationId);
        }


        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Bare,
                   RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public void RemoveStack(StackDetails stack)
        {
            _packingDAO.RemoveStack(stack.ChuteId, stack.PackstationId);
        }

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Bare,
                   RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public void UpdateStackSelection(StackDetails stack)
        {
            _packingDAO.UpdateStackSelection(stack.ChuteId, stack.PackstationId, stack.PreConfigured);
        }


        #endregion

    }
}
