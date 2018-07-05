using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IHF.BusinessLayer.BusinessClasses.Dashboard;
using IHF.EnterpriseLibrary.DataServices;
using System.Data;


namespace IHF.BusinessLayer.BusinessClasses.Dashboard
{
    public class LoadDetail: IDataService
    {

        #region private member


        private const string LOAD_DETAIL = "OMS_DASHBOARD_REPORT.F_LOAD_DETAIL";

        private List<LoadDetail> lstOpView = new List<LoadDetail>();

        #endregion

        #region Function Mapping


        public enum ClassMethods
        {

            GetLoadDetail
        }


        #endregion

        #region Properties

        public List<LoadDetail> OverviewInfo
        {

            get
            {

                return lstOpView;
            }

            set
            {

                lstOpView = value;
            }


        }

        public string LoadNumber { get; set; }

        public DateTime OrderDate { get; set; }

        public string CarrierServiceGroup { get; set; }

        public string  ServiceGroupDescr { get; set; }

        public string  ServiceType  { get; set; }

        public string ServiceTypeDescr { get; set; }

        public string Sku { get; set; }

        public string SkuDescr { get; set; }

        public string Barcode { get; set; }

        public string ItemStatus { get; set; }

        public DateTime LastActionTime { get; set; }


        #endregion

        #region Local Functions


        [MethodMapper("GetLoadDetail", LoadDetail.LOAD_DETAIL)]
        public IList<IDataService> GetLoadManagementlOverview(IDataReader reader)
        {
            string loadnumber = string.Empty;
            string timerelease = string.Empty;
            string loadstatus = string.Empty;
            string totalmultis = string.Empty;

            IList<IDataService> lst = new List<IDataService>();

            List<LoadDetail> items = new List<LoadDetail>();

            while (reader.Read())
            {

                LoadDetail obj = new LoadDetail();

                obj.LoadNumber = reader["PICK_LOAD_NUM"].ToString();
                obj.OrderDate = Convert.ToDateTime(reader["ORDERDATE"].ToString());
                obj.CarrierServiceGroup = reader["SERVICE_GROUP"].ToString();
                obj.ServiceGroupDescr = reader["SERVICE_GROUP_DESC"].ToString();
                obj.ServiceType = reader["SERVICE_TYPE"].ToString();
                obj.ServiceTypeDescr = reader["SERVICE_TYPE_DESC"].ToString();
                obj.Sku = reader["SKU"].ToString();
                obj.SkuDescr = reader["SKUDESCR"].ToString();
                obj.Barcode = reader["BARCODE"].ToString();
                obj.LastActionTime = Convert.ToDateTime(reader["LASTACTIONTIME"].ToString());
                obj.ItemStatus = reader["ITEMSTATUS"].ToString();


                items.Add(obj);
            }


            this.OverviewInfo = items;
            lst.Add(this);
            reader.Close();

            return lst;
        }


        #endregion

    }
}
