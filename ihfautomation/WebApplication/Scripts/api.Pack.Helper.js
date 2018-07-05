//
// Name: api.pack.Helper.js
// Type: Java script file 
// Description: contains different helper functions for Packing.
//
//$Revision:   1.30  $
//
// Version   Date        Author    Reason
//  1.0      14/07/11    MSalman   Initial Released
//  1.1      14/07/11    MSalman   function updated.
//  1.2      14/07/11    MSalman   function updated.
//  1.3      19/07/11    MSalman   new function added.
//  1.4      21/07/11    MSalman   line feed added.
//  1.5      28/07/11    MSalman   function added.
//  1.6      01/08/11    MSalman   New Status updated.    
//  1.7      04/08/11    MSalman   Status values changed.
//  1.8      08/08/11    MSalman   new field updated.
//  1.9      12/08/11    MSalman   new function added.
//  1.10     12/08/11    MSalman   new status added.
//  1.11     18/08/11    MSalman   new type added.
//  1.12     30/08/11    MSalman   new type added.
//  1.13     01/09/11    MSalman   New Type Added.
//  1.14     05/09/11    MSalman   New function is added.
//  1.15     05/09/11    MSalman   New Type Added.
//  1.16     05/09/11    MSalman   New field is added.
//  1.17     09/09/11    MSalman   New field is added.
//  1.18     14/09/11    MSalman   New Fild is added.
//  1.19     15/09/11    MSalman   New Fild is added.
//  1.20     19/09/11    MSalman   New mode is added.
//  1.21     20/09/11    MSalman   New Error Etype is added.
//  1.22     21/09/11    MSalman   Part located type is added.
//  1.23     23/09/11    MSalman   New Function is added.
//  1.24     01/12/11    MSalman   New Function is added.
//  1.27     20/03/12    M Khan    New type added for user options
//  1.28     28/06/12    M Khan    New class for order location
//  1.30     20/10/14    S Green   New user authorisation function

var Action = {

    ContainerValidation: "CV",
    OrderItem: "OI",
    ExcessItem: "EI",
    FailOrder: "FT",
    FailOrderPack: "FO",
    RePack: "RP",
    OpenOrder: "OO",
    MissingItem: "MI"

};

var Mode = {
    MultiItemScan: "MIS",
    SingleItemScan: "SIS"
};

var TransportMode = {
    Road:"ROAD",
    Air:"AIR",
    Both:"BOTH",
    RIVan: "RIVAN"
};

var DType = {
    Demostic: "DOM",
    International: "INT"
};


var EType = {
    Fail: "FE",
    Success: "SE",
    TrolleyEnd: "TE",
    ExcessItem: "EI",
    ToteEnd: "TTE",
    OrderEnd: "OE",
    WaitMsg: "WM",
    RestrictedItem: "RI",
    AcknowledgeMsg: "ACK",
    EndPackProcess: "EPP",
    PartLocated: "PL"

};

var PackRequest = {
    Barcode: "",
    UserName: "",
    HostName: "",
    TrolleyId: "",
    ToteId: "",
    PreviousToteId: "",
    ContainerNo: "",
    ActionId: "",
    PreviousActionId: "",
    PackMode: "",
    DestinationType: "",
    ExcessItemInd: "",
    OrderNo: "",
    ReasonCode: "",
    InOrder: "F",
    EndOfTrolley: "F",
    NewBagWaitingItem: "F",
    PreviousLocation: "",
    PreviousTotalParcelBag: "",
    PreviousOrderCount: "",
    MissingItemToteId: "",
    TransportMode: ""
};

var UserOptions = {
    NoSelection:0,
    SinglePack:1,
    MultiPack:2,
    PackFromFT:3,
    OtherActivity:4,
    EndSession:5
};

var PackOption = {
    SelectedOption:0
};

var BestOrder = {
    CurrentLocation:""
}


function ErrorMessage(t, c, msg) {

    this.code = c
    this.type = jQuery.trim(t);
    this.message = msg;

}

ErrorMessage.prototype.getMsgType = function () {
    return this.type;
};

ErrorMessage.prototype.getErrorCode = function () {

    return this.code;
};

ErrorMessage.prototype.getMessageText = function () {
    return this.message;

};


var lookups = function () { };

lookups.prototype.getReasonCode = function (options) {

    var config = $.extend({
        success: function () { },
        error: function () { }
    }, options);

    $.apiCall({
        url: 'DataLoaders/DataLoader.svc/ReasonCode',
        success: function (result) { config.success(result); },
        error: config.error
    });
}

//

var packApi = function () { };
packApi.prototype.getScanItem = function (obj) {

    var config = $.extend({
        success: function () { },
        error: function () { }
    }, obj);

    $.apiCall({
        url: 'DataLoaders/DataLoader.svc/PackScanRequest',
        data: config.data,
        success: function (result) { config.success(result); },
        error: config.error
    });

};

packApi.prototype.unDockContainer = function (obj) {

    var config = $.extend({
        success: function () { },
        error: function () { }

    }, obj);

    $.apiCall({
        url: 'DataLoaders/DataLoader.svc/UnDockContainer',
        data: config.data,
        success: function (result) { config.success(result); },
        error: config.error
    });
};

packApi.prototype.rePrintDoc = function (obj) {

    var config = $.extend({
        success: function () { },
        error: function () { }

    }, obj);

    $.apiCall({
        url: 'DataLoaders/DataLoader.svc/RePrintDoc',
        data: config.data,
        success: function (result) { config.success(result); },
        error: config.error
    });


};

packApi.prototype.getMissingItem = function (obj) {

    var config = $.extend({
        success: function () { },
        error: function () { }
    }, obj);

    $.apiCall({
        url: 'DataLoaders/DataLoader.svc/GetMissingItem',
        data: config.data,
        success: function (result) { config.success(result); },
        error: config.error
    });

};

packApi.prototype.getTrolleyView = function (obj) {

    var config = $.extend({
        success: function () { },
        error: function () { }
    }, obj);

    $.apiCall({
        url: 'DataLoaders/DataLoader.svc/GetTrolleyView',
        data: config.data,
        success: function (result) { config.success(result); },
        error: config.error
    });

};

packApi.prototype.getStackView = function (obj) {

    var config = $.extend({
        success: function () { },
        error: function () { }
    }, obj);

    $.apiCall({
        url: 'DataLoaders/DataLoader.svc/StackView',
        success: function (result) { config.success(result); },
        error: config.error
    });

};

packApi.prototype.PrintDoc = function (obj) {
    var config = $.extend({
        success: function () { },
        error: function () { }

    }, obj);

    $.apiCall({
        url: 'DataLoaders/DataLoader.svc/PrintDocs',
        data: config.data,
        success: function (result) { config.success(result); },
        error: config.error
    });
}

packApi.prototype.EndPackProcess = function (obj) {
    var config = $.extend({
        success: function () { },
        error: function () { }

    }, obj);

    $.apiCall({
        url: 'DataLoaders/DataLoader.svc/EndPackProcess',
        data: config.data,
        success: function (result) { config.success(result); },
        error: config.error
    });
}

packApi.prototype.UpdateStackSelection = function (obj) {
    var config = $.extend({
        success: function () { },
        error: function () { }

    }, obj);

    $.apiCall({
        url: 'DataLoaders/DataLoader.svc/UpdateStackSelection',
        data: config.data,
        success: function (result) { config.success(result); },
        error: config.error
    });
}

packApi.prototype.UserPrintAuth = function (obj) {
    var config = $.extend({
        success: function () { },
        error: function () { }

    }, obj);

    $.apiCall({
        url: 'DataLoaders/DataLoader.svc/MasterPacker',
        data: config.data,
        success: function (result) { config.success(result); },
        error: config.error
    });
}

var queryString = function () { };

queryString.prototype.getQueryStringValue = function (variable) {

    var query = window.location.hash.substring(1);
    var vars = query.split("&");
    for (var i = 0; i < vars.length; i++) {
        var pair = vars[i].split("=");
        if (pair[0] == variable) {
            return pair[1];
        }
    }

};


queryString.prototype.getStringValue = function (str, p) {

    var vars = str.split("&");

    for (var i = 0; i < vars.length; i++) {
        var pair = vars[i].split("=");
        if (pair[0] == p) {
            return pair[1];
        }
    }

};



var fnCapLock = function () { };

fnCapLock.prototype.isCapLockOn = function (e) {

    kc = e.keyCode ? e.keyCode : e.which;

    sk = e.shiftKey ? e.shiftKey : ((kc == 16) ? true : false);

    if (((kc >= 65 && kc <= 90) && !sk) || ((kc >= 97 && kc <= 122) && sk))

        return true;
    else

        return false;
}

function stack(chuteId, stackLabel, preConfigured, trolleyId, packstationId) {
    this.ChuteId       = chuteId,
    this.StackLabel    = stackLabel,
    this.PreConfigured = preConfigured,
    this.PackstationId = packstationId,
    this.TrolleyId     = trolleyId
};

var stackArray = new Array();
