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

namespace IHF.ApplicationLayer.Web.Pages.Returns.Service
{    
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]


    // NOTE: the ReturnService with the method GetResponse of OrderheaderResponse type to get all the required details
    public class ReturnService
    {
         [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Bare,
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]

        
        public OrderHeaderResponse GetResponse(ConsignmentOrderNumberRequest  ConsignmentOrderNumber)
        {
              {
            ReturnsDAO dao = new ReturnsDAO();
                  // to all the Order header deatils
            DataSet orderH = dao.getOrderDetails(ConsignmentOrderNumber.SearchKey);

            var OrderHdr = orderH.Tables[0];
            if (OrderHdr.Rows.Count == 0)
            {
                return null;
            }

          DataSet OrderD = dao.getItemsToReturn(int.Parse(orderH.Tables[0].Rows[0]["ORDERNUMBER"].ToString()));
                  var OrderDtl = OrderD.Tables[0];

                  var Hdr = OrderHdr.Rows[0];
                  var Dtl = OrderDtl.Rows[0];

                  var itemsList = new List<OrderDetailResponse>();

                  int? reasonCode;
                  if (Dtl["action"] == DBNull.Value)
                  {
                      reasonCode = null;
                  }
                  else
                  {
                      reasonCode = int.Parse(Dtl["action"].ToString());
                  }

                  foreach(var RowValue in OrderDtl.Rows) {
                      var item = new OrderDetailResponse();

                      item.Sku = int.Parse(Dtl["sku"].ToString());
                      item.ItemNumber = int.Parse(Dtl["itemnumber"].ToString());
                      item.Description= Dtl["skudescr"].ToString();
                      item.ReasonCode =  reasonCode;
                      itemsList.Add(item);
                  }

            return new OrderHeaderResponse
            {
                OrderNumber  = int.Parse(Hdr["ORDERNUMBER"].ToString()),
                CustomerURN = int.Parse(Hdr["CUSTOMERURN"].ToString()),
                CustomerName = Hdr["customername"].ToString(),
                OrderDate = Hdr["ORDERDATE"].ToString(),
                PostCode = Hdr["customername"].ToString(),
                Parcel_Scanned_Ind = Hdr["PARCEL_SCANNED_IND"].ToString(),
                Items = itemsList
            };
        }

        }
    }


public class ConsignmentOrderNumberRequest
{
    public string SearchKey{get; set;}

}

    // Response to get the Order Header Information and the Order Details information as a sub class
    public class OrderHeaderResponse
{
    public int OrderNumber{get; set;}
    public int CustomerURN{get; set;}
    public string CustomerName{get; set;}
    public string OrderDate{get; set;}
    public string PostCode{get; set;}
    public string Parcel_Scanned_Ind{get; set;}
    public List<OrderDetailResponse> Items{get; set;}

}
    // Response to get the Order Details 
    public class OrderDetailResponse
    {

        public int Sku{get; set;}
        public int ItemNumber{get; set;}
        public string  Description {get; set;}
        public int? ReasonCode{get; set;}
    }

}



