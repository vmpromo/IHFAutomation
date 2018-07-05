<%@ Page Title="" Language="C#" MasterPageFile="~/Handheld/Handheld.Master" AutoEventWireup="true" CodeBehind="ManualCage.aspx.cs" Inherits="IHF.ApplicationLayer.Web.Handheld.ManualCage" %>
<%@ MasterType VirtualPath="~/Handheld/Handheld.Master" %>
<asp:Content 
    ID="ManualCage" 
    ContentPlaceHolderID="Content" 
    runat="server">

    <input 
        id="step" 
        type="hidden" 
        runat="server" 
        value="Initial" />

</asp:Content>
