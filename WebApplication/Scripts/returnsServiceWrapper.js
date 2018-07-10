// Name: returnsServiceWrapper.js
// Type: java script file 
// Description: Javascript code for the new returns services
//   
//
//$Revision:   1.13  $
//
// Version   Date        Author     Reason
//  1.0      14/02/18    A Petrescu Initial Revision
//  1.1      16/02/18    A Petrescu Bug fixes, error handling
//  1.2      19/02/18    A Petrescu Integration of Service2, minor UI fixes
//  1.3      19/02/18    A Petrescu Bug fixes, service 2, error reporting on oracle crashes.
//  1.4      22/02/18    A Petrescu Virtual Console changes
//  1.5      01/03/18    A Petrescu Calling location microservice story
//  1.6      06/03/18    M Cackett  Added partial success function to ReturnService to handle print failure.
//                       J Duru
//  1.7      19/03/18    A Petrescu Added failure with delay branch in service wrapper
//  1.8      27/03/18    A Petrescu Decoupled messaging
//  1.9      27/03/18    A Petrescu 2 guys returning the same item
//  1.10     11/04/18    M Cackett Added the reprint lpn
//                       S Remedios
//  1.11     24/04/18    M Cackett  Authorisation dialog
//                       S Remedios
//                       A Petrescu
//  1.12     31/05/18    A Petrescu Fixed IE "get" caching issue, by changing it to Post
//  1.13     31/05/18    A Petrescu Fully disabled cache



var returnServiceWrapperInitializer = function (app) {
    app.factory('returnsServiceWrapper', [
        '$rootScope',
        function ($rootScope) {
            $.ajaxSetup({ cache: false });

            return {
                searchOrderConsignment: function (searchKey) {

                    var url = '/pages/Returns/Service/ReturnService.svc/GetResponse?searchKey=' + searchKey;
                    //call service
                    $.ajax({
                        type: 'GET',
                        url: url,
                        contentType: 'application/json',
                        //data: requestData,
                        success: function (result) {
                            $rootScope.$broadcast(MessageTypes.OrderOrConsignmentFound, result);
                        },
                        error: function (result) {
                            if (result.status === 404) {
                                var message = JSON.parse(result.responseText);
                                $rootScope.$broadcast(MessageTypes.SearchFailed, message);
                            } else if (result.status === 401) {
                                $rootScope.$broadcast(MessageTypes.UnauthenticatedError);
                            } else {
                                virtualConsole.error('Service call ' + url + ' FAILED with status ' + result.status);
                            }
                        }
                    });
                },

                returnItem: function (requestData, item) {
                    var url = '/pages/Returns/Service/ReturnService.svc/ReturnItem';

                    $.ajax({
                        type: 'POST',
                        url: url,
                        contentType: 'application/json',
                        data: JSON.stringify(requestData),
                        success: function () { $rootScope.$broadcast(MessageTypes.ItemSuccessfullyReturned, { Item: item }); },
                        error: function (result) {
                            var message;
                            if (result.status === 417) {
                                message = JSON.parse(result.responseText);
                                message.Item = item;
                                $rootScope.$broadcast(MessageTypes.ServiceFailureWithRedirect, message);
                            } else if (result.status === 450) {
                                message = JSON.parse(result.responseText);
                                message.Item = item;
                                $rootScope.$broadcast(MessageTypes.ServiceFailureWithShowMessage, message);
                            } else if (result.status === 451) {
                                message = JSON.parse(result.responseText);
                                message.Item = item;
                                $rootScope.$broadcast(MessageTypes.ServiceFailureWithAlert, message);
                            } else if (result.status === 401) {
                                $rootScope.$broadcast(MessageTypes.UnauthenticatedError);
                            } else {
                                virtualConsole.error('Service call ' + url + ' FAILED with status ' + result.status);
                            }
                        }
                    });
                },

                ReprintLPN: function (item) {
                    var url = '/pages/Returns/Service/ReturnService.svc/ReprintLPN';

                    $.ajax({
                        type: 'POST',
                        url: url,
                        data: JSON.stringify({ ItemNumber: item.ItemNumber }),
                        contentType: 'application/json',
                        success: function () { $rootScope.$broadcast(MessageTypes.LpnSuccessfullyReprinted, { Item: item }); },
                        error: function (result) {
                            var message;
                            //404
                            if (result.status === 450 || result.status === 404) {
                                message = JSON.parse(result.responseText);
                                message.Item = item;
                                $rootScope.$broadcast(MessageTypes.ServiceFailureWithShowMessage, message);
                            } else if (result.status === 401) {
                                $rootScope.$broadcast(MessageTypes.UnauthenticatedError);
                            } else {
                                virtualConsole.error('Service call ' + url + ' FAILED with status ' + result.status);
                            }
                        }
                    });
                },

                AuthoriseUser: function (userBarcode, callbackData) {
                    //                    var url = '/pages/Packing/DataLoaders/DataLoader.svc/MasterPacker?userBarcode=' + userBarcode;
                    var url = '/pages/Packing/DataLoaders/DataLoader.svc/MasterPacker';

                    $.ajax({
                        type: 'POST',
                        url: url,
                        contentType: 'application/json',
                        data: JSON.stringify(userBarcode),
                        success: function (result) {
                            if (result === "T") {
                                $rootScope.$broadcast(MessageTypes.AuthorisationSuccess, callbackData);
                            }
                            else {
                                $rootScope.$broadcast(MessageTypes.AuthorisationFail);
                            }
                        },
                        error: function (result) {
                            if (result.status === 401) {
                                $rootScope.$broadcast(MessageTypes.UnauthenticatedError);
                            } else {
                                var message = { Message: 'Failed to call MasterPacker service', Item: null };
                                $rootScope.$broadcast(MessageTypes.ServiceFailureWithShowMessage, message);
                            }
                        }
                    });
                }




            }
        }
    ]);
};
