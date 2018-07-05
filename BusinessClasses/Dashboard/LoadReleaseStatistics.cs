using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IHF.BusinessLayer.BusinessClasses.Dashboard;
using IHF.EnterpriseLibrary.DataServices;
using System.Data;

namespace IHF.BusinessLayer.BusinessClasses.Dashboard
{
    [Serializable]
    public class LoadReleaseStatistics : IDataService
    {
        #region private member

        private List<LoadReleaseStatistics> _loadreleasestatistics = new List<LoadReleaseStatistics>();
        private List<string> _linkedUnreleasedLoads = new List<string>();
        private List<string> _linkedReleasedLoads = new List<string>();
        private const string _getloadstatistics = "OMS_DASHBOARD_REPORT.F_LOAD_RELEASE_STATISTICS";
 //       private const string _getlinkedloads = "OMS_DASHBOARD_REPORT.F_LINKED_LOAD_LIST";

        #endregion

        #region Function Mapping

        public enum ClassMethods
        {

            GetLoads
//            GetLinkedLoads

        }

        #endregion


        #region Properties

        public string LoadNumber { get; set; }

        public DateTime InterfaceDateTime { get; set; }

        public Int32 SingleOrders { get; set; }

        public Int32 MultiOrders { get; set; }

        public Int32 InternationalOrders { get; set; }

        public Int32 MultiItems { get; set; }

        public float PercentageComplete { get; set; }

        public Int32 MissingItems { get; set; }

        public List<LoadReleaseStatistics> LoadStatistics
        {
            get
            {
                return _loadreleasestatistics;
            }
            set
            {
                _loadreleasestatistics = value;
            }
        }
        

        //public List<string> LinkedReleasedLoads
        //{
        //    get
        //    {
        //        return _linkedReleasedLoads;
        //    }

        //    set
        //    {
        //        _linkedReleasedLoads = value;
        //    }
        //}

        //public List<string> LinkedUnreleasedLoads
        //{
        //    get
        //    {
        //        return _linkedUnreleasedLoads;
        //    }

        //    set
        //    {
        //        _linkedUnreleasedLoads = value;
        //    }
        //}


        #endregion


        #region LocalFunctions

        [MethodMapper("GetLoads", LoadReleaseStatistics._getloadstatistics)]
        public IList<IDataService> GetLoads(IDataReader reader)
        {
            string pctComplete = String.Empty;
            string singleOrders = String.Empty;
            string multiOrders = String.Empty;
            string multiItems = String.Empty;
            string missingItems = String.Empty;
            string ifdate = String.Empty;
            string internationalOrders = String.Empty;

            IList<IDataService> lst = new List<IDataService>();

            List <LoadReleaseStatistics> list = new List<LoadReleaseStatistics>();

            while (reader.Read())
            {

                LoadReleaseStatistics obj = new LoadReleaseStatistics();

                obj.LoadNumber = reader["PICK_LOAD_NUM"].ToString();
                ifdate = reader["INTERFACED_DTM"].ToString();
                obj.InterfaceDateTime = Convert.ToDateTime(ifdate);

                //singleOrders = reader["SINGLEORDERS"].ToString();
                multiOrders = reader["MULTIORDERS"].ToString();
                multiItems = reader["MULTIITEMS"].ToString();
                missingItems = reader["MISSINGITEMS"].ToString();
                internationalOrders = reader["INTERNATIONALORDERS"].ToString();

                obj.SingleOrders = (reader["SINGLEORDERS"].ToString() != string.Empty) ? int.Parse(reader["SINGLEORDERS"].ToString()) : 0;
                if (singleOrders != String.Empty) obj.SingleOrders = int.Parse(singleOrders);
                if (multiOrders != String.Empty) obj.MultiOrders = int.Parse(multiOrders);
                if (multiItems != String.Empty) obj.MultiItems = int.Parse(multiItems);
                if (missingItems != String.Empty) obj.MissingItems = int.Parse(missingItems);
                if (internationalOrders != String.Empty) obj.InternationalOrders = int.Parse(internationalOrders);

                pctComplete = reader["PERCENTCOMPLETE"].ToString();

                if (pctComplete != string.Empty)
                {
                    obj.PercentageComplete = float.Parse(reader["PERCENTCOMPLETE"].ToString());
                }
                else
                {
                    obj.PercentageComplete = 100;
                }

                list.Add(obj);
            }

            reader.Close();
            this.LoadStatistics = list;
            lst.Add(this);
            return lst;
        }
    }
        #endregion

}
