using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IHF.BusinessLayer.DataAccessObjects;
using System.Globalization;
using Telerik.Web.UI;
namespace IHF.ApplicationLayer.Web.Pages.TestHarness
{
    public partial class TestLoacCreation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lbmsg.Visible = false;
            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-GB");
            this.RadGrid1.Culture = culture;

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            TestLoadDAO dao = new TestLoadDAO();

            string pickload = dao.CreateTestLoad();

            

            lbmsg.Text = "Created pick load " + pickload;
            lbmsg.Visible = true;

            RadGrid1.DataBind();

        }

        protected void DeliverByDateTextBox_DataBinding(object sender, EventArgs e)
        {
            

        }

        protected void RadGrid1_InsertCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
       {

           if (e.Item is GridEditableItem)
           {
               GridEditableItem editedItem = e.Item as GridEditableItem;
               //here editedItem.SavedOldValues will be the dictionary which holds the
               //predefined values

               //Prepare new dictionary object
               Hashtable newValues = new Hashtable();
               e.Item.OwnerTableView.ExtractValuesFromItem(newValues, editedItem);
               //the newValues instance is the new collection of key -> value pairs
               //with the updated ny the user data

               //e.Canceled = true;
           }
 
        }

        protected void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                if (!(e.Item is GridEditFormInsertItem))
                {
                    GridEditableItem item = e.Item as GridEditableItem;
                    GridEditManager manager = item.EditManager;
                    //GridTextBoxColumnEditor editor = manager.GetColumnEditor("DeliverByDate") as GridTextBoxColumnEditor;
                    //editor.TextBoxControl.Enabled = false;
                }
            }
        }

        protected void RadGrid1_ItemCommand(object sender, GridCommandEventArgs e)
        {

        }
    }
}