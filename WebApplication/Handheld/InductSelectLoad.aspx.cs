using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IHF.ApplicationLayer.Web.Handheld
{
    public partial class InductSelectLoad : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Master.Reset();

            this.Master.RegisterStandardScript = true;

            this.Master.MessageBoard = "Enter 1 or 2 to select sorter";

            if (IsPostBack)
            {
                string areaselected = this.Master.BarcodeValue;

                if (areaselected == "1" || areaselected == "2")
                {
                    Response.Redirect("ManualInductLoad.aspx?areaid=" + areaselected);
                }

            }

        }
    }
}