<%@ Page Title="Print Order" Language="C#" MasterPageFile="~/Pages/RI.Master" AutoEventWireup="true"
    CodeBehind="RePrint.aspx.cs" Inherits="IHF.ApplicationLayer.Web.Pages.Packing.RePrint" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Styles/api.pack.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/jquery-1813-custom.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery-1.6.1.min.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-1813-custom.js" type="text/javascript"></script>
    <script type="text/javascript">

        function IsRowSelected() {

            var retVal = true;

            var grid = $find("<%=grdOrder.ClientID%>");
            var MasterTable = grid.get_masterTableView();
            var selectedRows = MasterTable.get_selectedItems();

            if (selectedRows.length === 0) {

                retVal = false;

            }

            return retVal;
        }


        function PrintDialog() {

            $("#dialog:ui-dialog").dialog("destroy");

            $("#dialog-Print").dialog({
                resizable: false,
                height: 250,
                width: 400,
                modal: true,
                closeOnEscape: true,
                focus: function () {
                    $(this).parents('.ui-dialog-buttonpane button:eq(0)').focus();
                },
                buttons: {
                    "Print": function () {
                        var strDoc = "";

                        $(".chkprint").each(function () {

                            if (true === this.checked)
                                strDoc = strDoc + this.id.toString() + ",";
                        });

                        if (strDoc.length === 0)
                            alert("Please select item(s) to print.")
                        else {

                            $("#hdnOrder").val(strDoc);

                            __doPostBack('PrintOrder', '');

                            $(this).dialog("close");
                        }
                    }

                }
            });

        }

        $(document).ready(function () {

            $("#txtOrderNo").focus();

            $("form").submit(function () {

                var arg = $("#__EVENTTARGET").val();
                var q = $("#txtOrderNo").val();

                if ((arg === undefined || arg === "") && (q !== ""))
                    $("#__EVENTTARGET").val("SearchOrder");

            });

            $("#printDoc").click(function () {
                if (IsRowSelected())
                    PrintDialog();
                else
                    alert("Please select order to print.");

            });

        });


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dialog-Print" title="Print Documents" style="display: none; font-size: small;">
        <ul style="list-style-type: none">
            <li>
                <input type="checkbox" id="DP" class="chkprint" />
                Despatch Note </li>
            <li>
                <input type="checkbox" id="L" class="chkprint" />
                Labels </li>
            <li>
                <input type="checkbox" id="D" class="chkprint" />
                Customs Documents </li>
        </ul>

         <div style="font-size: x-small; font-style: italic">
            Esc to close the window</div>
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table width="100%" border="0">
        <tbody>
            <tr>
                <td align="center">
                    <table>
                        <tbody>
                            <tr style="height: 80px;" valign="top">
                                <td align="center">
                                    <div id="actionCtrls">
                                        <div>
                                            <asp:TextBox runat="server" ID="txtOrderNo" CssClass="oSearch" ClientIDMode="Static" />
                                        </div>
                                        <div id="Div1" style="float: none; padding-left: 20px; padding-top: 5px;" runat="server"
                                            onclick="javascript:__doPostBack('SearchOrder','')">
                                            <div class="actionButton">
                                                Search Order
                                            </div>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <telerik:RadGrid ID="grdOrder" runat="server" AllowPaging="true" ShowFooter="true"
                        ShowHeader="true" ShowStatusBar="true" AllowSorting="True" PageSize="15" GridLines="Both"
                        Width="100%" OnNeedDataSource="grdOrder_NeedDataSource">
                        <PagerStyle Mode="NextPrevNumericAndAdvanced"></PagerStyle>
                        <MasterTableView AutoGenerateColumns="false" CommandItemDisplay="None">
                            <Columns>
                                <telerik:GridClientSelectColumn UniqueName="CheckboxSelectColumn" />
                                <telerik:GridBoundColumn DataField="OrderNo" Display="true" HeaderText="Order No"  DataType="System.Int32"/>
                                <telerik:GridBoundColumn DataField="OrderStatus" Display="true" HeaderText="Order Status" />
                                <telerik:GridBoundColumn DataField="LastAction" Display="true" HeaderText="Last Action" DataType="System.DateTime" />
                                <telerik:GridBoundColumn DataField="User" Display="true" HeaderText="User" />
                                <telerik:GridBoundColumn DataField="Location" Display="true" HeaderText="Location" />
                                <telerik:GridBoundColumn DataField="OrderDate" Display="true" HeaderText="Order Date" DataType="System.DateTime" />
                                <telerik:GridBoundColumn DataField="ServiceGroup" Display="true" HeaderText="Service Group" />
                                <telerik:GridBoundColumn DataField="ServiceGroupName" Display="true" HeaderText="Service" />
                                <telerik:GridBoundColumn DataField="CollectionWindow" Display="true" HeaderText="Collection Window" />
                                <telerik:GridBoundColumn DataField="OrderItem" Display="true" HeaderText="OrderItem" DataType="System.Int32" />
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </td>
            </tr>
            <tr>
                <td>
                    <input type="hidden" runat="server" id="hdnOrder" clientidmode="Static" />
                    <div style="padding-top: 10px;">
                        <div id="printDoc" style="float: right; padding-left: 20px;">
                            <div class="actionButton">
                                Print Order
                            </div>
                        </div>
                    </div>
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
