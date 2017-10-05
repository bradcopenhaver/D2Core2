using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Destiny2App.Models
{
    public class ManifestResp
    {
        public class mobileGearCDN
        {
            public string Geometry { get; set; }
            public string Texture { get; set; }
            public string PlateRegion { get; set; }
            public string Gear { get; set; }
            public string Shader { get; set; }
        }

        public class mobileWorldContentPaths
        {
            public string en { get; set; }
        }

        public class mobileGearAssetDataBasesItem
        {
            public int version { get; set; }
            public string path { get; set; }
        }

        public class Response
        {
            public string version { get; set; }
            public string mobileAssetContentPath { get; set; }
            public List<mobileGearAssetDataBasesItem> mobileGearAssetDataBases { get; set; }
            public mobileWorldContentPaths mobileWorldContentPaths { get; set; }
            public string mobileClanBannerDatabasePath { get; set; }
            public mobileGearCDN mobileGearCDN { get; set; }
        }

        public class Root
        {
            public Response Response { get; set; }
            public int ErrorCode { get; set; }
            public int ThrottleSeconds { get; set; }
            public string ErrorStatus { get; set; }
            public string Message { get; set; }
            public object MessageData { get; set; }
        }

        public static Root GetManifest()
        {
            var request = new RestRequest();
            request.Resource = "/Destiny2/Manifest/";
            //request.RootElement = "Response";

            APIRequest execute = new APIRequest();

            return execute.Execute<Root>(request);
        }
    }
}
