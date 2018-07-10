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
using System.Web.UI.HtmlControls;
using System.Text;

namespace IHF.ApplicationLayer.Web.Pages.Admin.Setup
{
    public partial class SkuLabelForTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // first time

                TBLoad.Visible = false;
                TBChute.Visible = false;
                DD_trolley.Visible = false;
                //Button1.Visible = false;

                LBresult.Visible = false;

                TBChute.Text = string.Empty;
                TBLoad.Text = string.Empty;

                Getdropdown();
            }
            
        }

        private void Getdropdown()
        {

            SkuLabelDAO skudao = new SkuLabelDAO();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();


            ds = skudao.Get_trolley_dropdown();
            dt = ds.Tables[0];

            foreach (DataRow row in dt.Rows)
            {
                string item_code_str = row["trolley_id"].ToString();
                string item_desc = row["trolley_label"].ToString();
                DD_trolley.Items.Insert(0, new ListItem(item_desc, item_code_str));
            }

        }

        protected void RBLoad_CheckedChanged(object sender, EventArgs e)
        {
            TBLoad.Visible = true;
            TBChute.Visible = false;
            DD_trolley.Visible = false;
        }

        protected void RBChute_CheckedChanged(object sender, EventArgs e)
        {

            TBChute.Visible = true;
            TBLoad.Visible = false;
            DD_trolley.Visible = false;

        }

        protected void RBTrolley_CheckedChanged(object sender, EventArgs e)
        {
            TBChute.Visible = false;
            TBLoad.Visible = false;
            DD_trolley.Visible = true;
        }


        protected void DDSkunumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            
 
        }

        private string Print(Int32 I_sku)
        {
            // call the print application


            string machinename = Shared.UserHostName;
            string reportname = "11";
            string devicetype = "6";
            
            PrintService ps = new PrintService();
            string printstatus = ps.PrintLabel(reportname, machinename, devicetype, I_sku, true);

            return printstatus;
           

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string loadnum = null;
            string chute_id = null;
            string trolley_id = null;
            string printstatus = null;

            SkuLabelDAO skudao = new SkuLabelDAO();
            LBresult.Text = string.Empty;
            LBresult.Visible = false;

            if (RBChute.Checked)
            {
                SetFocus(TBChute);
            }

            else if (RBLoad.Checked)
            {
                SetFocus(TBLoad);
            }

            try
            {
                if (RBLoad.Checked)
                {

                    if (TBLoad.Text.ToString().Length > 0)
                    {
                        loadnum = TBLoad.Text.ToString();

                        if (loadnum.Length > 0)
                        {

                            DataSet ds = skudao.SkuForLoad(loadnum);
                            if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                            {
                                LBresult.Visible = true;
                                LBresult.Text = "Error: SKUs not found for Load: " + loadnum;
                                LBresult.ForeColor = Color.Red;

                                
                            }
                            else
                            {
                                DataTable dt = ds.Tables[0];


                                foreach (DataRow row in dt.Rows)
                                {

                                    string sku_id_str = (row["sku"].ToString());
                                    printstatus = Print(Int32.Parse(sku_id_str));

                                }

                                LBresult.Visible = true;
                                LBresult.Text = "SKU Labels sent to printer";
                                LBresult.ForeColor = Color.Blue;
                            }


                            TBLoad.Text = string.Empty;


                        }

                    }
                }

                else if (RBChute.Checked)
                {

                    if (TBChute.Text.ToString().Length > 0)
                    {
                        chute_id = TBChute.Text.ToString();



                        if (chute_id.Length > 0)
                        {

                            DataSet ds = skudao.SkuForChute(Int32.Parse(chute_id));
                            if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                            {
                                LBresult.Visible = true;
                                LBresult.Text = "Error: SKUs not found for Chute: " + chute_id;
                                LBresult.ForeColor = Color.Red;
                            }
                            else
                            {
                                DataTable dt = ds.Tables[0];


                                foreach (DataRow row in dt.Rows)
                                {

                                    string sku_id_str = (row["sku"].ToString());
                                    printstatus = Print(Int32.Parse(sku_id_str));

                                }

                                LBresult.Visible = true;
                                LBresult.Text = "SKU Labels sent to printer";
                                LBresult.ForeColor = Color.Blue;
                            }

                            TBChute.Text = string.Empty;

                        }

                    }
                }
                //else if trolley
                else if (RBTrolley.Checked)
                {

                    trolley_id = DD_trolley.SelectedValue.ToString();

                    if (trolley_id.Length > 0)
                    {

                        DataSet ds = skudao.SkuFortrolley(Int32.Parse(trolley_id));
                        if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                        {
                            LBresult.Visible = true;
                            LBresult.Text = "Error: SKUs not found for Load: " + trolley_id;
                            LBresult.ForeColor = Color.Red;
                        }
                        else
                        {
                            DataTable dt = ds.Tables[0];


                            foreach (DataRow row in dt.Rows)
                            {

                                string sku_id_str = (row["sku"].ToString());
                                printstatus = Print(Int32.Parse(sku_id_str));

                            }

                            LBresult.Visible = true;
                            LBresult.Text = "SKU Labels sent to printer";
                            LBresult.ForeColor = Color.Blue;
                        }


                    }

                }


            }
            catch (Exception ex)
            {
                LBresult.Visible = true;
                LBresult.Text = "Error: " + ex.Message;
                LBresult.ForeColor = Color.Red;
            }
        }
    }


}