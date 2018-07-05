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
using IHF.BusinessLayer.Util;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Printing;
using System.Drawing.Imaging;
using System.IO;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace IHF.ApplicationLayer.Web.Pages.Dashboard
{
    public partial class ItemsPacked : System.Web.UI.Page
    {
        DateTime startdate = DateTime.Now;
        DateTime enddate = DateTime.Now;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            string scriptStrst = "javascript:return popUpCalendar(this," + getClientIDst() + @", 'dd/mm/yyyy', '__doPostBack(\'" + getClientIDst() + @"\')')";
            imgCalendarst.Attributes.Add("onclick", scriptStrst);
            string scriptStrend = "javascript:return popUpCalendar(this," + getClientIDend() + @", 'dd/mm/yyyy', '__doPostBack(\'" + getClientIDend() + @"\')')";
            imgCalendarend.Attributes.Add("onclick", scriptStrend);

            Label1.Visible = false;

            if (!IsPostBack)
            {

                 startdate = DateTime.MinValue;
                 enddate = DateTime.MinValue;

            }
            else
            {

                try
                {


                    DateTime stdate = DateTime.MinValue;
                    DateTime enddate = DateTime.Now;

                    if (txt_stDate.Text.ToString() != string.Empty)
                        stdate = DateTime.Parse(txt_stDate.Text.ToString());
                    if (txt_endDate.Text.ToString() != string.Empty)
                        enddate = DateTime.Parse(txt_endDate.Text.ToString());

                    this.BindData_itempacked(stdate, enddate);




                }
                catch (Exception Ex)
                {
                    string errmsg = "Error While Processing Request";
                    Label1.Visible = true;
                    Label1.Text = errmsg;
                    Label1.ForeColor = Color.Red;
                }

            }
        }

        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);

        }

        public string getClientIDst()
        {
            return txt_stDate.ClientID;
        }

        public string getClientIDend()
        {
            return txt_endDate.ClientID;
        }

        public string CalendarDatest
        {
            get
            {
                return txt_stDate.Text;
            }
            set
            {
                txt_stDate.Text = value;
            }
        }

        public string CalendarDateend
        {
            get
            {
                return txt_endDate.Text;
            }
            set
            {
                txt_endDate.Text = value;
            }
        }
        

        public void BindData_itempacked(DateTime stdt, DateTime enddt)
        {
            
            ItemcompleteDAO itemdao = new ItemcompleteDAO();
            DataSet ds_item = itemdao.Get_itemspacked(stdt, enddt);

            DataTable dt_item = PivotTable(ds_item.Tables[0]);

            RadGrid2.DataSource = dt_item;
        }

        protected void RadGrid2_ItemCommand(object sender, GridCommandEventArgs e)
        {
        }

        protected void RadGrid2_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {

            this.BindData_itempacked(startdate, enddate);

        }

        protected void RadGrid2_ItemDataBound(object sender, GridItemEventArgs e)
        {
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            DateTime sdate = DateTime.MinValue;
            DateTime edate = DateTime.MinValue; 
            if (txt_stDate.Text.ToString() != string.Empty)
            {
                sdate = DateTime.Parse(txt_stDate.Text);
            }
            if (txt_endDate.Text.ToString() != string.Empty)
            {
                edate = DateTime.Parse(txt_endDate.Text);
            }

            if (txt_stDate.Text.ToString() == string.Empty && txt_endDate.Text.ToString() != string.Empty)
            {
                string errmsg = "Please Enter Start Date";
                Label1.Visible = true;
                Label1.Text = errmsg;
                Label1.ForeColor = Color.Red;

            }
            else
            {
                if (sdate > edate)
                {
                    string errmsg = "Start Date should be less than equal to End Date";
                    Label1.Visible = true;
                    Label1.Text = errmsg;
                    Label1.ForeColor = Color.Red;
                }
                else
                {
                    if (edate.Subtract(sdate).Days > 100)
                    {
                        string errmsg = "Search cannot exceed 100 days";
                        Label1.Visible = true;
                        Label1.Text = errmsg;
                        Label1.ForeColor = Color.Red;
                    }
                    else
                    {
                        RadGrid2.Rebind();
                    }
                }
            }

        }


        public DataTable PivotTable(DataTable source)
        {
            DataTable dest = new DataTable("Pivoted" + source.TableName);
            List<string> dayarr = new List<string>();

            dest.Columns.Add("Date");
            for (int i = 0; i < 25; i++)
            {
                dest.Columns.Add(i.ToString());

            }
            dest.Columns.Add("Total");
            for (int i = 0; i < source.Rows.Count; i++)
            {

                if (i > 0)
                {
                    if (source.Rows[i][0].ToString() != source.Rows[i - 1][0].ToString())
                    {
                        dest.Rows.Add(source.Rows[i][0].ToString());
                        dayarr.Add(source.Rows[i][0].ToString());
                    }
                }
                else
                {
                    dest.Rows.Add(source.Rows[i][0].ToString());
                    dayarr.Add(source.Rows[i][0].ToString());
                }
            }

            // find distinct days in the source datatable

            for (int j=0; j < dayarr.Count; j++ )
            {
                for (int i = 0; i < source.Rows.Count; i++)
                {

                    if (dayarr[j].ToString() == source.Rows[i][0].ToString())
                    {
                        Int16 hr = Int16.Parse(source.Rows[i][1].ToString());

                        dest.Rows[j][hr + 1] = source.Rows[i][2].ToString();
                    }
                }

            }
            
            // adding the total hours column

            Int32 itemcount = 0;
            Int32 totalcol = dest.Columns.Count;

            for (int r = 0; r < dest.Rows.Count; r++)
            {

                if (r > 0)
                {
                    dest.Rows[r - 1][totalcol - 1] = itemcount;
                }

                itemcount = 0;

                for (int c = 1; c < dest.Columns.Count; c++)
                {

                    if (dest.Rows[r][c].ToString() != string.Empty)
                    {
                        itemcount = itemcount + Int32.Parse(dest.Rows[r][c].ToString());
                    }
                }

                if (r == dest.Rows.Count - 1)
                {
                    dest.Rows[r][totalcol - 1] = itemcount;
                
                }
            }


            // calculating the sum total the last row

            dest.Rows.Add("Total Items");

            Int32 totalrow = dest.Rows.Count;
            Int32 coltotal = 0;

            

            for (int c = 1; c < dest.Columns.Count; c++)
            {
                coltotal = 0;
                for (int r = 0; r < dest.Rows.Count - 1; r++)
                {
                    if (dest.Rows[r][c].ToString() != string.Empty)
                    {
                        coltotal = coltotal + Int32.Parse(dest.Rows[r][c].ToString());
                    }
                }
                dest.Rows[totalrow - 1][c] = coltotal;

            }
            dest.AcceptChanges();
            return dest;
        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            RadGrid2.ExportSettings.IgnorePaging = true;
            RadGrid2.ExportSettings.OpenInNewWindow = true;
            RadGrid2.ExportSettings.ExportOnlyData = true;
            RadGrid2.MasterTableView.ExportToExcel();
        }



    }
}