using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using IHF.BusinessLayer.BusinessClasses;
using Oracle.DataAccess.Client;
using System.Data;

using IHF.BusinessLayer.Util;
using IHF.EnterpriseLibrary;
using IHF.EnterpriseLibrary.Data;
using IHF.EnterpriseLibrary.DataServices;

namespace IHF.BusinessLayer.DataAccessObjects
{
    public class SkuLabelDAO
    {
        #region "private constants"

        private const string SearchSkunumber = "oms_sku_label.f_get_sku_numbers";
        private const string GetSkudetails = "oms_sku_label.f_get_sku_barcode";
        private const string SKUsearch = "oms_sku_label.f_sku_search";
        private const string SKUdtlsearch = "oms_sku_label.f_sku_detail_search";
        private const string status = "oms_sku_label.f_item_status";
        private const string trolleyDropdown = "oms_sku_label.f_get_trolley_dropdown";
        private const string GetSkuForTrolley = "oms_sku_label.f_get_sku_for_trolley";
        private const string GetSkuForChute = "oms_sku_label.f_get_sku_for_chute";
        private const string GetSkuForLoad = "oms_sku_label.f_get_sku_for_load";
        private const string areas = "oms_sku_label.f_get_area_dropdown";

        
        #endregion

        #region "private variables"

        private DataManager dataManager = new DataManager(Util.DBInstanceEnum.Ora);
        

        #endregion

        #region "private methods"

        private List<Order> StrongOrderList(List<IDataService> listOfResult)
        {
            List<Order> listOfOrders = new List<Order>();

            foreach (Order order in listOfResult)
            {
                listOfOrders.Add((Order)order);
            }
            return listOfOrders;
        }

        #endregion

        #region "Methods available to the presentation layer (web)"


        public DataSet Search_sku(string I_barcode)
        {

            Object[] SkunumParams = new Object[] { I_barcode };
            return dataManager.ExecuteDataset(SearchSkunumber.ToString(),
                                                   SkunumParams);
        
        }


        public DataSet Get_sku_details(string I_sku)
        {

            Object[] SkudtlsParams = new Object[] { I_sku };
            return dataManager.ExecuteDataset(GetSkudetails.ToString(),
                                                   SkudtlsParams);

        }


        public DataSet SkuSearch(string I_sku)
        {

            Object[] SkudtlsParams = new Object[] { I_sku};
            return dataManager.ExecuteDataset(SKUsearch.ToString(),
                                                   SkudtlsParams);

        }

        public DataSet SkudetailsSearch(string I_sku, Int32 I_area_id, string I_load)
        {

            Object[] SkudtlsParams = new Object[] { I_sku, I_area_id, I_load };
            return dataManager.ExecuteDataset(SKUdtlsearch.ToString(),
                                                   SkudtlsParams);

        }

        public DataSet itemstatus()
        {

           
            return dataManager.ExecuteDataset(status.ToString(),
                                                   null);

        }

        public DataSet Get_area_dropdown()
        {

            return dataManager.ExecuteDataset(areas.ToString(), null);

        }

        public DataSet Get_trolley_dropdown()
        {


            return dataManager.ExecuteDataset(trolleyDropdown.ToString(),
                                                   null);

        }
        public DataSet SkuFortrolley(Int32 I_trolley_id)
        {

            Object[] SkudtlsParams = new Object[] { I_trolley_id };
            return dataManager.ExecuteDataset(GetSkuForTrolley.ToString(),
                                                   SkudtlsParams);

        }
        public DataSet SkuForChute(Int32 I_chute_id)
        {

            Object[] SkudtlsParams = new Object[] { I_chute_id };
            return dataManager.ExecuteDataset(GetSkuForChute.ToString(),
                                                   SkudtlsParams);

        }
        public DataSet SkuForLoad(string I_load_num)
        {

            Object[] SkudtlsParams = new Object[] { I_load_num };
            return dataManager.ExecuteDataset(GetSkuForLoad.ToString(),
                                                   SkudtlsParams);

        }


        #endregion
    }
}
