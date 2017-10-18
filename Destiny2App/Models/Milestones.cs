using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace Destiny2App.Models
{
    public class Milestones
    {
        public List<string> Hashes { get; set; }

        
        public static object GetMilestones()
        {
            DataClass db = new DataClass();
            APIRequest request = new APIRequest();
            StreamReader rdr = new StreamReader(request.Execute("/Destiny2/Milestones/"));
            JObject milestonesResp = JObject.Parse(rdr.ReadToEnd());

            List<string> milestoneHashes = new List<string> { };
            foreach (KeyValuePair<string, JToken> child in (JObject)milestonesResp["Response"])
            {
                milestoneHashes.Add(child.Key);
            }

            List<string> rawDbJsonList = new List<string>{ };
            foreach (string hash in milestoneHashes)
            {
                int id;
                try
                {
                    id = Int32.Parse(hash);
                }
                catch
                {
                    id = (int)(Int64.Parse(hash) - 4294967296);
                }
                
                rawDbJsonList.Add(db.RunQuery(string.Format("SELECT json FROM DestinyMilestoneDefinition WHERE id = {0}", id)));

            }

            return milestoneHashes;
        }
    }
}
