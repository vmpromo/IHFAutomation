using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using IHF.EnterpriseLibrary.DataServices;

namespace IHF.BusinessLayer.BusinessClasses.ManualSort
{

    public class ManualSingleLocate : IDataService
    {



        #region private members

        private const string GET_LOCATE_SINGLE = "OMS_MANUAL_SORT.F_LOCATE_SINGLE";

        private List<ManualSingleLocate> _lstSingleLocate = new List<ManualSingleLocate>();

        #endregion


        #region Function Mapping

        public enum ClassMethods
        {
            GetSingleLocate
        }

        #endregion


        #region Properties

        public string ChuteID { get; set; }
        public string TrolleyID { get; set; }
        public string ScanMode { get; set; }
        public string  ScanBarcode { get; set; }
        public bool ActionResult { get; set; }
        public string ActionMessage { get; set; }


        public List<ManualSingleLocate> SingleLocateInfo
        {
            get
            {
                return _lstSingleLocate;
            }
        }

        #endregion


        #region Local Functions

        [MethodMapper("GetSingleLocate", ManualSingleLocate.GET_LOCATE_SINGLE)]
        public IList<IDataService> GetSingleLocate(IDataReader reader)
        {

            IList<IDataService> lst = new List<IDataService>();


            if (reader.Read())
            {


                this.ChuteID = reader["CHUTE_ID"].ToString();
                this.TrolleyID = reader["TROLLEY_ID"].ToString();
                this.ScanMode = reader["SCAN_MODE"].ToString();
                this.ActionResult = reader["ACTION_RESULT"].ToString() == "T" ? true : false;
                this.ActionMessage = reader["ERROR_MSG"].ToString();

            }


            lst.Add(this);

            reader.Close();

            return lst;
        }



        #endregion


    }
}
