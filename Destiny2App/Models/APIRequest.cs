using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Destiny2App.Models
{
    public class APIRequest
    {
        public string Endpoint { get; set; }
        public const string Base = "https://bungie.net/Platform";

        public APIRequest()
        {
        }

        public T Execute<T>(RestRequest request) where T : new()
        {
            RestClient client = new RestClient();
            client.BaseUrl = new System.Uri(Base);
            request.AddHeader("API-X-Key", EnvironmentVariables.key1);
            var response = client.Execute<T>(request);

            if (response.ErrorException != null)
            {
                const string message = "Error retrieving response. Check inner details for more info.";
                ApplicationException APIException = new ApplicationException(message, response.ErrorException);
                throw APIException;
            }

            return response.Data;
        }
    }
}
