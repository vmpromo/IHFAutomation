using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IHF.BusinessLayer.BusinessClasses;
using IHF.BusinessLayer.DataAccessObjects;
using IHF.EnterpriseLibrary.DataServices;
using IHF.BusinessLayer.Util;

namespace PackingMock
{
    public partial class Test : System.Web.UI.Page
    {
        PrintService ps = new PrintService();


        protected void Page_Load(object sender, EventArgs e)
        {


        }


        protected void btnPrintTest_Click(object sender, EventArgs e)
        {

            this.lblPrintStatus.Text = ps.TestPrint(Shared.UserHostName);



        }
    }
}