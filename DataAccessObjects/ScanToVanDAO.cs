using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IHF.EnterpriseLibrary.Data;
using System.Data;
using IHF.BusinessLayer.Util;

namespace IHF.BusinessLayer.DataAccessObjects
{
    public class ScanToVanDAO
    {
        #region "private variables and constants"

        private DataManager _dataManager = new DataManager(Util.DBInstanceEnum.Ora);

        private const string CAGE_SCAN = "oms_van_despatch.p_scan_cage";
        private const string VAN_SCAN = "oms_van_despatch.p_scantovan";

        #endregion

        public decimal getCageIdForBarcode(string cageBarcode, string user, string terminal)
        {
            decimal cageID = 0;
            

            cageID = _dataManager.ExecuteReturnMethod(CAGE_SCAN,
                                              new Object[]{
                                                 cageID,
                                                  cageBarcode,
                                                  user,
                                                  terminal
                                              });
            return cageID;
        }



        public void scanToVan (decimal cageID, string vanrunBarcode, string user, string terminal)
        {
            _dataManager.ExecuteNonQuery(VAN_SCAN,
                                           new Object[]{
                                               cageID,
                                               vanrunBarcode,
                                               user,
                                               terminal
                                           });
        }
    }
}
