using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IHF.EnterpriseLibrary.Data;
using IHF.BusinessLayer.BusinessClasses;
using System.Data;

namespace IHF.BusinessLayer.DataAccessObjects
{
    public abstract class WorkstationDAO
    {
        #region "protected varaibles and functions"
        protected DataManager _dataManager = new DataManager(Util.DBInstanceEnum.Ora);
        protected Workstation _workstation = new Packstation();

        protected const string ADD = "oms_workstation_util.p_add_workstation";
        protected const string WORKSTATIONS = "oms_workstation_util.f_get_all_workstations";
        protected const string DELETE = "oms_workstation_util.p_delete_workstation";
        protected const string MODIFY = "oms_workstation_util.p_modify_workstation";
        #endregion

        #region "abstract functions"
        
        public abstract decimal Add(Workstation workstation);

        public abstract int Modify(Workstation workstation);

        #endregion

        #region "public functions"

        public DataSet GetWorkstations()
        {
            DataSet workstations =
                        new DataSet();

            workstations = 
                _dataManager.ExecuteDataset(
                    WORKSTATIONS.ToString(),
                    null);

            workstations.Tables[0].TableName = 
                "mds_workstation";

            return workstations;
                
        }
 
        public Workstation GetWorkstationByID(int workstationID)
        {
            return (Workstation)_dataManager.Get(
                                    Workstation.ClassMethods.GetWorkstations.ToString(),
                                    this._workstation,
                                    new object[]
                                    {
                                        workstationID
                                    })[0];
                                    
        }

        public int Delete(Workstation workstation)
        {
            int returnResult =
                (int)_dataManager.ExecuteReturnMethod(
                    DELETE,
                    new object[]{
                        workstation.ID,
                        workstation.LastChangedBy
                    });

            return returnResult;
        }

        #endregion


    }
}
