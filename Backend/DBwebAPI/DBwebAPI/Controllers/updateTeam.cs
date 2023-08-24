using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using DBwebAPI.Models;
using Microsoft.AspNetCore.Identity;
using static DBwebAPI.Controllers.ForgetPasswordController;
using System.Security.Principal;
using Oracle.ManagedDataAccess.Types;
using Microsoft.AspNetCore.JsonPatch.Internal;
using SqlSugar.Extensions;
using System.Security.Policy;
using System.Security.AccessControl;
using System.DirectoryServices.ActiveDirectory;

namespace DBwebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class updateTeamController : ControllerBase
    {

        //public class TeamInfo
        //{
        //    public string? teamName { get; set; }
        //    public string? city { get; set; }
        //    public int coach_id { get; set; }
        //    public string? manager { get; set; }
        //    public string? boss { get; set; }
        //    public string? teamLogo { get; set; }
        //}






        //[HttpPost]
        //public async Task<IActionResult> insertTeam([FromBody] TeamInfo json)
        //{

        //    ORACLEconn ORACLEConnectTry = new ORACLEconn();
        //    ORACLEConnectTry.getConn();
        //    try
        //    {
        //        SqlSugarClient sqlORM = ORACLEConnectTry.sqlORM;


        //        Team newTeam = new Team();
        //        int team_id = await sqlORM.Queryable<Team>().MaxAsync(it => it.team_id)+1;
        //        string teamName = json.teamName;
        //        string city = json.city;
        //        string manager = json.manager;
        //        string boss = json.boss;
        //        int coach_id = json.coach_id;
        //        string teamLogo = json.teamLogo;

        //        newTeam.team_id = team_id;
        //        newTeam.city = city;
        //        newTeam.manager = manager;
        //        newTeam.boss = boss;
        //        newTeam.teamName = teamName;
        //        newTeam.coach_id = coach_id;
        //        newTeam.teamLogo = teamLogo;

        //        Console.WriteLine("team_id = " + team_id.ToString());
        //        Console.WriteLine("city = " +city );
        //        Console.WriteLine("manager = " + manager);
        //        Console.WriteLine("boss = " + boss);
        //        Console.WriteLine("teamName = " + teamName);
        //        Console.WriteLine("coach_id = " + coach_id.ToString());
        //        Console.WriteLine("teamLogo = " + teamLogo);

        //        sqlORM.Insertable(newTeam).ExecuteCommand();

        //        return Ok(new CustomResponse { ok = "yes", value = "Success" });


        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("UNKNOWN");
        //        Console.WriteLine(ex);
        //        return Ok(new CustomResponse { ok = "no", value = "UNKNOWN" }); // Internal server error
        //    }
        //}

        //public class CoachInfo
        //{
        //    public string? coachName { get; set; }
        //    public int? coachYear { get; set; }
        //}

        //[HttpPost]
        //public async Task<IActionResult> insertCoach([FromBody] CoachInfo json)
        //{

        //    ORACLEconn ORACLEConnectTry = new ORACLEconn();
        //    ORACLEConnectTry.getConn();
        //    try
        //    {
        //        SqlSugarClient sqlORM = ORACLEConnectTry.sqlORM;


        //        Coach newCoach = new Coach();
        //        int coach_id = await sqlORM.Queryable<Coach>().MaxAsync(it => it.coach_id) + 1;
        //        string coachName = json.coachName;
        //        int? coachYear = json.coachYear;


        //        newCoach.coach_id = coach_id;
        //        newCoach.coachName = coachName;
        //        newCoach.coachYear = coachYear;


        //        Console.WriteLine("coach_id = " + coach_id.ToString());
        //        Console.WriteLine("coachName = " + coachName);
        //        Console.WriteLine("coachYear = " + coachYear.ToString());

        //        sqlORM.Insertable(newCoach).ExecuteCommand();

        //        return Ok(new CustomResponse { ok = "yes", value = "Success" });


        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("UNKNOWN");
        //        Console.WriteLine(ex);
        //        return Ok(new CustomResponse { ok = "no", value = "UNKNOWN" }); // Internal server error
        //    }
        //}

        //public class GameInfo
        //{
        //    public int? homeTeam { get; set; }
        //    public int? guestTeam { get; set; }
        //    public string? gameName { get; set; }
        //    public int? gameType { get; set; }
        //    public int? status { get; set; }
        //    public DateTime? dateTime { get; set; }
        //    public string? city { get; set; }
        //    public string? mainReferee { get; set; }
        //    public int? homeScore { get; set; }
        //    public int? guestScore { get; set; }
        //}

        //[HttpPost]
        //public async Task<IActionResult> insertGame([FromBody] GameInfo json)
        //{

        //    ORACLEconn ORACLEConnectTry = new ORACLEconn();
        //    ORACLEConnectTry.getConn();
        //    try
        //    {
        //        SqlSugarClient sqlORM = ORACLEConnectTry.sqlORM;


        //        Game newGame = new Game();
        //        int game_id = await sqlORM.Queryable<Game>().MaxAsync(it => it.game_id) + 1;
        //        string gameName = json.gameName;
        //        int? homeTeam = json.homeTeam;
        //        int? guestTeam = json.guestTeam;
        //        int? gameType = json.gameType;
        //        int? status = json.status;
        //        string city=json.city;
        //        string mainReferee=json.mainReferee;
        //        DateTime? dateTime = json.dateTime;
        //        int? homeScore = json.homeScore;
        //        int ? guestScore = json.guestScore;



        //        newGame.game_id = game_id;
        //        newGame.gameName = gameName;
        //        newGame.homeTeam = homeTeam;
        //        newGame.guestTeam = guestTeam;
        //        newGame.gameType = gameType;
        //        newGame.status = status;
        //        newGame.dateTime = dateTime;
        //        newGame.city = city;
        //        newGame.mainReferee = mainReferee;
        //        newGame.homeScore = homeScore;
        //        newGame.guestScore = guestScore;


        //        Console.WriteLine("game_id = " + game_id.ToString());
        //        Console.WriteLine("gameName = " + gameName);
        //        Console.WriteLine("homeTeam = " + homeTeam.ToString());
        //        Console.WriteLine("guestTeam = " + guestTeam.ToString());
        //        Console.WriteLine("gameType = " + gameType.ToString());
        //        Console.WriteLine("status = " + status.ToString());
        //        Console.WriteLine("dateTime = " + dateTime.ToString());
        //        Console.WriteLine("city = " + city);
        //        Console.WriteLine("mainReferee = " + mainReferee);
        //        Console.WriteLine("homeScore = " + homeScore);
        //        Console.WriteLine("guestScore = " + guestScore);




        //        //sqlORM.Insertable(newGame).ExecuteCommand();

        //        List<DateTime?> list = sqlORM.Queryable<Game>().Select(it => it.dateTime).ToList();
        //        Console.WriteLine(list.Count());


        //        //sqlORM.Queryable<Game>().Select(it => it.homeTeam).ToList();

        //        return Ok(new CustomResponse { ok = "yes", value = "Success" });



        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("UNKNOWN");
        //        Console.WriteLine(ex);
        //        return Ok(new CustomResponse { ok = "no", value = "UNKNOWN" }); // Internal server error
        //    }
        //}

        public class TeamInGameTimePara
        {
            public string? dateTime { get; set; }
            public int gameType { get; set; }
        }
        public class TeamInGameTimeVal
        {
            public string? startTime { get; set; }
            public int? homeTeam { get; set; }
            public int? guestTeam { get; set; }
            public string? homeTeamName { get; set; }
            public string? guestTeamName { get; set; }
            public string? status { get; set; }
            public int? homeScore { get; set; }
            public int? guestScore { get; set; }
            public string? homeLogo { get; set; }
            public string? guestLogo { get; set;}
            public string? gameUid { get; set; }
            
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
                List<string> gameNames = new List<string> {"", "英超", "西甲" ,"意甲","德甲","法甲","中超"};
                string? dateTime = json.dateTime;
                Console.WriteLine("dateTime = "+dateTime);
                string? year = dateTime.Substring(0, 4);
                string? month = dateTime.Substring(5, 2);
                string? day = dateTime.Substring(8, 2);
                int gameType = json.gameType;
                Console.WriteLine("gameType = " + gameType.ToString());

                DateTime temp;

                ans = await sqlORM.Queryable<Game>()
                    .LeftJoin<Team>((g, home) => g.homeTeam == home.team_id)
                    .LeftJoin<Team>((g, home, guest) => g.guestTeam == guest.team_id)
                    .Where((g, home, guest) =>
                    g.startTime.Value.ToString("yyyy-MM-dd") == dateTime
                    //g.startTime.Value.Year.ToString()==year
                    //&& (g.startTime.Value.Month < 10 ? "0" : "") + g.startTime.Value.Month.ToString() == month
                    //&& (g.startTime.Value.Day < 10 ? "0" : "") + g.startTime.Value.Day.ToString() == day
                    && ((gameType != 0 && g.type == gameNames[gameType])||gameType==0)
                    )
                    .Select((g, home, guest) => new TeamInGameTimeVal
                    {
                        gameUid=g.game_id.ToString(),
                        startTime=g.startTime.Value.ToString("HH") + ":" + (g.startTime.Value.Minute < 10 ? "0" : "") + g.startTime.Value.Minute.ToString(),
                        //    startTime = (g.startTime.Value.Hour<10?"0":"")+ g.startTime.Value.Hour.ToString() + ":"
                        //+(g.startTime.Value.Minute<10? "0":"") + g.startTime.Value.Minute.ToString(),
                        homeTeamName = home.chinesename,
                        homeTeam=home.team_id,
                        guestTeam=guest.team_id,
                        guestTeamName = guest.chinesename,
                        status=g.status,
                        homeLogo=home.logo,
                        guestLogo=guest.logo
                        //homeScore=g.homeScore,
                        //guestScore=g.guestScore
                    })
                    .ToListAsync();

                Console.WriteLine("ansCnt = "+ans.Count().ToString());


                // 接下来对比赛进行比分筛选
                for (int i = 0; i < ans.Count(); i++)
                {
                    int? game_id = int.Parse(ans[i].gameUid);
                    int? homeTeam = ans[i].homeTeam;
                    int? guestTeam = ans[i].guestTeam;


                    ans[i].homeScore = await sqlORM.Queryable<TeamOwnPlayer>()
                        .LeftJoin<PlayerJoinGame>((top, pjg) => top.team_id == homeTeam && top.player_id == pjg.player_id && pjg.game_id == game_id)
                        .SumAsync((top, pjg) => pjg.goal);


                    ans[i].guestScore = await sqlORM.Queryable<TeamOwnPlayer>()
                        .LeftJoin<PlayerJoinGame>((top, pjg) => top.team_id == guestTeam && top.player_id == pjg.player_id && pjg.game_id == game_id)
                        .SumAsync((top, pjg) => pjg.goal);


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


        public class getGameByUidPara
        {
            public string? gameUid { get; set; }
        }
        public class recentGamesVal
        {
            public string? gameDate { get; set; }
            public string? opponentName { get; set; }
            public int? opponentTeamId { get; set; }
            public int? homeScore { get; set; }
            public int? opponentScore { get; set; }
            public string? opponentLogo { get; set; }
            public string? gameUid { get; set; }
        }
        public class getGameByUidVal
        {
            public string? dateTime { get; set; }
            public int homeTeam { get; set; }
            public int? guestTeam { get;set; }
            public string? homeTeamName { get; set; }
            public string? guestTeamName { get; set; }
            public string? status { get; set; }
            public int? homeScore { get; set; }
            public int? guestScore { get; set; }
            public string? homeLogo { get; set; }
            public string? guestLogo { get; set; }
            public string? homeLink { get; set; }
            public string? guestLink { get; set; }
            public string? liveStream { get; set; }
            public List<recentGamesVal?>? homeRecentGames { get; set; }

        }

        [HttpPost]
        public async Task<getGameByUidVal> getGameByUid([FromBody] getGameByUidPara json)
        {
            ORACLEconn ORACLEConnectTry = new ORACLEconn();
            ORACLEConnectTry.getConn();
            try
            {
                SqlSugarClient sqlORM = ORACLEConnectTry.sqlORM;

                int? gameUid = Convert.ToInt32(json.gameUid);
                Console.WriteLine(gameUid);

                List<getGameByUidVal> ans = new List<getGameByUidVal>();


                ans = await sqlORM.Queryable<Game>()
                    .LeftJoin<Team>((g, home) => g.homeTeam == home.team_id)
                    .LeftJoin<Team>((g, home, guest) => g.guestTeam == guest.team_id)
                    .Where((g, home, guest) => g.game_id == gameUid)
                    .Select((g, home, guest) => new getGameByUidVal
                    {
                        dateTime=g.startTime.Value.ToString("yyyy-MM-dd"),
                        homeTeamName = home.chinesename,
                        guestTeamName = guest.chinesename,
                        status = g.status,
                        homeLogo = home.logo,
                        guestLogo = guest.logo,
                        homeTeam = home.team_id,
                        guestTeam = guest.team_id,
                        homeLink = "www.baidu.com",
                        guestLink = "www.baidu.com",
                        liveStream = g.liveUrl
                    })
                    .ToListAsync();

                //计算分数
                if (ans.Count != 0)
                {
                    ans[0].homeScore= await sqlORM.Queryable<TeamOwnPlayer>()
                        .LeftJoin<PlayerJoinGame>((top, pjg) => top.team_id == ans[0].homeTeam && top.player_id == pjg.player_id && pjg.game_id == gameUid)
                        .SumAsync((top, pjg) => pjg.goal);

                    ans[0].guestScore = await sqlORM.Queryable<TeamOwnPlayer>()
                        .LeftJoin<PlayerJoinGame>((top, pjg) => top.team_id == ans[0].guestTeam && top.player_id == pjg.player_id && pjg.game_id == gameUid)
                        .SumAsync((top, pjg) => pjg.goal);

                }


                //查询后面三场赛事
                if (ans.Count != 0)
                {
                    ans[0].homeRecentGames = sqlORM.Queryable<Game>()
                        .LeftJoin<Team>((gg, myTeam) => gg.homeTeam == myTeam.team_id || gg.guestTeam == myTeam.team_id)
                        .LeftJoin<Team>((gg, myTeam, opponentTeam) => (gg.guestTeam + gg.homeTeam) == myTeam.team_id + opponentTeam.team_id)
                        .Where((gg, myTeam, opponentTeam) => myTeam.team_id == ans[0].homeTeam&&gg.status=="Played")
                        .OrderBy((gg, myTeam, opponentTeam) => gg.startTime, OrderByType.Desc)
                        .Take(3)
                        .Select((gg, myTeam, opponentTeam) => new recentGamesVal
                        {
                            //gameDate = (gg.dateTime.Value.Year.ToString()) + "-"
                            //+ (gg.dateTime.Value.Month < 10 ? "0" : "") + gg.dateTime.Value.Month.ToString() + "-"
                            //+ (gg.dateTime.Value.Day < 10 ? "0" : "") + gg.dateTime.Value.Day.ToString(),
                            gameDate=gg.startTime.Value.ToString("yyyy-MM-dd"),
                            opponentName = opponentTeam.chinesename,
                            opponentTeamId = opponentTeam.team_id,
                            //homeScore = (gg.homeTeam == myTeam.team_id ? gg.homeScore : gg.guestScore),
                            //opponentScore = (gg.homeTeam == myTeam.team_id ? gg.guestScore : gg.homeScore),
                            opponentLogo = opponentTeam.logo,
                            gameUid = gg.game_id.ToString()

                        })
                        .ToList();

                    //计算近几场得分
                    for (int i = 0; i < ans[0].homeRecentGames.Count(); i++)
                    {


                        int? game_id = int.Parse(ans[0].homeRecentGames[i].gameUid);
                        int? thisTeam = ans[0].homeTeam;
                        int? opponentTeam = ans[0].homeRecentGames[i].opponentTeamId;


                        ans[0].homeRecentGames[i].homeScore = await sqlORM.Queryable<TeamOwnPlayer>()
                            .LeftJoin<PlayerJoinGame>((top, pjg) => top.team_id == thisTeam && top.player_id == pjg.player_id && pjg.game_id == game_id)
                            .SumAsync((top, pjg) => pjg.goal);

                        ans[0].homeRecentGames[i].opponentScore= await sqlORM.Queryable<TeamOwnPlayer>()
                            .LeftJoin<PlayerJoinGame>((top, pjg) => top.team_id == opponentTeam && top.player_id == pjg.player_id && pjg.game_id == game_id)
                            .SumAsync((top, pjg) => pjg.goal);



                    }
                }

                Console.WriteLine(ans.Count());




                return ans[0];


            }
            catch (Exception ex)
            {
                Console.WriteLine("failed");
                Console.WriteLine(ex);
                return null;
            }
        }



        public class getTeamMatchesByNamePara
        {
            public string? teamName { get; set; }
        }
        public class getTeamMatchesByNameVal
        {
            public string? gameDate { get;set; }
            public int? homeTeam { get; set; }
            public int? opponentTeam { get; set; }
            public string? oppoentName { get; set; }
            public int? homeScore { get; set;}
            public int? opponentScore { get; set;}
            public string? opponentLogo { get; set; }
            public string? gameUid { get; set;}

        }



        [HttpPost]
        public async Task<List<getTeamMatchesByNameVal>> getTeamMatchesByName([FromBody] getTeamMatchesByNamePara json)
        {
            ORACLEconn ORACLEConnectTry = new ORACLEconn();
            ORACLEConnectTry.getConn();
            try
            {
                SqlSugarClient sqlORM = ORACLEConnectTry.sqlORM;

                string? teamName = json.teamName;

                List<getTeamMatchesByNameVal> ans = new List<getTeamMatchesByNameVal>();

                ans = await sqlORM.Queryable<Game>()
                    .LeftJoin<Team>((g, home) => g.homeTeam == home.team_id)
                    .LeftJoin<Team>((g, home, guest) => g.guestTeam == guest.team_id)
                    .Where((g, home, guest) =>( home.chinesename == teamName || guest.chinesename == teamName)&&g.status=="Played")
                    .OrderBy((g, home, guest) => g.startTime.Value, OrderByType.Desc)
                    .Take(3)
                    .Select((g, home, guest) => new getTeamMatchesByNameVal
                    {
                        gameDate=g.startTime.Value.ToString("yyyy-MM-dd"),
                        homeTeam= (home.chinesename == teamName ? home.team_id : guest.team_id),
                        opponentTeam = (home.chinesename == teamName ? guest.team_id : home.team_id),
                        oppoentName = (home.chinesename == teamName ? guest.chinesename : home.chinesename),
                        opponentLogo = (home.chinesename == teamName ? guest.logo : home.logo),
                        gameUid = g.game_id.ToString()

                    })
                    .ToListAsync();

                //计算分数
                for(int i = 0; i < ans.Count; i++)
                {

                    int? game_id = int.Parse(ans[i].gameUid);
                    int? homeTeam = ans[i].homeTeam;
                    int? opponentTeam = ans[i].opponentTeam;


                    ans[i].homeScore = await sqlORM.Queryable<TeamOwnPlayer>()
                        .LeftJoin<PlayerJoinGame>((top, pjg) => top.team_id == homeTeam && top.player_id == pjg.player_id && pjg.game_id == game_id)
                        .SumAsync((top, pjg) => pjg.goal);


                    ans[i].opponentScore = await sqlORM.Queryable<TeamOwnPlayer>()
                        .LeftJoin<PlayerJoinGame>((top, pjg) => top.team_id == opponentTeam && top.player_id == pjg.player_id && pjg.game_id == game_id)
                        .SumAsync((top, pjg) => pjg.goal);
                }

                return ans;


            }
            catch (Exception ex)
            {
                Console.WriteLine("failed");
                Console.WriteLine(ex);
                return null;
            }
        }






        public class TeamInGameTypePara
        {
            public int gameType { get; set; }
        }

        public class TeamInGameTypeVal
        {
            //public string? gameName { get; set; }
            public string? teamName { get; set; }
            public string ? teamLogo { get; set; }
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

                int gameType = json.gameType;
                List<string> gameNames = new List<string> { "", "英超", "西甲", "意甲", "德甲", "法甲", "中超" };


                var homeTeam = sqlORM.Queryable<Game>()
                    .LeftJoin<Team>((g, home) => g.homeTeam == home.team_id)
                    .Distinct()
                    .Where((g, home) => ((gameType != 0 && g.type == gameNames[gameType]) || gameType == 0))
                    .Select((g, home) => new TeamInGameTypeVal {
                        teamLogo = home.logo,
                        teamName = home.chinesename, });

                var guestTeam = sqlORM.Queryable<Game>()
                    .LeftJoin<Team>((g, guest) => g.guestTeam == guest.team_id)
                    .Distinct()
                    .Where((g, guest) => ((gameType != 0 && g.type == gameNames[gameType]) || gameType == 0))
                    .Select((g, guest) => new TeamInGameTypeVal { 
                        teamLogo=guest.logo,
                        teamName = guest.chinesename, });


                ans = await sqlORM.Union(homeTeam, guestTeam).ToListAsync();

                Console.WriteLine("team Count = "+ans.Count().ToString());

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



        public class getTeamInfoByNamePara
        {
            public string teamName{ get; set; }
        }
        public class teamMemberVal
        {
            public string? playerName { get; set; }
            public string? playerPhoto { get; set;}
        }
        public class getTeamInfoByNameVal
        {
            public string teamName { get; set; }
            public int? team_id { get; set; }
            public string? enName { get; set; }
            public string? logo { get; set; }
            public string? city { get; set; }
            public int? foundYear { get; set; }
            public string? coach { get; set; }
            public List<teamMemberVal>? teamMember { get; set; }
            public List<recentGamesVal?>? recentGames { get; set; }
        }
        [HttpPost]
        public async Task<List<getTeamInfoByNameVal>> getTeamInfoByName([FromBody] getTeamInfoByNamePara json)
        {

            ORACLEconn ORACLEConnectTry = new ORACLEconn();
            ORACLEConnectTry.getConn();
            try
            {
                SqlSugarClient sqlORM = ORACLEConnectTry.sqlORM;
                string? teamName = json.teamName;
                List<getTeamInfoByNameVal> ans = new List<getTeamInfoByNameVal>();
                ans = await sqlORM.Queryable<Team>()
                    .Where(it=>it.chinesename==teamName)
                    .Select(it => new getTeamInfoByNameVal {
                        teamName=it.chinesename,
                        team_id=it.team_id,
                        enName=it.enname,
                        logo=it.logo,
                        city=it.city,
                        foundYear=it.foundedyear,
                        coach=it.coach,
                    })
                    .ToListAsync();

                //添加最近赛事
                if (ans.Count() != 0)
                {
                    ans[0].teamMember=sqlORM.Queryable<TeamOwnPlayer>()
                        .LeftJoin<Players>((top,p)=>top.player_id==p.player_id)
                        .Where((top, p) => top.team_id == ans[0].team_id)
                        .Select((top,p)=>new teamMemberVal
                        {
                            playerName=p.chineseName,
                            playerPhoto=p.photo
                        })
                        .ToList() ;


                    ans[0].recentGames = sqlORM.Queryable<Game>()
                         .LeftJoin<Team>((gg, myTeam) => gg.homeTeam == myTeam.team_id || gg.guestTeam == myTeam.team_id)
                         .LeftJoin<Team>((gg, myTeam, opponentTeam) => (gg.guestTeam + gg.homeTeam) == myTeam.team_id + opponentTeam.team_id)
                         .Where((gg, myTeam, opponentTeam) => myTeam.team_id == ans[0].team_id && gg.status == "Played")
                         .OrderBy((gg, myTeam, opponentTeam) => gg.startTime, OrderByType.Desc)
                         .Take(3)
                         .Select((gg, myTeam, opponentTeam) => new recentGamesVal
                         {
                             gameDate = gg.startTime.Value.ToString("yyyy-MM-dd"),
                             opponentName = opponentTeam.chinesename,
                             opponentTeamId = opponentTeam.team_id,
                             opponentLogo = opponentTeam.logo,
                             gameUid = gg.game_id.ToString()
                         })
                         .ToList();

                    for (int i = 0; i < ans[0].recentGames.Count(); i++)
                    {

                        int? game_id = int.Parse(ans[0].recentGames[i].gameUid);
                        int? thisTeam = ans[0].team_id;
                        int? opponentTeam = ans[0].recentGames[i].opponentTeamId;


                        ans[0].recentGames[i].homeScore = await sqlORM.Queryable<TeamOwnPlayer>()
                            .LeftJoin<PlayerJoinGame>((top, pjg) => top.team_id == thisTeam && top.player_id == pjg.player_id && pjg.game_id == game_id)
                            .SumAsync((top, pjg) => pjg.goal);

                        ans[0].recentGames[i].opponentScore = await sqlORM.Queryable<TeamOwnPlayer>()
                            .LeftJoin<PlayerJoinGame>((top, pjg) => top.team_id == opponentTeam && top.player_id == pjg.player_id && pjg.game_id == game_id)
                            .SumAsync((top, pjg) => pjg.goal);


                    }
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







        public class topScorerVal
        {
            public string topScorerName { get; set; }
            public int? goals { get; set; }
        }
        [HttpGet]
        public async Task<List<topScorerVal>> getTopScorers()
        {

            ORACLEconn ORACLEConnectTry = new ORACLEconn();
            ORACLEConnectTry.getConn();
            try
            {
                SqlSugarClient sqlORM = ORACLEConnectTry.sqlORM;
                List<topScorerVal> ans = new List<topScorerVal>();
                ans = await sqlORM.Queryable<Players>()
                    .LeftJoin<PlayerJoinGame>((p, pjg) => p.player_id == pjg.player_id)
                    .GroupBy((p, pjg) => p.chineseName)
                    .Select((p, pjg) => new topScorerVal
                    {
                        topScorerName = p.chineseName,
                        goals= SqlFunc.AggregateSumNoNull(pjg.goal)
                    })
                    .MergeTable()
                    .OrderBy(it=>it.goals,OrderByType.Desc)
                    .Take(10)
                    .ToListAsync();


                return ans;
            }
            catch (Exception ex)
            {
                Console.WriteLine("UNKNOWN");
                Console.WriteLine(ex);
                return null;
            }


        }


        public class searchTeamOrPlayerPara
        {
            public string key { get;set; }
        }
        public class searchedTeamVal
        {
            public string? searchedTeamName { get; set; }
            public string? searchedTeamLogo { get; set; }
        }
        public class searchedPlayerVal
        {
            public string? searchedPlayerName { get; set; }
            public string? searchedPlayerPhoto { get; set; }
        }
        [HttpPost]
        public async Task<List<searchedTeamVal>> searchForTeam([FromBody] searchTeamOrPlayerPara json)
        {

            ORACLEconn ORACLEConnectTry = new ORACLEconn();
            ORACLEConnectTry.getConn();
            try
            {
                SqlSugarClient sqlORM = ORACLEConnectTry.sqlORM;
                List<searchedTeamVal> ans = new List<searchedTeamVal>();

                string key = json.key;
                Console.WriteLine("key word is " + key);

                ans = sqlORM.Queryable<Team>()
                    .Where(t=>t.chinesename.Contains(key)||t.enname.Contains(key))
                    .Select(t => new searchedTeamVal
                    {
                        searchedTeamName=t.chinesename,
                        searchedTeamLogo=t.logo

                    })
                    .ToList();

                Console.WriteLine("team count = " + ans.Count());

                return ans;
            }
            catch (Exception ex)
            {
                Console.WriteLine("failed");
                Console.WriteLine(ex);
                return null;
            }
        }
        [HttpPost]
        public async Task<List<searchedPlayerVal>> searchForPlayer([FromBody] searchTeamOrPlayerPara json)
        {

            ORACLEconn ORACLEConnectTry = new ORACLEconn();
            ORACLEConnectTry.getConn();
            try
            {
                SqlSugarClient sqlORM = ORACLEConnectTry.sqlORM;
                List<searchedPlayerVal> ans = new List<searchedPlayerVal>();

                string key = json.key;
                Console.WriteLine("key word is " + key);

                ans = sqlORM.Queryable<Players>()
                    .Where(p => p.chineseName.Contains(key) || p.enName.Contains(key))
                    .Select(t => new searchedPlayerVal
                    {
                        searchedPlayerName = t.chineseName,
                        searchedPlayerPhoto = t.photo
                    })
                    .ToList();

                Console.WriteLine("player count = " + ans.Count());

                return ans;
            }
            catch (Exception ex)
            {
                Console.WriteLine("failed");
                Console.WriteLine(ex);
                return null;
            }


        }


        public class getPlayerDetailPara
        {
            public string? playerName { get;set; }
        }
        public class relatedPlayer
        {

        }

        public class evenData
        {
            public string? seasonName { get; set; }
            public int? playedTime { get; set; }
            public int? goal { get; set; }

        }
        public class getPlayerDetailVal
        {
            public string? club { get; set; }
            public string? position { get; set; }
            public string? number { get; set; }
            public string? nationality { get; set; }
            public string? age { get; set; }
            public string? height { get; set; }
            public string? dominantFoot { get; set; }

        }


    }
    
}
