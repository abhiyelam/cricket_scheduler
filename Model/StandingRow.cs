using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheCricketSeasonScheduler.Model
{
    public class StandingRow
    {
        public string team_id { get; set; }
        public int position { get; set; }
        public int points { get; set; }
        public int total { get; set; }
        public int win { get; set; }
        public int draw { get; set; }
        public int loss { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string stage_id { get; set; }
        public StandingTable StandingTable { get; set; }

    }
    public class StandingResult
    {
        public List<StandingTable> tables { get; set; }
    }
    public class StandingTable
    {
        public string id { get; set; }
        public string name { get; set; }
        public string stage_id { get; set; }
       public List<StandingRow> rows { get; set; }
    }

    public class StandingApiResponse
    {
        public int code { get; set; }
        public StandingResult results { get; set; }
    }
}
