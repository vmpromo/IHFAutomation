<%@ Page Language="C#"  MasterPageFile="~/Pages/RI.Master" AutoEventWireup="true" CodeBehind="ReturnsDC.aspx.cs" Inherits="IHF.ApplicationLayer.Web.Pages.Returns.ReturnsDC" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<link href="/Styles/Returns.css" rel="stylesheet" type="text/css" />
<link href="/Styles/jquery-1813-custom.css" rel="stylesheet" type="text/css" />
<script src="/Scripts/jquery-1.6.1.min.js" type="text/javascript"></script>
<script src="/Scripts/jquery-1813-custom.js" type="text/javascript"></script>
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
    <div id="scanpage"  > <!-- onclick ="javascript: handleMouseEvent();" -->
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
    <!--  INSERT HERE -->

    <asp:HiddenField ID="tbSearchByCustomerOrderNumber"  Runat="server"></asp:HiddenField >

    <div ng-app="IHFReturns" ng-controller="ReturnsController" id="topDiv">
	
    		
    <div authorise-dialog=""></div>
    
    <table border="0">
    <tbody>
    <tr>
        <td class="searchLabel">
            <span>{{getSearchboxLabel()}}</span>
        </td>
        <td class="style10">
            <input
                type="text"
                tabindex="1"
                style="width:200px;"
                id="scanText"
                ng-model="searchKey"
                ng-disabled="disableEnterSearchAndReject()"
            />
        </td>
        <td class="enter-button">
            <input
                type="button"
                value="Enter"
                tabindex="1"
                id="btnEnter"
                ng-click="enterPressed(false)"
                ng-disabled="disableEnterSearchAndReject()"
            />
            <img
                src="/Images/progress.gif"
                ng-show="searchInProgress"
                
            />
        </td>

        <td class="error-message">
            <span ng-disabled="!errorMessage">
                {{errorMessage}}
            </span>
        </td>
        
        <td class="style11">
            <input
                type="button"
                value="Reject"
                style="width:61px;"
                ng-click="rejectClicked()"
                ng-show="showRejectButton()"
                ng-disabled="disableEnterSearchAndReject()"
            />
        </td>

        <td class="print-button">
            <input
                type="button" 
                value="Print" 
                ng-click="printClicked()"
            />

        </td>
    </tr>
    </tbody></table>
    <div>
    <br />	
    <table frame="border" style="width:100%;" border="0" ng-if="!!orderData">
        <tbody><tr>
            <td class="style4">
                Order Number:</td>
            <td class="style2">
                <span>{{orderData.OrderNumber}}</span>
            </td>
            <td class="style11">
                Order Date:</td>
            <td class="style12">
                <span>{{orderData.OrderDate}}</span>
            </td>
            <td class="style1">
                Total Items:</td>
            <td class="style13">
                <span>{{orderData.Items.length}}</span>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style4">
                Customer:</td>
            <td class="style2" colspan="1">
                <span style="text-align: left">{{orderData.CustomerName}}</span>
            </td>
             <td class="style11">
                 Post Code:</td>
            <td class="style12">
                <span>{{orderData.PostCode}}</span>
            </td>

            <td class="style1">
                Items to Process:</td>
            <td class="style13">
                <span>{{getItemsToProcess()}}</span>
            </td>
            <td>
                &nbsp;</td>
        </tr>
    </tbody></table>
    <table>
    </table>
    <br>
	<div>
	    <table cellspacing="0" cellpadding="4" rules="rows"
               max-width="923px" style="color:Black;background-color:White;border-color:#CCCCCC;border-width:1px;border-style:None;width:100%;border-collapse:collapse;"
               ng-if="orderData">
			<thead>
                <tr style="color:White;background-color:#333333;font-weight:bold;">
					<th scope="col" class="hidden">&nbsp;</th>
                    <th align="left" scope="col" style="padding-left: 10px">SKU</th>
                    <th scope="col">&nbsp;</th>
                    <th align="left" scope="col">Description</th>
                    <th align="left" scope="col">Reason Code</th>
                    <th align="left" scope="col">LPN</th>
                    <th align="left" scope="col">Putaway?</th>
                    <th align="left" scope="col">&nbsp;</th>
				</tr>
            </thead>
            <tbody ng-repeat="item in orderData.Items">
                <tr ng-class="getRowCssClass(item)">
                    <%--style="background-color:Beige;"--%>

				    <td style="width:20px;" class="hidden">
                        <input
                            type="checkbox"
                            id="checkbox-{{item.ItemNumber}}"
                            ng-model="item.checked"
                            ng-click="checkboxClicked(item)"
                            ng-disabled="isCheckboxDisabled(item)"
                            tabindex="2"/>
                    </td>
                    <td style="width:50px; padding-left: 10px">{{item.Sku}}</td>
                    <td style="width:20px;"></td>
                    <td style="width:360px;">{{item.Description}}</td>
                    <td style="width:180px">
                        <span
                            id="reasonLabel-{{item.ItemNumber}}"
                            ng-show="!item.checked">
                            {{item.ReasonDescription}}
                        </span>
                        <select
                            tabindex="3"
                            id="reasonDropdown-{{item.ItemNumber}}"
                            ng-change="reasonSelected(item)"
                            ng-model="item.newAction"
                            ng-options="v as v.Description for v in getApplicableReasonCodes(item)"
                            ng-show="item.checked"
                            ng-disabled="item.acceptInProgress"
                            ng-keypress="keypressedInReasonDropdown($event)"
                        >
						</select>
                        <textarea
                            id="customerServiceMsg-{{item.ItemNumber}}"
                            rows="2"
                            cols="20"
                            style="height:75px;width:390px;"
                            ng-show="isCustomerServiceReturn(item)"
                            ng-model="item.customerServiceMessage"
                            ng-disabled="item.acceptInProgress"
                        >
                        </textarea>

                    </td>
                    <td style="width:100px;" id="cellLpn-{{item.ItemNumber}}">{{item.LPN}}</td>
                    <td>
                        <span id="putawayCheck-{{item.ItemNumber}}">{{putawayCheckText(item)}}</span>
                     </td>

                    <td>
                        <input
                            type="button"
                            id="btn-{{item.ItemNumber}}"
                            ng-click="acceptOrReprintClicked(item)"
                            ng-disabled="disableAccept(item)"
                            ng-keypress="keyPressedInAcceptButton($event)"
                            value={{acceptReprint(item)}} />

                         <img
                            src="/Images/progress.gif"
                            ng-show="showRowRoller(item)"
                         />
                    </td>
				</tr>
			</tbody>
         </table>
	</div>
        </div>

        
		</div>
        <script src="/Scripts/underscore-min.js"></script>
        <script src="/Scripts/angular-1.2.32.min.js"></script>
        <script src="/Scripts/virtualConsole.js"></script>
        <script src="/Scripts/returnsConstants.js"></script>
        <script src="/Scripts/returnsServiceWrapper.js"></script>
        <script src="/Scripts/AuthoriseDialog.js"></script>
        <script src="/Scripts/returns.js"></script>
    
    <!-- END OF INSERT HERE -->
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
    .enter-button {
        width: 92px;
    }

    .error-message 
    {
        width: 390px;
        color: Red;
    }

    .print-button {
        width: 45px;
        text-align: right;
    }
    
    .searchLabel 
    {
        width: 135px;
    }
    </style>
</asp:Content>
