using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using IHF.EnterpriseLibrary.DataServices;

namespace IHF.BusinessLayer.BusinessClasses.ManualSort
{
    public class SortArea : IDataService
    {


        #region private members

        private const string GET_MANUAL_AREA = "OMS_MANUAL_SORT.F_MANUAL_SORT_AREA";

        private List<SortArea> _lstSortAreas = new List<SortArea>();

        #endregion


        #region Function Mapping

        public enum ClassMethods
        {
            GetManualAreas
        }

        #endregion




        #region Properties

        public string AreaID { get; set; }
        public string AreaType { get; set; }
        public string Description { get; set; }
        public bool HandleSplitLoad { get; set; }


        public List<SortArea> SortAreaInfo
        {
            get
            {
                return _lstSortAreas;
            }
        }

        #endregion



        #region Local Functions

        [MethodMapper("GetManualAreas", SortArea.GET_MANUAL_AREA)]
        public IList<IDataService> GetManualAreas(IDataReader reader)
        {

            IList<IDataService> lst = new List<IDataService>();

            List<SortArea> items = new List<SortArea>();


            while (reader.Read())
            {

                SortArea obj = new SortArea();

                obj.AreaID = reader["AREA_ID"].ToString();
                obj.AreaType = reader["AREA_TYPE_ID"].ToString();
                obj.Description = reader["AREA_DESCR"].ToString();
                obj.HandleSplitLoad = reader["HANDLE_SPLIT_LOAD_IND"].ToString() == "T" ? true : false;

                items.Add(obj);
            }

            this._lstSortAreas = items;

            lst.Add(this);

            reader.Close();

            return lst;
        }



        #endregion




    }
}
