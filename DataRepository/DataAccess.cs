using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using System.Data.Common;
using Oracle.DataAccess.Client;

namespace IHF.EnterpriseLibrary.DataRepository
{
    public class DataAccess:Database
    {

        private Database _database;
        public Database Database
        {
            get { return _database; }
            set { _database = value; }
        }
        public DataAccess(string connectionString):base(connectionString,OracleClientFactory.Instance)
        {
            _database = (Database)EnterpriseLibraryContainer.Current.GetInstance<Database>(connectionString.ToString());
        }



        protected override void DeriveParameters(DbCommand discoveryCommand)
        {
            OracleCommandBuilder.DeriveParameters((OracleCommand)discoveryCommand);
        }
    }
}
