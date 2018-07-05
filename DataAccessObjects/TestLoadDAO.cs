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
    public class TestLoadDAO : IDataService
    {
        #region "private constants"
        
        public const string _GetLines = "oms_testharness.f_lines";
        private const string _InsertLine = "oms_testharness.p_insertline";
        private const string _UpdateLine = "oms_testharness.p_updateline";
        private const string _Deleteline = "oms_testharness.p_deleteline";
        private const string _createload = "oms_testharness.f_create_test_load";

        #endregion

        #region "private variables"

        private DataManager dataManager = new DataManager(Util.DBInstanceEnum.Ora);


        #endregion


        public decimal LineId
        {
            get; set;
        }

        public decimal OrderCount
        {
            get;
            set;
        }

        public decimal ItemsPerOrder
        {
            get;
            set;
        }

        public string CarrierServiceGroup
        {
            get;
            set;
        }

        public String DeliverByDate
        {
            get;
            set;
        }

        public decimal ItemVolume
        {
            get;
            set;
        }

        public string CountryCode
        {
            get; set;
        }

        public List<TestLoadDAO> GetLines()
        {
            List<TestLoadDAO> linelist = new List<TestLoadDAO>();

            IDataReader dr = dataManager.ExecuteReader(_GetLines, new object[] { });

            while (dr.Read())
            {
                TestLoadDAO obj = new TestLoadDAO();
                obj.LineId = Convert.ToDecimal(dr["line_id"].ToString());
                obj.OrderCount = Convert.ToDecimal(dr["ordercount"].ToString());
                obj.ItemsPerOrder = Convert.ToDecimal(dr["itemsperorder"].ToString());
                obj.ItemVolume = Convert.ToDecimal(dr["item_volume"].ToString());
                obj.DeliverByDate = dr["deliverby_dt"].ToString();
                obj.CountryCode = dr["countrycode"].ToString();
                obj.CarrierServiceGroup = dr["carrier_service_group"].ToString();

                linelist.Add(obj);
            }
           

            return linelist;
        }

        public void UpdateLine(TestLoadDAO line)
        {
            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-GB");

            DateTime dt = Convert.ToDateTime(line.DeliverByDate, culture);

            dataManager.ExecuteNonQuery(_UpdateLine, new object[] { line.LineId, line.OrderCount, line.ItemsPerOrder, line.ItemVolume, dt, line.CarrierServiceGroup, line.CountryCode }); 
        }

        public void InsertLine(TestLoadDAO line)
        {
            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-GB");

            DateTime dt = Convert.ToDateTime(line.DeliverByDate, culture);
            dataManager.ExecuteNonQuery(_InsertLine, new object[] { line.LineId, line.OrderCount, line.ItemsPerOrder, line.ItemVolume, dt, line.CarrierServiceGroup, line.CountryCode });
        }


        public void DeleteLine (TestLoadDAO line)
        {
            dataManager.ExecuteNonQuery(_Deleteline, new object[] { line.LineId});
        }

        public string CreateTestLoad()
        {
            IDataReader dr = dataManager.ExecuteReader(_createload, new object[] { });

            dr.Read();

            return dr[0].ToString();
            

        }
    

    }
}
