using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IHF.BusinessLayer.BusinessClasses.Dashboard;
using IHF.EnterpriseLibrary.DataServices;
using System.Data;


namespace IHF.BusinessLayer.BusinessClasses.Dashboard
{
    public class LoadManagementOverview : IDataService
    {
        #region private member


        private const string LOAD_MANAGEMENT_OVERVIEW = "OMS_DASHBOARD_REPORT.F_LOAD_MANAGEMENT";

        private List<LoadManagementOverview> lstOpView = new List<LoadManagementOverview>();

        #endregion

       #region Function Mapping


        public enum ClassMethods
        {

            GetLoadManagementOverview
        }


        #endregion

        #region Properties

        public List<LoadManagementOverview> OverviewInfo
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

        public DateTime TimeReleased { get; set; }

        public Int32 LoadStatusCode { get; set; }

        public string LoadStatus { get; set; }

        public Int32 TotalMultis { get; set; }

        public Int32 TotalSingles { get; set; }

        public Int32 TotalItems { get; set; }

        public Int32 ReadyReleaseMultis { get; set; }

        public Int32 TotReadyReleaseMultis { get; set; }

        public Int32 ReadyReleaseSingles { get; set; }

        public Int32 TotReadyReleaseSingles { get; set; }

        public Int32 ReleasedMultis { get; set; }

        public Int32 TotReleasedMultis { get; set; }

        public Int32 ReleasedSingles { get; set; }

        public Int32 TotReleasedSingles { get; set; }

        public Int32 SortedMultis { get; set; }

        public Int32 TotSortedMultis { get; set; }

        public Int32 SortedSingles { get; set; }

        public Int32 TotSortedSingles { get; set; }

        public Int32 LocatedMultis { get; set; }

        public Int32 TotLocatedMultis { get; set; }

        public Int32 LocatedSingles { get; set; }

        public Int32 TotLocatedSingles { get; set; }

        public Int32 ReadpackMultis { get; set; }

        public Int32 TotReadyPackMultis { get; set; }

        public Int32 ReadyPackSingles { get; set; }

        public Int32 TotReadyPackSingles { get; set; }

        public Int32 PackingMultis { get; set; }

        public Int32 TotPackingMultis { get; set; }

        public Int32 PackingSingles { get; set; }

        public Int32 TotPackingSingles { get; set; }

        public Int32 PackedSingles { get; set; }

        public Int32 TotPackedSingles { get; set; }

        public Int32 PackedMultis { get; set; }

        public Int32 TotPackedMultis { get; set; }

        public Int32 CagedMultis { get; set; }

        public Int32 TotCagedMultis { get; set; }

        public Int32 CagedSingles { get; set; }

        public Int32 TotCagedSingles { get; set; }

        public Int32 ReadyDespatchMultis { get; set; }

        public Int32 TotReadyDespatchMultis { get; set; }

        public Int32 ReadyDespatchSingles { get; set; }

        public Int32 TotReadyDespatchSingles { get; set; }

        public Int32 DespatchedMultis { get; set; }

        public Int32 TotDespatchedMultis { get; set; }

        public Int32 DespatchedSingles { get; set; }

        public Int32 TotDespatchedSingles { get; set; }

         #endregion

        #region Local Functions


        [MethodMapper("GetLoadManagementOverview", LoadManagementOverview.LOAD_MANAGEMENT_OVERVIEW)]
        public IList<IDataService> GetLoadManagementlOverview(IDataReader reader)
        {
            string loadnumber = string.Empty;
            string timerelease = string.Empty;
            string loadstatus = string.Empty;
            string totalmultis = string.Empty;

            IList<IDataService> lst = new List<IDataService>();

            List<LoadManagementOverview> items = new List<LoadManagementOverview>();

            while (reader.Read())
            {

                LoadManagementOverview obj = new LoadManagementOverview ();

                obj.LoadNumber = reader["PICK_LOAD_NUM"].ToString();

                if (reader["RELEASE_DTM"].ToString() != string.Empty)
                    obj.TimeReleased = Convert.ToDateTime(reader["RELEASE_DTM"].ToString());

                obj.LoadStatus = reader["LOAD_STATUS"].ToString();
                obj.LoadStatusCode = (reader["LOAD_STATUS_CODE"].ToString() != string.Empty) ? int.Parse(reader["LOAD_STATUS_CODE"].ToString()) : 0;

                obj.TotalItems = (reader["TOTALITEMS"].ToString() != string.Empty) ? int.Parse(reader["TOTALITEMS"].ToString()) : 0;
                obj.TotalMultis = (reader["TOTALMULTIS"].ToString() != string.Empty) ? int.Parse(reader["TOTALMULTIS"].ToString()) : 0;
                obj.TotalSingles = (reader["TOTALSINGLES"].ToString() != string.Empty) ? int.Parse(reader["TOTALSINGLES"].ToString()) : 0;

                obj.ReadyReleaseMultis = (reader["RRMULTI"].ToString() != string.Empty) ? int.Parse(reader["RRMULTI"].ToString()) : 0;
                obj.TotReadyReleaseMultis =(reader["TOTRRMULTI"].ToString() != string.Empty) ? int.Parse(reader["TOTRRMULTI"].ToString()) : 0;
                obj.ReadyReleaseSingles = (reader["RRSINGLES"].ToString() != string.Empty) ? int.Parse(reader["RRSINGLES"].ToString()) : 0;
                obj.TotReadyReleaseSingles = (reader["TOTRRSINGLES"].ToString() != string.Empty) ? int.Parse(reader["TOTRRSINGLES"].ToString()) : 0;

                obj.ReleasedMultis = (reader["RELMULTI"].ToString() != string.Empty) ? int.Parse(reader["RELMULTI"].ToString()) : 0;
                obj.TotReleasedMultis = (reader["TOTRELMULTI"].ToString() != string.Empty) ? int.Parse(reader["TOTRELMULTI"].ToString()) : 0;
                obj.ReleasedSingles = (reader["RELSINGLES"].ToString() != string.Empty) ? int.Parse(reader["RELSINGLES"].ToString()) : 0;
                obj.TotReleasedSingles = (reader["TOTRELSINGLES"].ToString() != string.Empty) ? int.Parse(reader["TOTRELSINGLES"].ToString()) : 0;
               
                obj.SortedMultis = (reader["SORTEDMULTIS"].ToString() != string.Empty) ? int.Parse(reader["SORTEDMULTIS"].ToString()) : 0;
                obj.TotSortedMultis = (reader["TOTSORTEDMULTIS"].ToString() != string.Empty) ? int.Parse(reader["TOTSORTEDMULTIS"].ToString()) : 0;
                obj.SortedSingles = (reader["SORTEDSINGLES"].ToString() != string.Empty) ? int.Parse(reader["SORTEDSINGLES"].ToString()) : 0;
                obj.TotSortedSingles = (reader["TOTSORTEDSINGLES"].ToString() != string.Empty) ? int.Parse(reader["TOTSORTEDSINGLES"].ToString()) : 0;

                obj.LocatedMultis = (reader["LOCATEDMULTIS"].ToString() != string.Empty) ? int.Parse(reader["LOCATEDMULTIS"].ToString()) : 0;
                obj.TotLocatedMultis = (reader["TOTLOCATEDMULTIS"].ToString() != string.Empty) ? int.Parse(reader["TOTLOCATEDMULTIS"].ToString()) : 0;
                obj.LocatedSingles = (reader["LOCATEDSINGLES"].ToString() != string.Empty) ? int.Parse(reader["LOCATEDSINGLES"].ToString()) : 0;
                obj.TotLocatedSingles = (reader["TOTLOCATEDSINGLES"].ToString() != string.Empty) ? int.Parse(reader["TOTLOCATEDSINGLES"].ToString()) : 0;

                obj.PackingMultis = (reader["PACKINGMULTIS"].ToString() != string.Empty) ? int.Parse(reader["PACKINGMULTIS"].ToString()) : 0;
                obj.TotPackingMultis = (reader["TOTPACKINGMULTIS"].ToString() != string.Empty) ? int.Parse(reader["TOTPACKINGMULTIS"].ToString()) : 0;
                obj.PackingSingles = (reader["PACKINGSINGLES"].ToString() != string.Empty) ? int.Parse(reader["PACKINGSINGLES"].ToString()) : 0;
                obj.TotPackingSingles = (reader["TOTPACKINGSINGLES"].ToString() != string.Empty) ? int.Parse(reader["TOTPACKINGSINGLES"].ToString()) : 0;

                obj.PackedMultis = (reader["PACKEDMULTIS"].ToString() != string.Empty) ? int.Parse(reader["PACKEDMULTIS"].ToString()) : 0;
                obj.TotPackedMultis = (reader["TOTPACKEDMULTIS"].ToString() != string.Empty) ? int.Parse(reader["TOTPACKEDMULTIS"].ToString()) : 0;
                obj.PackedSingles = (reader["PACKEDSINGLES"].ToString() != string.Empty) ? int.Parse(reader["PACKEDSINGLES"].ToString()) : 0;
                obj.TotPackedSingles = (reader["TOTPACKEDSINGLES"].ToString() != string.Empty) ? int.Parse(reader["TOTPACKEDSINGLES"].ToString()) : 0;

                obj.CagedMultis = (reader["CAGEDMULTIS"].ToString() != string.Empty) ? int.Parse(reader["CAGEDMULTIS"].ToString()) : 0;
                obj.TotCagedMultis = (reader["TOTCAGEDMULTIS"].ToString() != string.Empty) ? int.Parse(reader["TOTCAGEDMULTIS"].ToString()) : 0;
                obj.CagedSingles = (reader["CAGEDSINGLES"].ToString() != string.Empty) ? int.Parse(reader["CAGEDSINGLES"].ToString()) : 0;
                obj.TotCagedSingles = (reader["TOTCAGEDSINGLES"].ToString() != string.Empty) ? int.Parse(reader["TOTCAGEDSINGLES"].ToString()) : 0;

                obj.ReadyDespatchMultis = (reader["RDYDSPMULTIS"].ToString() != string.Empty) ? int.Parse(reader["RDYDSPMULTIS"].ToString()) : 0;
                obj.TotReadyDespatchMultis = (reader["TOTRDYDSPMULTIS"].ToString() != string.Empty) ? int.Parse(reader["TOTRDYDSPMULTIS"].ToString()) : 0;
                obj.ReadyDespatchSingles = (reader["RDYDSPSINGLES"].ToString() != string.Empty) ? int.Parse(reader["RDYDSPSINGLES"].ToString()) : 0;
                obj.TotReadyDespatchSingles = (reader["TOTRDYDSPSINGLES"].ToString() != string.Empty) ? int.Parse(reader["TOTRDYDSPSINGLES"].ToString()) : 0;

                obj.DespatchedMultis = (reader["DSPMULTIS"].ToString() != string.Empty) ? int.Parse(reader["DSPMULTIS"].ToString()) : 0;
                obj.TotDespatchedMultis = (reader["TOTDSPMULTIS"].ToString() != string.Empty) ? int.Parse(reader["TOTDSPMULTIS"].ToString()) : 0;
                obj.DespatchedSingles = (reader["DSPSINGLES"].ToString() != string.Empty) ? int.Parse(reader["DSPSINGLES"].ToString()) : 0;
                obj.TotDespatchedSingles = (reader["TOTDSPSINGLES"].ToString() != string.Empty) ? int.Parse(reader["TOTDSPSINGLES"].ToString()) : 0;

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
