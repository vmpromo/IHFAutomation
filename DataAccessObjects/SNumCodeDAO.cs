using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IHF.EnterpriseLibrary.Data;
using System.Data;

namespace IHF.BusinessLayer.DataAccessObjects
{
    public class SNumCodeDAO
    {
        #region "private variables and constants"
        private DataManager _dataManager = new DataManager(Util.DBInstanceEnum.Ora);

        private const string CODES = "oms_common.f_get_codes_by_type";
        #endregion

        #region "public functions"
        public DataSet GetCodesByType(string codeType)
        {
            DataSet codes =
                        new DataSet();

            codes =
                _dataManager.ExecuteDataset(
                    CODES.ToString(),
                    new object[]
                    {
                        codeType
                    });

            codes.Tables[0].TableName = codeType;

            return codes;
        }
        #endregion
    }
}
