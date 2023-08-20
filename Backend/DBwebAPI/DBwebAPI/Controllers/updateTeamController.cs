using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using DBwebAPI.Models;
using Microsoft.AspNetCore.Identity;
using static DBwebAPI.Controllers.ForgetPasswordController;
using System.Security.Principal;
using Oracle.ManagedDataAccess.Types;
using Microsoft.AspNetCore.JsonPatch.Internal;
using SqlSugar.Extensions;

namespace DBwebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class updateTeamController : ControllerBase
    {
        public class TeamInfo
        {
            public string? teamName { get; set; }
            public string? city { get; set; }
            public int coach_id { get; set; }
            public string? manager { get; set; }
            public string? boss { get; set; }
        }






        [HttpPost]
        public async Task<IActionResult> insertTeam([FromBody] TeamInfo json)
        {

            ORACLEconn ORACLEConnectTry = new ORACLEconn();
            ORACLEConnectTry.getConn();
            try
            {
                SqlSugarClient sqlORM = ORACLEConnectTry.sqlORM;


                Team newTeam = new Team();
                int team_id = await sqlORM.Queryable<Team>().MaxAsync(it => it.team_id) + 1;
                string teamName = json.teamName;
                string city = json.city;
                string manager = json.manager;
                string boss = json.boss;
                int coach_id = json.coach_id;

                newTeam.team_id = team_id;
                newTeam.city = city;
                newTeam.manager = manager;
                newTeam.boss = boss;
                newTeam.teamName = teamName;
                newTeam.coach_id = coach_id;

                Console.WriteLine("team_id = " + team_id.ToString());
                Console.WriteLine("city = " + city);
                Console.WriteLine("manager = " + manager);
                Console.WriteLine("boss = " + boss);
                Console.WriteLine("teamName = " + teamName);
                Console.WriteLine("coach_id = " + coach_id.ToString());

                sqlORM.Insertable(newTeam).ExecuteCommand();

                return Ok(new CustomResponse { ok = "yes", value = "Success" });


            }
            catch (Exception ex)
            {
                Console.WriteLine("UNKNOWN");
                Console.WriteLine(ex);
                return Ok(new CustomResponse { ok = "no", value = "UNKNOWN" }); // Internal server error
            }
        }

        public class CoachInfo
        {
            public string? coachName { get; set; }
            public int? coachYear { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> insertCoach([FromBody] CoachInfo json)
        {

            ORACLEconn ORACLEConnectTry = new ORACLEconn();
            ORACLEConnectTry.getConn();
            try
            {
                SqlSugarClient sqlORM = ORACLEConnectTry.sqlORM;


                Coach newCoach = new Coach();
                int coach_id = await sqlORM.Queryable<Coach>().MaxAsync(it => it.coach_id) + 1;
                string coachName = json.coachName;
                int? coachYear = json.coachYear;


                newCoach.coach_id = coach_id;
                newCoach.coachName = coachName;
                newCoach.coachYear = coachYear;


                Console.WriteLine("coach_id = " + coach_id.ToString());
                Console.WriteLine("coachName = " + coachName);
                Console.WriteLine("coachYear = " + coachYear.ToString());

                sqlORM.Insertable(newCoach).ExecuteCommand();

                return Ok(new CustomResponse { ok = "yes", value = "Success" });


            }
            catch (Exception ex)
            {
                Console.WriteLine("UNKNOWN");
                Console.WriteLine(ex);
                return Ok(new CustomResponse { ok = "no", value = "UNKNOWN" }); // Internal server error
            }
        }

        public class GameInfo
        {
            public int? homeTeam { get; set; }
            public int? guestTeam { get; set; }
            public string? gameName { get; set; }
            public int? gameType { get; set; }
            public int? status { get; set; }
            public DateTime? dateTime { get; set; }
            public string? city { get; set; }
            public string? mainReferee { get; set; }
            public int? homeScore { get; set; }
            public int? guestScore { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> insertGame([FromBody] GameInfo json)
        {

            ORACLEconn ORACLEConnectTry = new ORACLEconn();
            ORACLEConnectTry.getConn();
            try
            {
                SqlSugarClient sqlORM = ORACLEConnectTry.sqlORM;


                Game newGame = new Game();
                int game_id = await sqlORM.Queryable<Game>().MaxAsync(it => it.game_id) + 1;
                string gameName = json.gameName;
                int? homeTeam = json.homeTeam;
                int? guestTeam = json.guestTeam;
                int? gameType = json.gameType;
                int? status = json.status;
                string city = json.city;
                string mainReferee = json.mainReferee;
                DateTime? dateTime = json.dateTime;
                int? homeScore = json.homeScore;
                int? guestScore = json.guestScore;



                newGame.game_id = game_id;
                newGame.gameName = gameName;
                newGame.homeTeam = homeTeam;
                newGame.guestTeam = guestTeam;
                newGame.gameType = gameType;
                newGame.status = status;
                newGame.dateTime = dateTime;
                newGame.city = city;
                newGame.mainReferee = mainReferee;
                newGame.homeScore = homeScore;
                newGame.guestScore = guestScore;


                Console.WriteLine("game_id = " + game_id.ToString());
                Console.WriteLine("gameName = " + gameName);
                Console.WriteLine("homeTeam = " + homeTeam.ToString());
                Console.WriteLine("guestTeam = " + guestTeam.ToString());
                Console.WriteLine("gameType = " + gameType.ToString());
                Console.WriteLine("status = " + status.ToString());
                Console.WriteLine("dateTime = " + dateTime.ToString());
                Console.WriteLine("city = " + city);
                Console.WriteLine("mainReferee = " + mainReferee);
                Console.WriteLine("homeScore = " + homeScore);
                Console.WriteLine("guestScore = " + guestScore);




                //sqlORM.Insertable(newGame).ExecuteCommand();

                List<DateTime?> list = sqlORM.Queryable<Game>().Select(it => it.dateTime).ToList();
                Console.WriteLine(list.Count());


                //sqlORM.Queryable<Game>().Select(it => it.homeTeam).ToList();

                return Ok(new CustomResponse { ok = "yes", value = "Success" });



            }
            catch (Exception ex)
            {
                Console.WriteLine("UNKNOWN");
                Console.WriteLine(ex);
                return Ok(new CustomResponse { ok = "no", value = "UNKNOWN" }); // Internal server error
            }
        }

        public class TeamInGameTimePara
        {
            public string? dateTime { get; set; }
            public int? gameType { get; set; }
        }
        public class TeamInGameTimeVal
        {
            public string? dateTime { get; set; }
            //public int? homeTeam { get; set; }
            public string? homeTeamName { get; set; }
            //public int? guestTeam { get; set; }
            public string? guestTeamName { get; set; }
            public int? status { get; set; }
            public int? homeScore { get; set; }
            public int? guestScore { get; set; }
        }

        [HttpPost]
        public async Task<List<TeamInGameTimeVal>> searchTeamInGameTime([FromBody] TeamInGameTimePara json)
        {

            ORACLEconn ORACLEConnectTry = new ORACLEconn();
            ORACLEConnectTry.getConn();
            try
            {
                SqlSugarClient sqlORM = ORACLEConnectTry.sqlORM;

                List<TeamInGameTimeVal> ans = new List<TeamInGameTimeVal>();

                string? dateTime = json.dateTime;
                Console.WriteLine("dateTime = " + dateTime);
                string? year = dateTime.Substring(0, 4);
                string? month = dateTime.Substring(5, 2);
                string? day = dateTime.Substring(8, 2);
                int? gameType = json.gameType;
                Console.WriteLine("gameType = " + gameType.ToString());

                ans = await sqlORM.Queryable<Game>()
                    .LeftJoin<Team>((g, home) => g.homeTeam == home.team_id)
                    .LeftJoin<Team>((g, home, guest) => g.guestTeam == guest.team_id)
                    .Where((g, home, guest) =>
                    g.dateTime.Value.Year.ToString() == year
                    && (g.dateTime.Value.Month < 10 ? "0" : "") + g.dateTime.Value.Month.ToString() == month
                    && (g.dateTime.Value.Day < 10 ? "0" : "") + g.dateTime.Value.Day.ToString() == day
                    && g.gameType == gameType)
                    .Select((g, home, guest) => new TeamInGameTimeVal
                    {
                        dateTime = (g.dateTime.Value.Hour < 10 ? "0" : "") + g.dateTime.Value.Hour.ToString() + ":"
                    + (g.dateTime.Value.Minute < 10 ? "0" : "") + g.dateTime.Value.Minute.ToString(),
                        homeTeamName = home.teamName,
                        guestTeamName = guest.teamName,
                        status = g.status,
                        homeScore = g.homeScore,
                        guestScore = g.guestScore
                    })
                    .ToListAsync();

                Console.WriteLine("ansCnt = " + ans.Count().ToString());

                return ans;


            }
            catch (Exception ex)
            {
                Console.WriteLine("UNKNOWN");
                Console.WriteLine(ex);
                return null;
            }

        }


        public class TeamInGameTypePara
        {
            public int? gameType { get; set; }
        }

        public class TeamInGameTypeVal
        {
            //public string? gameName { get; set; }
            public string? teamName { get; set; }
            //public string? guestTeamName { get; set; }

        }

        [HttpPost]
        public async Task<List<TeamInGameTypeVal>> searchTeamInGameType([FromBody] TeamInGameTypePara json)
        {

            ORACLEconn ORACLEConnectTry = new ORACLEconn();
            ORACLEConnectTry.getConn();
            try
            {
                SqlSugarClient sqlORM = ORACLEConnectTry.sqlORM;

                List<TeamInGameTypeVal> ans = new List<TeamInGameTypeVal>();

                int? GameType = json.gameType;

                //List<TeamInGameTypeVal> homeTeam = new List<TeamInGameTypeVal>();
                //List<TeamInGameTypeVal> guestTeam = new List<TeamInGameTypeVal>();

                var homeTeam = sqlORM.Queryable<Game>()
                    .LeftJoin<Team>((g, home) => g.homeTeam == home.team_id)
                    .Distinct()
                    .Where((g, home) => g.gameType == GameType)
                    .Select((g, home) => new TeamInGameTypeVal { teamName = home.teamName, });


                var guestTeam = sqlORM.Queryable<Game>()
                    .LeftJoin<Team>((g, guest) => g.guestTeam == guest.team_id)
                    .Distinct()
                    .Where((g, guest) => g.gameType == GameType)
                    .Select((g, guest) => new TeamInGameTypeVal { teamName = guest.teamName, });


                ans = await sqlORM.Union(homeTeam, guestTeam).ToListAsync();

                for (int i = 0; i < ans.Count(); i++)
                {
                    Console.WriteLine("teamName=" + ans[i].teamName);
                }


                return ans;


            }
            catch (Exception ex)
            {
                Console.WriteLine("UNKNOWN");
                Console.WriteLine(ex);
                return null;
            }
        }


    }

}
