//
// Name: LookUpService
// Type: Service  
// Description: contains functions for auto complete lookup.
//
//$Revision:   1.0  $
//
// Version   Date        Author    Reason
//  1.0      14/07/11    MSalman   Initial Released

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using IHF.BusinessLayer.DataAccessObjects;

namespace IHF.ApplicationLayer.Web.App_Code
{
    /// <summary>
    /// Summary description for LookUpService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class LookUpService : System.Web.Services.WebService
    {

        #region Private Members 

        LookupDAO _lookup = new LookupDAO();
        
        #endregion



        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod]
        public string[] GetPackOrders(string prefixText, int count) {

           // string param = contextKey;

            List<string> items = new List<string>();

            items = _lookup.GetPackOrders(prefixText);

            return items.ToArray();
        }


       
    }
}
