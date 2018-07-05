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
using System.Drawing;
using IHF.BusinessLayer.Util;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace IHF.ApplicationLayer.Web.Admin.Setup
{
    public partial class ChuteSetup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Error.Visible = false;
            BindData();
        }
        protected void RadGrid1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            BindData();

        }
        public void BindData()
        {
            ChuteDAO chutedao = new ChuteDAO();
            DataSet dataSet = chutedao.Search_chute();
            RadGrid1.DataSource = dataSet.Tables[0];
        }
        protected void RadGrid1_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;


            string ch_label = null;
            Int32 ch_status_int;
            Int32 ch_type_int;
            string enb_ind_str;
            string int_ind_str;
            decimal area_id_dec;
            string displayname = User.Identity.Name;
            Int32 trolley_type_int;

            decimal Chuteid = decimal.Parse(editedItem.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["chute_id"].ToString());

            RadTextBox chlabel = (RadTextBox)editedItem.FindControl("ch_label_TextBox");
            ch_label = chlabel.Text.ToString();

            //RadComboBox chstatus = (RadComboBox)editedItem.FindControl("ch_status_RadComboBox");
            RadComboBox chtype = (RadComboBox)editedItem.FindControl("ch_type_RadComboBox");
            RadComboBox enbind = (RadComboBox)editedItem.FindControl("ch_enb_ind_RadComboBox");
            //RadComboBox intind = (RadComboBox)editedItem.FindControl("ch_int_ind_RadComboBox");
            RadComboBox areaid = (RadComboBox)editedItem.FindControl("ch_area_RadComboBox");
            RadComboBox trolleytype = (RadComboBox)editedItem.FindControl("trolleytype_RadComboBox");

            //ch_status_int = Int32.Parse(chstatus.SelectedValue);
            ch_status_int = 3;
            ch_type_int = Int32.Parse(chtype.SelectedValue);
            enb_ind_str = enbind.SelectedValue.ToString();
            //int_ind_str = intind.SelectedValue.ToString();
            area_id_dec = decimal.Parse(areaid.SelectedValue);

            if (ch_type_int == 2)//multi chute
            {
                trolley_type_int = Int32.Parse(trolleytype.SelectedValue);
            }
            else
            {
                trolley_type_int = 0;
            }
            ChuteDAO chuteinsert = new ChuteDAO();
            try
            {
                decimal chuteid = chuteinsert.Update_Chute(Chuteid,
                                                            ch_label,
                                                            ch_status_int,
                                                            ch_type_int,
                                                            enb_ind_str,
                                                            area_id_dec,
                                                            displayname,
                                                            trolley_type_int);
                if (chuteid == 0)
                {
                    // display error
                    string err_msg = "Error while updating Chute ID: " + Chuteid;
                    HandleError(err_msg, 1);
                }
                else if (chuteid == -1)
                {
                    string err_msg = "Area in Use. Cannot update Chute";
                    HandleError(err_msg, 1);
                }
                else
                {
                    HandleError("Chute Updated", 0);
                }
            }
            catch (Exception ex_upd)
            {
                string err = ex_upd.Message;
                HandleError(err, 1);
            }
            BindData();
            
        }

        

        protected void RadGrid1_InsertCommand(object source, GridCommandEventArgs e)
        {
            // insert area record in the database

            GridEditableItem editedItem = e.Item as GridEditableItem;
            GridEditManager editMan = editedItem.EditManager;

            string ch_label = null;
            Int32 ch_status_int;
            Int32 ch_type_int;
            string enb_ind_str;
            string int_ind_str;
            decimal area_id_dec;
            string displayname = User.Identity.Name;
            Int32 trolley_type_int;


            RadTextBox chlabel = (RadTextBox)editedItem.FindControl("ch_label_TextBox");
            ch_label = chlabel.Text.ToString();

            //RadComboBox chstatus = (RadComboBox)editedItem.FindControl("ch_status_RadComboBox");
            RadComboBox chtype = (RadComboBox)editedItem.FindControl("ch_type_RadComboBox");
            RadComboBox enbind = (RadComboBox)editedItem.FindControl("ch_enb_ind_RadComboBox");
            //RadComboBox intind = (RadComboBox)editedItem.FindControl("ch_int_ind_RadComboBox");
            RadComboBox areaid = (RadComboBox)editedItem.FindControl("ch_area_RadComboBox");
            RadComboBox trolleytype = (RadComboBox)editedItem.FindControl("trolleytype_RadComboBox");

            //ch_status_int = Int32.Parse(chstatus.SelectedValue);
            ch_status_int = 3;
            ch_type_int = Int32.Parse(chtype.SelectedValue);
            enb_ind_str = enbind.SelectedValue.ToString();
            //int_ind_str = intind.SelectedValue.ToString();
            area_id_dec = decimal.Parse(areaid.SelectedValue);

            if (ch_type_int == 2)//multi chute
            {
                trolley_type_int = Int32.Parse(trolleytype.SelectedValue);
            }
            else
            {
                trolley_type_int = 0;
            }
            ChuteDAO chuteinsert = new ChuteDAO();
            try
            {
                decimal chuteid = chuteinsert.Create_Chute(ch_label,
                                                            ch_status_int,
                                                            ch_type_int,
                                                            enb_ind_str,
                                                            area_id_dec,
                                                            displayname,
                                                            trolley_type_int);
                if (chuteid == 0)
                {
                    // display error
                    string err_msg = "Error while creating Chute";
                    HandleError(err_msg, 1);
                }
                else if (chuteid == -1)
                {
                    string err_msg = "Area in Use. Cannot create Chute";
                    HandleError(err_msg, 1);
                }
                else
                {
                    HandleError("Chute Created", 0);
                }

            }
            catch (Exception ex_ins)
            {
                string err = ex_ins.Message;
                HandleError(err, 1);
            }
            BindData();
        }

        
        protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = (GridDataItem)e.Item;

                decimal Chuteid = decimal.Parse(dataItem.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["chute_id"].ToString());
                Label chtype = (Label)dataItem.FindControl("chtypid");
                Int32 ch_type_int = Int32.Parse(chtype.Text);
                //

                // for the edit image

                if (Chuteid >= 10000 && ch_type_int == 2)
                {
                    dataItem["EditCommandColumn"].Controls[0].Visible = true;
                    
                }
                else
                {
                    dataItem["EditCommandColumn"].Controls[0].Visible = false;
                }

            }
            if (e.Item is GridEditableItem && (e.Item as GridEditableItem).IsInEditMode)
            {


                GridEditableItem editedItem = e.Item as GridEditableItem;
                GridEditFormInsertItem insert = e.Item as GridEditFormInsertItem;




                ChuteDAO chute_dao = new ChuteDAO();

                // chute type drop down

                DataSet ds_chutetype = new DataSet();
                ds_chutetype = chute_dao.GetChuteType();

                RadComboBox chtype = (RadComboBox)editedItem
                                                .FindControl("ch_type_RadComboBox");

                chtype.DataSource = ds_chutetype.Tables["CT"];
                chtype.DataTextField = "type_short_name";
                chtype.DataValueField = "chute_type_id";
                chtype.DataBind();
                chtype.Items.Insert(0, new RadComboBoxItem("", "0"));




                // chute status drop down
                /*
                DataSet ds_chstatus = new DataSet();
                ds_chstatus = chute_dao.GetChuteStatus();

                RadComboBox chstatus = (RadComboBox)editedItem
                                                .FindControl("ch_status_RadComboBox");

                chstatus.DataSource = ds_chstatus.Tables["CS"];
                chstatus.DataTextField = "status_short_name";
                chstatus.DataValueField = "chute_status_id";
                chstatus.DataBind();
                chstatus.Items.Insert(0, new RadComboBoxItem("", "0"));*/


                // Enable indicator drop down

                DataSet ds_enbind = new DataSet();
                ds_enbind = chute_dao.GetEnableInd();

                RadComboBox enbind = (RadComboBox)editedItem
                                                .FindControl("ch_enb_ind_RadComboBox");

                enbind.DataSource = ds_enbind.Tables["EI"];
                enbind.DataTextField = "ch_enable_desc";
                enbind.DataValueField = "ch_enable_code";
                enbind.DataBind();
                enbind.Items.Insert(0, new RadComboBoxItem("", "0"));


                // International indicator drop down

                //DataSet ds_intind = new DataSet();
                //ds_intind = chute_dao.GetIntInd();

                //RadComboBox intind = (RadComboBox)editedItem
                //                                .FindControl("ch_int_ind_RadComboBox");

                //intind.DataSource = ds_intind.Tables["II"];
                //intind.DataTextField = "ch_int_desc";
                //intind.DataValueField = "ch_int_code";
                //intind.DataBind();
                //intind.Items.Insert(0, new RadComboBoxItem("", "0"));



                // Area drop down

                DataSet ds_area = new DataSet();
                ds_area = chute_dao.GetArea();

                RadComboBox area = (RadComboBox)editedItem
                                                .FindControl("ch_area_RadComboBox");

                area.DataSource = ds_area.Tables["AR"];
                area.DataTextField = "area_desc";
                area.DataValueField = "aread_id";
                area.DataBind();
                area.Items.Insert(0, new RadComboBoxItem("", "0"));


                TrolleyDAO trolleydao = new TrolleyDAO();
                DataSet trolleyCodes = new DataSet();
                trolleyCodes = trolleydao.GetCodesByType();
                RadComboBox trolleytype = (RadComboBox)editedItem
                                                    .FindControl("trolleytype_RadComboBox");

                trolleytype.DataSource = trolleyCodes.Tables["TT"];
                trolleytype.DataTextField = "type_short_name";
                trolleytype.DataValueField = "type_id";
                trolleytype.DataBind();
                trolleytype.Items.Insert(0, new RadComboBoxItem("", "0"));
                trolleytype.Visible = true;

                //RadComboBox trolleytype = (RadComboBox)editedItem
                //                                        .FindControl("trolleytype_RadComboBox");
                //trolleytype.Visible = false;

                //RequiredFieldValidator RequiredFieldValidator6 = (RequiredFieldValidator)editedItem
                //                                .FindControl("RequiredFieldValidator6");

                //RequiredFieldValidator6.Enabled = false;


                if (e.Item.ItemIndex != -1)
                {
                    Int32 ChuteID = Int32.Parse(
                                        editedItem
                                        .OwnerTableView
                                        .DataKeyValues[e.Item.ItemIndex]["chute_id"]
                                        .ToString());

                    

                    DataSet ds_chutedtls = new DataSet();
                    ds_chutedtls = chute_dao.Get_chuteclass(ChuteID);

                    DataTable dt_chutedtls = ds_chutedtls.Tables[0];

                    Int32 ch_type = 0;
                    Int32 tr_type = 0;
                    Int32 area_id = 0;
                    string label = null;
                    string enable = null;
                    string international = null;
                    

                    foreach (DataRow row in dt_chutedtls.Rows)
                    {
                        ch_type = Int32.Parse(row["chutetyp_id"].ToString());
                        tr_type = Int32.Parse(row["trtyp_id"].ToString());
                        enable = row["enb_ind"].ToString();
                        international = row["int_ind"].ToString();
                        area_id = Int32.Parse(row["area_id"].ToString());
                        label = row["chlabel"].ToString();
                    }

                    RadTextBox ch_label = (RadTextBox)editedItem.FindControl("ch_label_TextBox");
                    ch_label.Text = label;

                    chtype.SelectedValue = ch_type.ToString();
                    trolleytype.SelectedValue = tr_type.ToString();
                    area.SelectedValue = area_id.ToString();
                    //intind.SelectedValue = enable;
                    enbind.SelectedValue = international;
                    

                }

            }
        }
        protected void RadGrid1_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Print")
            {
                GridDataItem dataItem = (GridDataItem)e.Item;
                String strolleyid = dataItem.GetDataKeyValue("chute_id").ToString();
                Int32 itrolleyid = Int32.Parse(strolleyid);


                //HttpContext.Current.Response.Write("inside the btn_trolley_ps_Click");
                string machinename = Shared.UserHostName;
                string reportname = "1";
                string devicetype = "6";
                //HttpContext.Current.Response.Write("before calling webservice " + machinename + reportname + devicetype);

                PrintService ps = new PrintService();
                string test = ps.PrintLabel(reportname, machinename, devicetype, itrolleyid, true);
                //HttpContext.Current.Response.Write("after print" + test);


            }
        }

        protected void ch_type_RadComboBox_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {

            RadComboBox combobox = (RadComboBox)sender;
            GridEditFormItem edititem = (GridEditFormItem)combobox.NamingContainer;


            if (combobox.SelectedValue == "2")
            {
                // trolley type drop down
                /*
                TrolleyDAO trolleydao = new TrolleyDAO();
                DataSet trolleyCodes = new DataSet();
                trolleyCodes = trolleydao.GetCodesByType();
                RadComboBox trolleytype = (RadComboBox)edititem
                                                    .FindControl("trolleytype_RadComboBox");

                trolleytype.DataSource = trolleyCodes.Tables["TT"];
                trolleytype.DataTextField = "type_short_name";
                trolleytype.DataValueField = "type_id";
                trolleytype.DataBind();
                trolleytype.Items.Insert(0, new RadComboBoxItem("", "0"));
                trolleytype.Visible = true;



                
                RequiredFieldValidator RequiredFieldValidator6 = (RequiredFieldValidator)edititem
                                                    .FindControl("RequiredFieldValidator6");

                RequiredFieldValidator6.Enabled = true;*/
            }
            else
            {
                RadComboBox trolleytype = (RadComboBox)edititem
                                                    .FindControl("trolleytype_RadComboBox");

                trolleytype.Visible = false;

                RequiredFieldValidator RequiredFieldValidator6 = (RequiredFieldValidator)edititem
                                                    .FindControl("RequiredFieldValidator6");

                RequiredFieldValidator6.Enabled = false;



            }

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