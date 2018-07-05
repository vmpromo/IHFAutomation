//
// StackDetails.cs
// Type: entity class
// Description: contains properties and functions for Stacks
//
//$Revision:   1.1  $
//
// Version   Date        Author    Reason
//  1.0      18/04/12    M Khan    Initial Release
//  1.1      18/04/12    M Khan    Assigned to packstationId in ListstackInfo function

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IHF.EnterpriseLibrary.DataServices;
using System.Data;

namespace IHF.BusinessLayer.BusinessClasses.Stack
{
    
    public class StackDetails: IDataService
    {
        #region Private constants and variables

        private const string AVAILABLE_STACKS = "OMS_PACK_UTIL.F_AVAILABLE_STACKS";
        private const string ASSIGNED_STACKS = "OMS_PACK_UTIL.F_ASSIGNED_STACKS";

        #endregion

        #region "Properties"

        public string PackstationId { get; set; }
        
        public int ChuteId { get; set; }

        public string StackLabel { get; set; }

        public string TrolleyId { get; set; }

        public string PreConfigured { get; set; }

        public IList<StackDetails> StackInfo { get; set; }
        
        #endregion

        #region Enum for class methods
        
        public enum ClassMethods
        {
            ListAvailableStack,
            ListAssignedStack,
            ListStackInfo
        }

        #endregion


        #region Data Layer methods

        [MethodMapper("ListAvailableStack", AVAILABLE_STACKS)]
        public List<IDataService> ListAvailableStack(IDataReader dataReader)
        {
            List<IDataService> list = new List<IDataService>();

            while (dataReader.Read())
                list.Add(
                    new StackDetails
                    {
                        PackstationId = dataReader[0].ToString(),
                        ChuteId       = dataReader[1].ToString() == "" ? 0 : int.Parse(dataReader[1].ToString()),
                        StackLabel    = dataReader[2].ToString(),
                        TrolleyId     = dataReader[3].ToString() ?? "NA" 
                    }
                );

            return list;
        }

        [MethodMapper("ListAssignedStack", ASSIGNED_STACKS)]
        public List<IDataService> ListAssignedStack(IDataReader dataReader)
        {
            List<IDataService> list = new List<IDataService>();

            while (dataReader.Read())
                list.Add(
                    new StackDetails
                    {
                        PackstationId = dataReader[0].ToString(),
                        ChuteId       = dataReader[1].ToString() == "" ? 0 : int.Parse(dataReader[1].ToString()),
                        StackLabel    = dataReader[2].ToString(),
                        TrolleyId     = dataReader[3].ToString() ?? "NA"
                    }
                );

            return list;
        }

        [MethodMapper("ListStackInfo", ASSIGNED_STACKS)]
        public List<IDataService> ListStackInfo(IDataReader dataReader)
        {
            List<IDataService> list = new List<IDataService>();

            List<StackDetails> stackInfo = new List<StackDetails>();

            while (dataReader.Read())
                stackInfo.Add(
                    new StackDetails
                    {
                        ChuteId = dataReader[0].ToString() == "" ? 0 : int.Parse(dataReader[0].ToString()),
                        StackLabel = dataReader[1].ToString(),
                        PreConfigured = dataReader[2].ToString(),
                        PackstationId = dataReader[3].ToString()
                    }
                );

            this.StackInfo =  stackInfo;

            list.Add(this);

            return list;
        }

        #endregion

    }
}
