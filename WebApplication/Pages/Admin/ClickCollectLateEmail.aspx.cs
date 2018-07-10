using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Resources;
using Telerik.Web.UI;
using IHF.BusinessLayer.DataAccessObjects;


namespace IHF.ApplicationLayer.Web.Pages.Admin
{
    public partial class ClickCollectLateEmail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            populateStoreDDL();
        }

        

        protected void RadButton1_Click(object sender, EventArgs e)
        {
            CustEmailUtilDAO dao = new CustEmailUtilDAO();

            Int16 storeId = Int16.Parse(rcbStore.SelectedValue);
            Int16 numDays = (Int16)rnbNumDays.Value;

            try
            {
                dao.LateVanRunNotify(storeId, numDays);
                lblError.Visible = false;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                bool iserror = isErrorMessage(ref msg);

                lblError.Text = msg;
                lblError.Visible = true;
            }
        }

        private void populateStoreDDL()
        {
            LookupDAO lkp = new LookupDAO();

            List<KeyValuePair<string, string>> stores = lkp.GetStore();

            List<KeyValuePair<Int16, string>> storesconcat = new List<KeyValuePair<Int16, string>>();

            foreach (KeyValuePair<string, string> store in stores)
            {
                Int16 storeid = Int16.Parse(store.Key);
                KeyValuePair<Int16, string> listItem = new KeyValuePair<short, string>(storeid, (store.Key + " - " + store.Value));
                storesconcat.Add(listItem);
            }

            rcbStore.DataSource = storesconcat;
            rcbStore.DataTextField = "value";
            rcbStore.DataValueField = "key";
        }

        protected bool isErrorMessage(ref string msg)
        {
            string theMessage = "";
            bool result = true;

            try
            {
                string[] lines = msg.Split("\n".ToCharArray());
                theMessage = lines[0];
            }
            catch
            {
                theMessage = msg;
                return false;
            }
            if (theMessage.IndexOf("ERROR: ") > 0)
            {
                msg = theMessage.Substring(18);
            }
            else
            {
                msg = theMessage.Substring(11);
                result = false;
            }
            return result;
        }
  
    }
}