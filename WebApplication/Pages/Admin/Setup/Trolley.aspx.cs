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

namespace IHF.ApplicationLayer.Web.Pages.Admin.Setup
{
    public partial class Trolley : System.Web.UI.Page
    {
        protected void Page_PreRender(object sender, System.EventArgs e)
        {

        }

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
            TrolleyDAO tmgr = new TrolleyDAO();
            DataSet dataSet = tmgr.Search_trolley();
            RadGrid1.DataSource = dataSet.Tables[0];
        }
        protected void RadGrid1_UpdateCommand(object source, GridCommandEventArgs e)
        {

            GridEditableItem editedItem = e.Item as GridEditableItem;
            GridEditManager editMan = editedItem.EditManager;

            string ID = editedItem.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["trolley_id"].ToString();
            Int32 tr_type;
            string label = null;            

            label = (editedItem["trolley_label"].Controls[0] as TextBox).Text;

            Int32 itrolleyid = Int32.Parse(ID);

            TrolleyDAO tmgr = new TrolleyDAO();
            DataSet ds_edit = tmgr.Get_trolleyclass(itrolleyid);

            DataTable dt_edit = new DataTable();
            dt_edit = ds_edit.Tables[0];

            Int32 class_typ = 0;
            foreach (DataRow row in dt_edit.Rows)
            {
                string class_typ_str = row["class_id"].ToString();
                class_typ = Int32.Parse(class_typ_str);
                        
            }
            RadComboBox trolleytype = (RadComboBox)editedItem.FindControl("trolleytype_RadComboBox");

            
            tr_type = 0;
            
            string displayname = User.Identity.Name;

            TrolleyDAO tmgrupd = new TrolleyDAO();
            decimal trolley_id = tmgrupd.Update_trolley(itrolleyid, label, tr_type, displayname);
            //RadGrid1.DataSource = dataSet.Tables[0];
            BindData();
        }


        protected void RadGrid1_InsertCommand(object source, GridCommandEventArgs e)
        {


            GridEditableItem editedItem = e.Item as GridEditableItem;
            GridEditManager editMan = editedItem.EditManager;

            string label = null;
            Int32 class_typ;
            Int32 tr_type;

            label = (editedItem["trolley_label"].Controls[0] as TextBox).Text;

            RadComboBox classtype = (RadComboBox)editedItem.FindControl("trolleyclass_type_RadComboBox");
            RadComboBox trolleytype = (RadComboBox)editedItem.FindControl("trolleytype_RadComboBox");

            class_typ = Int32.Parse(classtype.SelectedValue);
            if (class_typ == 3)//multi trolley
            {
                tr_type = Int32.Parse(trolleytype.SelectedValue);
            }
            else

            {
                tr_type = 0;
            }
            string displayname = User.Identity.Name;

            TrolleyDAO tmgrins = new TrolleyDAO();
            decimal trolleyreturnid = tmgrins.Create_trolley(class_typ, tr_type, label, displayname);
            //RadGrid1.DataSource = dataSet.Tables[0];
            BindData();
        }
        protected void RadGrid1_DeleteCommand(object source, GridCommandEventArgs e)
        {
            /***Removed on request from Business***/
            
            //GridDataItem dataItem = (GridDataItem)e.Item;
            //String strolleyid = dataItem.GetDataKeyValue("trolley_id").ToString();
            //Int32 itrolleyid = Int32.Parse(strolleyid);

            
            //string displayname = User.Identity.Name;
            //TrolleyDAO tmgrdel = new TrolleyDAO();
            //decimal trolleyid = tmgrdel.Delete_trolley(itrolleyid, displayname);
            ////RadGrid1.DataSource = dataSet.Tables[0];
            //BindData();

        }

        protected void RadGrid1_EditCommand(object sender, GridCommandEventArgs e)
        {


        }

        protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        {


            if (e.Item is GridEditableItem && (e.Item as GridEditableItem).IsInEditMode)
            {

                DataSet ds_codes = new DataSet();
                GridEditableItem editedItem = e.Item as GridEditableItem;


                //testing
                GridEditFormInsertItem insert = e.Item as GridEditFormInsertItem;
                if (insert == null) // for edit hide classtype
                {
                    
                    //in editedItem mode hide both dropdowns
                    // disable the validation
                    RadComboBox trolleytype = (RadComboBox)editedItem
                                                            .FindControl("trolleytype_RadComboBox");
                    trolleytype.Visible = false;

                    RadComboBox trolleyclassType = (RadComboBox)editedItem
                                                .FindControl("trolleyclass_type_RadComboBox");

                    trolleyclassType.Visible = false;


                    RequiredFieldValidator RequiredFieldValidator1 = (RequiredFieldValidator)editedItem
                                                    .FindControl("RequiredFieldValidator1");

                    RequiredFieldValidator1.Enabled = false;

                    RequiredFieldValidator RequiredFieldValidator2 = (RequiredFieldValidator)editedItem
                                                    .FindControl("RequiredFieldValidator2");

                    RequiredFieldValidator2.Enabled = false;

                    

                }
                else
                {
                    

                    TrolleyDAO trolleydao = new TrolleyDAO();
                    ds_codes = trolleydao.GetClassCodes();
                    RadComboBox trolleyclassType = (RadComboBox)editedItem
                                                    .FindControl("trolleyclass_type_RadComboBox");
                    trolleyclassType.DataSource = ds_codes.Tables["CD"];

                    trolleyclassType.DataTextField = "char_short_translation";
                    trolleyclassType.DataValueField = "code";
                    trolleyclassType.DataBind();

                    trolleyclassType.Items.Insert(0, new RadComboBoxItem("", "0"));
                    



                    // trolley type drop down
                    
                     
                    RadComboBox trolleytype = (RadComboBox)editedItem
                                                        .FindControl("trolleytype_RadComboBox");
                    trolleytype.Visible = false;

                    RequiredFieldValidator RequiredFieldValidator2 = (RequiredFieldValidator)editedItem
                                                    .FindControl("RequiredFieldValidator2");

                    RequiredFieldValidator2.Enabled = false;
                    
                }



            }
        }


        protected void RadGrid1_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName.ToLower() == "print")
            {
                
                GridDataItem dataItem = (GridDataItem)e.Item;
                String strolleyid = dataItem.GetDataKeyValue("trolley_id").ToString();
                Int32 itrolleyid = Int32.Parse(strolleyid);


                //HttpContext.Current.Response.Write("inside the btn_trolley_ps_Click");
                string machinename = Shared.UserHostName;
                string reportname = "2";
                string devicetype = "6";
                //HttpContext.Current.Response.Write("before calling webservice " + machinename + reportname + devicetype);
                
                PrintService ps = new PrintService();
                string test = ps.PrintLabel(reportname, machinename, devicetype, itrolleyid, true);
                //HttpContext.Current.Response.Write("after print" + test);
                
            }
        }


        protected void trolleyclass_type_RadComboBox_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {

            RadComboBox combobox = (RadComboBox)sender;
            GridEditFormItem edititem = (GridEditFormItem)combobox.NamingContainer;

            
            if (combobox.SelectedValue == "3")
            {
                // trolley type drop down
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

                RequiredFieldValidator RequiredFieldValidator2 = (RequiredFieldValidator)edititem
                                                    .FindControl("RequiredFieldValidator2");

                RequiredFieldValidator2.Enabled = true;
            }
            else
            {
                RadComboBox trolleytype = (RadComboBox)edititem
                                                    .FindControl("trolleytype_RadComboBox");

                trolleytype.Visible = false;
            }
            
        }

    }
}