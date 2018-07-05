using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using IHF.BusinessLayer.DataAccessObjects.Packing;
using IHF.BusinessLayer.DataAccessObjects;
using System.Drawing;


namespace IHF.ApplicationLayer.Web.Pages.Packing
{
    public partial class Relabel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Page_PreRender(Object o, EventArgs e)
        {
            this.RadTextBox2.Focus();            
        }


        protected void RadTextBox1_TextChanged(object sender, EventArgs e)
        {

            RadTextBox rtb = (RadTextBox)sender;

            PackingDAO dao = new PackingDAO();

            try
            {
                // Clone the necessary database objects and get the ID of the new created consignment
                dao.RelabelParcel(rtb.Text.ToUpper(), User.Identity.Name, Shared.UserHostName);


                lblMsg.Text = "Parcel Re-labelled";
                lblMsg.Visible = true;
                lblMsg.ForeColor = Color.Blue;

            }
            catch (Exception ex)
            {
                    lblMsg.Text = FormatErrorMessage(ex.Message);
                    lblMsg.Visible = true;
                    lblMsg.ForeColor = Color.Red;
            }

            rtb.Text = "";                
        }

        protected void RadButton1_Click(object sender, EventArgs e)
        {

        }

        protected string FormatErrorMessage(string msg)
        {
            string theMessage = "";

            try
            {
                string[] lines = msg.Split("\n".ToCharArray());
                theMessage = lines[0];
                if (lines.Length == 1) return theMessage;
                else
                    return theMessage.Substring(11);
            }
            catch
            {
                return theMessage;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }
    }
}