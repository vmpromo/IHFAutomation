<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/RI.Master" AutoEventWireup="true"
    CodeBehind="SkuLabel.aspx.cs" Inherits="IHF.ApplicationLayer.Web.Pages.Admin.Setup.SkuLabel" %>

<%--<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>--%>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">--%>
    <table width="100%">
        <tr>
            <td style="text-align:center;color:Black;padding:5px 5px 5px 5px;">SKU Barcode Label Printing</td>
        </tr>
    </table>
    <asp:RadioButton ID="RBBarcode" runat="server" GroupName="sku" AutoPostBack="True"
        OnCheckedChanged="RBBarcode_CheckedChanged" Font-Size="Small" 
        ForeColor="Black" Text="Scan SKU Barcode" />
    <br />
    <asp:TextBox ID="TBbarcode" runat="server" BackColor="White" Font-Size="Small"></asp:TextBox>
    <br />
    <br />
    <br />
    <asp:RadioButton ID="RBSku" runat="server" AutoPostBack="True" 
        Font-Size="Small" ForeColor="Black" GroupName="sku" 
        oncheckedchanged="RBSku_CheckedChanged" Text="Enter SKU number" />
    <br />
    <asp:TextBox ID="TBSku" runat="server" AutoPostBack="True" Font-Size="Small"></asp:TextBox>
    <br />
    <br />
    <br />
    <asp:Label ID="Lbdropdown" runat="server" Text="Select SKU" 
        Font-Size="Small" ForeColor="Black"></asp:Label>
    <br />
    <asp:DropDownList ID="DDSkunumber" runat="server" Height="30px" Width="175px" 
        AutoPostBack="True" Font-Size="Small" 
        onselectedindexchanged="DDSkunumber_SelectedIndexChanged">
    </asp:DropDownList>
    <br />
    <br />
    <asp:Label ID="LBresult" runat="server" Text="Label"></asp:Label>
    <br />

</asp:Content>
