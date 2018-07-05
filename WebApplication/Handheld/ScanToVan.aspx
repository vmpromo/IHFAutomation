<%@ Page Language="C#" MasterPageFile="~/Handheld/Handheld.Master" AutoEventWireup="true" CodeBehind="ScanToVan.aspx.cs" Inherits="IHF.ApplicationLayer.Web.Handheld.ScanToVan" %>
<%@ MasterType VirtualPath="~/Handheld/Handheld.Master" %>
<asp:Content 
    ID="ScanToVan" 
    ContentPlaceHolderID="Content" 
    runat="server">

    <input 
        id="step" 
        type="hidden" 
        runat="server" 
        value="Initial" />

</asp:Content>