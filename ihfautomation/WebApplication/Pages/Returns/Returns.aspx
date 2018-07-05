<%@ Page Language="C#"  MasterPageFile="~/Pages/RI.Master" AutoEventWireup="true" CodeBehind="Returns.aspx.cs" Inherits="IHF.ApplicationLayer.Web.Pages.Returns.Returns" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script src="/Scripts/jquery-1.6.1.min.js" type="text/javascript"></script>
<script type="text/javascript">
    window.onbeforeunload = function () { return "Select OK or Cancel"; } 
</script>
    <telerik:RadScriptManager ID="RadScriptManager1" Runat="server">
    </telerik:RadScriptManager>
    <script language='javascript' type='text/javascript'>
                        var prm = Sys.WebForms.PageRequestManager.getInstance();
                        prm.add_pageLoaded(pageLoadedFunc); 
                        function pageLoadedFunc(sender, eventArgs) {
                            var ddl = document.getElementById('ddlAct'); if (ddl != null) {ddl.focus()};
                        }
                    </script>
       <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" Runat="server" 
        IsSticky="false" Skin="Default">
        </telerik:RadAjaxLoadingPanel>
    <div id="scanpage"  onclick ="javascript: handleMouseEvent();">
       <telerik:RadAjaxPanel ID="RadAjaxPanel1"  runat="server"
        LoadingPanelID="RadAjaxLoadingPanel1" Width="990px" 
        onload="RadAjaxPanel1_Load" RenderMode="Inline">

 
    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" 
        ontabclick="RadTabStrip1_TabClick" MultiPageID="RadMultiPage1" 
        oninit="RadTabStrip1_Init" SelectedIndex="0">
        <Tabs>
            <telerik:RadTab TabIndex="0" runat="server" PageViewID="RadPageView1" 
                Text="Return Items" Selected="True">
            </telerik:RadTab>
            <telerik:RadTab TabIndex ="1" runat="server" PageViewID="RadPageView2"
                Text="Search Customer">
            </telerik:RadTab>
        </Tabs>

    </telerik:RadTabStrip>

    <telerik:RadMultiPage ID="RadMultiPage1" Runat="server" SelectedIndex="0">
        <telerik:RadPageView ID="RadPageView1" runat="server" 
            onprerender="RadPageView1_PreRender">
    <br />
    <table border="0"><tr><td>
        <asp:Label ID="lblScan" runat="server" Text="Order Scan: "></asp:Label>
        </td><td class="style10">
        <asp:TextBox ID="tbPackageBarcode"  Runat="server" 
        ontextchanged="rtbPackageBarcode_TextChanged"  Width="200px" 
                AutoPostBack="True" TabIndex="1"></asp:TextBox>


        </td><td class="style10">

            <asp:Button ID="Button1" runat="server" Text="Enter" onclick="Button1_Click" 
                TabIndex="1" />

            <input id="Text1" type="text" style="visibility:hidden"/></td><td class="style10">

                &nbsp;</td><td class="style10">

            <asp:Label ID="lblCustomerUrn" runat="server" Text="CustomerUrn" 
                Visible="False"></asp:Label>
        </td><td class="style10">

                <asp:Button ID="btnAccept" runat="server" onclick="btnAccept_Click" 
                    Text="Accept" Visible="False" />
        </td>
        <td class="style16">
            <asp:Button ID="Button4" runat="server" onclientclick="window.print()" 
                Text="Print" />
        </td>
        </tr>
    <tr><td></td><td class="style10" colspan="2">
        <asp:Label ID="lblMessage" runat="server" ForeColor="#FF3300"></asp:Label>
        </td><td class="style10">&nbsp;</td><td class="style10">
        <asp:Label ID="Label1" runat="server" Text="lblCancelledCount" Visible="False"></asp:Label>
        </td>
        <td class="style10">
            <asp:Button ID="btnReject" runat="server" onclick="btnReject_Click" 
                Text="Reject" Visible="False" width="61px" />
        </td>
        <td class="style16">
            &nbsp;</td></tr>
        <tr>
            <td colspan="7">
                <asp:Label ID="lblItemsReturned" runat="server" Font-Bold="True" 
                    ForeColor="#0033CC" Visible="False"></asp:Label>
            </td>
        </tr>
    </table>
    <asp:Panel ID="pnlOrder" runat="server">
    <table frame="border" style="width:100%;" border="0">
        <tr>
            <td class="style4">
                Order Number:</td>
            <td class="style2">
                <asp:Label ID="lblOrdernumber" runat="server" Text="77777777777"></asp:Label>
            </td>
            <td class="style11">
                Order Date:</td>
            <td class="style12">
                <asp:Label ID="lblOrderDate" runat="server" Text="03 Sep 2012"></asp:Label>
            </td>
            <td class="style1">
                Total Items:</td>
            <td class="style13">
                <asp:Label ID="lblTotalItems" runat="server" Text="5"></asp:Label>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style4">
                Customer:</td>
            <td class="style2" colspan="1">
                <asp:Label ID="lblCustomerName" runat="server" style="text-align: left"></asp:Label>
            </td>
             <td class="style11">
                 Post Code:</td>
            <td class="style12">
                <asp:Label ID="lblPostCode" runat="server" Text="W5 1DT"></asp:Label>
            </td>

            <td class="style1">
                Items to Process:</td>
            <td class="style13">
                <asp:Label ID="lblItemsToProcess" runat="server" Text="5"></asp:Label>
            </td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <table>
    </table>
    <br />
	<asp:GridView ID="gvItems" runat="server" AutoGenerateColumns="False" 
        BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" 
        CellPadding="4" ForeColor="Black" GridLines="Horizontal"
        Width="100%" Max-Width="923px"
        onrowcancelingedit="gvItems_RowCancelingEdit" 
        onrowdatabound="gvItems_RowDataBound" onrowediting="gvItems_RowEditing" 
        onrowupdated="gvItems_RowUpdated" 
        ondatabinding="gvItems_DataBinding" 
        onrowcommand="gvItems_RowCommand" onrowupdating="gvItems_RowUpdating" 
        ondatabound="gvItems_DataBound">
        <Columns>
            <asp:TemplateField>
                <EditItemTemplate>
                    <asp:CheckBox ID="cbEditing" runat="server" AutoPostBack="True" 
                        oncheckedchanged="cbEditing_CheckedChanged" />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="cbEdited" runat="server" 
                        oncheckedchanged="cbEdited_CheckedChanged" AutoPostBack="True" 
                        TabIndex="2" /><asp:Hiddenfield runat="server" id="hidRowIndex" value='<%#Container.DataItemIndex %>' />
                </ItemTemplate>
                <ItemStyle Width="20px" />
            </asp:TemplateField>
            <asp:BoundField DataField="sku" HeaderText="SKU" ReadOnly="True" >
            <HeaderStyle HorizontalAlign="Left" />
            <ItemStyle Width="80px" />
            </asp:BoundField>
            
            <asp:ButtonField ButtonType="Image" CommandName="Edit" 
                ImageUrl="~/Images/Edit.GIF" Text="Edit" >
            
            <ItemStyle Width="20px" />
            </asp:ButtonField>
            
            <asp:BoundField DataField="skudescr" HeaderText="Description" ReadOnly="True">
            <HeaderStyle HorizontalAlign="Left" />
            <ItemStyle Width="450px" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="">
                <EditItemTemplate>
                    <asp:DropDownList ID="ddlAct" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlAct_SelectedIndexChanged" ClientIDMode="Static" 
                        TabIndex="3">
                    </asp:DropDownList>
                    <br />
                    <asp:TextBox ID="tbTaskDesc" runat="server" Height="75px" TextMode="MultiLine" 
                        Visible="False" Width="390px"></asp:TextBox>
                    <asp:Button ID="btnFinish" runat="server" onclick="btnFinish_Click" 
                        Text="Done" Visible="False" ClientIDMode="Static"/>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lbAction" runat="server"></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            
            <asp:BoundField DataField="action" HeaderText="actioncode" Visible="False" />
            
        </Columns>
        <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
        <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
        <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F7F7F7" />
        <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
        <SortedDescendingCellStyle BackColor="#E5E5E5" />
        <SortedDescendingHeaderStyle BackColor="#242121" />
    </asp:GridView>

    </asp:Panel>
	<script type="text/javascript">
	    //Put your JavaScript code here.
    </script>
        </telerik:RadPageView>
        <telerik:RadPageView ID="RadPageView2" runat="server">
  <table frame="border" border="0"><tr><td>
      &nbsp;</td>
      <td class="style3">
          <asp:Label ID="Label2" runat="server" Font-Bold="True" 
              style="top: 137px; left: 275px; height: 14px; width: 100px" 
              Text="Search Criteria"></asp:Label>
      </td>
      <td>&nbsp;</td>
      <td>
          &nbsp;</td>
      <td>
      &nbsp;</td>
      <td>
          &nbsp;</td>
      <td>
          &nbsp;</td><td>&nbsp;</td>
      <td>
          &nbsp;</td>
      <td>
      &nbsp;</td><td>&nbsp;</td></tr>
      <tr>
          <td>
              Last Name:</td>
          <td class="style3">
              <telerik:RadTextBox ID="rtbLastName" Runat="server" Skin="Sitefinity" 
                  width="150px">
              </telerik:RadTextBox>
          </td>
          <td>
          </td>
          <td>
              First Name:</td>
          <td>
              <telerik:RadTextBox ID="rtbFirstName" Runat="server" Skin="Sitefinity"  
                  Width="150px">
              </telerik:RadTextBox>
          </td>
          <td>
              Address:</td>
          <td>
              <telerik:RadTextBox ID="rtbAddress" Runat="server" Skin="Sitefinity" 
                   Width="200px">
              </telerik:RadTextBox>
          </td>
          <td>
          </td>
          <td>
              &nbsp;</td>
          <td>
              &nbsp;</td>
          <td>
          </td>
      </tr>
      <tr>
          <td class="style6">
              </td>
          <td class="style5">
          </td>
          <td class="style6">
              </td>
          <td class="style6">
          </td>
          <td class="style6">
              </td>
          <td class="style6">
          </td>
          <td class="style6">
              </td>
          <td class="style6">
              </td>
          <td class="style6">
          </td>
          <td class="style6">
              </td>
          <td class="style6">
              </td>
      </tr>
      <tr>
          <td>
              Telephone:</td>
          <td class="style3">
              <telerik:RadTextBox ID="rtbTelephone" Runat="server" Skin="Sitefinity" 
                  Width="150px">
              </telerik:RadTextBox>
          </td>
          <td>
              &nbsp;</td>
          <td>
              Email:</td>
          <td>
              <telerik:RadTextBox ID="rtbEmail" Runat="server" width="150px"
                   Skin="Sitefinity">
              </telerik:RadTextBox>
          </td>
          <td>
              Post Code:</td>
          <td>
              <telerik:RadTextBox ID="rtbPostCode" Runat="server" Skin="Sitefinity" 
                  Width="100px">
              </telerik:RadTextBox>
          </td>
          <td>
              &nbsp;</td>
          <td>
              &nbsp;</td>
          <td>
              &nbsp;</td>
          <td>
              &nbsp;</td>
      </tr>
      <tr>
          <td>
              &nbsp;</td>
          <td class="style3">
              <asp:Button ID="Button2" runat="server" onclick="rbSearch_Click" 
                  Text="Search" />
              <asp:Button ID="Button3" runat="server" onclick="Button3_Click" Text="Clear" />
          </td>
          <td>
              &nbsp;</td>
          <td>
              &nbsp;</td>
          <td>
              &nbsp;</td>
          <td>
              &nbsp;</td>
          <td>
              &nbsp;</td>
          <td>
              &nbsp;</td>
          <td>
              &nbsp;</td>
          <td>
              &nbsp;</td>
          <td>
              &nbsp;</td>
      </tr>
     </table> 
<telerik:RadStyleSheetManager id="RadStyleSheetManager1" runat="server" />
	<telerik:RadGrid ID="rgCustomers" runat="server" 
        AllowPaging="True" AllowSorting="True" CellSpacing="0" GridLines="None" 
        onneeddatasource="rgCustomers_NeedDataSource" 
        ondetailtabledatabind="rgCustomers_DetailTableDataBind" 
        AutoGenerateColumns="False" Skin="Sitefinity" 
                onitemcommand="rgCustomers_ItemCommand">
        <ClientSettings>
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
<MasterTableView DataKeyNames="customerurn" 
            Name="customers">
    <DetailTables>
        <telerik:GridTableView runat="server" Name="orders" 
            DataKeyNames="ordernumber" Width="100%">
            <DetailTables>
                <telerik:GridTableView runat="server" Name="items" Width="100%">
                    <CommandItemSettings ExportToPdfText="Export to PDF" />
                    <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
                        <HeaderStyle Width="20px" />
                    </RowIndicatorColumn>
                    <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
                        <HeaderStyle Width="20px" />
                    </ExpandCollapseColumn>
                    <Columns>
                        <telerik:GridBoundColumn DataField="itemnumber" 
                            FilterControlAltText="Filter column column" HeaderText="Item Number" 
                            UniqueName="column">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="sku" 
                            FilterControlAltText="Filter column1 column" HeaderText="SKU" 
                            UniqueName="column1">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="statusdescr" 
                            FilterControlAltText="Filter column2 column" HeaderText="Item Status" 
                            UniqueName="column2">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="skudescr" 
                            FilterControlAltText="Filter column2 column" HeaderText="SKU Description" 
                            UniqueName="skudesccolumn">
                        </telerik:GridBoundColumn>

                    </Columns>
                    <EditFormSettings>
                        <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                        </EditColumn>
                    </EditFormSettings>
                </telerik:GridTableView>
            </DetailTables>
            <CommandItemSettings ExportToPdfText="Export to PDF" />
            <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" HeaderText='Order Number'>
                <HeaderStyle Width="20px" />
            </RowIndicatorColumn>
            <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" 
                Visible="True">
                <HeaderStyle Width="20px" />
            </ExpandCollapseColumn>
            <Columns>
                <telerik:GridTemplateColumn FilterControlAltText="Filter TemplateColumn column" 
                    UniqueName="TemplateColumn">
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1"  AutoPostBack="true"   runat="server" 
                            oncheckedchanged="CheckBox1_CheckedChanged" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="ordernumber" 
                    FilterControlAltText="Filter column column" HeaderText="Order Number" 
                    UniqueName="column">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="orderdate" DataType="System.DateTime" 
                    EditFormHeaderTextFormat="{dd-MM-yyyy}" DataFormatString="{0:dd-MMM-yyyy}"
                    FilterControlAltText="Filter column1 column" HeaderText="Order Date" 
                    UniqueName="column1">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="totalitemsinorder" 
                    FilterControlAltText="Filter column2 column" HeaderText="Total Items" 
                    UniqueName="column2">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ordergrossvalue" DataType="System.Decimal" 
                    FilterControlAltText="Filter column3 column" HeaderText="Order Gross Value" 
                    UniqueName="column3">
                </telerik:GridBoundColumn>
            </Columns>
            <EditFormSettings>
                <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                </EditColumn>
            </EditFormSettings>
        </telerik:GridTableView>
    </DetailTables>
<CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>

<RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
<HeaderStyle Width="20px"></HeaderStyle>
</RowIndicatorColumn>

<ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" 
        Visible="True">
<HeaderStyle Width="20px"></HeaderStyle>
</ExpandCollapseColumn>

    <Columns>
        <telerik:GridBoundColumn DataField="customerurn" 
            FilterControlAltText="customerURNFilter" HeaderText="Customer URN" 
            UniqueName="columnCustomerURN">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="customername" 
            FilterControlAltText="NameFilter" HeaderText="Name" 
            UniqueName="columnName">
        </telerik:GridBoundColumn>
         <telerik:GridBoundColumn DataField="address" 
            FilterControlAltText="Filter column3 column" HeaderText="Address" 
            UniqueName="columnAddress">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="postcode" 
            FilterControlAltText="Filter column4 column" HeaderText="Post Code" 
            UniqueName="columnPostcode">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="emailaddress" 
            FilterControlAltText="Filter column5 column" HeaderText="Email Address" 
            UniqueName="columnEmailaddress">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="telephone" 
            FilterControlAltText="Filter column6 column" HeaderText="Telephone" 
            UniqueName="columnTelephone">
        </telerik:GridBoundColumn>
    </Columns>

<EditFormSettings>
<EditColumn FilterControlAltText="Filter EditCommandColumn column"></EditColumn>
</EditFormSettings>
</MasterTableView>

<FilterMenu EnableImageSprites="False"></FilterMenu>

<HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Default"></HeaderContextMenu>
    </telerik:RadGrid>
	<script type="text/javascript">
	    //Put your JavaScript code here.
    </script>


        </telerik:RadPageView>
    </telerik:RadMultiPage>
    	 </telerik:RadAjaxPanel>
             </div>

  <br />
  </asp:Content>
<asp:Content ID="Content3" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .style1
        {
            text-align: right;
        }
        .style2
        {
            text-align: left;
            width: 176px;
        }
        .style3
        {
            text-align: left;
            width: 173px;
        }
        .style4
        {
            text-align: left;
            }
        .style11
        {
            text-align: right;
            width: 92px;
        }
        .style12
        {
            text-align: left;
            width: 120px;
        }
    .style13
    {
        width: 5px;
    }
    .style16
    {
        width: 228px;
        text-align: right;
    }
    </style>
</asp:Content>
