// Name: PutawayDAO.cs
// Type: class file 
// Description: DAO class for putaway
//
//$Revision:   1.3  $
//
// Version   Date        Author     Reason
//  1.0      08/05/18    A Petrescu Initial Revision
//  1.1      21/05/18    M Cacket   Added code to update the putaway_dtl table.
//                       S Remedios
//  1.2      07/06/18    M Cacket   Include SKU in the LPN information query
//                       S Remedios
//                       S Patel


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IHF.EnterpriseLibrary.Data;
using IHF.BusinessLayer.BusinessClasses.Putaway;
using System.Web;

namespace IHF.BusinessLayer.DataAccessObjects
{
    public class PutawayDAO
    {
        private readonly DataManager dataManager;

        private const string LoadLpnInformationQuery = @"
SELECT p.itemnumber, p.actual_loc, oih.lastactioncode, oih.sku, oih.ordernumber
FROM putaway_dtl p
JOIN orderitemheader oih 
ON p.itemnumber = oih.itemnumber
WHERE lpn = :lpn";

        private const string UpdateActualLocation = @"
UPDATE putaway_dtl
SET actual_loc = :actual_loc,
putaway_userlogin = :putaway_userlogin,
putaway_dtm = SYSDATE
WHERE lpn = :lpn";


        public PutawayDAO()
        {
            dataManager = new DataManager(Util.DBInstanceEnum.Ora);
        }

        public LpnInformation LoadLPNInformation(string lpn)
        {
            using (var reader = dataManager.pipeReader(LoadLpnInformationQuery, lpn))
            {
                if (!reader.Read())
                {
                    return null;
                }

                var result = new LpnInformation
                {
                    OrderNumber = reader["ORDERNUMBER"].ToString(),
                    ItemNumber = reader["ITEMNUMBER"].ToString(),
                    ActualLocation = reader["ACTUAL_LOC"] == DBNull.Value ? null : reader["ACTUAL_LOC"].ToString(),
                    ActionCode = reader["LASTACTIONCODE"] == DBNull.Value ? null : (int?) int.Parse(reader["LASTACTIONCODE"].ToString()),
                    Sku = reader["SKU"] == DBNull.Value ? null : reader["SKU"].ToString()
                };

                if (reader.Read())
                {
                    throw new InvalidOperationException("More than 1 row returned by LoadLpnInformationQuery");
                }

                return result;
            }
        }

        public void UpdActualLocation(string lpn, string location)
        {

            Object[] updParams = new Object[] { location, HttpContext.Current.User.Identity.Name, lpn};


            int recordsUpdated = dataManager.ExecuteDML(UpdateActualLocation, updParams);

            if (recordsUpdated != 1)
            {
                throw new InvalidOperationException("Update failed");
            }


        }


    }
}
