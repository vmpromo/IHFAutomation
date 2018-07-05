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
using IHF.BusinessLayer.DataAccessObjects.ActivityLog;
using IHF.BusinessLayer.BusinessClasses.ActivityLog;

namespace IHF.ApplicationLayer.Web.Pages.Admin.Setup
{
    public partial class ChutesInArea : System.Web.UI.Page
    {
        Int32 areaID = 0;
        Int32 rowcount = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                Initialize();
                
                string areaidstr = Request.QueryString["Area_id"].ToString();
                if (areaidstr != null)
                    areaID = Int32.Parse(areaidstr);

                                
                


                ChuteDAO chmgr = new ChuteDAO();
                string area_status = chmgr.Check_area(areaID);

                //if (area_status == "T")
                //{
                //    RadGrid1.Enabled = false;
                //    Btn_chute.Enabled = false;

                //    lblArea.Text = " Area "+ areaID + " is in Use.";
                //    lblArea.ForeColor = Color.Blue;
                //}
                //else
                //{
                //    RadGrid1.Enabled = true;
                //    Btn_chute.Enabled = true;

                //    lblArea.Text = "Chutes for Area ID: " + areaID;
                //    lblArea.ForeColor = Color.Blue;
                
                //}

                this.BindData(areaID);

                if (rowcount > 0)
                {
                    if (area_status == "T")
                    {
                        RadGrid1.Enabled = false;
                        Btn_chute.Enabled = false;

                        lblArea.Text = " Area " + areaID + " is in Use.";
                        lblArea.ForeColor = Color.Blue;
                    }
                    else
                    {
                        RadGrid1.Enabled = true;
                        Btn_chute.Enabled = true;

                        lblArea.Text = "Chutes for Area ID: " + areaID;
                        lblArea.ForeColor = Color.Blue;

                    }

                }
                else
                {
                    Btn_chute.Enabled = false;

                    lblArea.Text = "No Chutes exist for Area ID: " + areaID;
                    lblArea.ForeColor = Color.Blue;
                }
                
            }
            catch (Exception ex1)
            {
                HandleError(ex1.Message, 1);
            }
        }

        
        public void BindData(Int32 area_ID)
        {
            ChuteDAO chmgr = new ChuteDAO();
            DataSet dataSet = chmgr.Chutes_In_Area(area_ID);
            RadGrid1.DataSource = dataSet.Tables[0];

            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                rowcount++; 
            
            } 
        }

        protected void RadGrid1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            this.BindData(areaID);
            
        }

        
        protected void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
        {
            
        }

        
        protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = (GridDataItem)e.Item;

                string loadid = dataItem.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["chute_id"].ToString();

                Label lab = (Label)dataItem.FindControl("ch_type_lab");
                Int32 ch_type_id = Int32.Parse(lab.Text);

                RadTextBox chseq = (RadTextBox)dataItem.FindControl("ch_seq_TextBox");

                if (ch_type_id == 2)
                    chseq.Enabled = true;
                else
                    chseq.Enabled = false;
            }
            
        }
        protected void RadGrid1_ItemCommand(object sender, GridCommandEventArgs e)
        {
            
        }

        
        protected void btnSave_Click(object sender, System.EventArgs e)
        {
            ChuteDAO chmgr = new ChuteDAO();
            UserActivity setclass = new UserActivity();
            ActivityLogDAO actlog = new ActivityLogDAO();

            string displayname = User.Identity.Name;

            // Create new DataTable and DataSource objects.
            DataTable dt_seq = new DataTable();

            // Declare DataColumn and DataRow variables.
            DataColumn dc_seq;
            DataRow dr_seq;


            // Create new DataColumn, set DataType, ColumnName and add to DataTable.    
            dc_seq = new DataColumn();
            dc_seq.DataType = System.Type.GetType("System.Int32");
            dc_seq.ColumnName = "chute_id";
            dt_seq.Columns.Add(dc_seq);

            // Create second column.
            dc_seq = new DataColumn();
            dc_seq.DataType = System.Type.GetType("System.Int32");
            dc_seq.ColumnName = "chute_seq";
            dt_seq.Columns.Add(dc_seq);

            try
            {
                foreach (GridItem item in RadGrid1.MasterTableView.Items)
                {
                    if (item is GridEditableItem)
                    {
                        GridEditableItem editableItem = item as GridDataItem;
                        string chuteid_str = editableItem.OwnerTableView.DataKeyValues[item.ItemIndex]["chute_id"].ToString();
                        Int32 chuteid = Int32.Parse(chuteid_str);

                        Label ch_typ_lab = (Label)editableItem.FindControl("ch_type_lab");
                        Int32 ch_type_id = Int32.Parse(ch_typ_lab.Text);

                        if (ch_type_id == 2)
                        {
                            RadTextBox ch_seq_tb = (RadTextBox)editableItem.FindControl("ch_seq_TextBox");
                            Int32 ch_seq_id = Int32.Parse(ch_seq_tb.Text);

                            // Create new DataRow objects and add to DataTable.    
                        
                            dr_seq = dt_seq.NewRow();
                            dr_seq["chute_id"] = chuteid;
                            dr_seq["chute_seq"] = ch_seq_id;
                            dt_seq.Rows.Add(dr_seq);


                        }
                    
                    }
                }

                dt_seq.DefaultView.Sort = "chute_seq";
                dt_seq = dt_seq.DefaultView.ToTable();

                Int32 realseq = 1;

            
                foreach (DataRow row in dt_seq.Rows)
                {
                    string id_str = row["chute_id"].ToString();
                    Int32 id = Int32.Parse(id_str);
                    
                    decimal Chute_id = chmgr.Update_Chute_seq(id, realseq, displayname);

                    realseq++;
                }

                HandleError("Updated Chute Sequence Successfully",0);

                Initialize_save();


            }
            catch (Exception ex)
            {
                // activity logging
                HandleError(ex.Message, 1);

            }

            BindData(areaID);
            RadGrid1.Rebind();
            
           


        }

        protected void Btn_stack_Click(object sender, System.EventArgs e)
        {
            ChuteDAO chmgr = new ChuteDAO();
            UserActivity setclass = new UserActivity();
            ActivityLogDAO actlog = new ActivityLogDAO();

            Int32 stack_seq = 0;
            string displayname = User.Identity.Name;

            try
            {
                

                stack_seq = Int32.Parse(tb_stack.Text.ToString());

                if (stack_seq != 0)
                {
                    decimal upd_status =  chmgr.Manage_stack(areaID, stack_seq, displayname);

                    if (upd_status == 0)
                        HandleError("Stacks and Trolleys Created successfully", 0);
                    else if (upd_status == -1)
                        HandleError("Trolley detach Failed", 1);
                    else
                        HandleError("Error Occurred", 1);
                }
                else
                {
                    HandleError("Invalid Stack Sequence", 1);
                }
                Initialize_stack();
                
            }
            catch (Exception ex2)
            {
                HandleError(ex2.Message, 1);
                Initialize_save();
            }

            BindData(areaID);
            RadGrid1.Rebind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Admin/Setup/AreaSetup.aspx");
        }

        void Initialize()
        {
            Error.Visible = false;
            lbl_stack.Visible = false;
            tb_stack.Visible = false;
            RequiredFieldValidator1.Visible = false;
            Btn_stack.Visible = false;
        
        }

        void Initialize_save()
        {

            Btn_chute.Enabled = false;

            Btn_stack.Visible = true;
            lbl_stack.Visible = true;
            tb_stack.Visible = true;
            RequiredFieldValidator1.Visible = true;
            tb_stack.Text = string.Empty;

            ChuteDAO chutedao = new ChuteDAO();
            if (chutedao.CheckStack(areaID.ToString()))
                Btn_stack.Enabled = false;
            else
                Btn_stack.Enabled = true;
            

        }

        void Initialize_stack()
        {
            Btn_chute.Enabled = false;
            Btn_stack.Enabled = false;
            lbl_stack.Enabled = false;
            tb_stack.Enabled = false;
            RequiredFieldValidator1.Enabled = false;
            Btn_stack.Visible = true;
            lbl_stack.Visible = true;
            tb_stack.Visible = true;

        }

        protected void HandleError(string I_message, Int32 I_status)
        {
            Error.Text = I_message;
            Error.Visible = true;

            if (I_status == 0)// success
                Error.ForeColor = Color.Blue;
            else //failure
                Error.ForeColor = Color.Red;
        }

    }
}