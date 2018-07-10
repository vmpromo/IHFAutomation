// Name: LocationServiceWrapper.cs
// Type: class file 
// Description: Wrapper class for the location service
//
//$Revision:   1.2  $
//
// Version   Date        Author     Reason
//  1.0      01/03/18    A Petrescu Initial Revision
//  1.1      23/03/18    S Patel    Returns SKU Location with ORDS implemented.
//  1.2      21/05/18    M Cacket   Added code to check valid location.
//                       S Remedios

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;
using IHF.BusinessLayer.DataAccessObjects.Returns.Dto;
using System.Net;
using System.Configuration;


namespace IHF.BusinessLayer.DataAccessObjects.Returns
{
    public enum LocationStatusCode
    {
        NotFound = 1,
        Invalid = 2,
        Valid = 3,
        HTTPerror = 4
    }

    public class LocationServiceWrapper
    {
        private readonly string _baseServiceUrl;
        private readonly string _urlPath;
        private readonly string _noLocationString;
        private LoggerDAO _logger = new LoggerDAO();


        public LocationServiceWrapper()
        {
            var locationURL = ConfigurationManager.AppSettings["WarehouseORDSEndpoint"];
            var uri = new Uri(locationURL);


            //http://its-ecm-915:8080/LocationService/getLocationBySkuBarcode
            _baseServiceUrl = string.Format("{0}://{1}", uri.Scheme, uri.Authority);
            _urlPath = uri.LocalPath;
            _noLocationString = ConfigurationManager.AppSettings["NoLocationString"];

        }

        public string RetrieveLocationBySkuBarcode(string skuBarcode)
        {
            string location;

            var client = new RestClient(_baseServiceUrl);
            var request = new RestRequest(_urlPath + "skulocation/{skuBarcode}", Method.GET);
            request.AddUrlSegment("skuBarcode", skuBarcode); // replaces matching token in request.Resource

            // execute the request
            // or automatically deserialize result
            // return content type is sniffed but can be explicitly set via RestClient.AddHandler();
            var response = client.Execute<LocationServiceResponse>(request);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                // Something other than HTTP-200 returned. Therefore default to noLocationString.
                location = _noLocationString;
            }
            else
            {
                // Response back, but did we get a Location or an empty list? Need to check.
                if (response.Data.items == null || response.Data.items.Count == 0) 
                {
                    location = _noLocationString;
                }
                else
                // We have had a value back from ORDS. Therefore pick it from the JSON to populate locally.
                {
                    location = response.Data.items[0].loc;
                }

                //location = response.Data.Location == null ? _noLocationString : response.Data.Location;
            }

            return location;
        }

        public LocationStatusCode ChkValidLocation(string location)
        {

                LocationStatusCode locationStatus;

                var client = new RestClient(_baseServiceUrl);
                var request = new RestRequest(_urlPath + "chklocation/{location}", Method.GET);
                request.AddUrlSegment("location", location); // replaces matching token in request.Resource

                // execute the request
                // or automatically deserialize result
                // return content type is sniffed but can be explicitly set via RestClient.AddHandler();
                var response = client.Execute<ChkLocationServiceResponse>(request);

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    // Something other than HTTP-200 returned. Therefore default to noLocationString.
                    //locationStatus = LocationStatusCode.NotFound;

                    // Something other than HTTP-200 returned.
                    _logger.LogError(string.Format("HTTP ERROR: {0} Returned from ChkLocationService", response.StatusCode));
                    locationStatus = LocationStatusCode.HTTPerror;
                }
                else
                {
                    // Response back, but did we get a Location or an empty list? Need to check.
                    if (response.Data.items.Count == 0)
                    {
                        locationStatus = LocationStatusCode.NotFound;

                    }
                    else
                    // We have had a value back from ORDS. Therefore pick it from the JSON to populate locally.
                    {
                        if (response.Data.items[0].locn_status == "T")
                        {
                            locationStatus = LocationStatusCode.Valid;
                        }
                        else
                        {
                            locationStatus = LocationStatusCode.Invalid;
                        }
                    }

                }

                return locationStatus;
            }

        }

    }

