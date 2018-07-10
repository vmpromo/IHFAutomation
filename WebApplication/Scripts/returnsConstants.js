// Name: returnsConstants.js
// Type: java script file 
// Description: Javascript constants for the new returns screen (DC improvements)
//   
//
//$Revision:   1.3  $
//
// Version   Date        Author     Reason
//  1.0      27/03/18    A Petrescu Initial Revision
//  1.1      27/03/18    A Petrescu 2 guys returning the same item
//  1.2      23/04/18    M Cackett  Supervisor authorisation
//  1.3      25/04/18    M Cackett  Removed EnterPressedInAuthCodeFieldEvent

var MessageTypes = {
    EnterPressedInScanFieldEvent: 'EnterPressedInScanFieldEvent',

    OrderOrConsignmentFound: 'OrderOrConsignmentFound',
    SearchFailed: 'SearchFailed',
    UnauthenticatedError: 'UnauthenticatedError',

    ItemSuccessfullyReturned: 'ItemSuccessfullyReturned',
    ServiceFailureWithRedirect: 'ServiceFailureWithRedirect',
    ServiceFailureWithShowMessage: 'ServiceFailureWithShowMessage',
    ServiceFailureWithAlert: 'ServiceFailureWithAlert',
    RequireAthorisation: 'RequireAthorisation',
    AuthorisationDialogClosed: 'AuthorisationDialogClosed',
    LpnSuccessfullyReprinted: 'LpnSuccessfullyReprinted',
    AuthorisationSuccess: 'AuthorisationSuccess',
    AuthorisationFail: 'AuthorisationFail'

};