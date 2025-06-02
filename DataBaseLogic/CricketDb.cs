using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheCricketSeasonScheduler.Model;

namespace TheCricketSeasonScheduler.DataBaseLogic
{
    public class CricketDb
    {
        private readonly SQLHelper _sqlHelper;

        public CricketDb()
        {
            _sqlHelper = new SQLHelper();
        }
        public List<Tournament> GetSeasonId()
        {



            List<Tournament> list = new List<Tournament>();
     
            try
            {
                DbDataReader reader = null;
                using (reader = _sqlHelper.ExecuteReader("theCricketSeason_GetSeasonId"))
                {
                    while (reader.Read())
                    {
                        Tournament obj = new Tournament();
                      //  obj.id = reader["id"].ToString();
                        obj.SeasonId = reader["season_id"].ToString();
                        obj.unique_tournament_id = reader["unique_tournament_id"].ToString();
                        obj.TournamentId = reader["tournament_id"].ToString();
                        list.Add(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return list;

        }
        public bool CricketSeason_Standing_masterSave(Tournament t)
        {
            bool flag = false;
            try
            {

                SqlParameter[] prm = {
                     // new SqlParameter("@Id", SqlDbType.VarChar),
                      new SqlParameter("@SeasonId", SqlDbType.VarChar),
                      new SqlParameter("@UniqueTournamentId", SqlDbType.VarChar),
                      new SqlParameter("@TournamentId", SqlDbType.VarChar),
                         new SqlParameter("@Name", SqlDbType.VarChar),
                      new SqlParameter("@Id", SqlDbType.VarChar)
                   

               };
                //prm[0].Value = t.id;
                prm[0].Value = t.SeasonId;
                prm[1].Value = t.unique_tournament_id;
                prm[2].Value = t.TournamentId;
                prm[3].Value = t.Name;
                prm[4].Value = t.id;
               

                bool result = _sqlHelper.ExecuteNonQuery("theCricketSeason_Standing_masterSave", prm);
                if (result)
                {
                    flag = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return flag;
        }

        public bool CricketSeason_UpdateStandingMaster(StandingTable t,int smid)
        {
            bool flag = false;
            try
            {

                SqlParameter[] prm = {
                     // new SqlParameter("@Id", SqlDbType.VarChar),
                    //  new SqlParameter("@SeasonId", SqlDbType.VarChar),
                     /// new SqlParameter("@UniqueTournamentId", SqlDbType.VarChar),
                     // new SqlParameter("@TournamentId", SqlDbType.VarChar),
                      new SqlParameter("@Name", SqlDbType.VarChar),
                      new SqlParameter("@Id", SqlDbType.VarChar),
                      new SqlParameter("@smid", SqlDbType.Int)
                };
           
             
                prm[0].Value = t.name;
                prm[1].Value = t.id;
                prm[2].Value = smid;


               flag = _sqlHelper.ExecuteNonQuery("theCricketSeason_UpdateStandingMaster", prm);
               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return flag;
        }
        public bool SaveStandingTeamData(StandingRow s,int smid)
        {
            bool isSaved = false;

            try
            {
                SqlParameter[] parameters =
                {
        
                   new SqlParameter("@StandingId", SqlDbType.Int) { Value = smid },
                
                   new SqlParameter("@Position", SqlDbType.Int) { Value = s.position },
                   new SqlParameter("@Points", SqlDbType.Int) { Value = s.points},
                   new SqlParameter("@Total", SqlDbType.Int) { Value = s.total },
                   new SqlParameter("@Win", SqlDbType.Int) { Value = s.win },
                   new SqlParameter("@Draw", SqlDbType.Int) { Value = s.draw },
                   new SqlParameter("@Loss", SqlDbType.Int) { Value = s.loss },
                   new SqlParameter("@TeamId", SqlDbType.VarChar) { Value = s.team_id }
               };

                isSaved = _sqlHelper.ExecuteNonQuery("CricketSeason_SaveStandingTableData", parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving standing team row: " + ex.Message);
            }

            return isSaved;
        }
        public List<Tournament> GetStandingMasterIdForSave()
        {



            List<Tournament> list = new List<Tournament>();
         
            try
            {
                DbDataReader reader = null;
                using (reader = _sqlHelper.ExecuteReader("theCricketSeason_GetStandingMasterIdForSave"))
                {
                    while (reader.Read())
                    {
                        Tournament obj = new Tournament();
                        obj.Standing_MasterId = Convert.ToInt32(reader["Standing_masterId"].ToString());
                       // obj.id = reader["id"].ToString();
                        obj.SeasonId = reader["season_id"].ToString();
                        obj.unique_tournament_id = reader["unique_tournament_id"].ToString();
                        obj.TournamentId = reader["tournament_id"].ToString();
                        list.Add(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return list;

        }


        public bool CricketSeason_DeleteStandingData(string sid,string uid,string tid,int id)
        {
            bool flag = false;
            try
            {

                SqlParameter[] prm = {
                   
                      new SqlParameter("@sid", SqlDbType.VarChar),
                     new SqlParameter("@uid", SqlDbType.VarChar),
                     new SqlParameter("@tid", SqlDbType.VarChar),
                        new SqlParameter("@standing_masterid", SqlDbType.Int)
               };
                prm[0].Value = sid;
                prm[1].Value = uid;
                prm[2].Value = tid;
                prm[3].Value = id;
               

                flag = _sqlHelper.ExecuteNonQuery("theCricketSeason_DeleteStandingData", prm);
                if (flag)
                {
                    flag = true;
                }



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return flag;
        }


        //public bool DeleteStandingMasterData()
        //{
        //    bool flag = false;
        //    try
        //    {

        //         flag = _sqlHelper.ExecuteNonQuery("theCricketSeason_DeleteStanding_MasterData");
        //        if (flag)
        //        {
        //            flag = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //    return flag;
        //}

    }
}

