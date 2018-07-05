using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using IHF.EnterpriseLibrary.DataServices;

namespace IHF.BusinessLayer.BusinessClasses.ManualSort
{
    public class ManualSortScan : IDataService
    {


        #region private members

        private const string GET_ITEM_SCAN = "OMS_MANUAL_SORT.F_MANUAL_SORT";

        private List<ManualSortScan> _lstSortScan = new List<ManualSortScan>();

        #endregion

        #region Function Mapping

        public enum ClassMethods
        {
            GetItemScan
        }

        #endregion

        #region Properties


        public string AreaID { get; set; }

        public string LoadNo { get; set; }

        public string ChuteID { get; set; }

        public string OrderNo { get; set; }

        public string ItemNo { get; set; }

        public string DisplayChute { get; set; }

        public string ActionScan { get; set; }

        public bool ActionResult { get; set; }

        public string ActionMessage { get; set; }

        public string ScanBarcode { get; set; }

        public bool LoadSorted { get; set; }

        public bool PushToChuteOverFlow { get; set; }


        public List<ManualSortScan> SortScanInfo
        {
            get
            {
                return _lstSortScan;
            }
        }



        #endregion

        #region Local Functions

        [MethodMapper("GetItemScan", ManualSortScan.GET_ITEM_SCAN)]
        public IList<IDataService> GetItemScan(IDataReader reader)
        {

            IList<IDataService> lst = new List<IDataService>();


            if (reader.Read())
            {


                this.AreaID = reader["AREA_ID"].ToString();
                this.LoadNo = reader["LOAD_NO"].ToString();
                this.ChuteID = reader["CHUTE_ID"].ToString();
                this.OrderNo = reader["ORDER_NUMBER"].ToString();
                this.ItemNo = reader["ITEM_NUMBER"].ToString();
                this.DisplayChute = reader["CHUTE_LABEL"].ToString();
                this.ActionScan = reader["SCAN_MODE"].ToString();
                this.ActionResult = reader["ACTION_RESULT"].ToString() == "T" ? true : false;
                this.ActionMessage = reader["ERROR_MSG"].ToString();
                this.LoadSorted = reader["LOAD_SORTED"].ToString() == "T" ? true : false;
                this.PushToChuteOverFlow = reader["CHUTE_OVERFLOW"].ToString() == "T" ? true : false;


            }


            lst.Add(this);

            reader.Close();

            return lst;
        }



        #endregion


    }
}
