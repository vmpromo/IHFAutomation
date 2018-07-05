<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/RI.Master" AutoEventWireup="true"
    CodeBehind="PackstationConfig.aspx.cs" Inherits="IHF.ApplicationLayer.Web.Pages.Packing.PackstationConfig" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Styles/api.pack.css" rel="Stylesheet" type="text/css" />
    <script src="/Scripts/json2.js" type="text/javascript"></script>
    <script src="/Scripts/knockout-2.0.0.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-1.6.1.min.js" type="text/javascript"></script>
    <script src="/scripts/api.packstationconfig.funs.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="childheader">
        Packstation Configuration
    </div>
   
    <div class="packList">
        <select height="10" style="font-weight: bold; font-size: medium;" data-bind="options: packstations, 
                           optionsCaption:'...Choose Packstation...',
                           optionsText:  function(item) { return item.text; }, 
                           optionsValue: function(item) { return item.value; }, 
                           value: packstationSelect, 
                           valueUpdate: 'change'">
        </select>
    </div>
     <div class="areaList">
        <select height="10" style="font-weight: bold; font-size: medium;" data-bind="options:sortareas, 
                           optionsCaption:'...Choose Area...',
                           optionsText:  function(item) { return item.value; }, 
                           optionsValue: function(item) { return item.key; },
                           value:GetSelectedSortArea,
                           valueUpdate:'change'">
        </select>
    </div>
    
    <hr />
    <div style="width: 100%; text-align: center;">
        <div id="availableStacks" style="position: relative; float: left; left: 25px; border-right: 1px solid Red;
            width: 450px; text-align: left; background-color: #EEEEDD; padding-left: 5px;
            padding-right: 5px; overflow: scroll; height: 400px;">
            <table class="list">
                <thead>
                    <tr>
                        <th data-bind="visible:false">
                            Packstation Id
                        </th>
                        <th>
                            Chute No.
                        </th>
                        <th>
                            Stack Set
                        </th>
                        <th>
                            Assign
                        </th>
                    </tr>
                </thead>
                <tbody data-bind="foreach: availableStacks">
                    <tr>
                        <td data-bind="text:PackstationId, visible:false">
                        </td>
                        <td data-bind="text:ChuteId, visible:true">
                        </td>
                        <td data-bind="text:StackLabel">
                        </td>
                        <td>
                            <a href="#" data-bind="click:$root.assignStack">assign</a>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div id="assignedStacks" style="position: relative; float: left; left: 25px; width: 450px;
            text-align: left; background-color: #EEEEDD; padding-left: 10px; padding-right: 5px;
            overflow: scroll; height: 400px;">
            <table class="list">
                <thead>
                    <tr>
                        <th data-bind="visible:false">
                            Packstation Id
                        </th>
                        <th>
                            Chute No.
                        </th>
                        <th>
                            Stack Set
                        </th>
                        <th>
                            Remove
                        </th>
                    </tr>
                </thead>
                <tbody data-bind="foreach:assignedStacks">
                    <tr>
                        <td data-bind="text:PackstationId, visible:false">
                        </td>
                        <td data-bind="text: ChuteId, visible:true">
                        </td>
                        <td data-bind="text: StackLabel">
                        </td>
                        <td>
                            <a href="#" data-bind="click:$root.removeStack">remove</a>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <br />
    </div>
    <br />
    <br />
</asp:Content>
