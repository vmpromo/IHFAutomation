// Name: ReturnsService.svc.cs
// Type: class file 
// Description: Code behind ReturnService.svc
//   
//
//$Revision:   1.22  $
//
// Version   Date        Author     Reason
//  1.0      13/02/18    S Remedios Initial Revision
//  1.1      14/02/18    A Petrescu Changed POST to get, fixed minor issue
//  1.2      16/02/18    A Petrescu Introduced 404 status for not found; minor fixes.
//  1.3      19/02/18    A Petrescu Fixes, service2
//  1.4      19/02/18    A Petrescu Fixes for IE8.
//  1.5      20/02/18    A Petrescu Fixed DNS resolve crash
//  1.6      23/02/18    M Cackett  Fixed bug with hostname, added banner and comments.
//  1.7      27/02/18    A Petrescu Changed customer URN to string, to avoid truncating leading zeroes
//  1.8      01/03/18    A Petrescu Returned action code description from service 1
//  1.9      01/03/18    A Petrescu Calling location microservice
//  1.10     01/03/18    A Petrescu Saving sku barcode to OMS Activity log
//  1.11     02/03/18    c Changed validation, to accept search keys up to 18 characters
//  1.12     02/03/18    M Cackett   Stopped calling location microservice for mispick, cust serv, damaged
//                       A Petrescu
//  1.13     06/03/18    M Cackett  Integration with PrintService for putaway label
//                       J Duru
//  1.14     12/03/18    A Petrescu Sku barcode validation
//  1.15     14/03/18    M Cackett  Call f_create_lpn for DC Improvements
//                       S Remedios
//  1.16     15/03/18    A Petrescu  Removed substring function
//  1.17     16/03/18    A Petrescu  Surfaced error when creating LPN twice. Fixed returning same item multiple times.
//  1.18     27/03/18    A Petrescu  Sku barcode returned in f_get_items_to_return
//  1.19     27/03/18    A Petrescu  Implemented 2 users returning same item
//  1.20     10/04/18    M Cackett  added the LPN and Putaway_flag for the screen(UI) for DC Improvements
//                       S Remedios
//  1.22     31/05/18    A Petrescu Changed service method to POST from GET, because of IE caching issue.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using IHF.BusinessLayer.DataAccessObjects.Returns;
using System.Data;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Data.OracleClient;
using System.Configuration;
using IHF.BusinessLayer.DataAccessObjects.ActivityLog;
using IHF.BusinessLayer.BusinessClasses.ActivityLog;
using IHF.BusinessLayer.Util;
using IHF.BusinessLayer.DataAccessObjects;
using IHF.ApplicationLayer.Web.Code.WcfBehaviours;



namespace IHF.ApplicationLayer.Web.Pages.Returns.Service
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ErrorLoggingBehaviour]


    // CLASS: ReturnService 
    // Has methods:     GetResponse of OrderheaderResponse type to get all the required details
    //                  ReturnItem of ReturnRequest type.  Performs the return actions.

    public class ReturnService
    {
        private readonly Dictionary<ActionCode, string> _locationStrings;

        private readonly ReturnsDAO _returnsDao;
        private readonly LocationServiceWrapper _locationService;
        private readonly string _noLocationString;
        private readonly ActivityLogDAO _activityLog;
        private readonly PrintService _printService;

        private const HttpStatusCode FailAndShowError = (HttpStatusCode)450;
        private const HttpStatusCode FailAndAlert = (HttpStatusCode)451;
        

        public ReturnService()
        {
            _returnsDao = new ReturnsDAO();
            _locationService = new LocationServiceWrapper();
            _noLocationString = ConfigurationManager.AppSettings["NoLocationString"];
            _activityLog = new ActivityLogDAO();
            _printService = new PrintService();

            _locationStrings = new Dictionary<ActionCode, string> {
                { ActionCode.DamagedReturn, "DAMAGED" },
                { ActionCode.CustomerServicesReturn, "CUSTOMER SERVICES" },
                { ActionCode.MispickReturn, "MISPICK"}
            };
        }

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Bare,
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]

        //
        //  GetResponse
        //  Returns:    OrderHeaderResponse (including nested OrderDetailResponses)
        //  Params:     searchKey  - order/consignment number to search for.
        //
        public bool ReprintLPN(LpnReprintRequest reprintRequest)
        {
            
            // to all the the lpn details
            DataSet ReprintLPN = _returnsDao.getLpnToReprint(reprintRequest.ItemNumber);

            var ReprintLpnDtl = ReprintLPN.Tables[0];
            if (ReprintLpnDtl.Rows.Count == 0)
            {
                //return status 404, return message "Order not found"
                throw new WebFaultException<ErrorMessage>(
                    new ErrorMessage { Message = "No LPN Details  " + reprintRequest.ItemNumber },
                    HttpStatusCode.NotFound);
            }


            var Dtl = ReprintLpnDtl.Rows[0];


            var ipAddress = HttpContext.Current.Request.ServerVariables["remote_addr"];
            string hostName;
            try
            {
                hostName = System.Net.Dns.GetHostEntry(ipAddress).HostName;
            }
            catch
            {
                hostName = ipAddress;
            }

            
            try
            {
                // DC Improvements: Call Print service to reprint PutAway label          
                _printService.PrintPutAway( Dtl["lpn"].ToString(),
                                            Dtl["suggested_loc"].ToString(),
                                            Dtl["skualias"].ToString(),
                                            Dtl["sku"].ToString(),
                                            hostName, true);


            }
            catch (Exception exception)
            {
                //return status 450, return error message from _printService.PrintPutAway
                // in case of print failure.
                throw new WebFaultException<ErrorMessage>(
                    new ErrorMessage { Message = exception.Message },
                    FailAndShowError);
            }

            return true;
        }





        
        [OperationContract]
        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.Bare,
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]

        //
        //  GetResponse
        //  Returns:    OrderHeaderResponse (including nested OrderDetailResponses)
        //  Params:     searchKey  - order/consignment number to search for.
        //
        public OrderHeaderResponse GetResponse(string searchKey)
        {
            var cleansedSearchKey = Regex.Replace(searchKey, "[^.0-9]", "");
            if (cleansedSearchKey.Length > 18)
            {
                throw new WebFaultException<ErrorMessage>(
                       new ErrorMessage { Message = "Search key exceeds maximum length." },
                       HttpStatusCode.NotFound);
            }


            // to all the Order header deatils
            DataSet orderH = _returnsDao.getOrderDetails(cleansedSearchKey);

            var OrderHdr = orderH.Tables[0];
            if (OrderHdr.Rows.Count == 0)
            {
                //return status 404, return message "Order not found"
                throw new WebFaultException<ErrorMessage>(
                    new ErrorMessage { Message = "Order or Consignment " + searchKey + " not found" },
                    HttpStatusCode.NotFound);
            }

            DataSet OrderD = _returnsDao.getItemsToReturn(int.Parse(orderH.Tables[0].Rows[0]["ORDERNUMBER"].ToString()));
            var OrderDtl = OrderD.Tables[0];

            if (OrderDtl.Rows.Count == 0)
            {
                //return status 404, return message "Order not found"
                throw new WebFaultException<ErrorMessage>(
                    new ErrorMessage { Message = "Order or Consignment " + searchKey + " not found" },
                    HttpStatusCode.NotFound);
            }

            var Hdr = OrderHdr.Rows[0];

            var itemsList = new List<OrderDetailResponse>();

            foreach (DataRow Dtl in OrderDtl.Rows)
            {

                //transform reasonCode null into 0, because IE8 can't deserialize both numbers and null in the same array
                int reasonCode;
                if (Dtl["action"] == DBNull.Value)
                {
                    reasonCode = 0;
                }
                else
                {
                    reasonCode = int.Parse(Dtl["action"].ToString());
                }

                string reasonDescripton;
                if (Dtl["actiondescr"] == DBNull.Value)
                {
                    reasonDescripton = null;
                }
                else
                {
                    reasonDescripton = Dtl["actiondescr"].ToString();
                }
                string skuBarcode;
                if (Dtl["skualias"] == DBNull.Value)
                {
                    skuBarcode = null;
                }
                else
                {
                    skuBarcode = Dtl["skualias"].ToString();
                }


                var item = new OrderDetailResponse();

                item.Sku = int.Parse(Dtl["sku"].ToString());
                item.ItemNumber = int.Parse(Dtl["itemnumber"].ToString());
                item.Description = Dtl["skudescr"].ToString();
                item.LPN = Dtl["lpn"].ToString();
                item.PutawayLoc = Dtl["putaway_loc"].ToString();
                item.ReasonCode = reasonCode;
                item.ReasonDescription = reasonDescripton;
                item.SkuBarcode = skuBarcode;
                itemsList.Add(item);
            }

            var orderDate = (DateTime)Hdr["ORDERDATE"];

            //add no-cache and immediate expiration because IE8 caches Get requests
            HttpContext.Current.Response.CacheControl = "no-cache";
            HttpContext.Current.Response.Expires = -1;

            return new OrderHeaderResponse
            {
                OrderNumber = int.Parse(Hdr["ORDERNUMBER"].ToString()),
                CustomerURN = Hdr["CUSTOMERURN"].ToString(),
                CustomerName = Hdr["CUSTOMERNAME"].ToString(),
                OrderDate = orderDate.ToString("dd-MMM-yyyy"),
                PostCode = Hdr["POSTCODE"].ToString(),
                Parcel_Scanned_Ind = Hdr["PARCEL_SCANNED_IND"].ToString(),
                Items = itemsList
            };
        }


        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Bare,
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]

        //
        //  ReturnItem
        //  Returns:    Nothing.
        //  Params:     returnRequest  - details of the item being returned.
        //
        public void ReturnItem(ReturnRequest returnRequest)
        {
            string location;

            var skuAlias = _returnsDao.GetSkuAliasFromSku(returnRequest.Sku.ToString());
            if (skuAlias == null)
            {
                throw new WebFaultException<ErrorMessage>(
                    new ErrorMessage { Message = string.Format("Unable to locate SKU {0} Barcode. Check STOCKSKUALIAS table.", returnRequest.Sku) },
                    (HttpStatusCode)450);
            }


            if (returnRequest.ActionCode == (int)ActionCode.SellableReturn)
            {    
                location = _locationService.RetrieveLocationBySkuBarcode(skuAlias);
            }
            else
            {
                location = _locationStrings[(ActionCode)returnRequest.ActionCode];
            }   

            // Create the LPN 
            string lpn;
            try
            {
                lpn = _returnsDao.createLPN(HttpContext.Current.User.Identity.Name, returnRequest.ActionCode, returnRequest.ItemNumber, location);
            }
            catch
            {
                throw new WebFaultException<ErrorMessage>(
                    new ErrorMessage { Message = string.Format(
                        "Failed to create LPN for item {0}, sku {1}. Item might have been returned by different user.",
                        returnRequest.ItemNumber,
                        returnRequest.Sku) },
                    FailAndAlert);
            }

            // Try to get the machine name, if we can't (ie if called remotely) then use ip address
            var ipAddress = HttpContext.Current.Request.ServerVariables["remote_addr"];
            string hostName;
            try
            {
                hostName = System.Net.Dns.GetHostEntry(ipAddress).HostName;
            }
            catch
            {
                hostName = ipAddress;
            }


            try
            {
                _returnsDao.ReturnItem(
                    returnRequest.Parcel_Scanned_Ind,
                    returnRequest.ItemNumber,
                    returnRequest.OrderNumber,
                    returnRequest.ActionCode.ToString(),
                    returnRequest.TaskDescription,
                    returnRequest.CustomerURN,
                    returnRequest.Sku.ToString(),
                    HttpContext.Current.User.Identity.Name,
                    hostName);

            }
            catch (Exception exception)
            {
                //return status 417, return error message from oms_returns.p_return_item
                throw new WebFaultException<ErrorMessage>(
                    new ErrorMessage { Message = exception.Message },
                    HttpStatusCode.ExpectationFailed);
            }

 
            try
            {
                // DC Improvements: Call Print service to print PutAway label
                //  Hard coded LPN parameters to be changed as part of generate LPN story            
                _printService.PrintPutAway(lpn, location, skuAlias, returnRequest.Sku.ToString(), hostName, true);

                //  Hard coded hostname for local wip testing - To be removed before go live
                //  _printService.PrintPutAway("KS10002000", "KS10002000", location, skuAlias, returnRequest.Sku.ToString(), "tsdcpri-tst02.hq.river-island.com", true);

            }
            catch (Exception exception)
            {
                //return status 450, return error message from _printService.PrintPutAway
                // in case of print failure.
                throw new WebFaultException<ErrorMessage>(
                    new ErrorMessage { Message = exception.Message },
                    FailAndShowError);
            }
        }

        private void RecordActivity(UserActivity userActivity)
        {
            throw new NotImplementedException();
        }
    }

    // CLASS:   ErrorMessage
    //          Utility class for handling errors.
    //
    public class ErrorMessage
    {
        public string Message { get; set; }
    }

    // CLASS:   OrderHeaderResponse
    // Response to get the Order Header Information and the Order Details information as a sub class
    //
    public class OrderHeaderResponse
    {
        public int OrderNumber { get; set; }
        public string CustomerURN { get; set; }
        public string CustomerName { get; set; }
        public string OrderDate { get; set; }
        public string PostCode { get; set; }
        public string Parcel_Scanned_Ind { get; set; }
        public List<OrderDetailResponse> Items { get; set; }

    }

    // CLASS:   OrderDetailResponse
    // Response to get the Order Details 
    //
    public class OrderDetailResponse
    {
        public int Sku { get; set; }
        public int ItemNumber { get; set; }
        public string Description { get; set; }
        public int ReasonCode { get; set; }
        public string ReasonDescription { get; set; }
        public string SkuBarcode { get; set; }
        public string LPN { get; set; }
        public string PutawayLoc { get; set; }
    }

    // CLASS:   ReturnRequest
    // Details of the item being returned.
    //
    public class ReturnRequest
    {
        public string Parcel_Scanned_Ind { get; set; }
        public int ItemNumber { get; set; }
        public int OrderNumber { get; set; }
        public int ActionCode { get; set; }
        public string TaskDescription { get; set; }
        public string CustomerURN { get; set; }
        public int Sku { get; set; }
    }

    public class LpnReprintRequest
    {
        public int ItemNumber { get; set; }
    }
}



