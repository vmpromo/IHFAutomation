using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using IHF.BusinessLayer.Util;

namespace IHF.BusinessLayer.BusinessClasses.Returns
{
    public class ReturnsConfig
    {
        public static IReturns LoadReturns(string database)
        {
            return new Returns(database);
        }

    }
}

#region "commented code"
/*if (database.ToLower().Contains(Enumerations.IHFServer.dev.ToString()))
                returns = new ReturnsDev(database);
            else if (database.ToLower().Contains(Enumerations.IHFServer.tst.ToString()))
                returns = new ReturnsTst(database);
            else if (database.ToLower().Contains(Enumerations.IHFServer.uat.ToString()))
                returns = new ReturnsUat(database);
            else if (database.ToLower().Contains(Enumerations.IHFServer.tr1.ToString()))
                returns = new ReturnsUat(database);
            else if (database.ToLower().Contains(Enumerations.IHFServer.tr2.ToString()))
                returns = new ReturnsUat(database);
            else if (database.ToLower().Contains(Enumerations.IHFServer.prd.ToString()))
                returns = new ReturnsUat(database);
            */
#endregion