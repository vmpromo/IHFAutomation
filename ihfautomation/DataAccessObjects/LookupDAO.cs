//
// Name: LookupADO.cs
// Type: ADO class
// Description: contains the class variables, methods and pl/sql calls
//              associated with the Lookup entities
//
//$Revision:   1.3 $
//
// Version   Date        Author    Reason
//  1.0      11/07/11    MS        Initial Release
//  1.1      14/07/11    MSalman   New lookup function Added.
//  1.2      19/07/11    MSalman   New function Added.
//  1.3      01/08/11    MSalman   Namespaces updated.
//  1.4      19/08/11    MSalman   Update the list.


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using IHF.EnterpriseLibrary.Data;
using IHF.BusinessLayer.BusinessClasses.Packing;
using IHF.BusinessLayer.DataAccessObjects;
using IHF.BusinessLayer.Util;

namespace IHF.BusinessLayer.DataAccessObjects
{
    public class LookupDAO
    {

        #region Private Memeber

        private const int FIRST_COLUMN_INDEX = 0;

        protected DataManager _dal = new DataManager(Util.DBInstanceEnum.Ora);

        private const string GET_ORDER_PACK = "OMS_PACK.F_OPEN_ORDER_LIST";

        private const string GET_MISSING_ITEM = "OMS_PACK.F_FIND_MISSING_ITEM";

        private const string GET_SORT_AREAS = "OMS_MANUAL_SORT.F_SORT_AREA";


        #endregion


        #region Local Functions

        public List<KeyValuePair<string, string>> GetStore ()
        {

            return FillListCodeAndDescription("OMS_COMMON.F_STORE", new object[]{});
        }


        public List<KeyValuePair<string, string>> GetReasonCode()
        {
            return FillListCodeAndDescription("OMS_PACK.F_GET_REASON_CODE", new object[] { "PACK_REASON_CODES" });
        }

        public List<string> GetPackOrders(string orderNo)
        {

            return FillListDescriptionOnly(GET_ORDER_PACK, new object[] { orderNo });

        }

        public List<KeyValuePair<string, string>> GetMissingItem(string orderNo)
        {
            List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();

            IDataReader dr = this._dal.ExecuteReader(GET_MISSING_ITEM, new object[] { orderNo, Shared.CurrentUser, Shared.UserHostName });

            while (dr.Read())
                list.Add(new KeyValuePair<string, string>(dr["SKU"].ToString(), dr["FAILEDTOTELABEL"].ToString()));

            dr.Close();

            return list.Where(x => x.Key != "").ToList<KeyValuePair<string,string>>();

        }


        public List<KeyValuePair<string, string>> GetSortArea()
        {
            List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();

            IDataReader dr = this._dal.ExecuteReader(GET_SORT_AREAS, new object[] {});

            while (dr.Read())
                list.Add(new KeyValuePair<string, string>(dr["AREA_ID"].ToString(), dr["AREA_DESCR"].ToString()));

            dr.Close();

            return list.Where(x => x.Key != "").ToList<KeyValuePair<string, string>>();

        }

        #endregion


        #region Helper Functions

        private List<KeyValuePair<string, string>> FillListCodeAndDescription(string cmdName, object[] paramaters)
        {
            List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();

            IDataReader dr = this._dal.ExecuteReader(cmdName, paramaters);

            while (dr.Read())
                list.Add(new KeyValuePair<string, string>(dr[0].ToString(), dr[1].ToString()));

            dr.Close();

            return list;
        }


        private List<string> FillListDescriptionOnly(string cmdName, object[] paramaters)
        {
            List<string> list = new List<string>();

            IDataReader dr = this._dal.ExecuteReader(cmdName, paramaters);

            while (dr.Read())
                list.Add(dr[FIRST_COLUMN_INDEX].ToString());

            dr.Close();

            return list;
        }

        #endregion



    }
}
