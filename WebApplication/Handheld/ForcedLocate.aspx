<%@ Page Title="" Language="C#" MasterPageFile="~/Handheld/Handheld.Master" AutoEventWireup="true" CodeBehind="ForcedLocate.aspx.cs" Inherits="IHF.ApplicationLayer.Web.Handheld.ForcedLocate" %>
<%@ MasterType VirtualPath="~/Handheld/Handheld.Master" %>
<asp:Content 
    ID="ForcedLocate" 
    ContentPlaceHolderID="Content" 
    runat="server">

    <input 
        id="step" 
        type="hidden" 
        runat="server" 
        value="Initial" />

</asp:Content>
