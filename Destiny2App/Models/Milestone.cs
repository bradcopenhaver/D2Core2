using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace Destiny2App.Models
{
    public class Milestone
    {
        public MilestoneResp MilestoneResp { get; set; }
        public DisplayProperties displayProperties { get; set; }
        
        public static object GetMilestones()
        {
            DataClass db = new DataClass();
            APIRequest request = new APIRequest();
            StreamReader rdr = new StreamReader(request.Execute("/Destiny2/Milestones/"));
            JObject milestonesResp = JObject.Parse(rdr.ReadToEnd());

            List<Milestone> milestoneList = new List<Milestone> { };
            foreach (KeyValuePair<string, JToken> child in (JObject)milestonesResp["Response"])
            {
                //convert hash to database id
                int id;
                try
                {
                    id = Int32.Parse(child.Key);
                }
                catch
                {
                    id = (int)(Int64.Parse(child.Key) - 4294967296);
                }

                //create Milestone object
                Milestone milestone = new Milestone();

                //convert API response json to MilestoneResp object
                milestone.MilestoneResp = JsonConvert.DeserializeObject<MilestoneResp>(child.Value.ToString());

                //retreive database json
                JObject dbJson = JObject.Parse(db.RunQuery(string.Format("SELECT json FROM DestinyMilestoneDefinition WHERE id = {0}", id)));
                
                //check start and end date to see if active

                //add milestone object to list
                milestoneList.Add(milestone);
            }
            

            return milestoneList;
        }
    }
}
