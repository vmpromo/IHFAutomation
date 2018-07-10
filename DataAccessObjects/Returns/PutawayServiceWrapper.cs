// Name: PutawayServiceWrapper.cs
// Type: class file 
// Description: Wrapper class for the WMS Putaway service
//
//$Revision:   1.1  $
//
// Version   Date        Author     Reason
//  1.0      07/06/18    M Cackett  Initial Revision
//                       S Patel
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
    public class PutawayServiceWrapper
    {
        private readonly string _baseServiceUrl;
        private readonly string _urlPath;
        private LoggerDAO _logger = new LoggerDAO();


        public PutawayServiceWrapper()
        {
            var locationURL = ConfigurationManager.AppSettings["WarehouseORDSEndpoint"];
            var uri = new Uri(locationURL);

            _baseServiceUrl = string.Format("{0}://{1}", uri.Scheme, uri.Authority);
            _urlPath = uri.LocalPath;
        }

        //bool
        public bool PutawayIntoWMS(string lpn, string sku, string locn)
        {
            var client = new RestClient(_baseServiceUrl);
            var request = new RestRequest(_urlPath + "putaway/{lpn}/{sku}/{locn}", Method.POST);
            request.AddUrlSegment("lpn", lpn);   // replaces matching token in request.Resource
            request.AddUrlSegment("sku", sku);   // replaces matching token in request.Resource
            request.AddUrlSegment("locn", locn); // replaces matching token in request.Resource


            // execute the request
            // or automatically deserialize result
            // return content type is sniffed but can be explicitly set via RestClient.AddHandler();
            var response = client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                // Something other than HTTP-200 returned.
                _logger.LogError(string.Format("HTTP ERROR: {0} Returned from PutawayIntoWMS", response.StatusCode));
                return false;
            }

            return true;
        }
    }
}
