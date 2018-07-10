using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IHF.EnterpriseLibrary.Data;
using System.Data;
using IHF.BusinessLayer.Util;

namespace IHF.BusinessLayer.DataAccessObjects
{
    public class StoreManifestDAO
    {

        #region "private variables and constants"

        private DataManager _dataManager = new DataManager(Util.DBInstanceEnum.Ora);

        private const string STORESTOMANIFEST = "oms_van_despatch.f_stores_to_manifest";
        private const string CREATESTOREMANIFEST = "oms_van_despatch.p_create_store_manifest";
        private const string MANIFESTLIST = "oms_van_despatch.f_store_manifest_list";
        private const string VANRUNSTOMANIFEST = "oms_van_despatch.f_routes_to_manifest";


        #endregion

        public DataSet GetStoreList ()
        {
            return _dataManager.ExecuteDataset(STORESTOMANIFEST,
                                              new Object[]{});
        }

        public DataSet GetVanRunList()
        {
            return _dataManager.ExecuteDataset(VANRUNSTOMANIFEST,
                                              new Object[] { });
        }


        public decimal CreateStoreManifest(decimal ? storeid, string routeId, string user, string terminal)
        {
            decimal manifestID = 0;

            return _dataManager.ExecuteReturnMethodDecimal(CREATESTOREMANIFEST,
                                   new Object[]{  manifestID,
                                                  storeid,
                                                  routeId,
                                                  user,
                                                  terminal
                                              });
        }

        public DataSet GetManifestList()
        {
            return _dataManager.ExecuteDataset(MANIFESTLIST,
                                              new Object[] { });
        }


    }
}
