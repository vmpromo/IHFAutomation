<%@ Page Language="C#" MasterPageFile="~/Pages/RI.Master" AutoEventWireup="true" CodeBehind="testservice.aspx.cs" Inherits="IHF.ApplicationLayer.Web.Pages.testservice" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <table width="100%">
            <tr>
                <td style="text-align: center; color: Black; padding: 5px 5px 5px 5px;">
                    Test Printing
                </td>
            </tr>
        </table>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <br />
        <br />
        <asp:RadioButton ID="RB_Workstation" runat="server" Font-Bold="True" GroupName="print"
            Text="Test all the Printers on the workstation. " AutoPostBack="True"  OnCheckedChanged="RB_wk_CheckedChanged"/>
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <br />
        <asp:RadioButton ID="RB_label" runat="server" AutoPostBack="True" Font-Bold="True"
            GroupName="print" Text="Test Equipment Label Printing. " OnCheckedChanged="RB_label_CheckedChanged"/>
        <br />
        <asp:DropDownList ID="DD_reporttyp" runat="server" style="margin-left: 19px">
        </asp:DropDownList>
&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="TB_id" runat="server" Width="125px"></asp:TextBox>
        <br />
        <asp:RadioButton ID="RB_DN" runat="server" AutoPostBack="True" Font-Bold="True" GroupName="print"
            Text="Test Despatch Note Printing. "  OnCheckedChanged="RB_DN_CheckedChanged"/>
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="LB_DNO" runat="server" Text="OrderNumber"></asp:Label>
        <asp:TextBox ID="TB_DN" runat="server" Style="margin-left: 16px" Width="125px"></asp:TextBox>
        <br />
        <asp:RadioButton ID="RB_PL" runat="server" AutoPostBack="True" Font-Bold="True" GroupName="print"
            Text="Test Parcel Label Printing. "  OnCheckedChanged="RB_PL_CheckedChanged"/>
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="LB_PLO" runat="server" Text="OrderNumber"></asp:Label>
        <asp:TextBox ID="TB_PL" runat="server" Style="margin-left: 15px"></asp:TextBox>
        <br />
        <asp:RadioButton ID="RB_CD" runat="server" AutoPostBack="True" Font-Bold="True" GroupName="print"
            Text="Test Pack Custom Documents Printing. "  OnCheckedChanged="RB_CD_CheckedChanged"/>
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="LB_CDO" runat="server" Text="OrderNumber"></asp:Label>
        <asp:TextBox ID="TB_CD" runat="server" Style="margin-left: 13px"></asp:TextBox>
        <br />
        <asp:RadioButton ID="RB_FT" runat="server" AutoPostBack="True" Font-Bold="True" GroupName="print"
            Text="Test Failed Tote Report Printing. " 
            OnCheckedChanged="RB_FT_CheckedChanged" />
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="LB_FTO" runat="server" Text="OrderNumber"></asp:Label>
        <asp:TextBox ID="TB_FT" runat="server" Style="margin-left: 12px"></asp:TextBox>
        <br />
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Style="margin-left: 10px"
            Text="Print" Width="100px" />
        <br />
        <br />
        <br />
        &nbsp;&nbsp;
        <asp:Label ID="Label8" runat="server"></asp:Label>
        <br />
        &nbsp;&nbsp;
        <br />
        &nbsp;&nbsp;
        <asp:Label ID="Label7" runat="server" Font-Bold="False"></asp:Label>
        <br />
        <br />
        <br />
    </div>
    
</asp:Content>
