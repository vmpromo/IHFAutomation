// Name: Pack.cs
// Type: Code Behind Class for Pack.
// Description: Include local function and page 
//              events
//
//$Revision:   1.21$
//
// Version   Date        Author    Reason
//  1.0      11/07/11    MSalman   Initial Released
//  1.1      14/07/11    MSalman   Some code cleanup.  
//  1.2      14/07/11    MSalman   Updated style property.     
//  1.3      18/07/11    MSalman   Message width increased.     
//  1.4      18/07/11    MSalman   width style is adjusted.     
//  1.5      19/07/11    MSalman   Style values updated.      
//  1.6      20/07/11    MSalman   Reprint function added.
//  1.7      08/08/11    MSalman   field label updated.
//  1.8      10/08/11    MSalman   field ID is updated.  
//  1.9      14/08/11    MSalman   Removed the query string.   
//  1.10     18/08/11    MSalman   Message updated.
//  1.11     18/08/11    MSalman   Message Updaed.  
//  1.12     30/08/11    MSalman   Message Updaed.
//  1.13     01/09/11    MSalman   Field for overflow count removed.   
//  1.14     05/09/11    MSalman   Message Updaed. 
//  1.15     05/09/11    MSalman   New Button is addd.                                                                           
//  1.16     09/09/11    MSalman   Cosmetic changes updated.                                                                           
//  1.17     01/12/11    MSalman   Change Request New functions added for trolley view.     
//  1.21     20/03/12    M Khan    FLOW related changes


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using IHF.Security.UserManagement;

namespace PackingMock
{
    public partial class _Pack : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (HttpContext.Current.Session["OpenOrderVal"] != null)
            {
                string val = HttpContext.Current.Session["OpenOrderVal"].ToString();

                if (!string.IsNullOrEmpty(val))
                    this.hdnOpenOrderValues.Value = val;

                HttpContext.Current.Session["OpenOrderVal"] = string.Empty;

            }

            if (HttpContext.Current.Session["UserOption"] != null)
            {
                string val = HttpContext.Current.Session["UserOption"].ToString();

                if (!string.IsNullOrEmpty(val))
                    this.hdnUserOption.Value = val;

                HttpContext.Current.Session["UserOption"] = string.Empty;

            }

            // Assume not master packer
            this.hdnMasterPacker.Value="N";

            // Get the list of user groups which have access to the advanced packing funcionality
            string [] advancedPackingGroups = ConfigurationManager.AppSettings["AdvancedPackingGroups"].Split(',');

            IHFRoleProvider roleprovider = new IHFRoleProvider();

            // Now determine if one of these groups is 
            foreach (string grp in advancedPackingGroups)
            {
               if (roleprovider.IsUserInRole(User.Identity.Name, grp))
               {
                   hdnMasterPacker.Value = "Y";
               }
            }

        }
    }
}
