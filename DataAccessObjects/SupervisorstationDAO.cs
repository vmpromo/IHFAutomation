using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IHF.EnterpriseLibrary.Data;
using IHF.BusinessLayer.BusinessClasses;
using System.Data;

namespace IHF.BusinessLayer.DataAccessObjects
{
    public class SupervisorDAO:WorkstationDAO
    {
        #region "overriden functions"
        public override decimal Add(Workstation workstation)
        {
            int returnResult =
                (int)this._dataManager.ExecuteReturnMethod(
                    ADD,
                    new object[] { 
                    workstation.ID,
                    workstation.Type,
                    workstation.Status,
                    workstation.WorkstationLabel,
                    null,
                    workstation.IsInternational,
                    workstation.CreatedBy});

            return returnResult;
        }

        public override int Modify(Workstation workstation)
        {
            int returnResult =
                (int)_dataManager.ExecuteReturnMethod(
                    MODIFY,
                    new object[]{
                        workstation.ID,
                        workstation.Type,
                        workstation.Status,
                        workstation.WorkstationLabel,
                        null,
                        workstation.IsInternational,
                        workstation.LastChangedBy
                    });

            return returnResult;
        }
        #endregion
    }
}
