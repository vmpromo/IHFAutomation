using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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
    public partial class SystemParams : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Initialise();

            if (!IsPostBack)
            {

                this.Getdropdown();
 
            }
            
        }

        private void Initialise()
        {
            Label1.Text = string.Empty;
            Label1.Visible = false;
            SetFocus(txtParamValue);

        }

        private void Getdropdown()
        {

            SystemParamsDAO sydao = new SystemParamsDAO();
            DataSet ds_sp = new DataSet();
            DataTable dt_sp = new DataTable();

            
            ds_sp = sydao.Get_pararms();
            dt_sp = ds_sp.Tables[0];

            foreach (DataRow row in dt_sp.Rows)
            {
                string sp_code_str = row["param_id"].ToString();
                string sp_desc = row["param_name"].ToString();
                ddlSystemParams.Items.Insert(0, new ListItem(sp_desc, sp_code_str));
            }
            



        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //validation
            Int32 paramid = 0;
            string paramid_str = ddlSystemParams.SelectedItem.Value;
            string displayname = User.Identity.Name;
            SystemParamsDAO sydao = new SystemParamsDAO();

            try
            {
                if (paramid_str != string.Empty)
                    paramid = Int32.Parse(paramid_str);
                

                string paramtyp = sydao.Get_param_type_code(paramid);
                string paramval = txtParamValue.Text;

                if (paramtyp == "I")
                {
                    Int32 paramvalint = Int32.Parse(paramval);
                }
                else if (paramtyp == "N")
                {
                    decimal paramvaldec = decimal.Parse(paramval);
                }
                else if (paramtyp == "C")
                {

                }
                else if (paramtyp == "D")
                {
                    DateTime paramvaldt = DateTime.Parse(paramval);
                }

                // update database for param value
                decimal paramidop = sydao.Update_params(paramid, paramtyp, paramval, displayname);

                if (paramidop == 0)
                    HandleError("Invalid Parameter Type", 1);
                else
                    HandleError("Parameter Value updated successfully", 0);
            }
            catch (Exception ex1)
            { 
                HandleError(ex1.Message,1);
            }

            

            
        }

        protected void ddlSystemParams_SelectedIndexChanged(object sender, EventArgs e)
        {
            Int32 paramid = 0;
            string paramid_str = ddlSystemParams.SelectedItem.Value;

            try
            {
                if (paramid_str != string.Empty)
                    paramid = Int32.Parse(paramid_str);

                SystemParamsDAO sydao = new SystemParamsDAO();

                string paramtyp = sydao.Get_param_type(paramid);
                txtParmType.Text = paramtyp;

                string paramval = sydao.Get_param_value(paramid);
                txtParamValue.Text = paramval;
            }
            catch (Exception ex)
            { 
                HandleError(ex.Message,1);
            }


        }

        protected void HandleError(string I_message, Int32 I_status)
        {
            Label1.Text = I_message;
            Label1.Visible = true;

            if (I_status == 0)// success
                Label1.ForeColor = Color.Blue;
            else //failure
                Label1.ForeColor = Color.Red;
        }


    }
}