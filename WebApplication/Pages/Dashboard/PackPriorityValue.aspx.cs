using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Printing;
using System.Drawing.Imaging;
using System.IO;
using Telerik.Web.UI;
using IHF.BusinessLayer.BusinessClasses;
using IHF.BusinessLayer.DataAccessObjects;
using IHF.EnterpriseLibrary.DataServices;
using IHF.BusinessLayer.Util;
using IHF.BusinessLayer.DataAccessObjects.Despatch;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace IHF.ApplicationLayer.Web.Pages.Dashboard
{
    public partial class PackPriorityValue : System.Web.UI.Page
    {
        string criterion = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            

                try
                {
                    criterion = Request.QueryString["criterion_name"].ToString();

                    this.Bindgrid(criterion);
                
                }
                catch (Exception ex)
                {

                }


            
            
        }

        protected void Bindgrid(string icriterion)
        {
            
                PackPriorityDAO packpriorityval_dao = new PackPriorityDAO();

                DataSet packpriorityval_ds = new DataSet();

                packpriorityval_ds = packpriorityval_dao.Get_packPriorityVal(icriterion);

                RadGrid2.DataSource = packpriorityval_ds.Tables[0];
            


        }

        protected void RadGrid2_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {

            this.Bindgrid(criterion);

        }
        protected void RadGrid2_ItemDataBound(object sender, GridItemEventArgs e)
        {
            
        }
        protected void RadGrid2_ItemCommand(object sender, GridCommandEventArgs e)
        {

        }

        protected void RadGrid2_UpdateCommand(object source, GridCommandEventArgs e)
        {

            GridEditableItem editedItem = e.Item as GridEditableItem;
            GridEditManager editMan = editedItem.EditManager;

            string criterion_name = editedItem.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["criterion_name"].ToString();

            Int32 wt_int;
            string wt_str = null;

            wt_str = (editedItem.FindControl("TB1") as TextBox).Text;
            wt_int = Int32.Parse(wt_str);

            
            GridEditFormItem item = e.Item as GridEditFormItem;
            string val_type = item.ParentItem["value_type"].Text; 
            string val_type_char = val_type;

            string criterion_val = item.ParentItem["criterion_value"].Text; 
            
            string I_char_val = null;
            Int32 I_num_val=0;
            DateTime I_date_val=DateTime.MinValue;

            switch (val_type_char)  
            {  
                case ("C"):  
                    I_char_val = criterion_val;  
                    break;  
                case ("N"):  
                    I_num_val = Int32.Parse(criterion_val); 
                    break; 
                case ("D"):  
                    I_date_val = DateTime.Parse(criterion_val);
                    break; 
                default:
                    I_char_val = criterion_val;
                    break; 
            }



            if (wt_int > 0)
            {

                PackPriorityDAO packpriority_dao = new PackPriorityDAO();
                Decimal status = packpriority_dao.Update_packpriorityval(criterion_name, val_type_char, wt_int, I_char_val, I_num_val, I_date_val);
            }


            this.Bindgrid(criterion);
        }

        protected void RadGrid2_EditCommand(object sender, GridCommandEventArgs e)
        {


        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Dashboard/PackPriority.aspx");
        }
    }
}