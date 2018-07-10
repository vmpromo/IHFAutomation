// Name: authDialog.tests.js
// Type: java script file 
// Description: Javascript code for the automated unit tests for auth dialog
//   
//
//$Revision:   1.1  $
//
// Version   Date        Author     Reason
//  1.0      01/05/18    M Cackett  Initial Revision

describe('Auth Dialog Angular Module', function () {

    beforeEach(module('IHFReturns'));

    var $controllerBuilder;
    var $scopeBuilder;

    var uiWrapperMock;
    var returnsServiceWrapperMock;

    var orderData;

    beforeEach(inject(function ($rootScope, _$controller_) {
        $controllerBuilder = _$controller_;
        $scopeBuilder = $rootScope;

        uiWrapperMock = {
            focusOnAuthcodeInput: function () { },
            hideAuthDialog: function () { },
            showAuthDialog: function () { },
            alert: function () { }
        };


        returnsServiceWrapperMock = {
            AuthoriseUser: function (authCode, authCallbackData) { }
        };

        spyOn(uiWrapperMock, 'focusOnAuthcodeInput');
        spyOn(uiWrapperMock, 'hideAuthDialog');
        spyOn(uiWrapperMock, 'showAuthDialog');
        spyOn(uiWrapperMock, 'alert');
        spyOn(returnsServiceWrapperMock, 'AuthoriseUser');

    }));

    var scope, controller;

    beforeEach(function () {
        scope = $scopeBuilder.$new();
        directive = $controllerBuilder('autoriseDialogController', { $scope: scope, returnsServiceWrapper: returnsServiceWrapperMock, uiWrapper: uiWrapperMock });
    });

    it('Sets initial state correctly', function () {
        expect(directive).toBeTruthy();
        expect(scope.authCode).toBe('');
    });



    it('Calls AuthoriseUser service when enter key is pressed', function (done) {
        var authCallbackData;
        var event = { keyCode: 13 };

        scope.authCode = 'UI00000000000030';
        scope.keyPressedTextbox(event);

        setTimeout(function () {
            expect(returnsServiceWrapperMock.AuthoriseUser).toHaveBeenCalledWith('UI00000000000030', authCallbackData);
            done();
        }, 200);
    });


    it('Does not call AuthoriseUser service when any other key is pressed', function () {
        var event = { keyCode: 65 };

        scope.authCode = 'UI00000000000030';
        scope.keyPressedTextbox(event);

        expect(returnsServiceWrapperMock.AuthoriseUser).not.toHaveBeenCalled();
    });

    it('Closes the dialog', function () {
        scope.$broadcast(MessageTypes.AuthorisationDialogClosed);
        expect(scope.authCode).toBe('');
    });

    it('Pops up the dialog if user is not master packer', function () {
        scope.$broadcast(MessageTypes.RequireAthorisation);
        expect(uiWrapperMock.showAuthDialog).toHaveBeenCalled();
    });

    it('Hides the dialog if user barcode is validated successfully', function () {
        scope.$broadcast(MessageTypes.AuthorisationSuccess);
        expect(scope.authCode).toBe('');
        expect(uiWrapperMock.hideAuthDialog).toHaveBeenCalled();
    });

    it('Displays alert if user barcode is invalid', function () {
        scope.$broadcast(MessageTypes.AuthorisationFail);
        expect(scope.authCode).toBe('');
        expect(uiWrapperMock.alert).toHaveBeenCalledWith('Invalid user/barcode');
        expect(uiWrapperMock.focusOnAuthcodeInput).toHaveBeenCalled();
    });

});
