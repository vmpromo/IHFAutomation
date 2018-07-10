using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IHF.BusinessLayer.DataAccessObjects;
using System.Globalization;
using Telerik.Web.UI;

namespace IHF.ApplicationLayer.Web.Pages.TestHarness
{
    public partial class TestLiveLoads : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lbmsg.Visible = false;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            TestLiveLoadsDAO dao = new TestLiveLoadsDAO();

            Button button = (Button)sender;

            ContentPlaceHolder edititem = (ContentPlaceHolder)button.NamingContainer;

        RadComboBox rcb = (RadComboBox)edititem.FindControl("RadComboBox1");

        string selectedPickLoad = rcb.SelectedValue;


        string TestPickLoad = dao.ImportLiveLoad(selectedPickLoad);

        lbmsg.Text = "Created pick load " + TestPickLoad;
        lbmsg.Visible = true;

        }
    
    }



}