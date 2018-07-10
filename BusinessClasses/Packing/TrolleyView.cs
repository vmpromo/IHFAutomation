// Name: TrolleyView.cs
// Type: Business Entity Class for Packing.
// Description: Class contains properties, and methods use to call
//              database function for packing.
//
//$Revision:   1.2  $
//
// Version   Date        Author    Reason
//  1.1      01/12/11    MSalman   Initial Released


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using IHF.BusinessLayer.BusinessClasses.Packing;
using IHF.EnterpriseLibrary.DataServices;


namespace IHF.BusinessLayer.BusinessClasses.Packing
{
 
    public class TrolleyView : IDataService
    {


        #region Private Memeber

        private const string GET_TROLLEY_VIEW = "OMS_PACK.F_TROLLEY_VIEW";

        private const string TROLLEY_STATUS = "COMPLETE";

        private const string COMPLETE_TROLLEY = "Complete For Pack";

        private const string UN_COMPLETE_TROLLEY = "Packing Available";



        private IList<TrolleyView> _lstTrolleyView = new List<TrolleyView>();


        #endregion

        #region Function Mapping

        public enum ClassMethods
        {
            GetTrolleyView

        }

        #endregion

        #region  Properties

        public string  TrolleyId { get; set; }

        public string  TrolleyLabel { get; set; }

        public string  ChuteLabel { get; set; }

        public string Status { get; set; }

        public string StatusDescription { get; set; }


        public IList<TrolleyView> TrolleyViewInfo
        {
            get
            {
                return _lstTrolleyView;
            }

            set
            {
                _lstTrolleyView = value;

            }

        }


        #endregion

        #region Local Functions


        [MethodMapper("GetTrolleyView", TrolleyView.GET_TROLLEY_VIEW)]
        public IList<IDataService> GetTrolleyView(IDataReader reader)
        {

            IList<IDataService> lst = new List<IDataService>();

            List<TrolleyView> items = new List<TrolleyView>();


            while (reader.Read())
            {
                TrolleyView obj = new TrolleyView();

                obj.TrolleyId = reader["TROLLEY_ID"].ToString() ?? string.Empty;
                obj.TrolleyLabel = reader["TROLLEY_LABEL"].ToString() ?? string.Empty;
                obj.Status = reader["STATUS"].ToString() ?? string.Empty;

                if (obj.Status.Equals(TROLLEY_STATUS))
                    obj.StatusDescription = COMPLETE_TROLLEY;
                else
                    obj.StatusDescription = UN_COMPLETE_TROLLEY;

                obj.ChuteLabel = reader["CHUTE_LABEL"].ToString() ?? string.Empty;

                items.Add(obj);
            }

            this._lstTrolleyView = items;

            lst.Add(this);

            reader.Close();

            return lst;
        }

        #endregion



    }
}
