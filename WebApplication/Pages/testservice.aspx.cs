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
using System.Net;

namespace IHF.ApplicationLayer.Web.Pages
{
    public partial class testservice : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Label8.ForeColor = Color.Blue;

               
            if (!IsPostBack)
            {
                // populate the dropdownlist
                List<string> rptarr = new List<string>();
                rptarr.Add("Chute");
                rptarr.Add("Trolley");
                rptarr.Add("Workstation");
                rptarr.Add("Device");
                rptarr.Add("Cage");
                rptarr.Add("Sku");
                rptarr.Add("Despatch Lane");
                rptarr.Add("Carrier");
                rptarr.Add("Failed Tote");
                rptarr.Add("Overflow Tote");

                //DD_reporttyp.DataSource = rptarr;
                //DD_reporttyp.DataBind();



                Int16 count = 1;
                foreach (string rpt in rptarr)
                {
                    DD_reporttyp.Items.Add(new ListItem(rpt, count.ToString()));
                    count++;
                }
                
                Label7.Visible = false;
                Label8.Visible = false;

                TB_DN.Visible = false;
                TB_PL.Visible = false;
                TB_CD.Visible = false;
                TB_FT.Visible = false;

                DD_reporttyp.Visible = false;
                TB_id.Visible = false;

                LB_DNO.Visible = false;
                LB_CDO.Visible = false;
                LB_PLO.Visible = false;
                LB_FTO.Visible = false;
            }
            else
            {
                Label7.Visible = true;
                Label8.Visible = true;

                Label7.ForeColor = Color.Black;

                Label7.Text = string.Empty;
                Label8.Text = string.Empty;
            }

            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string hostname = Dns.GetHostName();
            
            if (RB_Workstation.Checked)
            {
                string machinename = Shared.UserHostName;
                Label8.Text = "Print Initiated from " + hostname +'/' + machinename;
                PrintService ps = new PrintService();
                string status = ps.TestPrint(Shared.UserHostName);
                Label7.Text = "Status: " + status;
            
            }
            else if (RB_label.Checked)
            {

                string machinename = Shared.UserHostName;
                string user = User.Identity.Name;
                string status = null;
                string reportname = null;
                string devicetype = null;
                

                try
                {
                    reportname = DD_reporttyp.SelectedValue;
                    if (reportname != "8")
                    {
                        if (TB_id.Text != string.Empty)
                        {

                            Int32 ID = Int32.Parse(TB_id.Text.ToString());

                            if (reportname == "5")
                            {
                                devicetype = "1";
                            }
                            else
                            {
                                devicetype = "6";
                            }
                            Label8.Text = "Print Initiated from " + hostname + '/' + machinename;
                            PrintService ps = new PrintService();
                            status = ps.PrintLabel(reportname, machinename, devicetype, ID, true);

                        }
                        else
                        {
                            status = "Enter unique ID";
                            Label7.ForeColor = Color.Red;
                        }
                    }
                    else
                    {
                        status = "Carrier Label cannot be generated";
                        Label7.ForeColor = Color.Red;
                    }
                }
                catch (Exception ex)
                {
                    status = "Invalid ID";
                    Label7.ForeColor = Color.Red;
                }

                Label7.Text = "Status: " + status;
                TB_id.Text = string.Empty;
            }
            else if (RB_DN.Checked)
            {
                //210414200//210413474//9709040//210414786//110413930
                
                string machinename = Shared.UserHostName;
                string user = User.Identity.Name;
                string status = null;
                try
                {
                    if (TB_DN.Text != string.Empty)
                    {
                        decimal ordernumber = decimal.Parse(TB_DN.Text.ToString());
                        Label8.Text = "Print Initiated from " + hostname + '/' + machinename;
                        PrintService ps = new PrintService();
                        status = ps.PrintPackDocuments(ordernumber, true, machinename, "DP", user);

                    }
                    else
                    {
                        Label7.ForeColor = Color.Red;
                        status = "Enter Ordernumber";
                    }
                }
                catch (Exception ex)
                {
                    status = "Invalid Ordernumber";
                    Label7.ForeColor = Color.Red;
                }

                Label7.Text = "Status: " + status;
                TB_DN.Text = string.Empty;
            }
            else if (RB_PL.Checked)
            {
                string machinename = Shared.UserHostName;
                string user = User.Identity.Name;
                string status = null;
                try
                {
                    if (TB_PL.Text != string.Empty)
                    {
                        decimal ordernumber = decimal.Parse(TB_PL.Text.ToString());
                        Label8.Text = "Print Initiated from " + hostname + '/' + machinename;
                        PrintService ps = new PrintService();
                        status = ps.PrintPackDocuments(ordernumber, true, machinename, "L", user);
                        
                    }
                    else
                    {
                        Label7.ForeColor = Color.Red;
                        status = "Enter Ordernumber";
                    }
                }
                catch(Exception ex)
                {
                    status = "Invalid Ordernumber";
                    Label7.ForeColor = Color.Red;
                }

                Label7.Text = "Status: " + status;
                TB_PL.Text = string.Empty;
            }
            else if (RB_CD.Checked)
            {
                string machinename = Shared.UserHostName;
                string user = User.Identity.Name;
                string status = null;
                try
                {
                    if (TB_CD.Text != string.Empty)
                    {
                        decimal ordernumber = decimal.Parse(TB_CD.Text.ToString());
                        Label8.Text = "Print Initiated from " + hostname + '/' + machinename;
                        PrintService ps = new PrintService();
                        status = ps.PrintPackDocuments(ordernumber, true, machinename, "D", user);

                    }
                    else
                    {
                        Label7.ForeColor = Color.Red;
                        status = "Enter Ordernumber";
                    }
                }
                catch (Exception ex)
                {
                    status = "Invalid Ordernumber";
                    Label7.ForeColor = Color.Red;
                }

                Label7.Text = "Status: " + status;
                TB_CD.Text = string.Empty;
            }
            else if (RB_FT.Checked)
            {
                //210414661
                string machinename = Shared.UserHostName;
                string user = User.Identity.Name;
                string status = null;
                try
                {
                    if (TB_FT.Text != string.Empty)
                    {
                        decimal ordernumber = decimal.Parse(TB_FT.Text.ToString());
                        Label8.Text = "Print Initiated from " + hostname + '/' + machinename;
                        PrintService ps = new PrintService();
                        status = ps.PrintFailedTote(ordernumber, true, machinename, user);
                    }
                    else
                    {
                        Label7.ForeColor = Color.Red;
                        status = "Enter Ordernumber";
                    }
                }
                catch (Exception ex)
                {
                    status = "Invalid Ordernumber";
                    Label7.ForeColor = Color.Red;
                }

                Label7.Text = "Status: " + status;
                TB_FT.Text = string.Empty;
            }

            else
            {
                Label7.ForeColor = Color.Red;
                Label7.Text = "Select one of the Options before clicking Print";
            }

        }
        
        protected void RB_DN_CheckedChanged(object sender, EventArgs e)
        {
            TB_DN.Visible = true;
            TB_PL.Visible = false;
            TB_CD.Visible = false;
            TB_FT.Visible = false;

            LB_DNO.Visible = true;
            LB_CDO.Visible = false;
            LB_PLO.Visible = false;
            LB_FTO.Visible = false;

            DD_reporttyp.Visible = false;
            TB_id.Visible = false;
        }

        protected void RB_PL_CheckedChanged(object sender, EventArgs e)
        {
            TB_DN.Visible = false;
            TB_PL.Visible = true;
            TB_CD.Visible = false;
            TB_FT.Visible = false;

            LB_DNO.Visible = false;
            LB_CDO.Visible = false;
            LB_PLO.Visible = true;
            LB_FTO.Visible = false;

            DD_reporttyp.Visible = false;
            TB_id.Visible = false;
        }

        protected void RB_CD_CheckedChanged(object sender, EventArgs e)
        {
            TB_DN.Visible = false;
            TB_PL.Visible = false;
            TB_CD.Visible = true;
            TB_FT.Visible = false;

            LB_DNO.Visible = false;
            LB_CDO.Visible = true;
            LB_PLO.Visible = false;
            LB_FTO.Visible = false;

            DD_reporttyp.Visible = false;
            TB_id.Visible = false;
        }

        protected void RB_FT_CheckedChanged(object sender, EventArgs e)
        {
            TB_DN.Visible = false;
            TB_PL.Visible = false;
            TB_CD.Visible = false;
            TB_FT.Visible = true;

            LB_DNO.Visible = false;
            LB_CDO.Visible = false;
            LB_PLO.Visible = false;
            LB_FTO.Visible = true;

            DD_reporttyp.Visible = false;
            TB_id.Visible = false;
        }
        protected void RB_wk_CheckedChanged(object sender, EventArgs e)
        {
            TB_DN.Visible = false;
            TB_PL.Visible = false;
            TB_CD.Visible = false;
            TB_FT.Visible = false;

            LB_DNO.Visible = false;
            LB_CDO.Visible = false;
            LB_PLO.Visible = false;
            LB_FTO.Visible = false;

            DD_reporttyp.Visible = false;
            TB_id.Visible = false;
        }

        protected void RB_label_CheckedChanged(object sender, EventArgs e)
        {
            TB_DN.Visible = false;
            TB_PL.Visible = false;
            TB_CD.Visible = false;
            TB_FT.Visible = false;

            LB_DNO.Visible = false;
            LB_CDO.Visible = false;
            LB_PLO.Visible = false;
            LB_FTO.Visible = false;

            DD_reporttyp.Visible = true;
            TB_id.Visible = true;
        }
    }
}