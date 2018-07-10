<%@ Page Title="Pack" Language="C#" MasterPageFile="~/Pages/RI.Master" AutoEventWireup="true"
    CodeBehind="Pack.aspx.cs" Inherits="PackingMock._Pack" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="head">
    <link href="/Styles/api.pack.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/jquery-1813-custom.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery-1.6.1.min.js" type="text/javascript"></script>
    <script src="/Scripts/json2.js" type="text/javascript"></script>
    <script src="/Scripts/api.pack.obj.js" type="text/javascript"></script>
    <script src="/Scripts/api.pack.funs.js" type="text/javascript"></script>
    <script src="/Scripts/api.Pack.Helper.js" type="text/javascript"></script>
    <script src="/Scripts/api.Pack.Load.js" type="text/javascript"></script>
    <script src="/Scripts/api.Pack.Shared.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-1813-custom.js" type="text/javascript"></script>
    <script type="text/javascript">
    </script>
    <script id="dtv" type="text/html">
    <div id="dialog-confirm" title="Trolley View" style="display: none;">
        <div style="margin-bottom: 10px;">
        </div>
        <table id="tv">
          <thead>
             <tr>
                <th align="left">
                     Trolley Label
                </th>
                <th align="left">
                    Chute No
                 </th>
                 <th align="left">
                    Status
                 </th>
              </tr>
          </thead>
            <tbody>
            {{each TrolleyViewInfo}}
                <tr>
                   <td>
                      ${TrolleyLabel}
                   </td>
                   <td>
                      ${ChuteLabel}
                   </td>
                   <td>
                       ${StatusDescription}
                       <div class="ic${Status}" style="">
                       </div>
                   </td>
                </tr>
            {{/each}}
            </tbody>
        </table>
    </div>
    </script>

    <!-- script tag for Stack View -->
    <script id="dsv" type="text/html">
    <div id="svdialog-confirm" title="Stack View" style="display: none;">
        <div style="margin-bottom: 10px;">
        </div>
        <table id="sv" border=1>
          <thead style="font-size:0.7em;">
             <tr>
                <th align="left">
                     Chute ID
                </th>
                <th align="left" width="150px">
                    Stack Set
                 </th>
                 <th align="center">
                    Update selection</br>
                    <a id="clear"  style="color:blue;"  href="#" onclick="clearAll()">clear all</a>
                    &nbsp;&nbsp;
                    <a id="select" style="color:blue;" href="#" onclick="selectAll()">select all</a>

                 </th>
              </tr>
          </thead>
            <tbody style="font-size:0.9em;">
            {{each StackInfo}}
                <tr>
                   <td>
                      ${ChuteId}
                   </td>
                   <td>
                      ${StackLabel}
                   </td>
                   <td style="text-align:center;">
                      <input id=${ChuteId} type="checkbox" name="filterOut" {{if PreConfigured == 'T'}}checked="checked"{{/if}} value=${ChuteId} onclick="UpdateStackArray(this,value)" />
                   </td>
                </tr>
            {{/each}}
            </tbody>
        </table>
    </div>
    </script>
    <!--                           -->

    <script id="dri" type="text/html">
        <div id="dialog-confirm" title="${title}" style="display: none;">
        <p>
            <span class=".ui-dialog ui-icon ui-icon-alert" style="float: left; margin: 0 10px 10px 0;">
            </span>${message}
        </p>
    </div>
    </script>
    <script id="dmi" type="text/html">
     <div id="dialog-confirm" title="${title}" style="display: none;">
        <ul style="list-style-type: none;font-size:small;">
          {{if hasValue=="true"}}
              {{each values}}
               <li>Item : ${key}    Location : ${value}</li>
              {{/each}}
           {{else}}      
            No item found please select reason and fail order.
           {{/if}}
          <ul>
    </div>
    </script>
    <script id="dynmsg" type="text/html">
     <div id="dialog-confirm" title="${title}" style="display: none;">
        <p>
            <span class=".ui-dialog ui-icon ui-icon-alert" style="float: left; margin: 0 10px 10px 0;">
            </span>${message}
        </p>
    </div>
    </script>
    <script id="msgSuccess" type="text/html">
     <div class="ui-widget">
        <div style="padding-bottom: 0px; margin-top: 20px; padding-left: 0.7em; padding-right: 0.7em;
            padding-top: 0px" class="ui-state-highlight ui-corner-all">
            <p>
                <span style="float: left; margin-right: 0.3em" class="ui-icon ui-icon-info"></span>
                <strong>${message}</strong>{{if val}}<div><img alt="" src="/Images/wait.gif" /></div>{{/if}}
            </p>
        </div>
     </div>
    </script>
    <script id="msgFail" type="text/html">
     <div class="ui-widget">
        <div style="padding-bottom: 0px; margin-top: 20px; padding-left: 0.7em; padding-right: 0.7em; padding-top: 0px"
            class="ui-state-error-msg ui-corner-all">
            <p>
                <span style="float: left; margin-right: 0.3em" class="ui-icon ui-icon-alert"></span>
                <strong>${message}</strong> 
            </p>
        </div>
    </div>
    </script>
    <script id="cstat" type="text/html">
     <table class="tblInforBoard" width="100%">
        <tbody>
            <tr align="left">
                <td>
                    <span class="infoLabel">{{if TrolleyId}}Trolley{{else}}Tote{{/if}} No. </span>
                </td>
                <td align="left">
                    <span class="infovalue">${ContainerLabel}</span>
                </td>
                <td>
                    <span class="infoLabel">Order Count</span><span class="infovalue">&nbsp;&nbsp;&nbsp;&nbsp;{{if PackMode ==="SIS"}}${OrderCount}{{/if}}</span>
                </td>
            </tr>
            <tr align="left">
                <td>
                    <span class="infoLabel">Order No. </span>
                </td>
                <td colspan="2" align="left">
                    <span class="infovalue">{{if PackMode!=="SIS"}}${OrderNo}{{/if}} </span>
                </td>
            </tr>
            <tr align="left">
                <td>
                    <span class="infoLabel">Item Scan</span>
                </td>
                <td >
                    <span class="infovalueBig">{{if CurrentItem}} ${CurrentItem} of ${TotalItem} {{/if}} </span>
                </td>
                <td>
                    <span class="infovalue"></span>
                </td>
            </tr>
            <tr align="left">
                <td>
                    <span class="infoLabel">No. of Parcels</span>
                </td>
                <td colspan="2" >
                    <span class="infovalue">${TotalParcelBag}</span>
                </td>
            </tr>
            <tr align="left">
                <td>
                    <span class="infoLabel">Order In Location(s)</span>
                </td>
                <td colspan="2">
                    <span class="infovalueBig">{{if PackMode!=="SIS"}}${CurrentLocation}{{/if}}</span>
                </td>
            </tr>
        </tbody>
    </table>
    
    </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="dialogYN-widget">
    </div>
    <div id="dialog-PrintAuth" title="Re Print Documents Authorisation" style="display: none; font-size: small;">
        <ul style="list-style-type: none">
            <li>
                Authorisation Code
                <input type="text" id="authCodeInput" />
            </li>
        </ul>
        <div style="font-size: x-small; font-style: italic">
            Esc to close the window</div>
    </div>
    <div id="dialog-Print" title="Re Print Documents" style="display: none; font-size: small;">
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
    <div class="packingLayout">
        <table id="packingLayout" width="100%" border="0">
            <tbody>
                <tr>
                    <td colspan="3" align="center">
                        <div id="msgBoard" style="width: 400px; height: 100px;">
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" align="center">
                        <table>
                            <tr>
                                <td>
                                    <div id="ScanInput">
                                        <input type="text" id="scancodeinput" />
                                        <input type="submit" id="hdnsubmit" value="Submit" 
                                               style="position: absolute; height: 0px; width: 0px; border: none; padding: 0px;"
                                               tabindex="-1" />
                                        <input type="hidden" id="hdnPackingState" value="" />
                                        <input type="hidden" id="hdnPreviousOrder" value="" />
                                        <input type="hidden" id="hdnOpenOrderValues" value="" runat="server" clientidmode="Static" />
                                        <input type="hidden" id="hdnUserOption"      value="" runat="server" clientidmode="Static" />
                                        <input type="hidden" id="hdnMasterPacker" value="N" runat="server" clientidmode = "Static" />
                                        <br />
                                        <%--<input type="button" id="run" value="Show Dialog" />--%>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="left" valign="top">
                        <div id="actions">
                            <%--<div id="TrolleyView" class="actionButton">
                                <span>Trolley View</span></div>--%>
                            <%--<div id="StackView" class="actionButton">
                                <span>Stack View </span>
                                <span style="font-size:xx-small;">[CTRL+1]</span>
                            </div>--%>
                            <%--<div id="MissingItemQuery" class="actionButton">
                                <span>Missing Item(s)</span><span style="font-size:xx-small;">[CTRL+2]</span>
                            </div>--%>
                            <div id="FailOrder" class="actionButton">
                                <span>Fail Order</span>
                                <%--<span style="font-size:xx-small;">[CTRL+3]</span>--%>
                            </div>
                            <%--<div id="ExcessItem" class="actionButton">
                                <span>Excess item(s)</span>
                                <span style="font-size:xx-small;">[CTRL+4]</span>
                            </div>--%>
                            <div id="RePrintDoc" class="actionButton">
                                <span>Management - PRT</span>
                                <%--<span style="font-size:xx-small;">[CTRL+5]</span>--%>
                            </div>
                            <%--<div id="RePack" class="actionButton">
                                <span>Re-Pack</span>
                                <span style="font-size:xx-small;">[CTRL+6]</span>
                            </div>--%>
                            <div id="NewBag" class="actionButton">
                                <span>New Bag</span>
                                <%--<span style="font-size:xx-small;">[CTRL+7]</span>--%>
                            </div>
                            <div id="ReasonCode">
                                <div>
                                    Reason Code</div>
                                <div>
                                    <select id="SelectReasonCode">
                                    </select>
                                </div>
                            </div>
                        </div>
                        <!-- div for instructions for selecting user options -->
                        <div id="options">
                            <%--<span><b>Enter one of following options:</b></span><br />
                            <span>1 - Single Pack</span><br />
                            <span>2 - Multi  Pack</span><br />
                            <span>3 - Pack from FT</span><br />
                            <span>4 - Other Activity</span><br />
                            <span>5 - End Session</span>--%>
                            <div id="StackView" class="actionButton">
                                <span>Stack View</span>
                            </div>
                            <div id="MultiPack" class="actionButton">
                                <span>Multi Pack</span>
                            </div>
                            <div id="SinglePack" class="actionButton">
                                <span>Single Pack</span>
                            </div>
                            <div id="RePack" class="actionButton">
                                <span>Management - PK</span>
                            </div>
                            <div id="PackFromFT" class="actionButton">
                                <span>Pack from Tote</span>
                            </div>
                            <div id="ExcessItem" class="actionButton">
                                <span>Excess item(s)</span>
                            </div>
                            <div id="RePrintDoc_2" class="actionButton">
                                <span>Management - PRT</span>
                                <%--<span style="font-size:xx-small;">[CTRL+5]</span>--%>
                            </div>

                        </div>
                    </td>
                    <td align="center" valign="top">
                        <table width="100%" border="0">
                            <tbody>
                                <tr>
                                    <td align="center" valign="top">
                                        <div id="packflow">
                                            <div id="MIS" class="flowbox">
                                                Multi Pack</div>
                                            <div id="SIS" class="flowbox">
                                                Single Pack</div>
                                            <div id="TransportMode"  class="transportMode">
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" valign="top">
                                        <div id="InforBoard">
                                        </div>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                    <td align="right" valign="top">
                        <div id="currentProcessLEds">
                            <table id="tblcpled" border="0">
                                <tbody>
                                    <tr>
                                        <td>
                                            <div id="DOM" class="flowbox">
                                                Domestic</div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div id="INT" class="flowbox">
                                                International
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div id="EI" class="flowbox">
                                                Excess Items
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div id="FT" class="flowbox">
                                                Tote
                                            </div>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" align="center">
                        <div id="EndPackProcess" class="actionButton2">
                            <span>End Session</span>
                        </div>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</asp:Content>
