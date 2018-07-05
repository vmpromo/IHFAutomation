//
// Name: api.pack.funs.js
// Type: Java script file 
// Description: contains different utility functions for Packing.
//
//$Revision:   1.75  $
//
// Version   Date        Author    Reason
//  1.0      14/07/11    MSalman   Initial Released
//  1.1      14/07/11    MSalman   function updated.
//  1.2      14/07/11    MSalman   function updated.
//  1.3      14/07/11    MSalman   function added.
//  1.4      14/07/11    MSalman   Error code updated.
//  1.5      14/07/11    MSalman   function condition are updated.
//  1.6      14/07/11    MSalman   comments updated.
//  1.7      19/07/11    MSalman   New function added.
//  1.8      20/07/11    MSalman   New function added.
//  1.9      21/07/11    MSalman   conversion string added.
//  1.10     01/08/11    MSalman   Status field updated.
//  1.11     04/08/11    MSalman   function changed for excess item.
//  1.12     08/08/11    MSalman   Update previous order no.
//  1.13     08/08/11    MSalman   Error Status updated.
//  1.14     08/08/11    MSalman   function state upated.
//  1.15     09/08/11    MSalman   Message updated.
//  1.16     10/08/11    MSalman   Excess item validation removed.
//  1.17     11/08/11    MSalman   Fail Tote flow updated.
//  1.18     12/08/11    MSalman   CR :Change to scan tote baracode to fail tote 
//                                 barcode .
//  1.19     12/08/11    MSalman   Message change to fail order.
//  1.20     12/08/11    MSalman   Open and RePack Order scanned.
//  1.21     12/08/11    MSalman   Pack from fail tote - status changed.
//  1.22     15/08/11    MSalman   Focus on the scan field enabled.#
//  1.23     18/08/11    MSalman   Messages and focus on  the field is changed.
//  1.24     18/08/11    MSalman   Status Messages are updated.
//  1.25     19/08/11    MSalman   Message Updated.
//  1.26     24/08/11    MSalman   Messages changes and Repack to ft.
//  1.27     25/08/11    MSalman   Messages corrected.
//  1.30     25/08/11    MSalman   RePack Message changes.
//  1.31     25/08/11    MSalman   New check is added.
//  1.32     30/08/11    MSalman   New Message Added from Restricted item
//  1.33     01/09/11    MSalman   Excess item for Repack open order.
//  1.34     05/09/11    MSalman   Excess Item Button is block if trolley is docked.
//  1.35     05/09/11    MSalman   Focus is set after dialog message
//  1.36     05/09/11    MSalman   New Function is added.
//  1.37     05/09/11    MSalman   New field is added.
//  1.38     06/09/11    MSalman   Message changed.
//  1.39     09/09/11    MSalman   New Message changed made for Excess Item Now no dialog msg
//                                 if within the order.
//  1.40     14/09/11    MSalman   New Message added.
//  1.41     15/09/11    MSalman   Missing item message blocked in repack.
//  1.42     19/09/11    MSalman   Missing item validation is added.
//  1.43     20/09/11    MSalman   Repack missing consignment no message changed.
//  1.44     20/09/11    MSalman   End Packing session on timeout is added.
//  1.45     21/09/11    MSalman   Part located check is added.
//  1.46     21/09/11    MSalman   End of trolley message changed.
//  1.47     22/09/11    MSalman   Repack message is changed.
//  1.48     23/09/11    MSalman   1479 bug fix
//  1.49     27/09/11    MSalman   Order not going to fail tote becos state was lost.
//  1.50     27/09/11    MSalman   Restricted item message changed.
//  1.51     28/09/11    MSalman   Space added in Restricted item message.
//  1.52     01/12/11    MSalman   Change Request New functions added for trolley view.
//  1.55     20/03/12    M Khan    FLOW related changes.
//  1.56     03/05/12    M Khan    Unit test fixes
//  1.57     17/05/12    M Khan    UAT changes
//  1.58     17/05/12    M Khan    UAT test 22nd May 12 - changes and fix
//  1.59     31/05/12    M Khan    UAT test 31nd May 12 - changes and fix
//  1.60     01/06/12    M Khan    UAT test - changes and fix
//  1.61     27/06/12    M Khan    User request to change few messages displayed on pack screen
//  1.62     25/09/12    J Watt    Change to error message display
//  1.63     04/10/12    J Watt    Spelling
//  1.64     12/20/12    J Watt    Don't show location of failed tote container label
//  1.65     09/05/13    J Watt    CLick and collect show store icon
//  1.66     09/01/14    J Watt    Trap invalid SKU barcode and disable text box between scans
//  1.69     01/04/14    J Watt    Enable text box even if AJAX call is not made
//  1.70     30/05/14    S Green   Changes for Remove For Pack
//  1.71     06/06/14    S Green   Changes following testing for Remove For Pack
//  1.72     23/06/14    S Green   Altered message when users presses the Fail Order button.
//  1.73     24/06/14    S Green   Minor wording change when selecting pack from tote,
//                                 clear location when packing from a packing tote as well as a failed tote
//  1.74     20/10/14    S Green   New authorisation window for reprint
//  1.75     11/05/15    J Watt    Fix for LANDesk 57856 (form was submitting even when no textbox visible

var NewBagWaitingItem = "F";

function InitOpenOrderState(obj) {

    var s = PackRequest;

    if (obj.toteId === "") {
        s.ActionId = Action.OpenOrder;

    }
    else {
        s.ActionId = Action.FailOrderPack;
        s.ToteId = obj.toteId;
    }




    s.PackMode = obj.pMode;
    s.OrderNo = obj.orderNo;
    s.DestinationType = obj.dType;
    s.ContainerLabel = obj.clabel;
    if (s.PackMode === Mode.SingleItemScan)
        s.OrderCount = 1;

    SetPackState(s);

    SetDestType(s.DestinationType);

    SetPackingMode(s.PackMode);

    SetCurrentStats(s);


}

function ReSetPackState() {

    $("#INT").removeClass("flowboxH").addClass("flowbox");
    $("#DOM").removeClass("flowboxH").addClass("flowbox");
    $("#MIS").removeClass("flowboxH").addClass("flowbox");
    $("#SIS").removeClass("flowboxH").addClass("flowbox");

    ActionLed("");

    $("#InforBoard").empty();


    $("#SelectReasonCode").val("-1");

    var req = {
        Barcode: "",
        UserName: "",
        HostName: "",
        ContainerId: "",
        ContainerNo: "",
        ActionId: "CV",
        PackMode: "",
        DestinationType: "",
        ExcessItemInd: "",
        OrderNo: "",
        ReasonCode: "",
        InOrder: "F"
    };


    return req;

}


function SetInputFocus() {

    $("#scancodeinput").focus();
}


function ReSetReasonCode() {

    $("#SelectReasonCode").val("-1");

    SetInputFocus();

}


function SetToWait(v) {

    if (v) {

        $("#imgWait").show();
    } else {

        $("#imgWait").hide();
    }

}

function InitPackState(scancode) {

    //This will go away later.     
    $("#hdnPackingState").val(JSON.stringify(PackRequest, function (k, v) { return v === "" ? "" : v }));

    var stateObj = JSON.parse($("#hdnPackingState").val());

    stateObj.Barcode = scancode;

    return stateObj;

}

function GetPackState() {

    var stateObj = JSON.parse($("#hdnPackingState").val());

    return stateObj;

}


function SetPackState(sobj) {

    if (sobj !== "") {
        $("#hdnPackingState").val("");
        PackRequest = sobj;
        $("#hdnPackingState").val(JSON.stringify(PackRequest, function (k, v) { return v === "" ? "" : v }));
    }

}


function SetCurrentStats(csobj) {
    if (csobj !== "") {
        // to persist CurrentLocation and to refresh ot when required
        if (csobj.CurrentLocation !== undefined) {
            csobj.CurrentLocation = refreshOrderLocation(csobj.CurrentLocation);
        }
        //
        if (csobj.ContainerLabel != null && 
               (csobj.ContainerLabel.substring(0, 2) == "FT" || csobj.ContainerLabel.substring(0, 1) == "P")) 
        {
            csobj.CurrentLocation = "";
        }

        $("#InforBoard").empty();
        $("#cstat")
		           .tmpl(csobj)
                   .appendTo("#InforBoard");
    }
}




function ShowTVMessage(d) {

    $("#dialogYN-widget").empty();

    var val = "false";

    $("#dtv")
		   .tmpl(d)
		   .appendTo("#dialogYN-widget");

    $("#dialog:ui-dialog").dialog("destroy");

    $("#dialog-confirm").dialog({
        modal: true,
        height: 300,
        width: 450,
        position: 'center',
        buttons: {
            Ok: function () {
                $(this).dialog("close");
            }
        }
    });

};

function ShowSVMessage(d) {

    $("#dialogYN-widget").empty();
    var val = "false";


    $("#dsv")
		   .tmpl(d)
           .appendTo("#dialogYN-widget");

    $("#dialog:ui-dialog").dialog("destroy");

    $("#StackView").hide();

    $("#svdialog-confirm").dialog({
        modal: true,
        closeOnEscape: false,
        height: 300,
        width: 450,
        position: 'center',
        buttons: {
            Save: function () {
                SendStackUpdates();
                setTimeout(function () {
                    $("#StackView").show();
                }, 1500);
                $(this).dialog("close");
            },
            'Save & Proceed to multi-pack': function () {
                SendStackUpdates();
                setTimeout(function () {
                    $("#StackView").show();
                }, 1500);
                $(this).dialog("close");
                openMultiPack();
            }
        }
    });

};

function SendStackUpdates() {
    for (i = 0; i < stackArray.length; i++) {
        stack = stackArray[i];
        var api = new packApi();
        api.UpdateStackSelection({
            data: JSON.stringify(stack),
            error: function () {
                alert('error while updating stack selections');
                PackOption.SelectedOption = UserOptions.NoSelection;
            },
            success: function () {
                SetInputFocus();
            }
        });
    }
};



function openMultiPack() {
    PackOption.SelectedOption = UserOptions.NoSelection;
    $("#scancodeinput").val("2");
    $("form").submit();
};

function PopulateStackArray(r) {
    ClearStackArray();
    stackArray.push(r.StackInfo);

    stackArray = stackArray[0];

    for (i = 0; i < stackArray.length; i++) {
        stack = stackArray[i];
    }
}

function ClearStackArray() {
    var length = stackArray.length;
    for (i = 0; i < length; i++) {
        stackArray.pop();
    }
}


function ShowMiMessage(msg) {

    $("#dialogYN-widget").empty();

    var val = "false";

    if (msg.hasValue === "true")
        val = "true";

    $("#dmi")
		   .tmpl(msg)
		   .appendTo("#dialogYN-widget");

    $("#dialog:ui-dialog").dialog("destroy");

    $("#dialog-confirm").dialog({
        resizable: false,
        height: 250,
        width: 400,
        modal: true,
        closeOnEscape: true,
        buttons: {
            "ok": function () {

                $(this).dialog("close");

                if (val === "true") {

                    var obj = GetPackState();
                    obj.PreviousActionId = obj.ActionId;
                    obj.ActionId = Action.MissingItem;
                    SetPackState(obj);

                    var m = new ErrorMessage(EType.Success, "", "Please scan fail tote barcode, then item(s). Repeat as required.");
                    DisplayMessage(m);
                }

                SetInputFocus();
            }
        }
    });

};


function ShowRIMessage(msg) {

    $("#dialogYN-widget").empty();

    $("#dri")
		   .tmpl(msg)
		   .appendTo("#dialogYN-widget");

    $("#dialog:ui-dialog").dialog("destroy");

    $("#dialog-confirm").dialog({
        resizable: false,
        height: 250,
        width: 400,
        modal: true,
        closeOnEscape: true,
        buttons: {
            "ok": function () {

                $(this).dialog("close");
                SetInputFocus();
            }
        }
    });

};



function ShowETDialogMessasge(msg, eventk) {

    $("#dialogYN-widget").empty();

    $("#dynmsg")
		                .tmpl(msg)
		                .appendTo("#dialogYN-widget");

    $("#dialog:ui-dialog").dialog("destroy");

    $("#dialog-confirm").dialog({
        resizable: false,
        height: 250,
        width: 400,
        modal: true,
        closeOnEscape: false,
        focus: function () {
            $(this).parents('.ui-dialog-buttonpane button:eq(0)').focus();
        },
        buttons: {
            "ok": function () {
                $(this).dialog("close");
                SetInputFocus();
                if (eventk) {
                    eventk();
                }
            }
        }//buttons
    }).parent().addClass("ui-state-error");           // dialog

};


function ShowDialogMessasge(msg, eventy, eventn) {

    $("#dialogYN-widget").empty();

    $("#dynmsg")
		                .tmpl(msg)
		                .appendTo("#dialogYN-widget");

    $("#dialog:ui-dialog").dialog("destroy");

    $("#dialog-confirm").dialog({
        resizable: false,
        height: 250,
        width: 400,
        modal: true,
        closeOnEscape: false,
        focus: function () {
            $(this).parents('.ui-dialog-buttonpane button:eq(0)').focus();
        },
        buttons: {
            "Yes": function () {
                $(this).dialog("close");
                SetInputFocus();
                if (eventy) {
                    eventy();
                }
            },
            "No": function () {

                $(this).dialog("close");
                SetInputFocus();
                if (eventn)
                    eventn(msg);


            }
        }//buttons
    }).parent().addClass("ui-state-error");           // dialog

};



function ShowSuccessMsg(msg) {

    $("#msgBoard").empty();

    $("#msgSuccess")
		                .tmpl({ message: msg })
		                .appendTo("#msgBoard");

    //    setTimeout(function () {
    //        $('#msgBoard').empty();

    //    }, 18000);

};

function ShowWaitMsg(msg) {

    $("#msgBoard").empty();

    $("#msgSuccess")
		            .tmpl({ message: msg, val: true })
		            .appendTo("#msgBoard");
};


function ShowFailMsg(msg) {

    $("#msgBoard").empty();

    $("#msgFail")
		                .tmpl({ message: msg })
		                .appendTo("#msgBoard");

};


// Add further types so that one function do all sort of messaging.
//
function DisplayMessage(objMsg) {



    if (objMsg.getErrorCode() !== "") {

        if (objMsg.getMsgType() === EType.Fail)
            ShowFailMsg(objMsg.getMessageText());

        return;
    }

    if (objMsg.getMsgType() === EType.EndPackProcess) {

        var req = ReSetPackState();

        SetPackState(req);


        if (objMsg.getMessageText() !== "")
            ShowFailMsg(objMsg.getMessageText());
        else
            ShowFailMsg("Redock the trolley again.");

    }

    if (objMsg.getMsgType() === EType.PartLocated) {

        var req = ReSetPackState();

        SetPackState(req);


        if (objMsg.getMessageText() !== "")
            ShowFailMsg(objMsg.getMessageText());
        else
            ShowFailMsg("Take the trolley back to Chute.");

    }



    if (objMsg.getMsgType() === EType.Success) {
        if (objMsg.getMessageText() !== "")
            ShowSuccessMsg(objMsg.getMessageText());
        else
            ShowSuccessMsg("Success");
        return;
    }

    if (objMsg.getMsgType() === EType.WaitMsg) {

        if (objMsg.getMessageText() !== "")
            ShowWaitMsg(objMsg.getMessageText());
        else
            ShowWaitMsg("Loading...");
        return;

    }

    if (objMsg.getMsgType() === EType.Fail) {
        if (objMsg.getMessageText() !== "")
            ShowFailMsg(objMsg.getMessageText());
        else
            ShowFailMsg("Fail");
        return;
    }

    if (objMsg.getMsgType() === EType.RestrictedItem) {
        if (objMsg.getMessageText() !== "")
            ShowRIMessage({ title: "Restricted Item", message: objMsg.getMessageText() });
        else
            ShowFailMsg("Success, Restricted Item. Please attach hazard label to the bag.");

        return;
    }


    if (objMsg.getMsgType() === EType.AcknowledgeMsg) {
        if (objMsg.getMessageText() !== "")
            ShowRIMessage({ title: "Confirm", message: objMsg.getMessageText() });
        else
            ShowFailMsg("Success");

        return;
    }

    if (objMsg.getMsgType() === EType.OrderEnd) {
        ShowDialogMessasge({ title: "Order Complete!", message: "Are there anymore item(s). to scan.", returnMessage: objMsg.getMessageText() },
                           ExcessOrderItem, UnDockContainer);
    }

    if (objMsg.getMsgType() === EType.TrolleyEnd) {
        // with new FLOW, there are chance that we show this message to the user, he turns around to check if there is any excess item.
        // If at the same time, there was an order located, the operator might consider them as excess items. To avoid this, we will not display the below message box.
        // Hence, commented, but still calling UnDockContainer function
        //
        //ShowETDialogMessasge({ title: "No more orders!", message: "Please scan any remaining items as excess.", returnMessage: objMsg.getMessageText() },
        //                   UnDockContainer);
        //
        UnDockContainer();
        InitTransportMode();
        //PackOption.SelectedOption = UserOptions.NoSelection;
        //HideActionButtons();
    }

    if (objMsg.getMsgType() === EType.ToteEnd) {
        ShowDialogMessasge({ title: "Tote Complete!", message: "Are there anymore item(s) in the Tote.", returnMessage: objMsg.getMessageText() },
                           ExcessOrderItem, UnDockContainer);
        InitTransportMode();
    }

    if (objMsg.getMsgType() === EType.ExcessItem) {
        ShowDialogMessasge({ title: "Excess Item", message: "Is there anymore excess item(s).   Selecting  'No' will complete Excess item process.", returnMessage: objMsg.getMessageText() },
                           MoreExcessItem, NoMoreExcessItem);

    }


} //end DisplayMessage


function UnDockContainer(o) {

    var obj = GetPackState();


    var api = new packApi();

    api.unDockContainer({
        data: JSON.stringify(obj, function (k, v) { return v === "" ? "" : v }),
        success: function (d) {
            if (d === 'T') {

                var req = ReSetPackState();

                SetPackState(req);

                if (o !== undefined && o.returnMessage !== "" && o.returnMessage !== null)
                    ShowSuccessMsg(o.returnMessage);
                else {
                    ShowSuccessMsg("No more order(s) to pack.");
                    PackOption.SelectedOption = UserOptions.NoSelection;
                    HideActionButtons();
                }

            }
        }
    });

    ReSetReasonCode();

}



function MoreExcessItem() {

    //For now do nothing...

}

function NoMoreExcessItem(o) {

    var state = GetPackState();

    if (state.PreviousToteId !== "") {
        state.ToteId = state.PreviousToteId;
        state.OrderNo = state.PreviousOrderNo;
    }
    else
        state.ToteId = "";

    if ((state.InOrder === "T" || state.OrderNo !== "") || state.PackMode === Mode.SingleItemScan) {

        if (state.PreviousActionId !== "")
            state.ActionId = state.PreviousActionId;
        else
            state.ActionId = Action.OrderItem;

        state.ExcessItemInd = "F";
        //(state.PreviousToteId === "" || state.PreviousActionId === "") && (state.TrolleyId === "" || state.TrolleyId === null)
        if (state.EndOfTrolley === "T") {

            var req = ReSetPackState();

            state = req;
        } else {

            SetCurrentStats({
                ContainerLabel: state.PreviousContainerLabel,
                OrderNo: state.PreviousOrderNo,
                PackMode: state.PreviousPackMode,
                CurrentItem: state.PreviousCurrentItem,
                TotalItem: state.PreviousTotalItem,
                CurrentLocation: state.PreviousLocation,
                TotalParcelBag: state.PreviousTotalParcelBag,
                OrderCount: state.PreviousOrderCount
            });

            state.PreviousToteId = "";
            state.PreviousOrderNo = "";
            state.PreviousActionId = "";
            state.PreviousCurrentItem = "";
            state.PreviousTotalItem = "";
            state.PreviousPackMode = "";
            state.PreviousContainerLabel = "";
            state.PreviousOrderCount = "";


            var msg = new ErrorMessage(EType.Success, "", "Please continue to scan next order item.");
            DisplayMessage(msg);

        }

    } else {

        state = ReSetPackState();
        PackOption.SelectedOption = UserOptions.NoSelection;
        HideActionButtons();

    }



    SetPackState(state);

    ActionLed("");

    if (state.EndOfTrolley === "T") {
        UnDockContainer(o);
    }

    ReSetReasonCode();
}

function ExcessOrderItem() {


    clearMessage();



    var obj = GetPackState();

    if (obj.InOrder !== "T" && obj.OrderNo === "") {

        obj.ActionId = Action.ExcessItem;
        obj.ExcessItemInd = "T";
        ActionLed(Action.ExcessItem);
        SetPackState(obj);

        //var msg = new ErrorMessage(EType.Success, "", "Please select reason code then Scan Failed Tote barcode.");
        var msg = new ErrorMessage(EType.Success, "", "This item is Excess. Select reason code ’Excess’ and scan a Failed Tote barcode.");
        DisplayMessage(msg);

        ShowActionButtons();

    }
    else {
        var msg = new ErrorMessage(EType.Fail, "", "Excess Item action is not allowed at this stage.");
        DisplayMessage(msg);

    }



}



function SetPackingMode(Id) {

    if (Mode.MultiItemScan === Id) {
        $("#" + Id).addClass("flowboxH");
        $("#" + Mode.SingleItemScan).addClass("flowbox");
    }

    if (Mode.SingleItemScan === Id) {

        $("#" + Id).addClass("flowboxH");
        $("#" + Mode.MultiItemScan).addClass("flowbox");
    }

    if (Id === "") {
        $("#" + Mode.MultiItemScan).removeClass().addClass("flowbox");
        $("#" + Mode.SingleItemScan).removeClass().addClass("flowbox");

    }

};

function SetDestType(Id) {

    if (DType.Demostic === Id) {
        $("#" + Id).addClass("flowboxH");
        $("#" + DType.International).addClass("flowbox");
    }

    if (DType.International === Id) {
        $("#" + Id).addClass("flowboxH");
        $("#" + DType.Demostic).addClass("flowbox");

    }

    if (Id === "") {
        $("#" + DType.Demostic).removeClass().addClass("flowbox");
        $("#" + DType.International).removeClass().addClass("flowbox");

    }

};

function ActionLed(Id) {

    $("#" + Id).removeClass("flowbox").addClass("flowboxH");

    if (Id === "") {
        $("#" + Action.FailOrder).removeClass("flowboxH").addClass("flowbox");
        $("#" + Action.ExcessItem).removeClass("flowboxH").addClass("flowbox");
    }




}

function clearMessage() {

    $("#msgBoard").empty();

}


function ValidateRequest(req) {
    var retVal = true;


    //var clfn = new fnCapLock();

    // if (clfn.isCapLockOn(e))
    //alert("Please un set the Caps Lock.")



    if (req.ActionId === Action.FailOrder && req.OrderNo !== '') {

        if (req.ReasonCode === "-1") {

            if (req.PreviousActionId !== "") {
                req.ActionId = req.PreviousActionId;
                SetPackState(req);
            }

            var error = new ErrorMessage(EType.Fail, "", "Fail: Please select Reason Code and then scan a Failed Tote Barcode.");

            DisplayMessage(error);
            retVal = false;

        }
        else {
            if (req.Barcode.substring(0, 2).toUpperCase() != "FT") {
                var error = new ErrorMessage(EType.Fail, "", "Fail : Only Failed Tote barcode accepted at this stage.");
                DisplayMessage(error);
                retVal = false;
            }

        }

    }

    if (req.ActionId === Action.ExcessItem && req.ExcessItemInd === 'T') {

        if (req.ReasonCode === "-1") {
            //var error = new ErrorMessage(EType.Fail, "", "Fail : Please select Reason Code. Then scan Fail Tote barcode.");
            var error = new ErrorMessage(EType.Fail, "", "This item is Excess. Select reason code ’Excess’ and scan a Failed Tote barcode.");
            DisplayMessage(error);
            retVal = false;
        }

    }

    //check the barcode to validate as per the use option selected
    if (req.ActionId === Action.ContainerValidation && PackOption.SelectedOption === UserOptions.SinglePack) {
        if (req.Barcode.substring(0, 2).toUpperCase() === "TS" || req.Barcode.substring(0, 2).toUpperCase() === "TA") {
            //do nothing
        }
        else {
            var error = new ErrorMessage(EType.Fail, "", "Fail : Only single trolley barcode accepted at this stage.");
            DisplayMessage(error);
            retVal = false;
        }
    }

    if (req.ActionId === Action.ContainerValidation && PackOption.SelectedOption === UserOptions.PackFromFT) {
        if (req.Barcode.substring(0, 2).toUpperCase() != "FT" && req.Barcode.substring(0, 1).toUpperCase() != "P") {
            var error = new ErrorMessage(EType.Fail, "", "Fail : Only Failed Tote barcode accepted at this stage.");
            DisplayMessage(error);
            retVal = false;
        }
    }

    //if FT then user shud scan sku barcode first and only then FT barcode
    if (req.ActionId === Action.FailOrder && req.OrderNo == '') {
        if (req.Barcode.substring(0, 2).toUpperCase() == "FT") {
            var error = new ErrorMessage(EType.Fail, "", "Fail : Invalid barcode scanned. Scan Item barcode");

            DisplayMessage(error);
            retVal = false;
        }
        //        if (req.ReasonCode === "-1") {

        //            if (req.PreviousActionId !== "") {
        //                req.ActionId = req.PreviousActionId;
        //                SetPackState(req);
        //            }

        //            var error = new ErrorMessage(EType.Fail, "", "Fail : Please select Reason Code and then click Fail Order.'");

        //            DisplayMessage(error);
        //            retVal = false;

        //        }

    }

    return retVal;
}


function TrolleyViewAction() {


    var obj = GetPackState();

    if (obj.OrderNo === "" && obj.ActionId === Action.ContainerValidation && obj.InOrder === "F") {

        var gtv = new packApi();

        gtv.getTrolleyView({
            success: function (r) {
                if (r !== "") {
                    ShowTVMessage(r);
                }
            }
        });

    }
}

function stackViewAction() {


    var obj = GetPackState();

    if (obj.OrderNo === "" && obj.ActionId === Action.ContainerValidation && obj.InOrder === "F" && PackOption.SelectedOption === UserOptions.OtherActivity) {

        var gtv = new packApi();

        gtv.getStackView({
            success: function (r) {
                if (r !== "") {
                    PopulateStackArray(r);

                    ShowSVMessage(r);
                }
            },
            error: function (r) {
                alert("Error while getting stack details.");
            }
        });

    }
    else {
        // display message
        var error = new ErrorMessage(EType.Fail, "", "End session first to view Stack settings. ");
        DisplayMessage(error);
    }
}


function MissingItemAction() {

    clearMessage();

    var obj = GetPackState();

    if (obj === "")
        obj = InitPackState("");
    if ((obj.ActionId === Action.OrderItem
        || obj.ActionId === Action.FailOrderPack
        || obj.ActionId === Action.OpenOrder
        || obj.ActionId === Action.MissingItem) && obj.OrderNo !== '') {

        var fmi = new packApi();

        fmi.getMissingItem({
            data: JSON.stringify(obj.OrderNo, function (k, v) { return v === "" ? "" : v }),
            success: function (r) {

                if (r.length > 0)
                    ShowMiMessage({ title: "Missing Item(s)", hasValue: "true", values: r });
                else
                    ShowMiMessage({ title: "Missing Item(s)", hasValue: "false", values: {} });

            }
        });

    } else if (obj.ActionId !== Action.OrderItem && obj.OrderNo === "") {

        var error = new ErrorMessage(EType.Fail, "", "Fail : There is no valid Order.");
        DisplayMessage(error);
    } else if (obj.ActionId === Action.RePack) {

        var error = new ErrorMessage(EType.Fail, "", "Fail : Missing item(s) is not allowed in Repack.");
        DisplayMessage(error);
    }

}

function ExcessItemAction() {

    ExcessOrderItem();

    //    clearMessage();

    //    var obj = GetPackState();

    //    //    if (obj === "")
    //    //        obj = InitPackState("");
    //    //    if (obj.ActionId === Action.ContainerValidation) {
    //    //        var error = new ErrorMessage(EType.Fail, "", "Fail : There is no valid trolley.");
    //    //        DisplayMessage(error);
    //    //    } else if (obj.ActionId === Action.OrderItem && obj.OrderNo !== "") {

    //    obj.ActionId = Action.ExcessItem;
    //    ActionLed(Action.ExcessItem);

    //  


    //    SetPackState(obj);

    //    var msg = new ErrorMessage(EType.Success, "", "Please scan tote barcode.");
    //    DisplayMessage(msg);
    //    }


}


function RePrintOption(ord) {

    $("#dialog:ui-dialog").dialog("destroy");

    $("#dialog-Print").dialog({
        resizable: false,
        height: 240,
        width: 370,
        modal: true,
        closeOnEscape: true,
        open: function () {
            $(".chkprint").each(function () {
                this.checked = false;
            });
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

                    var api = new packApi();

                    ord.Docs = strDoc;

                    api.rePrintDoc({
                        data: JSON.stringify(ord, function (k, v) { return v === "" ? "" : v }),
                        success: function (d) { }
                    });

                    $(this).dialog("close");

                    SetInputFocus();
                }
            }

        }//buttons
    });

}

function handleRePrintAuthCode () {

    var userbarcode = $("#authCodeInput").val();
    $("#authCodeInput").val("");

    var api = new packApi();

    api.UserPrintAuth({
        data: JSON.stringify(userbarcode, function (k, v) { return v === "" ? "" : v }),
        success: function (d) {
            if (d === "T") {
                $("#dialog-PrintAuth").dialog("close");
                var obj = GetPackState();
                var ordno = GetPreviousOrder();
                if (obj === "")
                    obj = InitPackState("");
                obj.OrderNo = ordno;
                RePrintOption(obj);
            }
            else {
                alert("Invalid user/barcode");
                $('#authCodeInput').focus();
            }
        }
    });

    return false;

}

function RePrintAuth(ord) {

    $("#dialog:ui-dialog").dialog("destroy");

    $("#dialog-PrintAuth").dialog({
        resizable: false,
        height: 240,
        width: 370,
        modal: true,
        closeOnEscape: true,
        open: function () {
            setTimeout(function () {
                $('#authCodeInput').focus();
            }, 420); // After 420 ms
        },
        close: function () {
            $("#authCodeInput").val("");
            SetInputFocus();
        }
    });

}

function RePrintDocAction() {

    clearMessage();

    var obj = GetPackState();

    var ordno = GetPreviousOrder();

    if (obj === "")
        obj = InitPackState("");


    if (ordno !== '') {
        obj.OrderNo = ordno;

        var mp = $("#hdnMasterPacker").val();

        if (mp == "Y")
            RePrintOption(obj);
        else
            RePrintAuth(obj);



    } else if (ordno === "") {

        var error = new ErrorMessage(EType.Fail, "", "Fail : There is no valid Order.");

        DisplayMessage(error);

    }


}

function NewBagAction() {

    clearMessage();

    var obj = GetPackState();

    if (obj === "")
        obj = InitPackState("");
    if ((obj.ActionId === Action.OrderItem
        || obj.ActionId === Action.OpenOrder
        || obj.ActionId === Action.RePack
        || obj.ActionId === Action.FailOrderPack
        || obj.ActionId === Action.MissingItem) && (obj.OrderNo !== '') && (obj.PackMode === Mode.MultiItemScan)) {
        $("#scancodeinput").val("PA00000000000002");

        $("form").submit();

    } else if (obj.ActionId === Action.ContainerValidation && obj.OrderNo === "") {

        var error = new ErrorMessage(EType.Fail, "", "Fail : There is no valid Order.");
        DisplayMessage(error);
    }
    else {

        if (obj.PackMode === Mode.SingleItemScan) {
            var error = new ErrorMessage(EType.Fail, "", "Fail : New bag is not allowed in Single item order.");
            DisplayMessage(error);

        }

    }

}


function FailOrderAction() {
    clearMessage();

    var obj = GetPackState();

    if (obj === "")
        obj = InitPackState("");
    if ((obj.ActionId === Action.OrderItem
        || obj.ActionId === Action.FailOrderPack
        || obj.ActionId === Action.RePack
        || obj.ActionId === Action.OpenOrder
        || obj.ActionId === Action.FailOrder
        || obj.ActionId === Action.MissingItem) && obj.OrderNo !== '') {

        obj.PreviousActionId = obj.ActionId;

        obj.ActionId = Action.FailOrder;

        SetPackState(obj);

        $("#scancodeinput").val("PA00000000000001");
        $("form").submit();

    }
    //for SIS, fail order and order number is empty
    //this will happen when in single pack
    //and user immediately clicks FailOrder button
    else if (obj.ActionId === Action.OrderItem && obj.OrderNo === '' && obj.PackMode === Mode.SingleItemScan) {

        obj.PreviousActionId = obj.ActionId;

        obj.ActionId = Action.FailOrder;

        SetPackState(obj);

        $("#scancodeinput").val("PA00000000000001");
        $("form").submit();

    }
    else if (obj.ActionId !== Action.OrderItem && obj.OrderNo === "") {

        var error = new ErrorMessage(EType.Fail, "", "Fail : There is no valid Order.");
        DisplayMessage(error);
    }
    else if (obj.ActionId === Action.ExcessItem) {
        var error = new ErrorMessage(EType.Fail, "", "Fail : Fail Order is not allowed at this stage.");
        DisplayMessage(error);
    }

}

function RePackAction() {

    clearMessage();

    var obj = GetPackState();

    if (obj === "")
        obj = InitPackState("");

    if ((obj.ActionId !== Action.ContainerValidation && obj.OrderNo !== "")) {
        var error = new ErrorMessage(EType.Fail, "", "Fail : Cannot Repack Order when order is being packed.");
        DisplayMessage(error);
    } else {
        obj.ActionId = Action.RePack;
        SetPackState(obj);
        var error = new ErrorMessage(EType.Success, "", "Please scan parcel barcode or Type in Order number to re-pack.");
        DisplayMessage(error);
    }


}

function EndPackProcessAction() {

    clearMessage();

    var obj = GetPackState();

    if (obj === "")
        obj = InitPackState("");

    if (obj.OrderNo !== "" && obj.ActionId !== Action.ContainerValidation && obj.TrolleyId !== "" && obj.InOrder === "F") {

        //        ShowWaitMsg("Validating your request. Please wait . . . ");
        //        SetToWait(true);

        //SetToWait(false);

        var error = new ErrorMessage(EType.WaitMsg, "", "Ending session. Please wait . . . ");
        DisplayMessage(error);

        var api = new packApi();

        api.EndPackProcess({
            data: JSON.stringify(obj, function (k, v) { return v === "" ? "" : v }),
            success: function (d) {

                if (d === "T") {

                    var req = ReSetPackState();

                    SetPackState(req);
                    clearMessage();
                }

            },
            error: function () { clearMessage(); }
        });



        PackOption.SelectedOption = UserOptions.NoSelection;



    }
    else if (obj.ActionId === Action.ContainerValidation && PackOption.SelectedOption !== UserOptions.NoSelection) {
        PackOption.SelectedOption = UserOptions.NoSelection;
    }
    //once user is in option OtherActivity, he can press 5 to end session and come out of it
    //also reset pack state which will change the action id to 'CV'
    else if (PackOption.SelectedOption === UserOptions.OtherActivity) {
        PackOption.SelectedOption = UserOptions.NoSelection;
        var req = ReSetPackState();
        SetPackState(req);
    }
    //end session while single packing
    else if (PackOption.SelectedOption === UserOptions.SinglePack && obj.OrderNo == "") {
        PackOption.SelectedOption = UserOptions.NoSelection;
        var req = ReSetPackState();
        SetPackState(req);
    }
    else {

        var error = new ErrorMessage(EType.Fail, "", "End Session is not allowed at this stage.");
        DisplayMessage(error);
    }




}


//&& (obj.CurrentItem !== "0" && obj.TotalItem !== "0") 
function PrintDocs(obj) {
    if ((obj.CurrentItem !== null && obj.TotalItem !== null) && (obj.CurrentItem !== "" && obj.TotalItem !== "")
         && (obj.PackingState.ActionId === Action.OrderItem
            || obj.PackingState.ActionId === Action.OpenOrder
            || obj.PackingState.ActionId === Action.RePack
            || obj.PackingState.ActionId === Action.FailOrderPack
            || obj.PackingState.ActionId === Action.MissingItem)
            || (obj.PackingState.ActionId === Action.FailOrder && obj.FailToteScaned === "T")) {

        if (jQuery.trim(obj.CurrentItem) === jQuery.trim(obj.TotalItem) || (obj.PackingState.ActionId === Action.FailOrder && obj.FailToteScaned === "T")) {

            SetPreviousOrder(obj.OrderNo);

            ActionLed("");

            SetToWait(false);

            var error = new ErrorMessage(EType.WaitMsg, "", "Please wait generating documents...");
            DisplayMessage(error);

            $("#scancodeinput").attr("disabled", "disabled");

            var api = new packApi();

            api.PrintDoc({
                data: JSON.stringify(obj, function (k, v) { return v === "" ? "" : v }),
                success: function (d) {

                    clearMessage();

                    ProcessResponse(d);

                    if ((d.PackingState.ActionId === Action.OpenOrder || d.PackingState.ActionId === Action.RePack) && (d.SuccessInd === 'T')) {

                        var state = ReSetPackState();

                        SetPackState(state);

                        PackOption.SelectedOption = UserOptions.NoSelection;
                        HideActionButtons();
                    }
                    if (d.PackingState.ActionId === Action.MissingItem && d.SuccessInd === "T") {
                        d.PackingState.ActionId = d.PackingState.PreviousActionId;
                        d.PackingState.PreviousActionId = "";
                    }

                    if (d.ErrorCode === "" && d.ErrorMessage === "") {

                        var error = new ErrorMessage(EType.Success, "", "Order Complete.");

                        DisplayMessage(error);

                    }
                    else if (d.ErrorCode === "TT000") {
                        // this is the case 
                        // 1. where an order hes been failed in to a failed tote
                        // 2. order has been re-packed from a failed tote
                        var error = new ErrorMessage(EType.Success, "", "Order Complete. Scan next tote.");

                        DisplayMessage(error);

                        var state = ReSetPackState();

                        SetPackState(state);

                        PackOption.SelectedOption = UserOptions.PackFromFT;
                        // HideActionButtons();

                    }
                    ReSetReasonCode();

                    //if (d.OrderCount 
                    //PackOption.SelectedOption = UserOptions.NoSelection;
                    //HideActionButtons();

                    //enalbe input box after the request finishes
                    $("#scancodeinput").removeAttr("disabled");
                    SetInputFocus();
                },
                error: function () {
                    //enalbe input box after the request finishes
                    $("#scancodeinput").removeAttr("disabled");
                    SetInputFocus();
                }
            });

            SetToWait(true);

            //enalbe input box after the request finishes
            //$("#scancodeinput").removeAttr("disabled");


        }
    }




}


function ProcessResponse(d) {

    $("#scancodeinput").removeAttr("disabled");

    if (d.SuccessInd === 'T') {

        if (d.PackingState.ActionId === Action.ContainerValidation && d.PackingState.TrolleyId !== "")
            d.PackingState.ActionId = Action.OrderItem;
        else if (d.PackingState.ActionId === Action.ContainerValidation && d.PackingState.ToteId !== "")
            d.PackingState.ActionId = Action.FailOrderPack;


        if (d.ActionMessage !== "" && d.ActionMessage !== null) {
            var error = new ErrorMessage(EType.Success, "", d.ActionMessage);
            DisplayMessage(error);

        } else {
            if (d.PackingState.ActionId === Action.RePack && d.CurrentItem === "") {

                var error = new ErrorMessage(EType.Success, "", "Success : Discard old documentation. Scan item for Pack.");
                DisplayMessage(error);
            }

            else if (d.RestrictedItemInd === "T" && d.DestinationType === "INT") {

                var error = new ErrorMessage(EType.RestrictedItem, "", "Success, Restricted Item. &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Please attach hazard label to the bag.");
                DisplayMessage(error);
            }
            else {
                var error = new ErrorMessage(EType.Success, "", "");
                DisplayMessage(error);
            }
        }


    }
    else if (d.ErrorCode === "FC001" && d.SuccessInd === 'F') {
        var error = new ErrorMessage(EType.EndPackProcess, "", d.ErrorMessage);
        DisplayMessage(error);
        return;
    }
    else if (d.ErrorCode === "MI001" && d.SuccessInd === 'F') {
        var error = new ErrorMessage(EType.Fail, d.ErrorCode, d.ErrorMessage);
        DisplayMessage(error);
        return;
    }
    else if (d.ErrorCode === "TL001" && d.SuccessInd === 'F') {
        //TL001 is returned when user selects to multi pack 
        //and there are no orders to pack
        //display message to the user
        //come out of multi pack

        var error = new ErrorMessage(EType.Fail, d.ErrorCode, d.ErrorMessage);
        DisplayMessage(error);

        PackOption.SelectedOption = UserOptions.NoSelection;
        HideActionButtons();

        return;
    }
    //
    else if (d.ErrorCode.substring(0, 2) === "TL" && d.SuccessInd === 'F') {
        var error = new ErrorMessage(EType.Fail, d.ErrorCode, d.ErrorMessage);
        DisplayMessage(error);
        return;
    }
    else if (d.ErrorCode === "TF003" && d.SuccessInd === 'F') {
        var error = new ErrorMessage(EType.Fail, d.ErrorCode, d.ErrorMessage);
        DisplayMessage(error);
        return;
    }
    else if (d.ErrorCode === "TL000" && d.SuccessInd === 'F') {
        var error = new ErrorMessage(EType.Fail, d.ErrorCode, d.ErrorMessage);
        DisplayMessage(error);
        return;
    }
    else if (d.ErrorCode === "SC001" && d.SuccessInd === 'F') {
        var error = new ErrorMessage(EType.AcknowledgeMsg, "", d.ErrorMessage);
        DisplayMessage(error);
        return;
    }
    else if (d.ErrorCode.substring(0, 2) === "SC" && d.SuccessInd === 'F') {
        var error = new ErrorMessage(EType.Fail, d.ErrorCode, d.ErrorMessage);
        DisplayMessage(error);
        return;
    }
    else if (d.ErrorCode.substring(0, 2) === "PK" && d.SuccessInd === 'F') {
        var error = new ErrorMessage(EType.Fail, d.ErrorCode, d.ErrorMessage);
        DisplayMessage(error);
        return;
    }
    else if (d.ErrorCode === "RP000" && d.SuccessInd === 'F') {

        d.PackingState.ActionId = Action.ContainerValidation;

        var error = new ErrorMessage(EType.Fail, d.ErrorCode, d.ErrorMessage);
        DisplayMessage(error);
    }
    else if (d.ErrorCode === "RP002" && d.SuccessInd === 'F') {

        d.PackingState.ActionId = Action.ContainerValidation;

        var error = new ErrorMessage(EType.Fail, d.ErrorCode, d.ErrorMessage);
        DisplayMessage(error);
    }

    else if (d.ErrorCode === "RP003" && d.SuccessInd === 'F') {

        d.PackingState.ActionId = Action.ContainerValidation;

        var error = new ErrorMessage(EType.Fail, d.ErrorCode, d.ErrorMessage);
        DisplayMessage(error);
        return;
    }
    else if (d.ErrorCode === "RP010" && d.SuccessInd === 'F') {

        var error = new ErrorMessage(EType.Fail, d.ErrorCode, d.ErrorMessage);
        DisplayMessage(error);
    }
    else if (d.ErrorCode === "" && d.SuccessInd === 'F') {
        var error = new ErrorMessage(EType.Fail, "", "Fail");
        DisplayMessage(error);

        return;
    }
    else if (d.ErrorCode === "PL001" && d.SuccessInd === 'F') {

        var error = new ErrorMessage(EType.PartLocated, "", d.ErrorMessage);
        DisplayMessage(error);
        return;
    }
    else if ((d.ErrorCode === "TF000" && d.SuccessInd === 'F') && (d.PackingState.ActionId === Action.OrderItem)) {
        // TF000 is returned when user is in multi pack
        // has packed the presented order
        // and now there are no more orders to pack
        // display message to the user
        // come out of multi pack

        d.PackingState.EndOfTrolley = "T";
        var error = new ErrorMessage(EType.TrolleyEnd, "", d.ErrorMessage);
        DisplayMessage(error);

        PackOption.SelectedOption = UserOptions.NoSelection;
        HideActionButtons();
    }
    else if (d.ErrorCode === "TF001" && d.SuccessInd === 'F') {

        d.PackingState.EndOfTrolley = "T";
        var error = new ErrorMessage(EType.ToteEnd, "", d.ErrorMessage);
        DisplayMessage(error);

    }
    else if (d.ErrorCode === "MT000" && d.SuccessInd === 'F') {

        d.PackingState.ActionId = Action.FailOrder;

        var error = new ErrorMessage(EType.Fail, d.ErrorCode, d.ErrorMessage + " Please scan Tote barcode to fail the order.");

        DisplayMessage(error);

    }
    else if ((d.ErrorCode === "TF000" && d.SuccessInd === 'F') && (d.PackingState.ActionId !== Action.OrderItem)) {

        d.PackingState.EndOfTrolley = "T";
        var error = new ErrorMessage(EType.OrderEnd, "", d.ErrorMessage);
        DisplayMessage(error);



    }
    else if (d.ErrorCode === "MT001" && d.SuccessInd === 'F' && d.PackingState.ActionId === Action.RePack) {

        d.PackingState.ActionId = Action.ContainerValidation;
        d.PackingState.OrderNo = "";
        d.OrderNo = "";

        var error = new ErrorMessage(EType.Fail, d.ErrorCode, d.ErrorMessage + " Try Repack later.");

        DisplayMessage(error);

    }
    else if (d.ErrorCode === "MT002" && d.SuccessInd === 'F' && d.PackingState.ActionId === Action.RePack) {

        d.PackingState.ActionId = Action.ContainerValidation;
        d.PackingState.OrderNo = "";
        d.OrderNo = "";

        var error = new ErrorMessage(EType.Fail, d.ErrorCode, d.ErrorMessage);

        DisplayMessage(error);

    }
    else if (d.ErrorCode === "TT000" && d.SuccessInd === "T") {


    }
    else if (d.ErrorCode === "TL002" && d.SuccessInd === 'F') {

//        d.PackingState.ActionId = Action.ContainerValidation;

        var error = new ErrorMessage(EType.Fail, d.ErrorCode, d.ErrorMessage);
        DisplayMessage(error);
        return;
    }



    SetCurrentStats(d);


    SetDestType(d.DestinationType);


    SetPackingMode(d.PackMode);

    SetTransportMode(d.TransportMode);


    //Need visiting for fail order. don't have data to test it.

    if ((d.PackingState.ActionId === Action.ExcessItem) && (d.PackingState.ExcessItemInd !== 'T')) {
        ActionLed(d.PackingState.ActionId);
    }
    else if (d.PackingState.ExcessItemInd === 'T' && d.PackingState.ActionId !== Action.ContainerValidation && d.PackingState.ActionId !== Action.ExcessItem) {

        d.PackingState.PreviousActionId = d.PackingState.ActionId;

        if (d.PackingState.ToteId !== "") {
            d.PackingState.PreviousToteId = d.PackingState.ToteId
        }

        d.PackingState.PreviousOrderNo = d.PackingState.OrderNo;
        d.PackingState.PreviousCurrentItem = d.CurrentItem;
        d.PackingState.PreviousTotalItem = d.TotalItem;
        d.PackingState.PreviousPackMode = d.PackingState.PackMode;
        d.PackingState.PreviousContainerLabel = d.ContainerLabel;
        d.PackingState.PreviousLocation = d.CurrentLocation;
        d.PackingState.PreviousTotalParcelBag = d.TotalParcelBag;
        d.PackingState.PreviousOrderCount = d.OrderCount;


        d.PackingState.ActionId = Action.ExcessItem;

        //var error = new ErrorMessage(EType.Success, "", "Please select reason code and scan fail tote barcode.");
        var error = new ErrorMessage(EType.Success, "", "This item is Excess. Select reason code ’Excess’ and scan a Failed Tote barcode.");
        DisplayMessage(error);

        ActionLed(Action.ExcessItem);
    }
    else if ((d.PackingState.ExcessItemInd === 'T' && d.PackingState.ActionId === Action.ExcessItem) && (d.PackingState.ToteId !== null && d.FailToteScaned === "F")) {

        if ((d.PackingState.InOrder === "F" && d.PackingState.OrderNo === "") && (d.PackingState.PackMode !== Mode.SingleItemScan)) {

            var error = new ErrorMessage(EType.ExcessItem, "", "");
            DisplayMessage(error);

        } else {

            NoMoreExcessItem({});
            return;
        }

    }
    else if ((d.PackingState.ExcessItemInd === 'T' && d.PackingState.ActionId === Action.ExcessItem) && (d.PackingState.ToteId !== null && d.FailToteScaned === "T")) {
        var error = new ErrorMessage(EType.Success, "", "Success, now scan excess item(s).");
        DisplayMessage(error);

    }
    else if ((d.PackingState.ActionId === Action.FailOrder) && (d.FailToteScaned === "F")) {
        //for testing taken out.
        //d.PackingState.ToteId === "" &&

        ActionLed(Action.FailOrder);

        var error = "";

        if (jQuery.trim(d.CurrentItem) < jQuery.trim(d.TotalItem) && d.PackMode === Mode.MultiItemScan) {

            var rcode = $("#SelectReasonCode :selected").val();

            if (rcode === "-1")
                //error = new ErrorMessage(EType.Success, "", "Your order requires checking select reason code 'requires check' and scan remaning item(s).");
                error = new ErrorMessage(EType.Success, "", "Your order has Failed, select  reason code ‘Requires checking’. You are required to scan any remaining items and then a Failed Tote Barcode.");
            else
                error = new ErrorMessage(EType.Success, "", "Please scan any remaining items and then a Failed Tote Barcode.");
        }
        else {
            if (d.PackMode === Mode.SingleItemScan && d.OrderNo === '') {
                error = new ErrorMessage(EType.Success, "", "Please scan Item barcode");
            }
            else {
                if (d.PackMode === Mode.SingleItemScan && d.OrderNo !== '' && jQuery.trim(d.CurrentItem) < jQuery.trim(d.TotalItem)) {
                    error = new ErrorMessage(EType.Success, "", "Please scan Item barcode");
                }
                else if (d.PackMode === Mode.SingleItemScan && d.OrderNo !== '' && jQuery.trim(d.CurrentItem) === jQuery.trim(d.TotalItem)) {
                    error = new ErrorMessage(EType.Success, "", "Please scan Fail Tote barcode");
                }
                else
                    error = new ErrorMessage(EType.Success, "", "Please scan Fail Tote barcode.");
            }
        }

        DisplayMessage(error);

    }
    else if (d.PackingState.ActionId === Action.FailOrderPack && d.PackingState.ToteId !== "") {

        ActionLed(Action.FailOrder);

    }
    else if (d.ExcessItemInd === 'F' && d.PackingState.ActionId !== Action.FailOrder) {
        ActionLed("");
    }


    //Set the State of the screen for presistence 
    SetPackState(d.PackingState);

    SetInputFocus();


    //    if (d.PackingState.ActionId === Action.RePack && d.CurrentItem === d.TotalItem) {
    //        PackOption.SelectedOption = UserOptions.NoSelection;
    //        HideActionButtons();
    //    }

} //ProcessResponse


function SetPreviousOrder(o) {

    $("#hdnPreviousOrder").val(o);

}


function GetPreviousOrder() {
    var r = "";

    r = $("#hdnPreviousOrder").val();

    return r;
}


//function to hide Action buttons on screen
function HideActionButtons() {
    $("#actions").hide();
    $("#options").show();
};

//function to show Action buttons on screen
function ShowActionButtons() {
    $("#actions").show();
    $("#options").hide();
};

// function to set orders transport mode,
// this wil be called on first sku scan 
// will be hidden at the start of order before scan of sku
// will be hidden on completion of order 
function SetTransportMode(Id) {
    /*if (Id == TransportMode.Road) {
    $("#TransportMode").hide('slow');
    $("#TransportMode").css("background-image", "url('/images/Road.jpg')");
    $("#TransportMode").show('slow');
    }
    else*/          
    if (Id == TransportMode.Air) {
        $("#TransportMode").hide('slow');
        $("#TransportMode").css("background-image", "url('/images/Air.jpg')");
        $("#TransportMode").show('slow');
    }
    else if (Id == TransportMode.Both) {
        $("#TransportMode").hide('slow');
        $("#TransportMode").css("background-image", "url('/images/Both.jpg')");
        $("#TransportMode").show('slow');
    }
    else if (Id == TransportMode.RIVan) {
        $("#TransportMode").hide('slow');
        $("#TransportMode").css("background-image", "url('/images/clickcollectsmall.jpg')");
        $("#TransportMode").show('slow');
    }
    else {
        $("#TransportMode").css("background-image", "none')");
        $("#TransportMode").hide('slow');
    }

};

function InitTransportMode() {
    $("#TransportMode").hide('slow');
};

function UpdateStackArray(obj, val) {
    for (i = 0; i < stackArray.length; i++) {
        stack = stackArray[i];
        if (stack.ChuteId == val) {
            if (obj.checked)
                stack.PreConfigured = 'T';
            else
                stack.PreConfigured = 'F';

            break;
        }
    }
}

// commented key handling function as requested bu user
/*function keyDownAction(e) {
if (e.ctrlKey) {
switch (e.which) {
case 49: // 1 stack View
{
stackViewAction();

break;
}
case 50: // 2 missing items
{
MissingItemAction();

break;
}
case 51: // 3 Fail Order
{
FailOrderAction();

break;
}
case 52: // 4 Excess items
{
ExcessItemAction();

break;
}
case 53: // 5 Re-print doc
{
RePrintDocAction();

break;
}
case 54: // 6 Re-pack
{
RePackAction()

break;
}
case 55: // 7 New bag
{
NewBagAction();

break;
}
default:
{
break;
}
} //switch
} //if
};*/

function clearAll() {
    $(":checkbox[name='filterOut']").attr("checked", false);
    $(":checkbox[name='filterOut']").each(function () {
        UpdateStackArray(this, this.value);
    });
}

function selectAll() {
    $(":checkbox[name='filterOut']").attr("checked", true);
    $(":checkbox[name='filterOut']").each(function () {
        UpdateStackArray(this, this.value);
    });
}

function refreshOrderLocation(orderLocation) {
    if (orderLocation !== "") {
        BestOrder.CurrentLocation = orderLocation;
    }

    return BestOrder.CurrentLocation;
}

function setmasterpacker () {

    // these were visible only to master packers, but now anyone can see them
    $("#RePrintDoc").css("visibility", "visible");
    $("#RePrintDoc_2").css("visibility", "visible");

    var mp = $("#hdnMasterPacker").val();

   if (mp == "Y")
   {
       $("#RePack").css("visibility", "visible");
   }
   else
   {
       $("#RePack").css("visibility", "hidden");
   }
}

//Main functoids
$(document).ready(function () {

    // bind the enter key on the authorisation barcode to the user authorisation dialog 
    $('#dialog-PrintAuth').keypress(function (event) {
        if (event.keyCode == $.ui.keyCode.ENTER) {
            handleRePrintAuthCode()
        }
    });

    $("#run").click(function (e) {

    });

    setmasterpacker();

    //hide transport mode at start
    InitTransportMode();

    var gv = new queryString();

    var qstr = $("#hdnOpenOrderValues").val();

    if (qstr !== undefined && qstr !== "") {

        var ord = gv.getStringValue(qstr, "ord");
        var dt = gv.getStringValue(qstr, "dt");
        var pm = gv.getStringValue(qstr, "pm");
        var tt = gv.getStringValue(qstr, "tt");
        var cl = gv.getStringValue(qstr, "cl");

    }

    if ((ord !== "" && ord !== undefined) && (dt !== undefined && dt !== "") && (pm !== undefined && pm !== "")) {



        InitOpenOrderState({
            orderNo: ord,
            dType: dt,
            pMode: pm,
            toteId: tt,
            clabel: cl
        });

        $("#hdnOpenOrderValues").val("");
    }

    //read hdn input hdnUserOption value and initialise UserOption
    var userOptionValue = $("#hdnUserOption").val();
    if (userOptionValue !== undefined && userOptionValue !== "") {
        PackOption.SelectedOption = parseInt(userOptionValue);
        $("#hdnUserOption").val("");
    }

    //Hide actions butons at the start and show them only hwhen user selects a user option.
    //also chk if user option is already set to a value.
    if (PackOption.SelectedOption === UserOptions.NoSelection)
        HideActionButtons();
    else
        ShowActionButtons();

    $(window).bind('beforeunload', function () {
        return 'This will delete all the data on the screen.';
    });


    $("#TrolleyView").click(function () {

        TrolleyViewAction();
    })

    $("#StackView").click(function () {
        PackOption.SelectedOption = UserOptions.OtherActivity;
        stackViewAction();
    })

    $("#SinglePack").click(function () {
        $("#scancodeinput").val("1");
        $("form").submit();

    })

    $("#MultiPack").click(function () {
        $("#scancodeinput").val("2");
        $("form").submit();
        ShowActionButtons();

    })

    $("#PackFromFT").click(function () {
        $("#scancodeinput").val("3");
        $("form").submit();
    })

    //    $("#scancodeinput").keydown(function (e) {
    //        keyDownAction(e);
    //    });


    //    $("#MissingItemQuery").click(function () {
    //        MissingItemAction();
    //    });


    $("#ExcessItem").click(function () {
        //PackOption.selectedOption = UserOptions.OtherActivity;
        $("#scancodeinput").val("4");
        $("form").submit();
        ExcessItemAction();

    });

    $("#RePrintDoc").click(function () {
        RePrintDocAction();
    });

    $("#RePrintDoc_2").click(function () {
        RePrintDocAction();
    });


    $("#NewBag").click(function () {

        NewBagAction();

    });


    $("#FailOrder").click(function () {

        FailOrderAction();
    });


    $("#RePack").click(function () {
        PackOption.SelectedOption = UserOptions.OtherActivity;
        //ch - 1
        ShowActionButtons();
        //
        RePackAction();
    });

    $("#EndPackProcess").click(function () {

        EndPackProcessAction();
        if (PackOption.SelectedOption === UserOptions.NoSelection)
            HideActionButtons();
        //ch - 2
        SetTransportMode(-1);
        //
    });


    $("#scancodeinput").focus();

    $("#scancodeinput").blur(function () {
        $("#scancodeinput").focus().text = "";
    });

    if ($("#hdnPackingState").val() === '') {

        PackRequest.ActionId = Action.ContainerValidation;
        InitPackState(PackRequest);
    }

    $("form").submit(function (event) {

        event.preventDefault();

        if ($("#scancodeinput").val() == "") return;

        clearMessage();

        //disable input box so that user doesnt scan before the first request finishes
        $("#scancodeinput").prop("disabled", true);

        //Check for user input.
        p = PackOption;
        if (p.SelectedOption === UserOptions.NoSelection || $("#scancodeinput").val() == "5" || $("#scancodeinput").val() == "") {
            SetUserOption();
            if (p.SelectedOption != UserOptions.MultiPack) {
                $("#scancodeinput").val("");
                //enable input box after the request finishes
                $("#scancodeinput").removeAttr("disabled");
                return false;
            }
        }
        else if (p.SelectedOption !== UserOptions.NoSelection && $("#scancodeinput").val() === "2") {
            var errorType = EType.Fail;
            var error = new ErrorMessage(errorType, "", "Packing not allowed. End Session first by pressing 5.");
            DisplayMessage(error);
            $("#scancodeinput").val("");
            //enable input box after the request finishes
            $("#scancodeinput").removeAttr("disabled");
            return false;
        }

        var obj = InitPackState($("#scancodeinput").val());

        if (obj.ActionId === Action.ExcessItem) {

            var rcode = $("#SelectReasonCode :selected").val();

            obj.ReasonCode = rcode;
        }



        if (obj.ActionId === Action.FailOrder) {

            var rcode = $("#SelectReasonCode :selected").val();

            obj.ReasonCode = rcode;
        }


        if (ValidateRequest(obj) === true) {

            MakeRequest(obj);
        }
        else $("#scancodeinput").removeAttr("disabled");



        //only bloody works in IE.Will give my blood to this later 
        $("#scancodeinput").val("");

        return false;

    }); //form submit


    //function to check and set user options
    function SetUserOption() {
        var userSelection = "";
        var errorType = EType.Success;
        if ($("#scancodeinput").val() === "1") {
            p.SelectedOption = UserOptions.SinglePack;
            userSelection = "Single Pack";
            ShowActionButtons();
            var error = new ErrorMessage(errorType, "", userSelection + " option selected.");
            DisplayMessage(error);

        }
        else if ($("#scancodeinput").val() === "2") {
            p.SelectedOption = UserOptions.MultiPack;
            userSelection = "Multi Pack";
            ShowActionButtons();
            var error = new ErrorMessage(errorType, "", userSelection + " option selected.");
            DisplayMessage(error);
        }
        else if ($("#scancodeinput").val() === "3") {
            p.SelectedOption = UserOptions.PackFromFT;
            userSelection = "Pack from Tote";
            ShowActionButtons();
            var error = new ErrorMessage(errorType, "", userSelection + " option selected. Please scan tote.");
            DisplayMessage(error);
        }
        else if ($("#scancodeinput").val() === "4") {
            p.SelectedOption = UserOptions.OtherActivity;
            userSelection = "Other Activity";
            ShowActionButtons();
            var error = new ErrorMessage(errorType, "", userSelection + " option selected.");
            DisplayMessage(error);
        }
        else if ($("#scancodeinput").val() === "5") {
            //p.SelectedOption = UserOptions.EndSession;
            userSelection = "End Session";
            EndPackProcessAction();
            if (PackOption.SelectedOption === UserOptions.NoSelection) {
                HideActionButtons();
                InitTransportMode();
                var error = new ErrorMessage(errorType, "", userSelection + " option selected.");
                DisplayMessage(error);
            }
        }
        else {
            p.SelectedOption = UserOptions.NoSelection;
            userSelection = "Invalid";
            errorType = EType.Fail;

            HideActionButtons();
            InitTransportMode();
            var error = new ErrorMessage(errorType, "", userSelection + " option selected.");
            DisplayMessage(error);
        }

        //        var error = new ErrorMessage(errorType, "", userSelection + " option selected.");
        //        DisplayMessage(error);

        //$("#scancodeinput").val("");
    };


    function MakeRequest(request) {

        clearMessage();

        ShowWaitMsg("Validating your request. Please wait . . . ");
        SetToWait(true);

        var api = new packApi();

        api.getScanItem({
            data: JSON.stringify(request, function (k, v) { return v === "" ? "" : v }),
            success: function (d) {

                ProcessResponse(d);

                PrintDocs(d);

                //
                if (d.OrderCount !== "undefined" && d.OrderCount === 0 && d.PackMode === "SIS") {
                    PackOption.SelectedOption = UserOptions.NoSelection;
                    HideActionButtons();
                }

            }, // success

            error: function () {
                showError;
                //enalbe input box after the request finishes
                //$("#scancodeinput").removeAttr("disabled");
            }
        });

        SetToWait(false);

    } //MakeRequest.


    var ddl = new lookups();

    ddl.getReasonCode({
        success: function (r) {

            $("#SelectReasonCode").get(0).options.length = 0;

            $("#SelectReasonCode").get(0).options[0] = new Option("", "-1");

            $.each(r, function (index, item) {
                $("#SelectReasonCode").get(0).options[index + 1] = new Option(item.value, item.key);
            });
        },
        error: reasonCodeeErr
    });


    function reasonCodeeErr(a) {

        $("#Error").text("Error loading Reason codes.");

    };

    function showError(a) {

        alert(a.responseText);
    };

    //    //function to hide Action buttons on screen
    //    function HideActionButtons() {
    //        $("#actions").hide();
    //        $("#options").show();
    //    };

    //    //function to show Action buttons on screen
    //    function ShowActionButtons() {
    //        $("#actions").show();
    //        $("#options").hide();
    //    };

});                    //Main functoids