<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Pages/RI.Master"
    Title="System Configuration" CodeBehind="SystemParams.aspx.cs" Inherits="IHF.ApplicationLayer.Web.Pages.Admin.Setup.SystemParams" %>

<%--<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>--%>
    <div class="childdefault">
        <table>
            <tr>
                <td class="childheader">
                    System Configuration
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    Params
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlSystemParams" OnSelectedIndexChanged = "ddlSystemParams_SelectedIndexChanged"
                    AutoPostBack =true>
                        <%--<asp:ListItem Value="1"> None</asp:ListItem>
                        <asp:ListItem Value="2"> Manual sort</asp:ListItem>
                        <asp:ListItem Value="3"> Despatch Service Internal</asp:ListItem>--%>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
            <td>
            Param Type
            </td>
            <td>
            <asp:TextBox runat="server" Enabled="false" Text="Char T/F" ID="txtParmType" 
                    Width="158px" />
            </td>
            </tr>
            <tr>
                <td>
                    Param Value
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtParamValue" Width="158px"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td >
                </td>
                <td align="left">
                    <asp:Button runat="server" ID="btnSave" Text="Save" onclick="btnSave_Click" />
                </td>
            </tr>

            <tr><td ></td></tr>
            <tr><td ></td></tr>

        </table>
        
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    </div>
</asp:Content>
