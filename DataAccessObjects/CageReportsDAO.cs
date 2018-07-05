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

namespace IHF.BusinessLayer.DataAccessObjects
{
    public class CageReportsDAO
    {
        #region "private constants"

        private const string GetCarrier       = "oms_cage_reports.f_get_carrier";
        private const string GetCagesdespatch = "oms_cage_reports.f_cages_to_despatch";
        private const string GetCagestatus    = "oms_cage_reports.f_get_cage_status";
        private const string GetCarrbar       = "oms_cage_reports.f_get_carrier_barcode";
        private const string GetCagebar       = "oms_cage_reports.f_get_cage_barcode";
        private const string GetCageids       = "oms_cage_reports.f_cageids_to_be_despatched";
        private const string Getparcels       = "oms_cage_reports.f_packed_to_be_caged";
        private const string Getcarrierselect = "oms_cage_reports.f_get_carrier_with_select";

        #endregion

        #region "private variables"

        private DataManager dataManager = new DataManager(Util.DBInstanceEnum.Ora);

        #endregion

        #region "Methods available to the presentation layer (web)"


        public DataSet GetCarriers()
        {

            return dataManager.ExecuteDataset(
                                                GetCarrier.ToString(),
                                                null);
        }


        public DataSet GetCarriersTobeDespatched(string I_carrier)
        {

            Object[] dtlParams = new Object[] { I_carrier };
            return dataManager.ExecuteDataset(GetCagesdespatch.ToString(), dtlParams);


        }

        public decimal GetCageStatus(Int32 I_cage_id)
        {

            Object[] dtlParams = new Object[] { I_cage_id };
            return dataManager.GetValuedecimal(GetCagestatus.ToString(), dtlParams);


        }

        

        public string GetCarrierBarcode(string I_carr_id)
        {

            Object[] dtlParams = new Object[] { I_carr_id };
            return dataManager.GetValue(GetCarrbar.ToString(), dtlParams);


        }


        public string GetCageBarcode(Int32 I_cage_id)
        {

            Object[] dtlParams = new Object[] { I_cage_id };
            return dataManager.GetValue(GetCagebar.ToString(), dtlParams);


        }


        public string Getcageidstring(string I_carr_id)
        {

            Object[] dtlParams = new Object[] { I_carr_id };
            return dataManager.GetValue(GetCageids.ToString(), dtlParams);


        }

        public DataSet GetParcelstobecaged(string I_carrier)
        {

            Object[] dtlParams = new Object[] { I_carrier };
            return dataManager.ExecuteDataset(Getparcels.ToString(), dtlParams);


        }

        public DataSet GetCarriersWithSelect()
        {

            return dataManager.ExecuteDataset(
                                                Getcarrierselect.ToString(),
                                                null);
        }

        #endregion
    }
}
