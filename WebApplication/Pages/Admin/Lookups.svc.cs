using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using Telerik.Web.UI;
using IHF.BusinessLayer.DataAccessObjects;
using System.ServiceModel.Web;

//namespace IHF.ApplicationLayer.Web.Resources
//{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Lookups" in code, svc and config file together.
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class Lookups
    {
        public void DoWork()
        {
        }

        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
            RequestFormat = WebMessageFormat.Xml, ResponseFormat = WebMessageFormat.Xml)]
        public RadComboBoxData LoadStoreDDL(RadComboBoxContext context)
        {
            //The RadComboBoxData object contains all required information for load on demand:
            // - the items
            // - are there more items in case of paging
            // - status message to be displayed (which is optional)
            RadComboBoxData result = new RadComboBoxData();

            LookupDAO lkp = new LookupDAO();

            List<KeyValuePair<string, string>> stores = lkp.GetStore();

            //Get all items from the Customers table. This query will not be executed untill the ToArray method is called.
            var allStores = from store in stores
                            orderby store.Key, store.Value
                            select new RadComboBoxItemData
                            {
                                Text = store.Key + " - " + store.Value,
                                Value = store.Key
                            };


            //In case the user typed something - filter the result set
            string text = context.Text;
            if (!String.IsNullOrEmpty(text))
            {
                allStores = allStores.Where(item => item.Text.StartsWith(text));
            }
            //Perform the paging
            // - first skip the amount of items already populated
            // - take the next 10 items
            int numberOfItems = context.NumberOfItems;
            var storelist = allStores.Skip(numberOfItems).Take(10);

            //This will execute the database query and return the data as an array of RadComboBoxItemData objects
            result.Items = storelist.ToArray();


            int endOffset = numberOfItems + storelist.Count();
            int totalCount = allStores.Count();

            //Check if all items are populated (this is the last page)
            if (endOffset == totalCount)
                result.EndOfItems = true;

            //Initialize the status message
            result.Message = String.Format("Items <b>1</b>-<b>{0}</b> out of <b>{1}</b>",
                                           endOffset, totalCount);

            return result;
        }


    }
//}
