using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
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

        public System.IO.Stream Execute(string endpoint)
        {
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, Base + endpoint);
            request.Headers.Add("X-API-Key", EnvironmentVariables.key1);
            HttpResponseMessage response = new HttpResponseMessage();
            System.IO.Stream responseBody = null;

            Task.Run(async () =>
            {
                response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                responseBody = await response.Content.ReadAsStreamAsync();
            }).Wait();


            if (!response.IsSuccessStatusCode)
            {
                string message = "Error retrieving response. Check inner details for more info." + response.ReasonPhrase;
                ApplicationException APIException = new ApplicationException(message);
                throw APIException;
            }

            return responseBody;
        }

        public System.IO.Stream GetContent(string endpoint)
        {
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "https://bungie.net" + endpoint);
            request.Headers.Add("X-API-Key", EnvironmentVariables.key1);
            HttpResponseMessage response = new HttpResponseMessage();
            System.IO.Stream responseBody = null;

            Task.Run(async () =>
            {
                response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                responseBody = await response.Content.ReadAsStreamAsync();
            }).Wait();


            if (!response.IsSuccessStatusCode)
            {
                string message = "Error retrieving response. Check inner details for more info." + response.ReasonPhrase;
                ApplicationException APIException = new ApplicationException(message);
                throw APIException;
            }

            return responseBody;
        }
    }
}
