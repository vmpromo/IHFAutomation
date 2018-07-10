<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Testpage.aspx.cs" Inherits="IHF.ApplicationLayer.Web.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="OrderNumber" runat="server"></asp:TextBox>
        <asp:TextBox ID="Description" runat="server"></asp:TextBox>
        <asp:Button ID="GetClassObject" runat="server" onclick="GetClassObject_Click" 
            Text="Get class object" />
        <br />
        <br />
        <asp:Button ID="NormalQuery" runat="server" onclick="NormalQuery_Click" 
            Text="Use normal query" />
        <br />
        <asp:GridView ID="GridView1" runat="server">
        </asp:GridView>
        <br />
        <br />
        <asp:TextBox ID="NewOrderNumber" runat="server"></asp:TextBox>
        <asp:TextBox ID="NewDescription" runat="server"></asp:TextBox>
        <asp:Button ID="AddOrder" runat="server" onclick="AddOrder_Click" 
            Text="Add new order" />
        <br />
        <br />
        <asp:Button ID="ListOfClasses" runat="server" 
            Text="GetListOfOrders" onclick="ListOfClasses_Click" />
        <asp:GridView ID="GridView2" runat="server">
        </asp:GridView>
        <br />
        <asp:Button ID="CreateWorkstation" runat="server" 
            onclick="CreateWorkstation_Click" Text="CreateWorkstation" />
        <br />
    </div>
    </form>
</body>
</html>
