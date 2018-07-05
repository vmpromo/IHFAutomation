//
// Name: BookingCodeParamsDAO.cs
// Type: DAO class
// Description: contains the class variables, methods and pl/sql calls
//              associated with the TestLiveLoads entities
//
//$Revision:   1.0  $
//
// Version   Date        Author     Reason
//  1.0      01/09/15    M Cackett  Initial Release

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

using System.Globalization;


namespace IHF.BusinessLayer.DataAccessObjects
{
    public class TestLiveLoadsDAO : IDataService
    {
        #region "private constants"
        
        public const string _GetLoads = "oms_import_live_loads.f_available_loads";
        private const string _ImportLoad = "oms_import_live_loads.f_import_live_loads";

        #endregion

        #region "private variables"

        private DataManager dataManager = new DataManager(Util.DBInstanceEnum.Ora);


        #endregion


        public string PickLoadNum
        {
            get;
            set;
        }


        public List<TestLiveLoadsDAO> GetLines()
        {
            List<TestLiveLoadsDAO> linelist = new List<TestLiveLoadsDAO>();

            IDataReader dr = dataManager.ExecuteReader(_GetLoads, new object[] { });

            while (dr.Read())
            {
                TestLiveLoadsDAO obj = new TestLiveLoadsDAO();
                obj.PickLoadNum = dr["pick_load_num"].ToString();

                linelist.Add(obj);
            }
           

            return linelist;
        }

        public string ImportLiveLoad(string PickLoadNum)
        {
            IDataReader dr = dataManager.ExecuteReader(_ImportLoad, new object[] { PickLoadNum });

            dr.Read();

            return dr[0].ToString();

        }

    }
}

