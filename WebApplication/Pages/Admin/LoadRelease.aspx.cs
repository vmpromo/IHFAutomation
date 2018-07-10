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
using System.Text.RegularExpressions;

namespace IHF.ApplicationLayer.Web.Pages.Admin
{
    public partial class LoadRelease : System.Web.UI.Page
    {
        Int32 load_status = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            Initialise();
            if (!IsPostBack)
            {

                this.Getdropdown();


                // this will load an empty radgrid in the page load
                load_status = -1;


            }
            else
            {

                try
                {


                    string load_status_cd = loadStatus.SelectedItem.Value;

                    if (load_status_cd != string.Empty)
                        load_status = Int32.Parse(load_status_cd);



                    this.BindData(load_status);




                }
                catch (Exception Ex1)
                {
                    HandleError(Ex1.Message, 1);
                }
            }

        }

        private void Initialise()
        {

          

            LoadReleaseDAO loadmgr = new LoadReleaseDAO();
            string mansort_status = loadmgr.Get_manual_sort_status();

            if (mansort_status != string.Empty && mansort_status == "F")
            {
                //if (loadStatus.SelectedItem.Value != "40")
                btnSave.Enabled = false;
                HandleError("Manual Sorting is Disabled. Load can only be released to Sorter", 1);
            }
            else
            {
                btnSave.Enabled = true;

                Error.Text = string.Empty;
                Error.Visible = false;
            }
            // display message as well



        }

        public void BindData(Int32 Load_status)
        {
            LoadReleaseDAO loadmgr = new LoadReleaseDAO();
            DataSet dataSet = loadmgr.Get_load(Load_status);

            grdLoadRelease.MasterTableView.DataKeyNames = new String[] { "pick_load_num", "tot_single_orders", "tot_multi_orders" };
            grdLoadRelease.DataSource = dataSet.Tables[0];

        }


        private void Getdropdown()
        {

            LoadReleaseDAO loadmgr = new LoadReleaseDAO();
            DataSet ds_load = new DataSet();
            DataTable dt_load = new DataTable();


            ds_load = loadmgr.Get_load_status();
            dt_load = ds_load.Tables[0];

            foreach (DataRow row in dt_load.Rows)
            {
                string item_code_str = row["code"].ToString();
                string item_desc = row["short_description"].ToString();
                loadStatus.Items.Insert(0, new ListItem(item_desc, item_code_str));
            }




        }

        protected void grdLoadRelease_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {

            this.BindData(load_status);
        }

        protected void grdLoadRelease_ItemDataBound(object sender, GridItemEventArgs e)
        {

        }


        protected void grdLoadRelease_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = (GridDataItem)e.Item;

                string loadid = dataItem.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["pick_load_num"].ToString();

                LoadReleaseDAO loadmgr = new LoadReleaseDAO();

                // dropdown for area
                //DropDownList rad = (DropDownList)dataItem.FindControl("releaseTo");
                RadComboBox rad = (RadComboBox)dataItem.FindControl("releaseTo");
                RadComboBox rad2 = (RadComboBox)dataItem.FindControl("release_action");
                CheckBox chklabel = (CheckBox)dataItem.FindControl("SelectRow");

                // dropdown for area
                if (load_status == 20) //ready for release
                {


                    DataSet relto_ds = new DataSet();

                    if (relto_ds.Tables.Count == 0 || relto_ds.Tables[0].Rows.Count == 0)
                        relto_ds = loadmgr.Get_area();


                    rad.Items.Insert(0, new RadComboBoxItem("None", "-1"));

                    foreach (DataRow r in relto_ds.Tables[0].Rows)
                    {
                        RadComboBoxItem item = new RadComboBoxItem();
                        item.Text = r["area_descr"].ToString();
                        item.Value = r["area_id"].ToString();
                        item.Attributes.Add("free_loc", r["free_loc"].ToString());
                        rad.Items.Add(item);
                        item.DataBind();


                    }

                    //rad.DataSource = relto_ds.Tables["AC"];
                    //rad.DataTextField = "area_descr";
                    //rad.DataValueField = "area_id";
                    ////rad.Items.Insert(0, new RadComboBoxItem("None", "0"));
                    //rad.DataBind();
                    rad.Visible = true;


                }
                else
                {
                    DataSet relto_ds2 = new DataSet();
                    relto_ds2 = loadmgr.Get_area_load(loadid);


                    rad.DataSource = relto_ds2.Tables["ACL"];
                    rad.DataTextField = "area_descr";
                    rad.DataValueField = "area_id";
                    rad.DataBind();
                    rad.Visible = true;
                    rad.Enabled = false;
                }


                // dropdown for action
                if (load_status == 20 || load_status == 50)// ready for release or released
                {

                    if (load_status == 20)
                        btnSave.Enabled = true;

                    DataSet relact_ds2 = new DataSet();
                    relact_ds2 = loadmgr.Get_action(load_status, loadid);


                    rad2.DataSource = relact_ds2.Tables["ACT"];
                    rad2.DataTextField = "action_desc";
                    rad2.DataValueField = "action_cd";
                    rad2.DataBind();
                    rad2.Visible = true;

                    //if (load_status == 20)
                    //{
                    //    if (rad2.SelectedValue == "3")// admin released load
                    //    {
                    //        rad.Enabled = false;                            

                    //    }
                    //}


                    if (load_status == 50)//released
                    {
                        if (rad.SelectedValue == "-1")// area released to is sorter
                        {
                            rad2.Enabled = false;
                            chklabel.Enabled = false;
                        }

                        if (rad2.SelectedValue == "0")// admin released load
                        {
                            rad2.Enabled = false;
                            chklabel.Enabled = false;

                        }

                    }


                }
                else// other load statuses
                {
                    rad2.Enabled = false;
                    chklabel.Enabled = false;
                    btnSave.Enabled = false;
                }
            }
        }

        protected void grdLoadRelease_ItemCommand(object sender, GridCommandEventArgs e)
        {

        }

        protected void release_to_DD_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {

            //RadComboBox combobox = (RadComboBox)sender;
            //GridEditFormItem edititem = (GridEditFormItem)combobox.NamingContainer;




        }

        protected void btnGetLoad_Click(object sender, System.EventArgs e)
        {
            grdLoadRelease.Rebind();
        }

        protected void btnSave_Click(object sender, System.EventArgs e)
        {
            LoadReleaseDAO loadmgr = new LoadReleaseDAO();

            string displayname = User.Identity.Name;
            Int32 area_id;
            Int32 action_ind;

            if (IsValidAreaSelected())
            {
                foreach (GridItem item in grdLoadRelease.MasterTableView.Items)
                {
                    if (item is GridEditableItem)
                    {
                        GridEditableItem editableItem = item as GridDataItem;
                        CheckBox chlabel = (CheckBox)editableItem.FindControl("SelectRow");
                        UserActivity setclass = new UserActivity();
                        ActivityLogDAO actlog = new ActivityLogDAO();

                        if (chlabel.Checked)
                        {
                            string loadid = editableItem.OwnerTableView.DataKeyValues[item.ItemIndex]["pick_load_num"].ToString();

                            string singleOrder = editableItem.OwnerTableView.DataKeyValues[item.ItemIndex]["tot_single_orders"].ToString();

                            string multiOrders = editableItem.OwnerTableView.DataKeyValues[item.ItemIndex]["tot_multi_orders"].ToString();


                            RadComboBox areaid = (RadComboBox)editableItem.FindControl("releaseTo");
                            RadComboBox actionind = (RadComboBox)editableItem.FindControl("release_action");




                            area_id = Int32.Parse(areaid.SelectedValue);
                            action_ind = Int32.Parse(actionind.SelectedValue);


                            try
                            {

                                decimal resultCode = loadmgr.Update_Load(
                                                                    loadid,
                                                                    area_id,
                                                                    action_ind,
                                                                    int.Parse(string.IsNullOrEmpty(singleOrder) ? "0" : singleOrder),
                                                                    int.Parse(string.IsNullOrEmpty(multiOrders) ? "0" : multiOrders),
                                                                    displayname);



                                if (resultCode == -1)
                                    HandleError("Load Release Failed for split load", 1);
                                else if (resultCode == -2)
                                    HandleError("Area selected for Load Release/Unrelease is disabled", 1);
                                else if (resultCode == -3)
                                    HandleError("Area does not accept store orders", 1);
                                else
                                    HandleError("Load(s) Updated", 0);
                            }
                            catch (Exception ex)
                            {
                                // activity logging
                                setclass.AppSystem = (Int32)ActivityLogEnum.AppSystem.IHF;
                                setclass.ApplicationId = (Int32)ActivityLogEnum.ApplicationID.ManualSort;
                                setclass.ModuleId = (Int32)ActivityLogEnum.ModuleID.MSLoadRelease;
                                setclass.EventType = (Int32)EventType.ManualSortLoadRelease;
                                setclass.ResultCode = (Int32)ActivityLogEnum.ResultCd.MSLoadReleaseFailed;
                                setclass.EventDateTime = DateTime.Now;
                                setclass.SortLoadId = loadid;
                                setclass.Value1 = action_ind.ToString();
                                setclass.Value2 = area_id.ToString();
                                setclass.UserId = displayname;


                                actlog.SaveUserActivity(setclass);


                                HandleError(CleanErrorMessage(ex.Message), 1);
                            }


                        }
                    }

                }
            }
            else
            {

                HandleError("Please select valid area to release the load.", 1);
            }

            //grdLoadRelease.Rebind();
            string load_status_cd = loadStatus.SelectedItem.Value;

            if (load_status_cd != string.Empty)
                load_status = Int32.Parse(load_status_cd);



            this.BindData(load_status);
            grdLoadRelease.Rebind();


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



        protected void RadComboBoxReleaseTo_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {

            string str = e.Text;

        }



        private string CleanErrorMessage(string msg)
        {

            string exceptionMessage = msg;
            string output = "";

            string[] errorLines = Regex.Split(exceptionMessage, "ORA-");


            var ermsg = from s in errorLines
                        where s.StartsWith("20999") || s.StartsWith("20998")
                        select s;

            output = string.Join(",", ermsg);

            //foreach (string s in errorLines)
            // {
            //     if (s.StartsWith("20999") || s.StartsWith("20998"))
            //     {
            //         output = s.Substring(7);
            //     }
            // }
            return output.Substring(7);
        }



        private bool IsValidAreaSelected()
        {

            bool retVal = true;

            foreach (GridItem item in grdLoadRelease.MasterTableView.Items)
            {
                if (item is GridEditableItem)
                {
                    GridEditableItem editableItem = item as GridDataItem;

                    CheckBox chlabel = (CheckBox)editableItem.FindControl("SelectRow");

                    RadComboBox areaid = (RadComboBox)editableItem.FindControl("releaseTo");

                    if (chlabel.Checked == true)
                    {
                        string area_id = areaid.SelectedValue;

                        if (area_id == "-1")
                        {
                            retVal = false;

                            break;
                        }
                    }

                }
            }


            return retVal;
        }


    }
}