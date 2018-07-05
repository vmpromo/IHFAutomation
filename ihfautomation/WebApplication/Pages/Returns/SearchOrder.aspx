
<%@ Page Language="C#" MasterPageFile="~/Pages/RI.Master" AutoEventWireup="true" CodeBehind="SearchOrder.aspx.cs" Inherits="IHF.ApplicationLayer.Web.Pages.Returns.SearchOrder" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" Runat="server">
    </telerik:RadScriptManager>
       	<telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" Runat="server" 
        IsSticky="false" Skin="Default">
        </telerik:RadAjaxLoadingPanel>
        <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" 
        LoadingPanelID="RadAjaxLoadingPanel1" Width="990px">
  <table frame="border"><tr><td>
      &nbsp;</td>
      <td class="style3">
          <asp:Label ID="Label1" runat="server" Font-Bold="True" 
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
              <telerik:RadButton ID="rbSearch" runat="server" onclick="rbSearch_Click" 
                  Skin="Sitefinity" Text="Search">
              </telerik:RadButton>
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
        AutoGenerateColumns="False" Skin="Sitefinity">
<MasterTableView DataKeyNames="customerurn" 
            Name="customers">
    <DetailTables>
        <telerik:GridTableView runat="server" Name="orders" 
            DataKeyNames="ordernumber">
            <DetailTables>
                <telerik:GridTableView runat="server" Name="items">
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
                <telerik:GridHyperLinkColumn DataNavigateUrlFields="ordernumber" 
                    DataNavigateUrlFormatString="./ReturnItems.aspx?ordernumber={0}" 
                    DataTextField="ordernumber" DataTextFormatString="Process Order" 
                    FilterControlAltText="Filter column4 column" UniqueName="column4">
                </telerik:GridHyperLinkColumn>
                <telerik:GridBoundColumn DataField="ordernumber" 
                    FilterControlAltText="Filter column column" HeaderText="Order Number" 
                    UniqueName="column">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="orderdate" DataType="System.DateTime" 
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
    	 </telerik:RadAjaxPanel>
	<script type="text/javascript">
	    //Put your JavaScript code here.
    </script>

  </asp:Content>



<asp:Content ID="Content3" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .style3
        {
            width: 213px;
        }
        .style4
        {
            width: 254px;
            height: 2px;
        }
        .style5
        {
            width: 213px;
            height: 7px;
        }
        .style6
        {
            width: 254px;
            height: 7px;
        }
    </style>
</asp:Content>




