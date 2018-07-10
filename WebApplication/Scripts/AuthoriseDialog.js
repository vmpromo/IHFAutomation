// Name: authoriseDialog.js
// Type: java script file 
// Description: Javascript code for the Authorise user dialog
//   
//
//$Revision:   1.4  $
//
// Version   Date        Author     Reason
//  1.0      20/04/18    A Petrescu Initial Revision
//                       M Cackett
//                       S Remedios
//  1.1      25/04/18    M Cackett  Bug fixes
//                       A Petrescu
//  1.2      26/04/18    A Petrescu Bug fix for IE8
//                       M Cackett
//  1.3      08/05/18    M Cackett  Refactored code.
//                       A Petrescu

var authDlgInitializer = function (app) {

    app.controller('autoriseDialogController',
        [
            '$scope',
            'returnsServiceWrapper',
            'uiWrapper',
            function ($scope, returnsServiceWrapper, uiWrapper) {
                var authCallbackData;
                $scope.authCode = '';

                $scope.keyPressedTextbox = function (event) {
                    if (event.keyCode === 13) {
                        setTimeout(function () {
                            returnsServiceWrapper.AuthoriseUser($scope.authCode, authCallbackData);
                        }, 100);
                    }
                }

                $scope.$on(MessageTypes.AuthorisationDialogClosed, function (event, data) {
                    $scope.authCode = '';
                });

                $scope.$on(MessageTypes.RequireAthorisation, function (event, data) {
                    authCallbackData = data;
                    uiWrapper.showAuthDialog();
                });

                $scope.$on(MessageTypes.AuthorisationSuccess, function (event, data) {
                    $scope.authCode = '';
                    uiWrapper.hideAuthDialog();
                });

                $scope.$on(MessageTypes.AuthorisationFail, function (event, data) {
                    $scope.authCode = '';
                    $scope.$apply();

                    uiWrapper.alert('Invalid user/barcode');
                    uiWrapper.focusOnAuthcodeInput();
                });
            }
        ]);

    app.directive(
        'authoriseDialog',
        function () {
            return {
                restrict: 'A',
                replace: true,
                template: '<div id="dialog-PrintAuth" style="display: none; font-size: small;"><ul style="list-style-type: none"><li>Authorisation Code<input type="text" id="authCodeInput" ng-model="authCode" ng-keypress="keyPressedTextbox($event)" /></li></ul><div style="font-size: x-small; font-style: italic">Esc to close the window</div></div>',
                scope: {},
                controller: 'autoriseDialogController'
            }
        }
        );
}

