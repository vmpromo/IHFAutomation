<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/RI.Master" AutoEventWireup="true" CodeBehind="SkuLabelForTest.aspx.cs" Inherits="IHF.ApplicationLayer.Web.Pages.Admin.Setup.SkuLabelForTest" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">--%>
    <table width="100%">
        <tr>
            <td style="text-align:center;color:Black;padding:5px 5px 5px 5px;"><strong>SKU Label Printing</strong></td>
        </tr>
    </table>
    <asp:RadioButton ID="RBLoad" runat="server" GroupName="sku" AutoPostBack="True"
        OnCheckedChanged="RBLoad_CheckedChanged" Font-Size="Small" 
        ForeColor="Black" Text="Enter Load Number" />
    <br />
    <asp:TextBox ID="TBLoad" runat="server" BackColor="White" Font-Size="Small"></asp:TextBox>
    <br />
    <br />
    <br />
    <asp:RadioButton ID="RBChute" runat="server" AutoPostBack="True" 
        Font-Size="Small" ForeColor="Black" GroupName="sku" 
        oncheckedchanged="RBChute_CheckedChanged" Text="Enter Chute number" />
    <br />
    <asp:TextBox ID="TBChute" runat="server" AutoPostBack="True" Font-Size="Small"></asp:TextBox>
    <br />
    <br />
    <br />
    <asp:RadioButton ID="RBTrolley" runat="server" AutoPostBack="True" 
        Font-Size="Small" ForeColor="Black" GroupName="sku" 
        oncheckedchanged="RBTrolley_CheckedChanged" Text="Enter Trolley ID" />
    <br />
    <asp:DropDownList ID="DD_trolley" runat="server" Height="21px" Width="129px">
    </asp:DropDownList>
    <br />
    <br />
    <br />
    <asp:Button ID="Button1" runat="server" Text="Print Labels" 
        onclick="Button1_Click" />
    <br />
    <br />
    <br />
    <asp:Label ID="LBresult" runat="server" Text="Label"></asp:Label>
    <br />

</asp:Content>