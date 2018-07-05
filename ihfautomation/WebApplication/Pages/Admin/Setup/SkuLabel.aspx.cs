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
    public partial class SkuLabel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                // first time

                TBbarcode.Visible = false;
                TBSku.Visible = false;

                Lbdropdown.Visible = false;
                DDSkunumber.Visible = false;

                LBresult.Visible = false;

                TBbarcode.Text = string.Empty;
                TBSku.Text = string.Empty;
            }
            if (IsPostBack)
            {
                // subsequently
                // call database to load the drop down
                string skubarcode = null;
                string skuno = null;
                string printstatus = null;
                SkuLabelDAO skunum = new SkuLabelDAO();
                LBresult.Text = string.Empty;
                LBresult.Visible = false;

                if (RBBarcode.Checked)
                {
                    SetFocus(TBbarcode);
                }
                
                else if (RBSku.Checked)
                {
                    SetFocus(TBSku);
                }

                try
                {
                    if (RBBarcode.Checked)
                    {
                            
                        if (TBbarcode.Text.ToString().Length > 0)
                        {
                            skubarcode = TBbarcode.Text.ToString();
                        
                            if (skubarcode.Length > 0)
                            {

                                DataSet ds = skunum.Search_sku(skubarcode);
                                DataTable dt = ds.Tables[0];
                                List<string> skus = new List<string>();

                                foreach (DataRow row in dt.Rows)
                                {
                                    
                                    skus.Add(row["skunum"].ToString());

                                }
                                
                                if (skus.Count > 0)
                                {
                                    if (skus.Count == 1)
                                    {
                                        // initiate print
                                        printstatus = Print(Int32.Parse(skus[0]));
                                        
                                        LBresult.Visible = true;
                                        LBresult.Text = "Success";
                                        LBresult.ForeColor = Color.Blue;
                                    }

                                }
                                else
                                {

                                    LBresult.Visible = true;
                                    LBresult.Text = "Error: SKU not found";
                                    LBresult.ForeColor = Color.Red;
                                }

                                TBbarcode.Text = string.Empty;
                                

                            }

                        }    
                    }
                    else if (RBSku.Checked) 
                    {

                        if (TBSku.Text.ToString().Length > 0)
                        {
                            skuno = TBSku.Text.ToString();



                            DataSet ds1 = skunum.Get_sku_details(skuno);
                            DataTable dt1 = ds1.Tables[0];
                            List<string> skualias = new List<string>();

                            foreach (DataRow row in dt1.Rows)
                            {
                                
                                skualias.Add(row["skualias"].ToString());

                            }
                            
                            if (skualias.Count > 0)
                            {
                                if (skualias.Count == 1)
                                {
                                    // initiate print
                                    printstatus = Print(Int32.Parse(skuno));

                                    LBresult.Visible = true;
                                    LBresult.Text = "Success";
                                    LBresult.ForeColor = Color.Blue;
                                }
                                else
                                {

                                    foreach (DataRow row in dt1.Rows)
                                    {
                                        DDSkunumber.Items.Add(row["skualias"].ToString());

                                        DDSkunumber.Visible = true;
                                        Lbdropdown.Visible = true;


                                    }
                                }

                            }
                            else
                            {

                                LBresult.Visible = true;
                                LBresult.Text = "Error: SKU not found";
                                LBresult.ForeColor = Color.Red;
                            }

                            TBSku.Text = string.Empty;
                            
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

        protected void RBBarcode_CheckedChanged(object sender, EventArgs e)
        {
            TBbarcode.Visible = true;            
            TBSku.Visible = false;
        }

        protected void RBskunumber_CheckedChanged(object sender, EventArgs e)
        {
            
            TBbarcode.Visible = false;
            TBSku.Visible = false;

        }

        protected void RBSku_CheckedChanged(object sender, EventArgs e)
        {
            TBbarcode.Visible = false;
            TBSku.Visible = true;
        }


        protected void DDSkunumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            string skuupc = DDSkunumber.SelectedValue.ToString();

            
        }

        private string Print(Int32 I_sku)
        {
            // call the print application
            
            //string skunumber = DDSkunumber.SelectedValue.ToString();
            //Int32 iskuid = Int32.Parse(skunumber);


            //HttpContext.Current.Response.Write("inside the btn_trolley_ps_Click");
            string machinename = Shared.UserHostName;
            string reportname = "6";
            string devicetype = "6";
            //HttpContext.Current.Response.Write("before calling webservice " + machinename + reportname + devicetype);

            PrintService ps = new PrintService();
            string printstatus = ps.PrintLabel(reportname, machinename, devicetype, I_sku, true);

            return printstatus;
            //HttpContext.Current.Response.Write("after print" + test);

        }

        
        
        
        
    }
}