<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SingleLocate.aspx.cs"  MasterPageFile="~/Handheld/Handheld.Master"  Inherits="IHF.ApplicationLayer.Web.SingleLocate" %>

<%@ MasterType VirtualPath="~/Handheld/Handheld.Master" %>


<asp:content id="c" contentplaceholderid="Content" runat="server">

     <asp:HiddenField ID="hdnScanMode" runat="server" Value="CS"/>
     <asp:HiddenField ID="hdnChuteId" runat="server" />
     <asp:HiddenField ID="hdnTrolleyId" runat="server" />

</asp:content>
