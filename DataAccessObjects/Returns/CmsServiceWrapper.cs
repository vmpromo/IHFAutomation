using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using RestSharp;
using IHF.BusinessLayer.DataAccessObjects.Returns.Dto;
using System.Net;

namespace IHF.BusinessLayer.DataAccessObjects.Returns
{
    public class CmsServiceWrapper
    {
        private readonly string _baseServiceUrl;
        private readonly string _urlPath;
        private LoggerDAO _logger = new LoggerDAO();

        public CmsServiceWrapper()
        {
            var locationURL = ConfigurationManager.AppSettings["CmsORDSEndpoint"];
            var uri = new Uri(locationURL);

            _baseServiceUrl = string.Format("{0}://{1}", uri.Scheme, uri.Authority);
            _urlPath = uri.LocalPath;
        }

        public bool PutawayItem(string sku, string lpn, string orderNumber)
        {
            var client = new RestClient(_baseServiceUrl);
            var request = new RestRequest(_urlPath + "putaway/{sku}/{lpn}/{ordernumber}", Method.POST);
            request.AddUrlSegment("lpn", lpn);   // replaces matching token in request.Resource
            request.AddUrlSegment("sku", sku);   // replaces matching token in request.Resource
            request.AddUrlSegment("ordernumber", orderNumber);   // replaces matching token in request.Resource


            // execute the request
            // or automatically deserialize result
            // return content type is sniffed but can be explicitly set via RestClient.AddHandler();
            var response = client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                // Something other than HTTP-200 returned.
                _logger.LogError(string.Format("HTTP ERROR: {0} Returned from CMS Inventory movement, for SKU {1} and LPN {2}", response.StatusCode, sku, lpn));
                return false;
            }


            return true;
        }
    }
}
