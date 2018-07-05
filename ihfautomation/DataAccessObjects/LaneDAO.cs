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
    public class LaneDAO
    {

        #region "private constants"

        private const string GetAllLanes = "oms_cage_maintenance.f_lane_list";
        private const string AddCageTypeToLane = "oms_cage_maintenance.p_add_lane_cagetype";
        private const string RemoveCageTypeFromLane = "oms_cage_maintenance.p_remove_lane_cagetype";
        private const string GetCageAssignments = "oms_cage_maintenance.f_cagetype_lane_list";

        #endregion

        #region "private variables"

        private DataManager dataManager = new DataManager(Util.DBInstanceEnum.Ora);

        #endregion

        #region "Methods available to the presentation layer (web)"


        public DataSet GetLanes()
        {

            return dataManager.ExecuteDataset(
                                                GetAllLanes.ToString(),
                                                null);
        }

        public DataSet GetLaneAssignments()
        {

            return dataManager.ExecuteDataset(
                                                GetCageAssignments.ToString(),
                                                null);
        }

        public void AddCageTypeLane(int I_lane_id, string I_cage_type_id, string I_userlogin)
        {
            Object[] updParams = new Object[] { I_lane_id, I_cage_type_id, I_userlogin };

            dataManager.ExecuteNonQuery(AddCageTypeToLane.ToString(),
                                                   updParams);

        }

        public void RemoveCageTypeLane(int I_lane_id, string I_userlogin)
        {
            Object[] delParams = new Object[] { I_lane_id, I_userlogin };

            dataManager.ExecuteNonQuery(RemoveCageTypeFromLane.ToString(),
                                                   delParams);
        }

        #endregion

    }
}
