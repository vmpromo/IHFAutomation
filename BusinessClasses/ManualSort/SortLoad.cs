using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using IHF.EnterpriseLibrary.DataServices;


namespace IHF.BusinessLayer.BusinessClasses.ManualSort
{
    public class SortLoad:IDataService 
    {



        #region private members

        private const string GET_AREA_LOAD = "OMS_MANUAL_SORT.F_AREA_LOAD ";

        private List<SortLoad> _lstAreaLoad = new List<SortLoad>();

        #endregion
        
        #region Function Mapping

        public enum ClassMethods
        {
            GetAreaLoad
        }

        #endregion
                
        #region Properties


        public string  PickLoadNo { get; set; }

        public string  PickLoadStatus{ get; set; }

        public bool IsAdminRelease { get; set; }

        public string TrolleyID { get; set; }

        public string ReleaseDate { get; set; }


        public List<SortLoad> SortLoadInfo
        {
            get
            {
                return _lstAreaLoad;
            }
        }



        #endregion 
        
        #region Local Functions

        [MethodMapper("GetAreaLoad", SortLoad.GET_AREA_LOAD)]
        public IList<IDataService> GetAreaLoad(IDataReader reader)
        {

            IList<IDataService> lst = new List<IDataService>();

            List<SortLoad> items = new List<SortLoad>();


            while (reader.Read())
            {

                SortLoad obj = new SortLoad();

                obj.PickLoadNo = reader["PICK_LOAD_NUM"].ToString();
                obj.PickLoadStatus = reader["PICK_LOAD_STATUS"].ToString();
                obj.IsAdminRelease = reader["ADMIN_RELEASED_IND"].ToString() == "T" ? true : false;
                obj.ReleaseDate = reader["RELEASE_DTM"].ToString(); 

                items.Add(obj);
            }

            this._lstAreaLoad = items;

            lst.Add(this);

            reader.Close();

            return lst;
        }



        #endregion



    }
}
