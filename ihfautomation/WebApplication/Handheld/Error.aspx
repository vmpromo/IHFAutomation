<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="IHF.ApplicationLayer.Web.Handheld.Error" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="KeyCapture" content="AccelerateKey:All;" />
    <link type="text/css" rel="Stylesheet" href="../Styles/Handheld.css" />
</head>
<body>
    <form id="form1" runat="server">
        
        <div class="page">
            <div 
                    id="Header" 
                    class="header">
                    <table>
                        <tr>
                            <td style="width:50%">
                                River Island
                            </td>
                            <td style="width:50%;text-align:center;">
                                IHF Automation
                            </td>
                        </tr>
                    </table>
                    
                </div>
                <div id="errorMessage" class="message" runat="server">
                </div>
                <div class="footer">
                    Press backspace to go back to previous screen.
                </div>
        
        </div>

    </form>
</body>
</html>
