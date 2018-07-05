<%@ Page Title="" Language="C#" MasterPageFile="~/Handheld/Handheld.Master" AutoEventWireup="true" CodeBehind="RemoveForPackClearup.aspx.cs" Inherits="IHF.ApplicationLayer.Web.Handheld.RemoveForPackClearup" %>
<%@ MasterType VirtualPath="~/Handheld/Handheld.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">

    <input 
        id="step" 
        type="hidden" 
        runat="server" 
        value="Initial" />

     <input 
        id="chuteid" 
        type="hidden" 
        runat="server" 
        value="" />

      <input 
        id="ordernumber" 
        type="hidden" 
        runat="server" 
        value="" />

    <input 
        id="toteservice" 
        type="hidden" 
        runat="server" 
        value="" />

    <input 
        id="nextlocbarcode" 
        type="hidden" 
        runat="server" 
        value="" />

    <input 
        id="nextloclabel" 
        type="hidden" 
        runat="server" 
        value="" />

    <input 
        id="failedtotelabel" 
        type="hidden" 
        runat="server" 
        value="" />

    <input 
        id="failedtotebarcode" 
        type="hidden" 
        runat="server" 
        value="" />

    <input 
        id="totetypename" 
        type="hidden" 
        runat="server" 
        value="" />

      <input 
        id="trolleyid" 
        type="hidden" 
        runat="server" 
        value="" />

</asp:Content>
