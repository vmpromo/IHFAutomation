// Name: return.tests.js
// Type: java script file 
// Description: Javascript code for the automated unit tests
//   
//
//$Revision:   1.14  $
//
// Version   Date        Author     Reason
//  1.0      27/03/18    A Petrescu Initial Revision
//  1.1      28/03/18    A Petrescu Additional tests
//  1.2      28/03/18    A Petrescu Added more tests
//  1.3      28/03/18    M Cackett  Added more checks, including one half finished one near the end.
//  1.4      29/03/18    A Petrescu Last 2 tests, added 1 more.
//  1.5      17/04/18    M Cackett  Added tests for Reprint functionality, added banner.
//  1.6      23/04/18    A Petrescu, S Patel  Added 4 new test cases for avoiding accept when scanning new item
//  1.7      23/04/18    A Petrescu, S Patel  Added tests for print button
//  1.8      24/04/18    A Petrescu, M Cackett  Added test cases for authorisation.
//  1.9      25/04/18    A Petrescu, M Cackett  Renamed acceptClicked to acceptOrReprintClicked
//  1.10     25/04/18    S Patel, A Petrescu - focus change when print or reject +cancel
//  1.11     26/04/18    A Petrescu Disabled selecting another item when the current one is in progress.
//  1.12     26/04/18    A Petrescu Added function for the check text
//  1.13     26/04/18    A Petrescu Test for disabling reprint when selecting a row.


describe('Returns Angular Module', function () {

    beforeEach(module('IHFReturns'));

    var $controllerBuilder;
    var $scopeBuilder;

    var uiWrapperMock;
    var returnsServiceWrapperMock;

    var orderData;

    beforeEach(inject(function ($rootScope, _$controller_) {
        $controllerBuilder = _$controller_;
        $scopeBuilder = $rootScope;

        window.refundParams = {
            isStoreUser: false,
            storeId: null,
            tillId: null,
            userId: null,
            actions: [
                { Id: 40, Description: "Damaged return", DisabledForStatus: [], IsStoreAction: false },
                { Id: 30, Description: "Saleable return", DisabledForStatus: [], IsStoreAction: false },
                { Id: 50, Description: "Mispick return", DisabledForStatus: [], IsStoreAction: false },
                { Id: 70, Description: "Customer service return", DisabledForStatus: [100], IsStoreAction: false },
                { Id: 100, Description: "Refunded return", DisabledForStatus: [], IsStoreAction: true }
           ],
            supervisor: false
        };

        orderData = {
            CustomerName: "Mandy Harbord",
            CustomerURN: "0000682111",
            Items: [
                { Description: "4 button linen trouser - 10R - White", ItemNumber: 373510, ReasonCode: 0, ReasonDescription: null, Sku: 4037852, SkuBarcode: '2105040216768', LPN: null, PutawayLoc: null },
                { Description: "4 button linen trouser - 10R - White", ItemNumber: 373511, ReasonCode: 0, ReasonDescription: null, Sku: 4037852, SkuBarcode: '2105040216768', LPN: 'KM000000386', PutawayLoc: null },
                { Description: "4 button linen trouser - 12R - White", ItemNumber: 373512, ReasonCode: 100, ReasonDescription: 'Refunded return', Sku: 4037853, SkuBarcode: '2105040216769', LPN: 'KD000000384', PutawayLoc: null },
                { Description: "4 button linen trouser - 12R - White", ItemNumber: 373513, ReasonCode: 100, ReasonDescription: 'Refunded return', Sku: 4037853, SkuBarcode: '2105040216769', LPN: 'KS000000383', PutawayLoc: 'JP 147D' },
                { Description: "sailor trouser - 10R - Cream", ItemNumber: 373522, ReasonCode: 30, ReasonDescription: 'Some status 1', Sku: 4053094, SkuBarcode: '2105040216769', LPN: null, PutawayLoc: null },
                { Description: "sailor trouser - 12R - Cream", ItemNumber: 373523, ReasonCode: 40, ReasonDescription: 'Some status 2', Sku: 4053095, SkuBarcode: '2105040216770', LPN: null, PutawayLoc: null },
                { Description: "sailor trouser - 12R - Cream", ItemNumber: 373524, ReasonCode: 50, ReasonDescription: 'Some status 3', Sku: 4053096, SkuBarcode: '2105040216771', LPN: null, PutawayLoc: null },
                { Description: "sailor trouser - 12R - Cream", ItemNumber: 373525, ReasonCode: 70, ReasonDescription: 'Some status 4', Sku: 4053097, SkuBarcode: '2105040216772', LPN: null, PutawayLoc: null },
                { Description: "sailor trouser - 12R - Cream", ItemNumber: 373526, ReasonCode: 10, ReasonDescription: 'Some status 5', Sku: 4053098, SkuBarcode: '2105040216773', LPN: null, PutawayLoc: null },
                { Description: "sailor trouser - 12R - Cream", ItemNumber: 373527, ReasonCode: 15, ReasonDescription: 'Some status 6', Sku: 4053099, SkuBarcode: '2105040216774', LPN: null, PutawayLoc: null },
                { Description: "sailor trouser - 12R - Cream", ItemNumber: 373528, ReasonCode: 20, ReasonDescription: 'Some status 7', Sku: 4053100, SkuBarcode: '2105040216775', LPN: null, PutawayLoc: null }
            ],
            OrderDate: "30-Jun-2008",
            OrderNumber: 98788,
            Parcel_Scanned_Ind: "F",
            PostCode: "NR218PH"
        };

        uiWrapperMock = {
            getOrderNumberHiddenField: function () { return ''; },
            setOrderNumberHiddenField: function (value) { },
            focusOnScanTextField: function () { },
            focusOnReasonDropdown: function () { },
            focusOnAcceptButton: function () { },
            focusOnCustomerServiceMessage: function () { },
            removeLeaveConfirmation: function () { },
            alert: function () { },
            confirm: function (message) { },
            navigateTo: function () { },
            printScreen: function () { }
        };

        returnsServiceWrapperMock = {
            searchOrderConsignment: function (searchKey) { },
            returnItem: function (requestData, item) { },
            ReprintLPN: function (item) { }
        };

        spyOn(uiWrapperMock, 'focusOnScanTextField');
        spyOn(uiWrapperMock, 'setOrderNumberHiddenField');
        spyOn(uiWrapperMock, 'removeLeaveConfirmation');
        spyOn(uiWrapperMock, 'alert');
        spyOn(uiWrapperMock, 'navigateTo');
        spyOn(uiWrapperMock, 'focusOnReasonDropdown');
        spyOn(returnsServiceWrapperMock, 'searchOrderConsignment');
    }));


    describe('Initialization scenarios', function () {

        it('Sets initial state correctly', function () {

            var scope = $scopeBuilder.$new();
            var controller = $controllerBuilder('ReturnsController', { $scope: scope, uiWrapper: uiWrapperMock, returnsServiceWrapper: {} });


            expect(scope.orderData).toBe(null);
            expect(scope.searchKey).toBe('');
            expect(scope.searchInProgress).toBe(false);
            expect(scope.reprintInProgress).toBe(false);
            expect(scope.checkedItem).toBe(null);

            expect(uiWrapperMock.focusOnScanTextField).toHaveBeenCalledWith();
        });


        it('Calls search service when navigating from search customer tab', function () {

            spyOn(uiWrapperMock, 'getOrderNumberHiddenField').and.returnValue('12345');

            var scope = $scopeBuilder.$new();
            var controller = $controllerBuilder('ReturnsController', { $scope: scope, uiWrapper: uiWrapperMock, returnsServiceWrapper: returnsServiceWrapperMock });


            expect(scope.orderData).toBe(null);
            expect(scope.searchKey).toBe('12345');
            expect(scope.searchInProgress).toBe(true);
            expect(scope.reprintInProgress).toBe(false);
            expect(scope.checkedItem).toBe(null);

            expect(returnsServiceWrapperMock.searchOrderConsignment).toHaveBeenCalledWith('12345');
        });
    });


    describe('Search scenarios', function () {
        var scope, controller;

        beforeEach(function () {
            scope = $scopeBuilder.$new();
            controller = $controllerBuilder('ReturnsController', { $scope: scope, uiWrapper: uiWrapperMock, returnsServiceWrapper: {} });
        });

        it('Loads search results correctly when search succeeds', function () {
            scope.searchKey = '98788';

            scope.$broadcast(MessageTypes.OrderOrConsignmentFound, orderData);

            expect(scope.orderData).toEqual(orderData);
            expect(scope.searchKey).toBe('');
            expect(scope.errorMessage).toBe(null);
            expect(uiWrapperMock.focusOnScanTextField).toHaveBeenCalled();
            expect(uiWrapperMock.setOrderNumberHiddenField).toHaveBeenCalledWith('98788')
        });

        it('Fails to search for order or consignment', function () {
            scope.searchKey = '98788';
            scope.orderData = orderData;

            var message = 'Order or consignment not found';
            scope.$broadcast(MessageTypes.SearchFailed, { Message: message });

            expect(scope.orderData).toEqual(orderData);
            expect(scope.errorMessage).toBe(message);
            expect(scope.searchKey).toBe('');
            expect(uiWrapperMock.focusOnScanTextField).toHaveBeenCalled();
        });

        it('Deals with unauthenticated correctly', function () {
            scope.searchKey = '98788'
            scope.$broadcast(MessageTypes.UnauthenticatedError);

            expect(uiWrapperMock.removeLeaveConfirmation).toHaveBeenCalled();
            expect(uiWrapperMock.alert).toHaveBeenCalled();
            expect(uiWrapperMock.navigateTo).toHaveBeenCalledWith('/pages/login.aspx?ReturnUrl=%2fpages%2fReturns%2fReturnsDC.aspx');
        });
    });

    describe('Enter pressed scenarios', function () {
        var scope, controller;

        beforeEach(function () {
            scope = $scopeBuilder.$new();
            controller = $controllerBuilder('ReturnsController', { $scope: scope, uiWrapper: uiWrapperMock, returnsServiceWrapper: returnsServiceWrapperMock });
        });

        it('Enter pressed when no order loaded calls service', function () {
            scope.searchKey = '123456';
            scope.enterPressed(false);
            expect(returnsServiceWrapperMock.searchOrderConsignment).toHaveBeenCalledWith('123456');
            expect(scope.searchInProgress).toBe(true);
            expect(scope.checkedItem).toBe(null);
        });

        it('Enter pressed when searching sku locates row', function () {
            scope.orderData = orderData;
            scope.searchKey = '4037852';
            scope.enterPressed(false);

            expect(scope.orderData.Items[0].checked).toBe(true);
            expect(scope.orderData.Items[0].newAction.Id).toBe(-1);
            expect(scope.checkedItem).toBe(scope.orderData.Items[0]);
            expect(uiWrapperMock.focusOnReasonDropdown).toHaveBeenCalledWith(373510);
            expect(returnsServiceWrapperMock.searchOrderConsignment).not.toHaveBeenCalled();
        });

        it('Enter pressed when searching skuBarcode locates row', function () {
            scope.orderData = orderData;
            scope.searchKey = '2105040216768';
            scope.enterPressed(false);

            expect(scope.orderData.Items[0].checked).toBe(true);
            expect(scope.orderData.Items[0].newAction.Id).toBe(-1);
            expect(scope.checkedItem).toBe(scope.orderData.Items[0]);

            expect(returnsServiceWrapperMock.searchOrderConsignment).not.toHaveBeenCalled();
        });


        it('Enter pressed when searching sku not found in order calls service', function () {
            scope.orderData = orderData;
            scope.searchKey = '12345678';
            scope.enterPressed(false);

            expect(returnsServiceWrapperMock.searchOrderConsignment).toHaveBeenCalledWith('12345678');
            expect(scope.searchInProgress).toBe(true);
        });

        it('Fails to select row already returned', function () {
            scope.orderData = orderData;
            scope.searchKey = '4053094';
            scope.enterPressed(false);

            expect(scope.checkedItem).toBe(null);
            expect(_.find(scope.orderData, function (item) { return item.checked; })).toBeUndefined();
            expect(scope.errorMessage).toBe('SKU 4053094 already processed.');
        });

        it('Fails to select new row when current not completed', function () {
            scope.orderData = orderData;
            orderData.Items[0].checked = true;
            orderData.Items[0].newAction = { Id: -1 };
            scope.checkedItem = orderData.Items[0];
            scope.searchKey = '34563456';
            scope.enterPressed(false);

            expect(scope.checkedItem).toBe(orderData.Items[0]);
            expect(orderData.Items[0].checked).toBe(true);
            expect(scope.errorMessage).toBe('Unsaved changes. Please complete current item or reject.');
        });

        it('Shows error when scanning item twice', function () {
            scope.orderData = orderData;
            scope.searchKey = '4037853';
            scope.enterPressed(false);

            expect(scope.checkedItem).toBe(orderData.Items[2]);
            expect(scope.orderData.Items[2].checked).toBe(true);
            expect(scope.errorMessage).toBe('');

            scope.searchKey = '4037853';
            scope.enterPressed(false);

            expect(scope.checkedItem).toBe(orderData.Items[2]);
            expect(scope.orderData.Items[2].checked).toBe(true);
            expect(scope.errorMessage).toBe('SKU 4037853 already scanned.');
        });

        it('Clears selection when clicking reject', function () {
            spyOn(uiWrapperMock, 'confirm').and.returnValue(true);

            scope.orderData = orderData;
            scope.checkedItem = orderData.Items[0];
            orderData.Items[0].checked = true;
            orderData.Items[0].newAction = window.refundParams.actions[1];
            scope.errorMessage = 'some error';


            scope.rejectClicked();

            expect(scope.checkedItem).toBe(null);
            expect(orderData.Items[0].checked).toBe(false);
            expect(orderData.Items[0].newAction).toBe(null);
            expect(uiWrapperMock.confirm).toHaveBeenCalled();
        });

        it('Doesnt clear selection when reject and cancel', function () {
            spyOn(uiWrapperMock, 'confirm').and.returnValue(false);

            scope.orderData = orderData;
            scope.checkedItem = orderData.Items[0];
            orderData.Items[0].checked = true;
            orderData.Items[0].newAction = window.refundParams.actions[1];
            scope.errorMessage = 'some error';

            scope.rejectClicked();

            expect(scope.checkedItem).toBe(orderData.Items[0]);
            expect(orderData.Items[0].checked).toBe(true);
            expect(orderData.Items[0].newAction).toBe(window.refundParams.actions[1]);
            expect(uiWrapperMock.confirm).toHaveBeenCalled();
        });
    });


    describe('Select reason scenarios', function () {
        var scope, controller;
        var item;

        beforeEach(function () {
            scope = $scopeBuilder.$new();
            controller = $controllerBuilder('ReturnsController', { $scope: scope, uiWrapper: uiWrapperMock, returnsServiceWrapper: returnsServiceWrapperMock });

            spyOn(uiWrapperMock, 'focusOnAcceptButton');
            spyOn(uiWrapperMock, 'focusOnCustomerServiceMessage');

            scope.orderData = orderData;
            item = orderData.Items[0];
            item.checked = true;
            scope.checkedItem = item;
        });

        it('Focuses on accept when selecting reason', function () {
            item.newAction = window.refundParams.actions[1]; //sellable return

            scope.reasonSelected(item);
            expect(uiWrapperMock.focusOnAcceptButton).toHaveBeenCalledWith(373510);
            expect(uiWrapperMock.focusOnCustomerServiceMessage).not.toHaveBeenCalled();
        });

        it('Doesnt focus on accept on selecting default reason', function () {
            item.newAction = { Id: -1 };  //-- Please Select --

            scope.reasonSelected(item);

            expect(uiWrapperMock.focusOnAcceptButton).not.toHaveBeenCalled();
            expect(uiWrapperMock.focusOnCustomerServiceMessage).not.toHaveBeenCalled();
        });

        it('Focuses on customer message when selecting customer service return', function () {
            item.newAction = window.refundParams.actions[3]; //customer service message

            scope.reasonSelected(item);

            expect(uiWrapperMock.focusOnAcceptButton).not.toHaveBeenCalled();
            expect(uiWrapperMock.focusOnCustomerServiceMessage).toHaveBeenCalledWith(373510);
        });

        it('Clears selection and reason when clicking reject', function () {
            item.newAction = window.refundParams.actions[1]; //sellable return
            spyOn(uiWrapperMock, 'confirm').and.returnValue(true);

            scope.rejectClicked();

            expect(scope.checkedItem).toBe(null);
            expect(item.checked).toBe(false);
            expect(item.newAction).toBe(null);
            expect(uiWrapperMock.confirm).toHaveBeenCalled();
            expect(uiWrapperMock.focusOnScanTextField).toHaveBeenCalled();
        });

        it('Lowercase S Keypressed on reason dropdown allows event to bubble', function () {
            var event = { keyCode: 83, preventDefault: function () { } };
            spyOn(event, 'preventDefault');
            scope.keypressedInReasonDropdown(event);
            expect(event.preventDefault).not.toHaveBeenCalled();
        });

        it('Uppercase S Keypressed on reason dropdown allows event to bubble', function () {
            var event = { keyCode: 115, preventDefault: function () { } };
            spyOn(event, 'preventDefault');
            scope.keypressedInReasonDropdown(event);
            expect(event.preventDefault).not.toHaveBeenCalled();
        });

        it('Any char Keypressed on reason dropdown cancels the event', function () {
            var event = { keyCode: 73, preventDefault: function () { } };
            spyOn(event, 'preventDefault');
            scope.keypressedInReasonDropdown(event);
            expect(event.preventDefault).toHaveBeenCalled();
        });
    });


    describe('Return scenarios', function () {
        var scope, controller, item;

        beforeEach(function () {
            scope = $scopeBuilder.$new();
            controller = $controllerBuilder('ReturnsController', { $scope: scope, uiWrapper: uiWrapperMock, returnsServiceWrapper: returnsServiceWrapperMock });

            scope.orderData = orderData;

            item = orderData.Items[0];
            item.checked = true;
            item.newAction = window.refundParams.actions[1];

            scope.checkedItem = item;

            spyOn(returnsServiceWrapperMock, 'returnItem');
            spyOn(returnsServiceWrapperMock, 'ReprintLPN');
        });

        it('Pressing accept calls return service', function () {
            scope.acceptOrReprintClicked(item);

            expect(returnsServiceWrapperMock.returnItem).toHaveBeenCalled();

            var retCallParams = returnsServiceWrapperMock.returnItem.calls.argsFor(0);
            expect(retCallParams[0].ItemNumber).toBe(373510);
            expect(retCallParams[0].OrderNumber).toBe(98788);
            expect(retCallParams[0].ActionCode).toBe(30);
            expect(retCallParams[0].TaskDescription).toBe(null);
            expect(retCallParams[0].CustomerURN).toBe('0000682111');
            expect(retCallParams[0].Sku).toBe(4037852);

            expect(retCallParams[1]).toBe(item);
        });

        it('Successful accept refreshes order', function () {
            scope.$broadcast(MessageTypes.ItemSuccessfullyReturned, { Item: item })

            expect(scope.searchInProgress).toBe(true);
            expect(scope.searchKey).toBe('98788');
            expect(returnsServiceWrapperMock.searchOrderConsignment).toHaveBeenCalledWith('98788');
        });

        it('Failed accept refreshes order and displays message', function () {
            var serviceErrorMessage = 'some error';
            scope.$broadcast(MessageTypes.ServiceFailureWithShowMessage, { Message: serviceErrorMessage, Item: item });

            expect(scope.searchInProgress).toBe(true);
            expect(scope.searchKey).toBe('98788');
            expect(scope.errorMessage).toBe(serviceErrorMessage);
            expect(returnsServiceWrapperMock.searchOrderConsignment).toHaveBeenCalledWith('98788');
        });

        it('Failed accept for already returned item displays alert and refreshes order', function () {
            var serviceErrorMessage = 'some error';
            scope.$broadcast(MessageTypes.ServiceFailureWithAlert, { Message: serviceErrorMessage, Item: item });

            expect(uiWrapperMock.alert).toHaveBeenCalled();

            expect(scope.searchInProgress).toBe(true);
            expect(scope.searchKey).toBe('98788');
            expect(scope.errorMessage).toBe('');
            expect(returnsServiceWrapperMock.searchOrderConsignment).toHaveBeenCalledWith('98788');
        });

        it('Returns database failure redirects to error page', function () {
            var serviceErrorMessage = 'some big oracle blow-up';
            scope.$broadcast(MessageTypes.ServiceFailureWithRedirect, { Message: serviceErrorMessage, Item: item });

            expect(uiWrapperMock.navigateTo).toHaveBeenCalled();
            var params = uiWrapperMock.navigateTo.calls.argsFor(0);
            expect(params[0]).toContain('error.aspx');
            expect(params[0]).toContain(serviceErrorMessage);
        });

        it('Barcode scanned on accept button does not trigger accept', function () {
            //arrange
            scope.keyPressedInAcceptButton({ keyCode: 56 });
            scope.keyPressedInAcceptButton({ keyCode: 13 });

            //act
            scope.acceptOrReprintClicked(orderData.Items[0]);

            //assert
            expect(returnsServiceWrapperMock.returnItem).not.toHaveBeenCalled();
            expect(returnsServiceWrapperMock.ReprintLPN).not.toHaveBeenCalled();
        });

        it('Grace period between keys pressed greater then 200ms accepts and prints', function (done) {
            //arrange
            scope.keyPressedInAcceptButton({ keyCode: 56 });

            setTimeout(function () {
                //arrange
                scope.keyPressedInAcceptButton({ keyCode: 13 });

                //act
                scope.acceptOrReprintClicked(orderData.Items[0]);

                //assert
                expect(returnsServiceWrapperMock.returnItem).toHaveBeenCalled();
                done();
            }, 250);
        });
    });



    describe('Reprint scenarios', function () {
        var scope, controller, item;

        beforeEach(function () {
            scope = $scopeBuilder.$new();
            controller = $controllerBuilder('ReturnsController', { $scope: scope, uiWrapper: uiWrapperMock, returnsServiceWrapper: returnsServiceWrapperMock });

            scope.orderData = orderData;

            item = orderData.Items[1];
            item.checked = true;
            item.newAction = window.refundParams.actions[1];

            scope.checkedItem = item;

            spyOn(returnsServiceWrapperMock, 'ReprintLPN');
        });

        it('Button text should say Reprint if item has LPN', function () {
            expect(scope.acceptReprint(item)).toBe('Reprint Label');
        });

        it('Button text should say Accept & Print if item has no LPN', function () {
            item.LPN = null;
            expect(scope.acceptReprint(item)).toBe('Accept & Print');
        });


        it('Pressing Reprint shows authorisation dialog for operator', function () {
            refundParams.supervisor = false;
            spyOn(scope, '$broadcast');
            scope.acceptOrReprintClicked(item);

            expect(returnsServiceWrapperMock.ReprintLPN).not.toHaveBeenCalled();
            expect(scope.$broadcast).toHaveBeenCalledWith(MessageTypes.RequireAthorisation, { item: item });
            expect(item.reprintInProgress).toBeFalsy();
            expect(scope.reprintInProgress).toBeFalsy();
        });

        it('Pressing Reprint calls reprint service for supervisor', function () {
            refundParams.supervisor = true;
            scope.acceptOrReprintClicked(item);

            expect(returnsServiceWrapperMock.ReprintLPN).toHaveBeenCalled();

            var reprintCallParams = returnsServiceWrapperMock.ReprintLPN.calls.argsFor(0);
            expect(reprintCallParams[0]).toBe(item);

            expect(item.reprintInProgress).toBe(true);
            expect(scope.reprintInProgress).toBe(true);
        });

        it('Grace period between keys pressed greater then 200ms reprints', function (done) {
            //arrange
            refundParams.supervisor = true;
            scope.keyPressedInAcceptButton({ keyCode: 56 });


            setTimeout(function () {
                //arrange
                scope.keyPressedInAcceptButton({ keyCode: 13 });

                //act
                scope.acceptOrReprintClicked(item);

                //assert
                expect(returnsServiceWrapperMock.ReprintLPN).toHaveBeenCalled();
                done();
            }, 250);
        });

        it('Reprint failed refreshes order, displays message and hides reprint in progress', function () {
            scope.reprintInProgress = true;
            item.reprintInProgress = true;

            var serviceErrorMessage = 'some error';
            scope.$broadcast(MessageTypes.ServiceFailureWithShowMessage, { Message: serviceErrorMessage, Item: item });

            expect(scope.searchInProgress).toBe(true);
            expect(scope.searchKey).toBe('98788');
            expect(scope.errorMessage).toBe(serviceErrorMessage);
            expect(returnsServiceWrapperMock.searchOrderConsignment).toHaveBeenCalledWith('98788');
            expect(scope.reprintInProgress).toBe(false);
            expect(item.reprintInProgress).toBe(false);
        });
    });




    describe('UI Presentation Functions', function () {

        describe('GetSearchboxLabel', function () {

            var scope, controller;

            beforeEach(function () {
                scope = $scopeBuilder.$new();
                controller = $controllerBuilder('ReturnsController', { $scope: scope, uiWrapper: uiWrapperMock, returnsServiceWrapper: returnsServiceWrapperMock });
            });



            it('GetSearchboxLabel value when page just initialized', function () {
                expect(scope.getSearchboxLabel()).toBe('Scan Order/Package:');
            });

            it('GetSearchboxLabel value when have searched for order', function () {
                scope.orderData = orderData;
                expect(scope.getSearchboxLabel()).toBe('Scan Item/Order:');
            });
        });


        describe('GetRowCssClass', function () {

            var scope, controller;

            beforeEach(function () {
                scope = $scopeBuilder.$new();
                controller = $controllerBuilder('ReturnsController', { $scope: scope, uiWrapper: uiWrapperMock, returnsServiceWrapper: returnsServiceWrapperMock });
                scope.orderData = orderData;
            });

            it('GetRowCssClass - returnable item', function () {
                expect(scope.getRowCssClass(scope.orderData.Items[0])).toBe('item_bisque');
            });

            it('GetRowCssClass - CBR item', function () {
                expect(scope.getRowCssClass(scope.orderData.Items[2])).toBe('item_yellow');
            });

            it('GetRowCssClass - not returnable item', function () {
                expect(scope.getRowCssClass(scope.orderData.Items[4])).toBe('item_chocolate');
                expect(scope.getRowCssClass(scope.orderData.Items[5])).toBe('item_chocolate');
                expect(scope.getRowCssClass(scope.orderData.Items[6])).toBe('item_chocolate');
                expect(scope.getRowCssClass(scope.orderData.Items[7])).toBe('item_chocolate');
                expect(scope.getRowCssClass(scope.orderData.Items[8])).toBe('item_chocolate');
                expect(scope.getRowCssClass(scope.orderData.Items[9])).toBe('item_chocolate');
                expect(scope.getRowCssClass(scope.orderData.Items[10])).toBe('item_chocolate');
            });

            it('GetRowCssClass - selected item', function () {
                orderData.Items[0].checked = true;
                scope.orderData.Items[0].newAction = { Id: -1 };

                expect(scope.getRowCssClass(scope.orderData.Items[0])).toBe('item_azure');
            });

            it('GetRowCssClass - selected with reason', function () {
                orderData.Items[0].checked = true;
                scope.orderData.Items[0].newAction = window.refundParams.actions[1];

                expect(scope.getRowCssClass(scope.orderData.Items[0])).toBe('item_beige');
            });

            it('GetApplicableReasonCodes - non CBR item', function () {
                var reasonCodeArray = scope.getApplicableReasonCodes(orderData.Items[0]);
                expect(reasonCodeArray[0]).toBe(window.refundParams.actions[0]);
                expect(reasonCodeArray[1]).toBe(window.refundParams.actions[1]);
                expect(reasonCodeArray[2]).toBe(window.refundParams.actions[2]);
                expect(reasonCodeArray[3]).toBe(window.refundParams.actions[3]);
                expect(reasonCodeArray[4].Id).toBe(-1);
            });

            it('GetApplicableReasonCodes - CBR item', function () {
                var reasonCodeArray = scope.getApplicableReasonCodes(orderData.Items[2]);
                expect(reasonCodeArray[0]).toBe(window.refundParams.actions[0]);
                expect(reasonCodeArray[1]).toBe(window.refundParams.actions[1]);
                expect(reasonCodeArray[2]).toBe(window.refundParams.actions[2]);
                expect(reasonCodeArray[3].Id).toBe(-1);
            });
        });


        describe('DisableAccept', function () {

            var scope, controller;

            beforeEach(function () {
                scope = $scopeBuilder.$new();
                controller = $controllerBuilder('ReturnsController', { $scope: scope, uiWrapper: uiWrapperMock, returnsServiceWrapper: returnsServiceWrapperMock });
                scope.orderData = orderData;
            });

            it('DisableAccept - row not selected', function () {
                expect(scope.disableAccept(scope.orderData.Items[0])).toBe(true);
            });


            it('DisableAccept - reason not selected', function () {
                orderData.Items[0].checked = true;
                scope.orderData.Items[0].newAction = { Id: -1 };
                expect(scope.disableAccept(scope.orderData.Items[0])).toBe(true);
            });

            it('DisableAccept - falsy when reason selected', function () {
                orderData.Items[0].checked = true;
                scope.orderData.Items[0].newAction = window.refundParams.actions[1];
                expect(scope.disableAccept(scope.orderData.Items[0])).toBeFalsy();
            });

            it('DisableAccept - accept in progress', function () {
                orderData.Items[0].checked = true;
                scope.orderData.Items[0].newAction = window.refundParams.actions[1];
                scope.orderData.Items[0].acceptInProgress = true;
                expect(scope.disableAccept(scope.orderData.Items[0])).toBe(true);
            });

            it('LPN present and item putaway disables reprint', function () {
                var item = orderData.Items[3];
                expect(scope.disableAccept(item)).toBeTruthy();
            });

            it('LPN present and item not putaway enables reprint', function () {
                var item = orderData.Items[2];
                expect(scope.disableAccept(item)).toBeFalsy();
            });

            it('LPN present, item not putaway and reprint in progress disables reprint', function () {
                var item = orderData.Items[2];
                scope.reprintInProgress = true;
                expect(scope.disableAccept(item)).toBeTruthy();
            });

            it('LPN present and another row selected, disables reprint', function () {
                var item = orderData.Items[3];
                scope.checkedItem = orderData.Items[0];
                orderData.Items[0].checked = true;

                expect(scope.disableAccept(item)).toBeTruthy();
            });
        });

        describe('General UI', function () {
            var scope, controller;

            beforeEach(function () {
                scope = $scopeBuilder.$new();
                controller = $controllerBuilder('ReturnsController', { $scope: scope, uiWrapper: uiWrapperMock, returnsServiceWrapper: returnsServiceWrapperMock });
                scope.orderData = orderData;
                spyOn(uiWrapperMock, 'printScreen');
            });



            it('EnterButton, SearchKey and Reject button disabled during accept', function () {
                var item = scope.orderData.Items[0];
                scope.checkedItem = item;
                item.checked = true;
                item.acceptInProgress = true;

                expect(scope.disableEnterSearchAndReject()).toBe(true);
            });

            it('EnterButton, SearchKey and Reject button enabled', function () {

                expect(scope.disableEnterSearchAndReject()).toBeFalsy();

                var item = scope.orderData.Items[0];
                scope.checkedItem = item;
                item.checked = true;
                expect(scope.disableEnterSearchAndReject()).toBeFalsy();
            });

            it('GetItemsToProcess computed correctly', function () {
                expect(scope.getItemsToProcess()).toBe(4);
            });


            it('Shows check for putaway item', function () {
                expect(scope.putawayCheckText(orderData.Items[3])).toBe('\u221A');
            });

            it('Doesnt show check for item not putaway', function () {
                expect(scope.putawayCheckText(orderData.Items[0])).toBe('');
            });

            it('Print screen button pressed', function () {
                scope.printClicked();
                expect(uiWrapperMock.focusOnScanTextField).toHaveBeenCalled();
                expect(uiWrapperMock.printScreen).toHaveBeenCalled();
            });

            it('Reject and cancel when no row selected focuses on scan field', function () {
                //arrange
                spyOn(uiWrapperMock, 'confirm').and.returnValue(false);

                //act
                scope.rejectClicked();

                //assert
                expect(uiWrapperMock.focusOnScanTextField).toHaveBeenCalled();
            });

            it('Reject and cancel when row selected focuses on reason code', function () {
                //arrange
                spyOn(uiWrapperMock, 'confirm').and.returnValue(false);
                var item = scope.orderData.Items[0];
                item.checked = true;
                scope.checkedItem = item;
                item.newAction = { Id: -1 };

                //act
                scope.rejectClicked();

                //assert
                expect(uiWrapperMock.focusOnReasonDropdown).toHaveBeenCalledWith(373510);
            });


            it('Reject and cancel when row and reason selected focuses on reason code', function () {
                //arrange
                spyOn(uiWrapperMock, 'confirm').and.returnValue(false);
                spyOn(uiWrapperMock, 'focusOnAcceptButton');

                var item = scope.orderData.Items[0];
                item.checked = true;
                scope.checkedItem = item;
                item.newAction = refundParams.actions[1];

                //act
                scope.rejectClicked();

                //assert
                expect(uiWrapperMock.focusOnAcceptButton).toHaveBeenCalledWith(373510);
            });

            it('Reject and cancel when row selected and customer service selected focuses on customer service message', function () {
                //arrange
                spyOn(uiWrapperMock, 'confirm').and.returnValue(false);
                spyOn(uiWrapperMock, 'focusOnCustomerServiceMessage');

                var item = scope.orderData.Items[0];
                item.checked = true;
                scope.checkedItem = item;
                item.newAction = refundParams.actions[3];

                //act
                scope.rejectClicked();

                //assert
                expect(uiWrapperMock.focusOnCustomerServiceMessage).toHaveBeenCalledWith(373510);
            });

        });

    });
});