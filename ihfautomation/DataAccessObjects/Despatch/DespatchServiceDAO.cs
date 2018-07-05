using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IHF.EnterpriseLibrary.Data;
using System.Diagnostics;
using System.Data;
using IHF.BusinessLayer.Util;

namespace IHF.BusinessLayer.DataAccessObjects.Despatch
{
    public class DespatchServiceDAO
    {
        private DataManager _dataManager = new DataManager(Util.DBInstanceEnum.Ora);

        private const string Carriers          = "oms_old_despatch.f_carriers_for_despatch";
        private const string Consignments      = "oms_old_despatch.f_consignments";
        private const string DespatchCages     = "oms_old_despatch.p_despatch_cages";
        private const string ServiceStatus     = "oms_old_despatch.p_update_service_runtime";
        private const string ErrorLog          = "oms_common.p_add_to_error_log";
        private const string Manifest          = "oms_old_despatch.p_add_manifest";
        private const string SystemParam       = "oms_old_despatch.f_param_name";
        private const string UpdateConsignment = "oms_old_despatch.p_update_consignment_status";
        
        //public IDataReader Despatch(string carrierId, string user){
        //    return _dataManager.GetListFromProcedure(DespatchCages,
        //                                             new object[]{carrierId,user});
        //}

        public void Despatch(string carrierId, string user)
        {
            _dataManager.ExecuteNonQuery(DespatchCages, new object[] { carrierId, user });
        }

        public void UpdateServiceStatus(string serviceName)
        {
            _dataManager.ExecuteNonQuery(ServiceStatus, new object[] { serviceName });
        }


        public IDataReader GetConsignments(string carrierId)
        {
            return _dataManager.ExecuteReader(Consignments, new object[]{carrierId});
        }

        public IDataReader GetCarriersForDespatch(){
            return _dataManager.ExecuteReader(Carriers,null);
        }

        public void WriteToErrorLog(int severitycode, string unitName, string shortMessage, string longMessage) {
            _dataManager.ExecuteNonQuery(ErrorLog, 
                                         new object[] {severitycode,unitName,shortMessage,longMessage });
        }

        public void AddManifestCodes(string carrierId, string manifestCodes,string user) {
            _dataManager.ExecuteNonQuery(Manifest, 
                                         new object[] { carrierId, manifestCodes,user });
        }

        public decimal GetMaxDespatchOffSet() {
            return _dataManager.GetValuedecimal(SystemParam, new object[] { Enumerations.SystemParameter.MAX_DESPATCH_OFFSET_DAYS.ToString() });
        }

        public decimal GetDespatchServiceInterval()
        {
            return _dataManager.GetValuedecimal(SystemParam, new object[] { Enumerations.SystemParameter.DESPATCH_SERVICE_INTERVAL.ToString() });
        }

        public void UpdateConsignmentstatus(string consignmentCode, int status, string user)
        {
            _dataManager.ExecuteNonQuery(UpdateConsignment,
                                         new object[] { consignmentCode, status, user });
        }

    }
}
