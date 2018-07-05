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
    public partial class FailedToteSetup : System.Web.UI.Page
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
            FailedtoteDAO ftdao = new FailedtoteDAO();
            DataSet dataSet = ftdao.Search_FTote();
            RadGrid1.DataSource = dataSet.Tables[0];
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
                string reportname = "9";
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

            FailedtoteDAO ftdao = new FailedtoteDAO();
            DataSet ftclass = new DataSet();
            DataTable fttable = new DataTable("ftotetable");



            ftclass = ftdao.Search_FTote();
            fttable = ftclass.Tables[0];

            foreach (DataRow row in fttable.Rows)
            {
                itoteid = Int32.Parse(row["tote_id"].ToString());

                string machinename = Shared.UserHostName;//System.Environment.MachineName;
                string reportname = "9";//ReportNameEnum.Trolley.ToString();
                string devicetype = "6";//DeviceType.ZS.ToString();
                //HttpContext.Current.Response.Write("before calling webservice " + machinename + reportname + devicetype);

                PrintService ps = new PrintService();
                string test = ps.PrintLabel(reportname, machinename, devicetype, itoteid, true);
                //HttpContext.Current.Response.Write("after print" + test);

            }
        }
    }
}