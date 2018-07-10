<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="PackingMock.Test" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form runat="server">
    Print Status : <asp:Label runat="server" ID="lblPrintStatus" />
    <br />
    <br />
    <br />
    <asp:Button runat="server" Text="Printing Test" ID="btnPrintTest" 
        onclick="btnPrintTest_Click" />
    </form>
</body>
</html>
