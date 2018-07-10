<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="IHF.ApplicationLayer.Web.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <meta http-equiv="KeyCapture" content="AccelerateKey:All;" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%" cellpadding="0" cellspacing="0" border="1">
	        <tr valign="top">
                <td width="100%" style ="background-color:Black;text-align:left;vertical-align: middle;font-size:large;color:White;padding-top:6px;">
		            IHF - Automation
		        </td>
		    </tr>
            <tr>
                <td>
                    <asp:Login ID="IHFLogin" runat="server" BackColor="#EFF3FB" BorderColor="#B5C7DE" 
                        BorderPadding="4" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" 
                        Font-Size="0.8em" ForeColor="#333333" oninit="IHFLogin_Init" 
                        onloggedin="IHFLogin_LoggedIn">
                        <InstructionTextStyle Font-Italic="True" ForeColor="Black" />
                        <LayoutTemplate>
                            <table cellpadding="4" cellspacing="0" style="border-collapse:collapse;">
                                <tr>
                                    <td align="left">
                                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" 
                                        ControlToValidate="UserName" ErrorMessage="User Name is required." 
                                        ToolTip="User Name is required." ValidationGroup="Login2" ForeColor="Red">*User Name is required.</asp:RequiredFieldValidator>
                                        <br />
                                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" 
                                        ControlToValidate="Password" ErrorMessage="Password is required." 
                                        ToolTip="Password is required." ValidationGroup="Login2" ForeColor="Red">*Password is required.</asp:RequiredFieldValidator>
                                        <br />
                                        <span style="color:Red;">
                                            <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                        </span>
                                        
                                        <table cellpadding="1">
                                            <tr>
                                                <td align="left" colspan="2" width="300px" 
                                                    style="color:White;background-color:white;font-size:0.9em;font-weight:bold;">
                                                        
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="2" width="300px" 
                                                    style="color:White;background-color:Black;font-size:0.9em;font-weight:bold;">
                                                    Log In</td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">User Name:</asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="UserName" runat="server" Font-Size="1.2em" Width="100px"></asp:TextBox>
                                                    
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="Password" runat="server" Font-Size="1.2em" TextMode="Password"  Width="100px"></asp:TextBox>
                                                    
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:CheckBox ID="RememberMe" runat="server" Text="Remember me next time." />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="2" style="color:Red;">
                                                    
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" colspan="2">
                                                    <asp:Button ID="LoginButton" runat="server" BackColor="#FFFBFF" 
                                                        BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CommandName="Login" 
                                                        Font-Names="Verdana" Font-Size="0.8em" ForeColor="#284775" Text="Log In" 
                                                        ValidationGroup="Login2" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </LayoutTemplate>
                        <LoginButtonStyle BackColor="White" BorderColor="#507CD1" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em" 
                            ForeColor="#284E98" />
                        <TextBoxStyle Font-Size="0.8em" />
                        <TitleTextStyle BackColor="#507CD1" Font-Bold="True" Font-Size="0.9em" 
                            ForeColor="White" />
                    </asp:Login>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
