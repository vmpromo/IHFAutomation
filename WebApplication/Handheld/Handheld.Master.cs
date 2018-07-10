using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Text;
using IHF.BusinessLayer.Util;
using System.Configuration;
using System.Net;

namespace IHF.ApplicationLayer.Web.Handheld
{
    public partial class Handheld : System.Web.UI.MasterPage
    {
        const string MAX_MANU_ITEMS = "6";

        StringBuilder _childScript = new StringBuilder();

        public void RegisterScript(StringBuilder cs = null)
        {
            if (!this.Page.ClientScript.IsClientScriptBlockRegistered("checkAndPost"))
            {
                StringBuilder script = new StringBuilder();
                //if (new Browser().IsDevice())
                if (true)
                {
                    script.Append("function checkAndPost(){\n");
                    script.Append("\t var barcode = document.getElementById('" +
                        BarcodeID + "');\n");

                    script.Append("\t if (document.activeElement.id == barcode.id){\n");

                    script.Append("\t \t  if (barcode.value == " +
                        (int)HandheldInput.Home + " ){\n");
                    script.Append("\t \t \t  document.getElementById('" +
                        ParentID + "').value = '" +
                        (int)HandheldInput.Root + "';\n");
                    script.Append("\t \t \t  window.navigate('home.aspx');\n");
                    script.Append("\t \t \t  return false;\n");
                    script.Append("\t \t } \n");

                    script.Append("\t \t  if (barcode.value ==  " +
                        (int)HandheldInput.SignOut + "  && barcode.value.length > 0){\n");
                    script.Append("\t \t \t window.navigate('home.aspx?Signout=1');\n");
                    script.Append("\t \t \t return false;\n");
                    script.Append("\t \t } \n");

                    script.Append("\t \t \t document.getElementById('" +
                        ParentID + "').value = barcode.value;\n");
                    script.Append("\t \t \t document.getElementById('" +
                        FormID + "').submit();\n");

                    script.Append("\t } \n");//end if for activeElement

                    if (cs != null && cs.Length > 0)
                        script.Append(cs);

                    script.Append("} \n \n");//function end
                }
                else
                {
                    //script for testing handheld screens on desktop browser
                    script.Append("function checkAndPost(){\n");

                    script.Append("\t var barcode = document.getElementById('" + BarcodeID + "');\n");

                    //script.Append("\t if (window.event.keyCode == " + (int)HandheldInput.Enter + " ){\n");
                    
                    script.Append("\t \t  if (barcode.value == " + (int)HandheldInput.Home + " ){\n");
                    //script.Append("\t \t \t  barcode.value = '';\n");
                    script.Append("\t \t \t  document.getElementById('" + ParentID + "').value = '" + (int)HandheldInput.Root + "';\n");
                    script.Append("\t \t \t  window.navigate('home.aspx');\n");
                    script.Append("\t \t \t  return false;\n");
                    script.Append("\t \t } \n");


                    script.Append("\t \t  if (barcode.value ==  " + (int)HandheldInput.SignOut + "  && barcode.value.length > 0){\n");
                    script.Append("\t \t \t //document.getElementById('" + ParentID + "').value = barcode.value;\n");
                    script.Append("\t \t \t //barcode.value = '';\n");
                    script.Append("\t \t \t window.navigate('home.aspx?Signout=1');\n");
                    script.Append("\t \t \t return false;\n");

                    script.Append("\t \t } \n");

                    if (cs != null && cs.Length > 0)
                        script.Append(cs);


                    //script.Append("\t } \n");//end if for keycode



                    script.Append("} \n \n");//function end
                }

                this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "PostBack",
                    script.ToString(), true);
            }
        }

        public string HostName
        {
            get
            {
                string hostName = String.Empty;
                try
                {
                    hostName = System.Net.Dns.GetHostEntry(Request.ServerVariables["remote_addr"]).HostName;
                }
                catch (Exception)
                {
                    hostName = "";
                }
                return hostName;
            }
        }

        public void Reset()
        {
            //initialise the error message panel style
            PopupMessage.InnerHtml = string.Empty;
            Panel1.Style.Add("display", "none");
        }

        public bool RegisterStandardScript
        {
            set
            {
                if (value == true)
                {
                    if (this._childScript.Length > 0)
                        RegisterScript(this._childScript);
                    else
                        RegisterScript();
                }
            }
        }

        public string ErrorMessage
        {
            set
            {
                if (!IsMessageVisible())
                {
                    PopupMessage.InnerHtml = "<embed id='snd' src='" +
                        ConfigurationManager.AppSettings["ErrorSound"] +
                        "' autostart='true' hidden='true'></embed>";
                    PopupMessage.InnerHtml += "<span style='color:Red;'>" + value + "</span>";

                }
            }
        }

        public string SuccessMessage
        {
            set
            {
                if (!IsMessageVisible())
                {
                    PopupMessage.InnerHtml = "<embed id='snd' src='" +
                        ConfigurationManager.AppSettings["SuccessSound"] +
                        "' autostart='true' hidden='true'></embed>";
                    PopupMessage.InnerHtml += "<span style='color:Green;'>" + value + "</span>";

                }
            }
        }

        public string MessageBoard
        {
            set
            {
                this.message.InnerHtml = value;
            }
        }

        public bool DisplayMessage
        {
            set
            {
                if (!IsMessageVisible())
                    Panel1.Style["display"] = value == true ? "block" : "none";
            }
        }

        private bool IsMessageVisible()
        {
            return Panel1.Style["display"] == "block";
        }

        public string BarcodeValue
        {
            get
            {
                return barcode.Text;
            }
            set
            {
                this.barcode.Text = value;
            }
        }

        public string BarcodeID
        {
            get
            {
                return barcode.ClientID;
            }
        }

        public string FormID
        {
            get
            {
                return this.form1.ClientID;
            }
        }

        public string ParentID
        {
            get
            {
                return this.ParentMenuId.ClientID;
            }
        }

        public string ParentValue
        {
            get
            {
                return this.ParentMenuId.Value;
            }
        }

        public string MessageWindowID
        {
            get
            {
                return this.Panel1.ClientID;
            }
        }

        public string MaxManuItems
        {
            get
            {
                return MAX_MANU_ITEMS;
            }
        }

        public StringBuilder ChildScript
        {
            set
            {
                if (value.Length > 0)
                {
                    _childScript = value;
                }

            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            user.InnerText = Membership.GetUser().UserName;

            this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "focusScript",
                "function Focus() {\n" +
                "\t if (document.getElementById('" + Panel1.ClientID + "').style.display == 'block'){\n" +
                "\t \t document.getElementById('" + btnCancel.ClientID + "').focus();\n" +
                "\t \t document.getElementById('" + barcode.ClientID + "').disabled = true;\n\t } \n" +
                "\t else \n" +
                "\t \t document.getElementById('" + barcode.ClientID + "').focus();\n" +
                "}\n\n", true);

            this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "HidePanel",
                "function HidePanel() {\n" +
                "\t document.getElementById('" + Panel1.ClientID + "').style.display = 'none';\n" +
                "\t document.getElementById('" + barcode.ClientID + "').disabled = false;\n" +
                "}\n", true);


            if (!new Browser().IsDevice())
                barcode.Attributes.Add("onkeyup", "return checkAndPost()");


        }
    }
}