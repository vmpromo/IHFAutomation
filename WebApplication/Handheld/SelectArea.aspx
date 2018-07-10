<%@ Page Language="C#" MasterPageFile="~/Handheld/Handheld.Master" AutoEventWireup="true"
    CodeBehind="SelectArea.aspx.cs" Inherits="IHF.ApplicationLayer.Web.Handheld.SelectArea" %>

<%@ MasterType VirtualPath="~/Handheld/Handheld.Master" %>


<asp:Content ID="c" ContentPlaceHolderID="Content" runat="server">

     <asp:HiddenField ID="hdnScanMode" runat="server" Value="IS"/>
    <asp:HiddenField ID="hdnAreaId" runat="server" />
    <asp:HiddenField ID="hdnLoadNo" runat="server" />
     <asp:HiddenField ID="hdnOrderNo" runat="server" />
    <asp:HiddenField ID="hdnItemNo" runat="server" />
    <asp:HiddenField ID="hdnChuteId" runat="server" />

</asp:Content>
