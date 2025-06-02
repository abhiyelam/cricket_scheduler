using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TheCricketSeasonScheduler.DataBaseLogic;
using TheCricketSeasonScheduler.Model;

namespace TheCricketSeasonScheduler.BLogic
{
    public class CricketService
    {
        private static readonly HttpClient client = new HttpClient();

        public List<StandingTable> GetTeamDataBySeasonId(string username, string password, string seasonId)
        {
           string apiUrl = $"https://api.thesports.com/v1/cricket/season/table/detail?user={username}&secret={password}&uuid={seasonId}";
            List<StandingTable> data = new List<StandingTable>();
       
            try
            {
                HttpResponseMessage response = client.GetAsync(apiUrl).Result;
                response.EnsureSuccessStatusCode();

                string responseBody = response.Content.ReadAsStringAsync().Result;
                JObject json = JObject.Parse(responseBody);

                var tables = json["results"]?["tables"];
                if (tables != null)
                {
                    foreach (var table in tables)
                    {
                       
                        var standingTable = table.ToObject<StandingTable>();

                       
                        data.Add(standingTable);
                    }
                }

                return data;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching data: {ex.Message}");
                return new List<StandingTable>();
                
            }
            
        }

    }
}
