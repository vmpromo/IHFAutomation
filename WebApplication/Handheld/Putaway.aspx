<%@ Page Title="" Language="C#" MasterPageFile="~/Handheld/Handheld.Master" AutoEventWireup="true" CodeBehind="Putaway.aspx.cs" Inherits="IHF.ApplicationLayer.Web.Handheld.Putaway" %>
<%@ MasterType VirtualPath="~/Handheld/Handheld.Master" %>
<asp:Content ID="c" ContentPlaceHolderID="Content" runat="server">
    <asp:HiddenField ID="pageState" runat="server" />
    <asp:HiddenField ID="scannedLPN" runat="server" />
    <asp:HiddenField ID="scannedItemNumber" runat="server" />
    <asp:HiddenField ID="orderNumber" runat="server" />
    <asp:HiddenField ID="putawaySku" runat="server" />
</asp:Content>
