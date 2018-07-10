using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using IHF.BusinessLayer.BusinessClasses;
using IHF.BusinessLayer.DataAccessObjects;
using IHF.EnterpriseLibrary.DataServices;
using System.Data;
using IHF.BusinessLayer.Util;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace IHF.ApplicationLayer.Web.Admin.Setup
{
    public partial class OverflowToteSetup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindData();
        }
        protected void RadGrid1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            BindData();

        }
        public void BindData()
        {
            OverflowtoteDAO overflowtotedao = new OverflowtoteDAO();
            DataSet dataSet = overflowtotedao.Search_OFTote();
            RadGrid1.DataSource = dataSet.Tables[0];
        }
        protected void RadGrid1_UpdateCommand(object source, GridCommandEventArgs e)
        {

            //update not allowed
        }

        protected void RadGrid1_EditCommand(object sender, GridCommandEventArgs e)
        {

            //edit not allowed
        }

        

        protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        {

        }
        protected void RadGrid1_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Print")
            {
                GridDataItem dataItem = (GridDataItem)e.Item;
                String strolleyid = dataItem.GetDataKeyValue("tote_id").ToString();
                Int32 itrolleyid = Int32.Parse(strolleyid);


                //HttpContext.Current.Response.Write("inside the btn_trolley_ps_Click");
                string machinename = Shared.UserHostName;
                string reportname = "10";
                string devicetype = "6";
                //HttpContext.Current.Response.Write("before calling webservice " + machinename + reportname + devicetype);

                PrintService ps = new PrintService();
                string test = ps.PrintLabel(reportname, machinename, devicetype, itrolleyid, true);
                //HttpContext.Current.Response.Write("after print" + test);


            }

            
        }

        protected void Btn_print_all_Click(object sender, EventArgs e)
        {
            Int32 itoteid = 0;

            OverflowtoteDAO ofdao = new OverflowtoteDAO();
            DataSet ofclass = new DataSet();
            DataTable oftable = new DataTable("oftotetable");



            ofclass = ofdao.Search_OFTote();
            oftable = ofclass.Tables[0];

            foreach (DataRow row in oftable.Rows)
            {
                itoteid = Int32.Parse(row["tote_id"].ToString());

                string machinename = Shared.UserHostName;//System.Environment.MachineName;
                string reportname = "10";//ReportNameEnum.Trolley.ToString();
                string devicetype = "6";//DeviceType.ZS.ToString();
                //HttpContext.Current.Response.Write("before calling webservice " + machinename + reportname + devicetype);

                PrintService ps = new PrintService();
                string test = ps.PrintLabel(reportname, machinename, devicetype, itoteid, true);
                //HttpContext.Current.Response.Write("after print" + test);

            }
        }
    }
}