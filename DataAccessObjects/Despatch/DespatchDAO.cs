using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IHF.EnterpriseLibrary.Data;
using System.Data;

namespace IHF.BusinessLayer.DataAccessObjects.Despatch
{
    public class DespatchDAO
    {
        private DataManager _dataManager = new DataManager(Util.DBInstanceEnum.Ora);

        private const string ValidCarrier     = "oms_despatch.f_valid_carrier_barcode";
        private const string ValidateBarcode  = "oms_despatch.p_process_barcode";
        private const string ValidateCages    = "oms_despatch.f_validate_cages_for_despatch";
        private const string QueueCages       = "oms_despatch.p_queue_for_despatch";
        private const string CageRemoval      = "oms_despatch.p_remove_cage";

        public string ValidateCarrier(string barcode, string userlogin)
        {
            return _dataManager.GetValue(ValidCarrier, 
                                         new object[] { barcode, 
                                                        userlogin });
        }

        public string ProcessBarcode(string carrierBarcode, string standardBarcode, string userlogin)
        {
            return _dataManager.GetStringforProcedure(ValidateBarcode,
                                                      new object[] { carrierBarcode, 
                                                                     standardBarcode, 
                                                                     userlogin });
        }

        public decimal ValidateCagesForDespatch(string carrierBarcode)
        {
            return _dataManager.GetValuedecimal(ValidateCages,
                                                new object[] { carrierBarcode });
        }


        public string QueueForDespatch(string carrierBarcode, string userLogin){
            return _dataManager.GetStringforProcedure(QueueCages,
                                                      new object[] { carrierBarcode, 
                                                                     userLogin });
        }

        public string RemoveCage(string carrierBarcode, string cageBarcode, string user) {
            return _dataManager.GetStringforProcedure(CageRemoval,
                                               new object[] { carrierBarcode,
                                                              cageBarcode,
                                                              user});
        }
    }
}
