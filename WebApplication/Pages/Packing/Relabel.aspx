<%@ Page Language="C#" MasterPageFile="~/Pages/RI.Master" AutoEventWireup="true" CodeBehind="Relabel.aspx.cs" Inherits="IHF.ApplicationLayer.Web.Pages.Packing.Relabel" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
<div class="childdefault">
       
    <table>
    <br />
    <br />
    <tr><td style="text-align: right" class="style1">Scan Store Parcel to Relabel: </td>
        <td style="text-align:left; vertical-align: middle;" class="style2">
            <telerik:RadTextBox ID="RadTextBox2" Runat="server" 
                ontextchanged="RadTextBox1_TextChanged" style="margin-left: 0px" Width="286px">
            </telerik:RadTextBox>
            <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="OK" />
        </td>
        <td>
            &nbsp;</td></tr>
        <tr><td colspan="2"></td></tr>
        <tr><td class="style4"></td><td class="style3">
            <asp:Label ID="lblMsg"  Visible="False" runat="server" Text="Label"></asp:Label></td></tr>
    
    </table>
    </div>
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .style1
        {
            height: 22px;
            width: 326px;
        }
        .style2
        {
            height: 22px;
            width: 435px;
        }
        .style3
        {
            width: 435px;
        }
        .style4
        {
            width: 326px;
        }
    </style>
</asp:Content>

