<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="IHF.ApplicationLayer.Web.Error" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Error</title>

</head>
<body  style="background-color:Black;">
    <form id="form1" runat="server">
        <!-- BANNER TABLE ---------->
        <table width="100%" cellpadding="0" cellspacing="0" border="0">
	        <tr valign="top">
                <!-- LOGO ---------->
		        <td style ="background-color:Black;width:auto;">
		            <a href="Home.aspx">
                        <img src= "/Images/logo.gif" alt="River Island" style="border:0px" />
                    </a>
		        </td>
                <!-- END OF LOGO --->
                
                <!-- APPLICATION HEADING ---------->
		        <td style ="width:70%;background-color:Black;text-align:center;
		            vertical-align: middle;font-size:large;color:White;padding-top:6px;">
		            IHF - Automation
		        </td>
		        <!-- END OF APPLICATION HEADING ---------->

	        </tr>
        </table>


        <div  style ="background-color:Silver;height:300px;width:100%;">
            <div style="height:10%">
                <h2>An error occurred</h2>
            </div>

            <div style="height:80%;">
                <table width="70%">
                    <tr>
                        <td style="background-color:Gray;width:150px;font-family:Arial;">
                            Error in
                        </td>
                        <td id="errorIn" runat="server" style="background-color:ButtonFace;">
                        
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color:Gray;width:150px;font-family:Arial;">
                            Error occurred on
                        </td>
                        <td id="errorOn" runat="server"  style="background-color:ButtonFace;">
                        
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color:Gray;width:150px;font-family:Arial;">
                            Error message
                        </td>
                        <td id="errorMessage" runat="server" style="background-color:ButtonFace;">
                        
                        </td>
                    </tr>
                    
                </table>                
            </div>
            <div  style="height:10%">

                <a href="Home.aspx">Back to home page</a>
            </div>
        </div>
    </form>
</body>
</html>
