using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using TheCricketSeasonScheduler.Model;
using TheCricketSeasonScheduler.DataBaseLogic;
using TheCricketSeasonScheduler.BLogic;

namespace TheCricketSeasonScheduler
{
    public class Program
    {
        public static string ApiUser = "";
        public static string SecretKey = "";

        static void Main(string[] args)
        {
            ApiUser = ConfigurationManager.AppSettings["APIUSER"].ToString();
            SecretKey = ConfigurationManager.AppSettings["SECRETKEY"].ToString();
           

            Console.WriteLine("The cricket Api Start at " + DateTime.Now.ToString("dd-MMM-yyyy HH:mm"));

            CricketService service = new CricketService();
            CricketDb db = new CricketDb();
            List<Tournament> ids = db.GetSeasonId();
            //db.DeleteStandingMasterData();
            List<StandingTable> seasondata = new List<StandingTable>();
            foreach (var t in ids)
            {
               
                CricketSeason_Standing_masterSave(t);
             
            }
            List<Tournament> season = db.GetStandingMasterIdForSave();
            foreach (var t in season)
            {
              
               DeleteStandingData(t.SeasonId,t.unique_tournament_id,t.TournamentId,t.Standing_MasterId);
         
                seasondata=service.GetTeamDataBySeasonId(ApiUser, SecretKey, t.SeasonId);
                foreach(var s in seasondata)
                {
                    db.CricketSeason_UpdateStandingMaster(s,t.Standing_MasterId);
                    foreach(var r in s.rows)
                    {
                        db.SaveStandingTeamData(r,t.Standing_MasterId);
                    }
                  
                }

            }

            Console.WriteLine("The cricket Api End at " + DateTime.Now.ToString("dd-MMM-yyyy HH:mm"));
            Console.ReadLine();


        }




        public static void DeleteStandingData(string sid,string uid,string tid,int id)
        {
            CricketDb db = new CricketDb();
            db.CricketSeason_DeleteStandingData(sid,uid,tid,id);

        }
        public static void CricketSeason_Standing_masterSave(Tournament t)
        {
            CricketDb db = new CricketDb();
             db.CricketSeason_Standing_masterSave(t);
            
        }
    }
}
