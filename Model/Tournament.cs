using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheCricketSeasonScheduler.Model
{
    public class Tournament
    {
        public string id { get; set; }
        public string unique_tournament_id { get; set; }
        public string TournamentId { get; set; }
        public string year { get; set; }
        public int start_time { get; set; }      // Unix timestamp
        public int end_time { get; set; }        // Unix timestamp
        public int is_current { get; set; }       // 0 or 1
        public int updated_at { get; set; }
        public string SeasonId { get; set; }
        public string Name { get; set; }
        public int Standing_MasterId { get; set; }
    }
    public class TeamApiResponse
    {
        public List<Tournament> results { get; set; }
    }
}
