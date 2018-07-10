// Name: returns.js
// Type: java script file 
// Description: Javascript code for the new returns screen
//   
//
//$Revision:   1.34  $
//
// Version   Date        Author     Reason
//  1.0      14/02/18    A Petrescu Initial Revision
//  1.1      16/02/18    A Petrescu Bug fixes, error handling
//  1.2      19/02/18    A Petrescu Integration of Service2, minor UI fixes.
//  1.3      19/02/18    A Petrescu Bug fixes, service 2, error reporting on oracle crashes.
//  1.4      19/02/18    A Petrescu Fixes for IE8
//  1.5      20/02/18    A Petrescu UI Fixes
//  1.6      22/02/18    A Petrescu Virtual Console changes.
//  1.7      26/02/18    M Cackett  Make selected line blue.  Added banner.
//  1.8      26/02/18    A Petrescu Showing error message when violating workflow;
//                                  Disabling parts of UI;
//                                  Changed scan item/order label depending on state
//  1.9      26/02/18    A Petrescu Added header, missed in the previous version
//  1.10     26/02/18    A Petrescu Avoid sending the customer return message when on any other return action
//  1.11     01/03/18    A Petrescu Status fixes. Added logic to disable all exception statuses.
//  1.12     01/03/18    A Petrescu Calling location micro service story
//  1.13     01/03/18    M Cackett  Added partial success function to ReturnService to handle print failure.
//                       J Duru
//  1.14     06/03/18    A Petrescu Fixed crash in javascript when unselecting row
//  1.15     14/03/18    A Petrescu Disabled reject when accepting; redirected to login when unauthenticated
//  1.16     22/03/18    M Cackett  Display alert message before redirecting to login page.
//  1.17     22/03/18    M Cackett,AP Hidden checkboxes, displayed reject button after selecting row
//  1.18     23/03/18    A Petrescu Fixed focusing when opening the page first time and after accept and print.
//  1.19     23/03/18    A Petrescu Added enter event back to $scope
//  1.20     26/03/18    A Petrescu Fixed bug when navigating back from customer search page
//  1.21     27/03/18    A Petrescu  Decoupled messaging
//  1.22     27/03/18    A Petrescu  2 guys returning the same item
//  1.23     27/03/18    A Petrescu  Fixed minor bug.
//  1.24     11/04/18    M Cackett   Added Re-print Funactionality
//                       S Remedios
//  1.25     23/04/18    M Cackett  Fixed error when reprint clicked by user who is not a supervisor
//  1.26     23/04/18    A Petrescu Avoid accept or reprint when scanning new item
//                       S Patel
//  1.26.1.0 24/04/18    M Cackett  Authorisation dialog
//                       S Remedios
//                       A Petrescu
//  1.27     23/04/18    A Petrescu Place focus onto Scan Order/Item field after Print button pressed
//                       S Patel
//  1.28     25/04/18    M Cackett  Merged v1.26.1.0 with v1.27
//                       A Petrescu
//  1.29     25/04/18    S Patel, A Petrescu - focus change when print or reject +cancel
//  1.30     25/04/18    M Cackett  Bug fixes
//                       A Petrescu
//  1.31     26/04/18    A Petrescu Disabled selecting another item when the current one is in progress.
//  1.32     26/04/18    A Petrescu Added function for the check text
//  1.33     26/04/18    A Petrescu Disabling reprint when selecting a row.
//  1.34     28/06/18    A Petrescu Disabled auto checkbox selection when pressing anything other than S


var initializeAngular = function () {
    var app = angular.module('IHFReturns', []);


    var uiWrapper = app.factory('uiWrapper', ['$rootScope', function ($rootScope) {

        //Initializing JQuery controls
        var orderNumberHiddenField = $('#ctl00_ContentPlaceHolder1_tbSearchByCustomerOrderNumber');
        var scanTextField = $('#scanText');


        //Use JQuery event, because Angular one won't work on IE8
        scanTextField.keypress(function (event) {
            if (event.which === 13) {
                setTimeout(function () {
                    $rootScope.$broadcast(MessageTypes.EnterPressedInScanFieldEvent, {});
                }, 50);
            }
        });


        return {
            removeLeaveConfirmation: function () {
                window.onbeforeunload = null;
            },
            alert: function (message) {
                alert(message);
            },
            confirm: function (message) {
                return confirm(message);
            },

            navigateTo: function (url) {
                window.location.replace(url);
            },

            focusOnReasonDropdown: function (itemNumber) {
                var dropdownId = "#reasonDropdown-" + itemNumber;
                setTimeout(function () { $(dropdownId).focus(); }, 20);
            },
            focusOnScanTextField: function () {
                scanTextField.focus();
            },
            focusOnCustomerServiceMessage: function (itemNumber) {
                var customerServiceMsgKey = "#customerServiceMsg-" + itemNumber;
                setTimeout(function () { $(customerServiceMsgKey).focus(); }, 20);
            },
            focusOnAcceptButton: function (itemNumber) {
                var btnSearchKey = "#btn-" + itemNumber;
                setTimeout(function () { $(btnSearchKey).focus(); }, 20);
            },

            getOrderNumberHiddenField: function () {
                return orderNumberHiddenField.val().trim();
            },
            setOrderNumberHiddenField: function (value) {
                orderNumberHiddenField.val(value);
            },
            printScreen: function () {
                window.print();
            },
            focusOnAuthcodeInput: function () {
                setTimeout(function () {
                    $('#authCodeInput').focus();
                }, 50); // After 420 ms
            },
            showAuthDialog: function () {
                var pThis = this;
                $("#dialog-PrintAuth").dialog("destroy");

                $("#dialog-PrintAuth").dialog({
                    resizable: false,
                    height: 160,
                    width: 370,
                    modal: true,
                    closeOnEscape: true,
                    open: function () {
                        pThis.focusOnAuthcodeInput();
                    },
                    close: function () {
                        $rootScope.$broadcast(MessageTypes.AuthorisationDialogClosed);
                    }
                });
            },
            hideAuthDialog: function () {
                $('#dialog-PrintAuth').dialog('close');
            }

        };
    } ]);

    returnServiceWrapperInitializer(app);




    app.controller(
        'ReturnsController',
        ['$scope', 'uiWrapper', 'returnsServiceWrapper',
        function ($scope, uiWrapper, returnsServiceWrapper) {



            var actions = _.toArray(refundParams.actions);
            var defaultAction = { Id: -1, Description: '-- Select Reason --', DisabledForStatus: [] };
            actions.push(defaultAction);
            var lastKeypressTimestampOnAcceptButton = 0;


            //Data
            $scope.orderData = null;
            $scope.searchKey = "";
            $scope.searchInProgress = false;
            $scope.reprintInProgress = false;
            $scope.checkedItem = null;
            $scope.errorMessage = null;
            //End of Data

            //Presentation Functions
            $scope.getSearchboxLabel = function () {
                if ($scope.orderData && $scope.orderData.Items) {
                    return 'Scan Item/Order:';
                } else {
                    return 'Scan Order/Package:';
                }
            }

            //function to disable the "Enter button", the "Search Key textbox" and the Reject button
            $scope.disableEnterSearchAndReject = function () {
                return $scope.checkedItem && $scope.checkedItem.acceptInProgress;
            }

            $scope.isFinalReturnStatus = function (item) {
                if (refundParams.isStoreUser) {
                    //from store, can only process items with reasoncode 0 (null in the refcursor)
                    return item.ReasonCode !== 0;
                } else {
                    //from DC, can process items with reasoncode 100 or 0 (null in refcursor)
                    return !_.contains([0, 100], item.ReasonCode);
                }
            }

            $scope.getRowCssClass = function (item) {
                if ($scope.isFinalReturnStatus(item)) return "item_chocolate";

                if (item.ReasonCode === 100) return "item_yellow";

                if (item.ReasonCode === 0) {
                    if (!item.checked) return "item_bisque";
                    if (item.newAction && item.newAction.Id === -1) return "item_azure";
                    return "item_beige";
                }

                return "";
            }

            $scope.getItemsToProcess = function () {
                var noActionCount = _.filter($scope.orderData.Items, function (item) { return item.ReasonCode === 0; }).length;
                var dcCbrReturns = refundParams.isStoreUser ? 0 : _.filter($scope.orderData.Items, function (item) { return item.ReasonCode === 100; }).length;

                return noActionCount + dcCbrReturns;
            }

            //compute reasons that apply to current item; should cater for store, cbr, dc
            $scope.getApplicableReasonCodes = function (item) {
                return _.filter(actions, function (action) {
                    return (action.Id === -1 || action.IsStoreAction === refundParams.isStoreUser) && !_.contains(action.DisabledForStatus, item.ReasonCode);
                });
            }

            $scope.disableAccept = function (item) {
                if (item.LPN) {
                    return item.PutawayLoc || $scope.reprintInProgress || $scope.checkedItem;
                } else {
                    return !item.checked || !item.newAction || item.newAction.Id === -1 || item.acceptInProgress;
                }
            }

            $scope.acceptReprint = function (item) {
                if (item.LPN) {
                    return "Reprint Label";
                }
                return "Accept & Print";
            }

            $scope.showRejectButton = function () {
                if (!$scope.orderData || !$scope.orderData.Items) {
                    return false;
                }

                return $scope.checkedItem;
            }

            $scope.isCustomerServiceReturn = function (item) {
                return item.newAction && item.newAction.Id === 70;
            }

            var itemAndActionSelected = function () {
                return $scope.checkedItem && $scope.checkedItem.newAction.Id !== -1;
            }

            $scope.isCheckboxDisabled = function (item) {
                return $scope.isFinalReturnStatus(item)                             //already returned
                    || ($scope.checkedItem && $scope.checkedItem.acceptInProgress)  //processing accept on any item
                    || (item !== $scope.checkedItem && itemAndActionSelected());    //another item is selected, and it has reason code
            }

            $scope.showRowRoller = function (item) {
                return item.acceptInProgress || item.reprintInProgress;
            }

            $scope.putawayCheckText = function (item) {
                if (item.PutawayLoc) {
                    return '\u221A';
                } else {
                    return '';
                }
            }
            //END OF Presentation Functions

            //UI Events
            $scope.checkboxClicked = function (item) {
                $scope.errorMessage = "";

                _.each($scope.orderData.Items, function (other) {
                    if (item !== other) {
                        other.checked = false;
                        other.newAction = null;
                    }
                });

                if (!item.checked) {
                    //the item will be checked;
                    item.newAction = defaultAction;
                    $scope.checkedItem = item;

                    uiWrapper.focusOnReasonDropdown(item.ItemNumber);
                } else {
                    //the item will be unchecked
                    $scope.checkedItem = null;
                    item.newAction = null;
                    item.customerServiceMessage = null;

                    uiWrapper.focusOnScanTextField();
                }
            }


            $scope.focusOnPreviouslySelectedItem = function () {
                if (!$scope.checkedItem) {
                    uiWrapper.focusOnScanTextField();
                } else if ($scope.checkedItem.newAction.Id === -1) {
                    uiWrapper.focusOnReasonDropdown($scope.checkedItem.ItemNumber);
                } else if ($scope.checkedItem.newAction.Id === 70) {
                    uiWrapper.focusOnCustomerServiceMessage($scope.checkedItem.ItemNumber);
                } else {
                    uiWrapper.focusOnAcceptButton($scope.checkedItem.ItemNumber);
                }
            }

            //When PRINT (top level) button pressed
            $scope.printClicked = function () {
                uiWrapper.printScreen();
                $scope.focusOnPreviouslySelectedItem();
            }


            //search for SKU function
            //returns true if SKU search was successful, false otherwise
            var searchForSku = function () {
                if (!$scope.orderData || !$scope.orderData.Items) {
                    return false;
                }

                var cleansedSearchKey = $scope.searchKey.replace(/[^0-9]+/, '');

                var matchItemFunction = function (item) {
                    return item.Sku.toString() === cleansedSearchKey || item.SkuBarcode === cleansedSearchKey;
                }

                if ($scope.checkedItem) {
                    if (matchItemFunction($scope.checkedItem)) {
                        $scope.errorMessage = "SKU " + $scope.searchKey + " already scanned.";
                    } else {
                        $scope.errorMessage = 'Unsaved changes. Please complete current item or reject.';
                    }

                    $scope.focusOnPreviouslySelectedItem();
                    $scope.searchKey = '';
                    return true;
                }

                var scannedItem = _.find($scope.orderData.Items, function (item) {
                    return matchItemFunction(item) && !$scope.isFinalReturnStatus(item);
                });

                if (scannedItem) {
                    $scope.checkboxClicked(scannedItem);
                    scannedItem.checked = true;
                    $scope.searchKey = "";

                    return true;
                }

                //search again, without isFinalReturnStatus condition, display error if something comes up
                scannedItem = _.find($scope.orderData.Items, function (item) {
                    return item.Sku.toString() === cleansedSearchKey || item.SkuBarcode === cleansedSearchKey;
                });

                if (scannedItem) {
                    //display message, return
                    $scope.errorMessage = "SKU " + $scope.searchKey + " already processed.";
                    $scope.searchKey = "";
                    return true;
                }

                return false;
            }

            var searchForOrder = function (orderRefresh) {
                if (!orderRefresh && $scope.orderData && _.find($scope.orderData.Items, function (item) {
                    return item.checked && item.newAction.Id !== -1;
                })) {
                    $scope.errorMessage = 'Unsaved changes. Please complete current item or reject.';
                    $scope.searchKey = '';
                    return;
                }

                $scope.searchInProgress = true;
                returnsServiceWrapper.searchOrderConsignment($scope.searchKey);
            }

            //Call with orderRefresh=true when you do not want to search in SKUs
            //neither validate that an item is in progress.
            $scope.enterPressed = function (orderRefresh) {
                //search in the current loaded order
                $scope.errorMessage = '';

                if (!orderRefresh && searchForSku()) {
                    return;
                }

                searchForOrder(orderRefresh);
            }

            $scope.reasonSelected = function (item) {
                if (item.newAction.Id === -1) {
                    return;
                }

                if (item.newAction.Id === 70) {
                    //If it's customer service return -> focus on the customerServiceMessage textarea
                    uiWrapper.focusOnCustomerServiceMessage(item.ItemNumber);
                } else {
                    //Introduce a delay, because button is initially disabled, and we need Angular to run a binding loop
                    uiWrapper.focusOnAcceptButton(item.ItemNumber);
                }
            }

            $scope.rejectClicked = function () {
                if (!uiWrapper.confirm('Are you sure?')) {
                    $scope.focusOnPreviouslySelectedItem();
                    return;
                }

                $scope.checkedItem.newAction = null;
                $scope.checkedItem.checked = false;
                $scope.checkedItem.customerServiceMessage = null;
                $scope.checkedItem = null;
                $scope.errorMessage = null;

                uiWrapper.focusOnScanTextField();
            }

            $scope.acceptOrReprintClicked = function (item) {
                var timestamp = new Date().getTime();
                if (timestamp - lastKeypressTimestampOnAcceptButton < 200) {
                    //might have scanned on accept
                    virtualConsole.log('might have scanned on accept');
                    return;
                }

                if (!item.LPN) {
                    item.acceptInProgress = true;
                    var requestData = {
                        ItemNumber: item.ItemNumber,
                        OrderNumber: $scope.orderData.OrderNumber,
                        ActionCode: item.newAction.Id,
                        TaskDescription: $scope.isCustomerServiceReturn(item) ? item.customerServiceMessage : null,
                        CustomerURN: $scope.orderData.CustomerURN,
                        Parcel_Scanned_Ind: $scope.orderData.Parcel_Scanned_Ind,
                        Sku: item.Sku
                    };

                    returnsServiceWrapper.returnItem(requestData, item);
                }
                else {
                    if (refundParams.supervisor) {
                        item.reprintInProgress = true;
                        $scope.reprintInProgress = true;
                        returnsServiceWrapper.ReprintLPN(item);
                    }
                    else {
                        $scope.$broadcast(MessageTypes.RequireAthorisation, { item: item });
                    }
                }
            }

            $scope.keyPressedInAcceptButton = function ($event) {
                if ($event.keyCode === 13) {
                    return;
                }

                lastKeypressTimestampOnAcceptButton = new Date().getTime();
            }

            $scope.keypressedInReasonDropdown = function ($event) {
                if ($event.keyCode !== 83 && $event.keyCode !== 115) {
                    //only S for sellable is allowed to be clicked.
                    $event.preventDefault();
                }
            }
            //END OF UI Events

            //Decoupled events
            $scope.$on(MessageTypes.EnterPressedInScanFieldEvent, function (event, data) {
                $scope.enterPressed(false);
                $scope.$apply();
            });

            $scope.$on(MessageTypes.OrderOrConsignmentFound, function (event, data) {
                uiWrapper.setOrderNumberHiddenField($scope.searchKey);
                $scope.orderData = data;
                $scope.searchInProgress = false;
                $scope.searchKey = "";
                $scope.checkedItem = null;
                $scope.$apply();

                uiWrapper.focusOnScanTextField();
            });

            $scope.$on(MessageTypes.SearchFailed, function (event, data) {
                $scope.errorMessage = data.Message;
                $scope.searchInProgress = false;
                $scope.searchKey = "";
                $scope.$apply();

                uiWrapper.focusOnScanTextField();
            });

            $scope.$on(MessageTypes.LpnSuccessfullyReprinted, function (event, data) {
                data.Item.reprintInProgress = false;
                $scope.reprintInProgress = false;
                $scope.$apply();

                uiWrapper.focusOnScanTextField();
            });

            $scope.$on(MessageTypes.ItemSuccessfullyReturned, function (event, data) {

                //here, might need to search by consignment, if original search was by consignment
                data.Item.newAction = defaultAction;
                $scope.searchKey = $scope.orderData.OrderNumber.toString();
                $scope.enterPressed(true);
                $scope.$apply();

                uiWrapper.focusOnScanTextField();
            });

            $scope.$on(MessageTypes.ServiceFailureWithRedirect, function (event, data) {
                uiWrapper.navigateTo(
                    '/pages/error.aspx?exceptionmessage=OrderNumber - ' + $scope.orderData.OrderNumber +
                    '; ItemNumber - ' + data.Item.ItemNumber +
                    '; Message - ' + data.Message + '&aspxerrorpath=/pages/returns/ReturnsDC.aspx');
            });

            $scope.$on(MessageTypes.ServiceFailureWithShowMessage, function (event, data) {
                //here, might need to search by consignment, if original search was by consignment
                //this might get hit from
                //     - search order/consignment
                //     - reprint
                //     - accept and print
                // Will need to reset state nicely for all of those cases
                if (data && data.Item) {
                    data.Item.newAction = defaultAction;
                    data.Item.reprintInProgress = false;
                }
                $scope.searchKey = $scope.orderData.OrderNumber.toString();
                $scope.reprintInProgress = false;
                $scope.enterPressed(true);
                $scope.errorMessage = data.Message;
                $scope.$apply();

                uiWrapper.focusOnScanTextField();
            });

            $scope.$on(MessageTypes.ServiceFailureWithAlert, function (event, data) {
                //show alert
                uiWrapper.alert('The item ' + data.Item.Sku + " that you are trying to process has been returned by another operative. Please select 'OK' to refresh the order and reprocess the item.");

                //refresh order
                data.Item.newAction = defaultAction;
                $scope.searchKey = $scope.orderData.OrderNumber.toString();
                $scope.enterPressed(true);
                $scope.$apply();

                uiWrapper.focusOnScanTextField();
            });

            $scope.$on(MessageTypes.UnauthenticatedError, function () {
                uiWrapper.removeLeaveConfirmation();

                if ($scope.searchKey) {
                    uiWrapper.alert('Your order ' + $scope.searchKey +
                            ' has not been processed as your session has expired.  Click OK to go back to the log in screen');
                }
                else if ($scope.orderData && $scope.orderData.OrderNumber) {
                    uiWrapper.alert('Your item for order ' + $scope.orderData.OrderNumber +
                            ' has not been processed as your session has expired.  Click OK to go back to the log in screen');
                }
                else {
                    uiWrapper.alert('Your session has expired. Click OK to go back to the log in screen');
                }

                uiWrapper.navigateTo('/pages/login.aspx?ReturnUrl=%2fpages%2fReturns%2fReturnsDC.aspx');
            });
            $scope.$on(MessageTypes.AuthorisationSuccess, function (event, data) {
                data.item.reprintInProgress = true;
                $scope.reprintInProgress = true;
                $scope.$apply();
                returnsServiceWrapper.ReprintLPN(data.item);
            });
            //END OF Decoupled events


            //Initalization when navigating from Customer Search


            var custSearchOrderNumber = uiWrapper.getOrderNumberHiddenField();
            if (custSearchOrderNumber !== '') {
                $scope.searchKey = custSearchOrderNumber;
                $scope.enterPressed(true);
            }

            //focusing on scan item when page
            uiWrapper.focusOnScanTextField();
        } ]
     );
    authDlgInitializer(app);
}

virtualConsole.log('InitializingAngular');

initializeAngular();

var firstInitialize = true;

if (typeof Sys !== 'undefined') {
    Sys.Application.add_load(function loadHandler() {
        virtualConsole.log('loadHandler called');

        var returnsTabView = $('#ctl00_ContentPlaceHolder1_RadPageView1');

        if (!returnsTabView.hasClass('rmpHiddenView')) {
            if (firstInitialize) {
                firstInitialize = false;
            } else {
                virtualConsole.log('BootstrappingAngular');
                angular.bootstrap($('#topDiv'), ['IHFReturns']);
            }
        }
    });
}
