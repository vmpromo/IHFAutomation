// Name: CageDAO.cs
// Type: DAO class
// Description: contains the class variables, methods and pl/sql calls
//              associated with the Cage entities
//
//$Revision:   1.9  $
//
// Version   Date        Author     Reason
//  1.0      21/06/11    J Watt     Initial Revision
//  1.1      21/06/11    J Watt     Cage creation
//  1.2      22/06/11    J Watt     checking
//  1.3      22/06/11    J Watt     No change.
//  1.4      23/06/11    J Watt     Caging
//  1.5      23/08/11    IT MK      Despatch - Interim version
//  1.6      05/12/11    IT MK      No change.
//  1.7      07/01/14    S Green    Changes for multiple cage creation and printing
//  1.8      03/01/17    M Cackett  Changes for Click and Collect project.
//                                  Added banner.
//  1.9      10/01/17    M Cackett  QA changes

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
    public class CageDAO
    {
        #region "private constants"

        private const string GetAllCages = "oms_cage_maintenance.f_cage_list";
        private const string CreateCageSet = "oms_cage_maintenance.p_create_cages";
        private const string UpdateCageStat = "oms_cage_maintenance.p_update_cage_status";
        private const string GetStoresForCageType = "oms_cage_maintenance.f_stores_for_cagetype";
        // Next one is a bit different as it is a pipelined function.  The parameters and the closing brackets
        // are added in later on.  Had to do it like this to call pipelined function from a SELECT.
        private const string GetToteLabel = "select * from table(oms_click_collect.f_get_tote_label";

        #endregion

        #region "private variables"

        private DataManager dataManager = new DataManager(Util.DBInstanceEnum.Ora);

        #endregion

        #region "Methods available to the presentation layer (web)"

        public IDataReader GetToteLabels(int I_num_cages, string I_cage_type_id, string I_userlogin)
        {
            string GetLabelsSQL = GetToteLabel + "(" + I_num_cages.ToString() + ", '" + I_cage_type_id + "', '" + I_userlogin + "'))";
            return dataManager.pipeReader(GetLabelsSQL);
        }

        public DataSet GetCages()
        {

            return dataManager.ExecuteDataset(
                                                GetAllCages.ToString(),
                                                null);
        }

        public void UpdateCageStatus(int I_cage_id, int I_cage_status_code, string I_userlogin)
        {
            Object[] updateParams = new Object[] { I_cage_id, I_cage_status_code, I_userlogin };

            dataManager.ExecuteNonQuery(UpdateCageStat.ToString(), updateParams);
        }
 

        public DataSet CreateCages(int I_num_cages, string I_cage_type_id, string I_userlogin)
        {
            Object[] createParams = new Object[] { I_num_cages, I_cage_type_id, I_userlogin };

            return dataManager.SelectDataSetProcedure(CreateCageSet.ToString(), createParams);

        }

        public DataSet GetStoresForCagetype(string I_cageType)
        {
            Object[] selectParams = new Object[] { I_cageType };

            return dataManager.ExecuteDataset(GetStoresForCageType.ToString(), selectParams);
        }

        #endregion

    }
}
