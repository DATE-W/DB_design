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
using System.Security.Permissions;
using System.Xml.Linq;

namespace DBwebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class updateTeamController : ControllerBase
    {
        public class TeamInGameTimePara
        {
            public string dateTime { get; set; }
            public int gameType { get; set; }
        }
        public class TeamInGameTimeVal
        {
            public string startTime { get; set; } = "null";
            public int homeTeam { get; set; } = -1;
            public int guestTeam { get; set; } = -1;
            public string homeTeamName { get; set; }= "null";
            public string guestTeamName { get; set; } = "null";
            public string status { get; set; }= "null";
            public int homeScore { get; set; } = -1;
            public int guestScore { get; set; } = -1;
            public string homeLogo { get; set; } = "null";
            public string guestLogo { get; set; } = "null";
            public string gameUid { get; set; } = "null";

        }


        [HttpPost]
        public async Task<List<TeamInGameTimeVal>> searchTeamInGameTime([FromBody] TeamInGameTimePara json)
        {

            Console.WriteLine("--------------------------searchTeamInGameTime--------------------------");
            if (json == null)
            {
                Console.WriteLine("json is null!");
                return null;
            }
            ORACLEconn ORACLEConnectTry = new ORACLEconn();
            ORACLEConnectTry.getConn();
            try
            {
                SqlSugarScope sqlORM = ORACLEConnectTry.sqlORM;

                List<TeamInGameTimeVal> ans = new List<TeamInGameTimeVal>();
                List<string> gameNames = new List<string> { "", "英超", "西甲", "意甲", "德甲", "法甲", "中超" };

                string dateTime = json.dateTime;
                Console.WriteLine("dateTime = " + dateTime);

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
                    && ((gameType != 0 && g.type == gameNames[gameType]) || gameType == 0)
                    )
                    .Select((g, home, guest) => new TeamInGameTimeVal
                    {
                        gameUid = g.game_id.ToString(),
                        startTime = g.startTime.Value.ToString("HH") + ":" + (g.startTime.Value.Minute < 10 ? "0" : "") + g.startTime.Value.Minute.ToString(),
                        homeTeamName = home.chinesename,
                        homeTeam = home.team_id,
                        guestTeam = guest.team_id,
                        guestTeamName = guest.chinesename,
                        status = g.status,
                        homeLogo = home.logo,
                        guestLogo = guest.logo
                    })
                    .ToListAsync();

                Console.WriteLine("ansCnt = " + ans.Count().ToString());


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

                //排序
                for (int i = 0; i < ans.Count(); i++)
                {
                    for (int j = 0; j < ans.Count() - i - 1; j++)
                    {
                        //if (ans[j].timeValue< ans[j + 1].timeValue)
                        if (System.String.Compare(ans[j].startTime, ans[j + 1].startTime) > 0)
                        {
                            TeamInGameTimeVal tempGame = new TeamInGameTimeVal();
                            tempGame = ans[j];
                            ans[j] = ans[j + 1];
                            ans[j + 1] = tempGame;
                        }
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
            public string? gameType { get; set;}
        }
        public class getGameByUidVal
        {
            public string dateTime { get; set; }
            public string startTime { get; set; }
            public int homeTeam { get; set; }
            public int guestTeam { get; set; }
            public string homeTeamName { get; set; }
            public string guestTeamName { get; set; }
            public string leagueName { get; set; }
            public int leagueType { get; set; }
            public string status { get; set; }
            public int homeScore { get; set; }
            public int guestScore { get; set; }
            public string homeLogo { get; set; }
            public string guestLogo { get; set; }
            public string homeLink { get; set; }
            public string guestLink { get; set; }
            public string liveStream { get; set; }
            public List<recentGamesVal?>? homeRecentGames { get; set; }
            public List<recentGamesVal?>? guestRecentGames { get; set; }

        }

        [HttpPost]
        public async Task<getGameByUidVal> getGameByUid([FromBody] getGameByUidPara json)
        {
            Console.WriteLine("--------------------------getGameByUidPara--------------------------");
            ORACLEconn ORACLEConnectTry = new ORACLEconn();
            ORACLEConnectTry.getConn();
            try
            {
                SqlSugarScope sqlORM = ORACLEConnectTry.sqlORM;

                int? gameUid = Convert.ToInt32(json.gameUid);
                Console.WriteLine(gameUid);

                List<getGameByUidVal> ans = new List<getGameByUidVal>();
                ans = await sqlORM.Queryable<Game>()
                    .LeftJoin<Team>((g, home) => g.homeTeam == home.team_id)
                    .LeftJoin<Team>((g, home, guest) => g.guestTeam == guest.team_id)
                    .Where((g, home, guest) => g.game_id == gameUid)
                    .Select((g, home, guest) => new getGameByUidVal
                    {
                        dateTime = g.startTime.Value.ToString("yyyy-MM-dd"),
                        startTime = g.startTime.Value.ToString("HH") + ":" + (g.startTime.Value.Minute < 10 ? "0" : "") + g.startTime.Value.Minute.ToString(),
                        homeTeamName = home.chinesename,
                        guestTeamName = guest.chinesename,
                        leagueName = g.type,
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

                if (ans.Count != 0)
                {
                    Console.WriteLine("found!");
                }
                //计算分数
                if (ans.Count != 0)
                {
                    List<string> gameNames = new List<string> { "英超", "西甲", "意甲", "德甲", "法甲", "中超" };
                    for (int i = 0; i < gameNames.Count(); i++)
                    {
                        if (ans[0].leagueName == gameNames[i])
                            ans[0].leagueType = i + 1;
                    }



                    ans[0].homeScore = await sqlORM.Queryable<TeamOwnPlayer>()
                        .LeftJoin<PlayerJoinGame>((top, pjg) => top.team_id == ans[0].homeTeam && top.player_id == pjg.player_id && pjg.game_id == gameUid)
                        .SumAsync((top, pjg) => pjg.goal);

                    ans[0].guestScore = await sqlORM.Queryable<TeamOwnPlayer>()
                        .LeftJoin<PlayerJoinGame>((top, pjg) => top.team_id == ans[0].guestTeam && top.player_id == pjg.player_id && pjg.game_id == gameUid)
                        .SumAsync((top, pjg) => pjg.goal);

                }


                //查询后面三场赛事
                if (ans.Count != 0)
                {
                    ans[0].homeRecentGames = await sqlORM.Queryable<Game>()
                        .LeftJoin<Team>((gg, myTeam) => gg.homeTeam == myTeam.team_id || gg.guestTeam == myTeam.team_id)
                        .LeftJoin<Team>((gg, myTeam, opponentTeam) => (gg.guestTeam + gg.homeTeam) == myTeam.team_id + opponentTeam.team_id)
                        .Where((gg, myTeam, opponentTeam) => myTeam.team_id == ans[0].homeTeam
                        && gg.status == "Played"
                        )
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
                        .ToListAsync();

                    //计算近几场得分
                    for (int i = 0; i < ans[0].homeRecentGames.Count(); i++)
                    {


                        int? game_id = int.Parse(ans[0].homeRecentGames[i].gameUid);
                        int? thisTeam = ans[0].homeTeam;
                        int? opponentTeam = ans[0].homeRecentGames[i].opponentTeamId;


                        ans[0].homeRecentGames[i].homeScore = await sqlORM.Queryable<TeamOwnPlayer>()
                            .LeftJoin<PlayerJoinGame>((top, pjg) => top.team_id == thisTeam && top.player_id == pjg.player_id && pjg.game_id == game_id)
                            .SumAsync((top, pjg) => pjg.goal);

                        ans[0].homeRecentGames[i].opponentScore = await sqlORM.Queryable<TeamOwnPlayer>()
                            .LeftJoin<PlayerJoinGame>((top, pjg) => top.team_id == opponentTeam && top.player_id == pjg.player_id && pjg.game_id == game_id)
                            .SumAsync((top, pjg) => pjg.goal);
                    }




                    ans[0].guestRecentGames =await sqlORM.Queryable<Game>()
                        .LeftJoin<Team>((gg, myTeam) => gg.homeTeam == myTeam.team_id || gg.guestTeam == myTeam.team_id)
                        .LeftJoin<Team>((gg, myTeam, opponentTeam) => (gg.guestTeam + gg.homeTeam) == myTeam.team_id + opponentTeam.team_id)
                        .Where((gg, myTeam, opponentTeam) => myTeam.team_id == ans[0].guestTeam
                        && gg.status == "Played"
                        )
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
                        .ToListAsync();

                    //计算近几场得分
                    for (int i = 0; i < ans[0].guestRecentGames.Count(); i++)
                    {


                        int? game_id = int.Parse(ans[0].guestRecentGames[i].gameUid);
                        int? thisTeam = ans[0].guestTeam;
                        int? opponentTeam = ans[0].guestRecentGames[i].opponentTeamId;


                        ans[0].guestRecentGames[i].homeScore = await sqlORM.Queryable<TeamOwnPlayer>()
                            .LeftJoin<PlayerJoinGame>((top, pjg) => top.team_id == thisTeam && top.player_id == pjg.player_id && pjg.game_id == game_id)
                            .SumAsync((top, pjg) => pjg.goal);

                        ans[0].guestRecentGames[i].opponentScore = await sqlORM.Queryable<TeamOwnPlayer>()
                            .LeftJoin<PlayerJoinGame>((top, pjg) => top.team_id == opponentTeam && top.player_id == pjg.player_id && pjg.game_id == game_id)
                            .SumAsync((top, pjg) => pjg.goal);
                    }


                    Console.WriteLine(ans.Count());


                    return ans[0];
                }

                else
                {
                    return null;
                }


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
            public string? gameDate { get; set; }
            public int? homeTeam { get; set; }
            public int? opponentTeam { get; set; }
            public string? opponentName { get; set; }
            public int? homeScore { get; set; }
            public int? opponentScore { get; set; }
            public string? opponentLogo { get; set; }
            public string? gameUid { get; set; }

        }



        [HttpPost]
        public async Task<List<getTeamMatchesByNameVal>> getTeamMatchesByName([FromBody] getTeamMatchesByNamePara json)
        {
            Console.WriteLine("--------------------------getTeamMatchesByName--------------------------");
            ORACLEconn ORACLEConnectTry = new ORACLEconn();
            ORACLEConnectTry.getConn();
            try
            {
                SqlSugarScope sqlORM = ORACLEConnectTry.sqlORM;

                string? teamName = json.teamName;

                List<getTeamMatchesByNameVal> ans = new List<getTeamMatchesByNameVal>();

                ans = await sqlORM.Queryable<Game>()
                    .LeftJoin<Team>((g, home) => g.homeTeam == home.team_id)
                    .LeftJoin<Team>((g, home, guest) => g.guestTeam == guest.team_id)
                    .Where((g, home, guest) => (home.chinesename == teamName || guest.chinesename == teamName) && g.status == "Played")
                    .OrderBy((g, home, guest) => g.startTime.Value, OrderByType.Desc)
                    .Take(3)
                    .Select((g, home, guest) => new getTeamMatchesByNameVal
                    {
                        gameDate = g.startTime.Value.ToString("yyyy-MM-dd"),
                        homeTeam = (home.chinesename == teamName ? home.team_id : guest.team_id),
                        opponentTeam = (home.chinesename == teamName ? guest.team_id : home.team_id),
                        opponentName = (home.chinesename == teamName ? guest.chinesename : home.chinesename),
                        opponentLogo = (home.chinesename == teamName ? guest.logo : home.logo),
                        gameUid = g.game_id.ToString()

                    })
                    .ToListAsync();

                //计算分数
                for (int i = 0; i < ans.Count; i++)
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
            public string? teamLogo { get; set; }
            //public string? guestTeamName { get; set; }

        }

        [HttpPost]
        public async Task<List<TeamInGameTypeVal>> searchTeamInGameType([FromBody] TeamInGameTypePara json)
        {
            Console.WriteLine("--------------------------searchTeamInGameType--------------------------");
            ORACLEconn ORACLEConnectTry = new ORACLEconn();
            ORACLEConnectTry.getConn();
            try
            {
                SqlSugarScope sqlORM = ORACLEConnectTry.sqlORM;

                List<TeamInGameTypeVal> ans = new List<TeamInGameTypeVal>();

                int gameType = json.gameType;
                List<string> gameNames = new List<string> { "", "英超", "西甲", "意甲", "德甲", "法甲", "中超" };


                var homeTeam = sqlORM.Queryable<Game>()
                    .LeftJoin<Team>((g, home) => g.homeTeam == home.team_id)
                    .Distinct()
                    .Where((g, home) => ((gameType != 0 && g.type == gameNames[gameType]) || gameType == 0))
                    .Select((g, home) => new TeamInGameTypeVal
                    {
                        teamLogo = home.logo,
                        teamName = home.chinesename,
                    });

                var guestTeam = sqlORM.Queryable<Game>()
                    .LeftJoin<Team>((g, guest) => g.guestTeam == guest.team_id)
                    .Distinct()
                    .Where((g, guest) => ((gameType != 0 && g.type == gameNames[gameType]) || gameType == 0))
                    .Select((g, guest) => new TeamInGameTypeVal
                    {
                        teamLogo = guest.logo,
                        teamName = guest.chinesename,
                    });


                ans = await sqlORM.Union(homeTeam, guestTeam).ToListAsync();

                Console.WriteLine("team Count = " + ans.Count().ToString());

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
            public string teamName { get; set; } = "null";
        }
        public class teamMemberVal
        {
            public int? player_id { get; set; }
            public string? playerName { get; set; }
            public string? playerPhoto { get; set; }
            public string? playerPosition { get; set; }
            public string? playerNumber { get; set;}
            public int? playerAppearance { get; set;}
            public int? playerShoot { get; set;}
            public int? playerGoal { get; set;}
            public string? playerNationality { get; set;}
        }
        public class getTeamInfoByNameVal
        {
            public string teamName { get; set; } = "null";
            public int? team_id { get; set; }
            public string? enName { get; set; }
            public string? logo { get; set; }
            public string? city { get; set; }
            public int? foundYear { get; set; }
            public string? coach { get; set; }
            public string? country { get; set; }
            public string? telephone { get; set; }
            public string? address { get; set; }
            public string? venue_name { get; set; }
            public string? email { get; set; }
            public int? venue_capacity { get; set; }
            public string? leagueType { get; set; }

            public List<teamMemberVal>? teamMember { get; set; }
            public List<recentGamesVal?>? recentGames { get; set; }
        }
        [HttpPost]
        public async Task<List<getTeamInfoByNameVal>> getTeamInfoByName([FromBody] getTeamInfoByNamePara json)
        {
            Console.WriteLine("--------------------------getTeamInfoByName--------------------------");
            ORACLEconn ORACLEConnectTry = new ORACLEconn();
            ORACLEConnectTry.getConn();
            try
            {
                SqlSugarScope sqlORM = ORACLEConnectTry.sqlORM;
                string? teamName = json.teamName;
                List<getTeamInfoByNameVal> ans = new List<getTeamInfoByNameVal>();
                ans = await sqlORM.Queryable<Team>()
                    .Where(it => it.chinesename == teamName)
                    .Select(it => new getTeamInfoByNameVal
                    {
                        teamName = it.chinesename,
                        team_id = it.team_id,
                        enName = it.enname,
                        logo = it.logo,
                        city = it.city,
                        foundYear = it.foundedyear,
                        coach = it.coach,
                        country = it.country,
                        telephone = it.telephone,
                        address = it.address,
                        venue_capacity = (it.venue_capacity == null ? 0 : it.venue_capacity),
                        venue_name = it.venue_name,
                        email = it.email
                    })
                    .ToListAsync();

                //添加最近赛事
                if (ans.Count() != 0)
                {
                    //先获取球员信息
                    ans[0].teamMember =await sqlORM.Queryable<TeamOwnPlayer>()
                        .LeftJoin<Players>((top, p) => top.player_id == p.player_id)
                        .Where((top, p) => top.team_id == ans[0].team_id)
                        .Select((top, p) => new teamMemberVal
                        {
                            player_id = p.player_id,
                            playerName = p.chineseName,
                            playerPhoto = p.photo,
                            playerNationality = p.country,
                            playerPosition = p.type,
                            playerNumber = p.playerNumber
                        })
                        .ToListAsync();

                    //接下来循环添加球员数据
                    for (int i = 0; i < ans[0].teamMember.Count(); i++)
                    {
                        var temp = await sqlORM.Queryable<Players>()
                            .LeftJoin<PlayerJoinGame>((p, pjg) => p.player_id == pjg.player_id)
                            .Where((p, pjg) => p.player_id == ans[0].teamMember[i].player_id)
                            .Select((p, pjg) => new teamMemberVal
                            {
                                playerAppearance = SqlFunc.AggregateCount(pjg.game_id),
                                playerGoal= SqlFunc.AggregateSum(pjg.goal),
                                playerShoot = SqlFunc.AggregateSum(pjg.shoot)
                            })
                            .ToListAsync();

                        ans[0].teamMember[i].playerAppearance = (temp[0].playerAppearance==null?0:temp[0].playerAppearance);
                        ans[0].teamMember[i].playerGoal = (temp[0].playerGoal == null ? 0 : temp[0].playerGoal);
                        ans[0].teamMember[i].playerShoot = (temp[0].playerShoot == null ? 0 : temp[0].playerShoot);

                    }

                    //添加最近比赛数据
                    ans[0].recentGames =await sqlORM.Queryable<Game>()
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
                             gameUid = gg.game_id.ToString(),
                             gameType=gg.type
                         })
                         .ToListAsync();

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

                        ans[0].leagueType = ans[0].recentGames[i].gameType;
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
            Console.WriteLine("--------------------------getTopScorers--------------------------");
            ORACLEconn ORACLEConnectTry = new ORACLEconn();
            ORACLEConnectTry.getConn();
            try
            {
                SqlSugarScope sqlORM = ORACLEConnectTry.sqlORM;
                List<topScorerVal> ans = new List<topScorerVal>();
                ans = await sqlORM.Queryable<Players>()
                    .LeftJoin<PlayerJoinGame>((p, pjg) => p.player_id == pjg.player_id)
                    .GroupBy((p, pjg) => p.chineseName)
                    .Select((p, pjg) => new topScorerVal
                    {
                        topScorerName = p.chineseName,
                        goals = SqlFunc.AggregateSumNoNull(pjg.goal)
                    })
                    .MergeTable()
                    .OrderBy(it => it.goals, OrderByType.Desc)
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

        public class topScorersInGameTypePara
        {
            public string gameName { get; set; }

        }
        public class topScorersInGameTypeVal
        {
            public int? player_id { get; set; }
            public int? goals { get; set; }
            public string? playerName { get; set; }
            public string? photo { get; set; }
            public string? teamName { get; set; }
            public string? teamLogo { get; set; }
        }
        [HttpPost]
        public async Task<List<topScorersInGameTypeVal>> topScorersInGameType([FromBody] topScorersInGameTypePara json)
        {
            Console.WriteLine("--------------------------topScorersInGameType--------------------------");
            ORACLEconn ORACLEConnectTry = new ORACLEconn();
            ORACLEConnectTry.getConn();

            string? gameName = json.gameName;
            Console.WriteLine("game name is " + gameName);
            try
            {
                SqlSugarScope sqlORM = ORACLEConnectTry.sqlORM;

                DBconn test1 = new DBconn();
                DBconn test2 = new DBconn();
                DBconn test3 = new DBconn();

                List<topScorersInGameTypeVal> ans = new List<topScorersInGameTypeVal>();
                //ans = await sqlORM.Queryable<Players>()
                //    .LeftJoin<PlayerJoinGame>((p, pjg) => p.player_id == pjg.player_id)
                //    .LeftJoin<Game>((p, pjg, g) => g.game_id == pjg.game_id)
                //    .Where((p, pjg, g) => g.type == gameName)
                //    .GroupBy((p, pjg, g) => p.player_id)
                //    .Select((p, pjg, g) => new topScorersInGameTypeVal
                //    {
                //        player_id = p.player_id,
                //        goals = SqlFunc.AggregateSumNoNull(pjg.goal),
                //    })
                //    .MergeTable()
                //    .OrderBy(it => it.goals, OrderByType.Desc)
                //    .Take(15)
                //    .ToListAsync();

                ans = await test1.Db.Queryable<Players>()
    .LeftJoin<PlayerJoinGame>((p, pjg) => p.player_id == pjg.player_id)
    .LeftJoin<Game>((p, pjg, g) => g.game_id == pjg.game_id)
    .Where((p, pjg, g) => g.type == gameName)
    .GroupBy((p, pjg, g) => p.player_id)
    .Select((p, pjg, g) => new topScorersInGameTypeVal
    {
        player_id = p.player_id,
        goals = SqlFunc.AggregateSumNoNull(pjg.goal),
    })
    .MergeTable()
    .OrderBy(it => it.goals, OrderByType.Desc)
    .Take(15)
    .ToListAsync();



                for (int i = 0; i < ans.Count(); i++)
                {
                    //var temp =await sqlORM.Queryable<Players>()
                    //    .LeftJoin<TeamOwnPlayer>((p, top) => p.player_id == top.player_id)
                    //    .LeftJoin<Team>((p, top, t) => top.team_id == t.team_id)
                    //    .Where((p, top, t) => p.player_id == ans[i].player_id)
                    //    .Select((p, top, t) => new topScorersInGameTypeVal
                    //    {
                    //        teamLogo = t.logo,
                    //        teamName = t.chinesename,
                    //        playerName = p.chineseName,
                    //        photo = p.photo
                    //    })
                    //    .ToListAsync();
                    var temp_orm= new DBconn();


                    var temp = await temp_orm.Db.Queryable<Players>()
    .LeftJoin<TeamOwnPlayer>((p, top) => p.player_id == top.player_id)
    .LeftJoin<Team>((p, top, t) => top.team_id == t.team_id)
    .Where((p, top, t) => p.player_id == ans[i].player_id)
    .Select((p, top, t) => new topScorersInGameTypeVal
    {
        teamLogo = t.logo,
        teamName = t.chinesename,
        playerName = p.chineseName,
        photo = p.photo
    })
    .ToListAsync();

                    ans[i].teamLogo = temp[0].teamLogo;
                    ans[i].teamName = temp[0].teamName;
                    ans[i].playerName = temp[0].playerName;
                    ans[i].photo = temp[0].photo;

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




        public class searchTeamOrPlayerPara
        {
            public string key { get; set; }
            public int gameType { get; set; }
        }
        public class searchedTeamVal
        {
            public int? searchedTeamId { get; set; }
            public string? searchedTeamName { get; set; }
            public string? searchedTeamLogo { get; set; }
        }
        public class searchedPlayerVal
        {
            public int? searchedPlayerId { get; set; }
            public string? searchedPlayerName { get; set; }
            public string? searchedPlayerPhoto { get; set; }
        }
        [HttpPost]
        public async Task<List<searchedTeamVal>> searchForTeam([FromBody] searchTeamOrPlayerPara json)
        {
            Console.WriteLine("--------------------------searchForTeam--------------------------");
            ORACLEconn ORACLEConnectTry = new ORACLEconn();
            ORACLEConnectTry.getConn();
            try
            {
                SqlSugarScope sqlORM = ORACLEConnectTry.sqlORM;
                List<searchedTeamVal> ans = new List<searchedTeamVal>();

                List<string> gameNames = new List<string> { "", "英超", "西甲", "意甲", "德甲", "法甲", "中超" };

                string key = json.key;
                int gameType = json.gameType;

                Console.WriteLine("key word is " + key);
                Console.WriteLine("game type is " + gameType.ToString());

                var allTeam =await sqlORM.Queryable<Team>()
                    .Where(t => t.chinesename.Contains(key) || t.enname.Contains(key))
                    .Select(t => new searchedTeamVal
                    {
                        searchedTeamId = t.team_id,
                        searchedTeamName = t.chinesename,
                        searchedTeamLogo = t.logo

                    })
                    .ToListAsync();


                //gameType=0,无需筛选
                if (gameType == 0)
                {
                    Console.WriteLine("team count = " + allTeam.Count());
                    return allTeam;
                }

                for (int i = 0; i < allTeam.Count(); i++)
                {
                    var relatedGame = await sqlORM.Queryable<Game>()
                        .Where(it => it.homeTeam == allTeam[i].searchedTeamId || it.guestTeam == allTeam[i].searchedTeamId)
                        .Select(it => it.type)
                        .Distinct()
                        .ToListAsync();
                    if (relatedGame.Contains(gameNames[gameType]))
                    {
                        ans.Add(allTeam[i]);
                    }
                }


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
            Console.WriteLine("--------------------------searchForPlayer--------------------------");
            ORACLEconn ORACLEConnectTry = new ORACLEconn();
            ORACLEConnectTry.getConn();
            try
            {
                SqlSugarScope sqlORM = ORACLEConnectTry.sqlORM;
                List<searchedPlayerVal> ans = new List<searchedPlayerVal>();

                List<string> gameNames = new List<string> { "", "英超", "西甲", "意甲", "德甲", "法甲", "中超" };

                string key = json.key;
                int gameType = json.gameType;

                Console.WriteLine("key word is " + key);
                Console.WriteLine("game type is " + gameType.ToString());

                var allPlayer =await sqlORM.Queryable<Players>()
                    .Where(p => p.chineseName.Contains(key) || p.enName.Contains(key))
                    .Select(t => new searchedPlayerVal
                    {
                        searchedPlayerId = t.player_id,
                        searchedPlayerName = t.chineseName,
                        searchedPlayerPhoto = t.photo
                    })
                    .ToListAsync();

                //gameType=0,无需筛选
                if (gameType == 0)
                {
                    Console.WriteLine("player count = " + allPlayer.Count());
                    return allPlayer;
                }


                for (int i = 0; i < allPlayer.Count(); i++)
                {
                    var relatedGame =await sqlORM.Queryable<Players>()
                        .LeftJoin<PlayerJoinGame>((p, pjg) => p.player_id == pjg.player_id)
                        .LeftJoin<Game>((p, pjg, g) => pjg.game_id == g.game_id)
                        .Where((p, pjg, g) => p.player_id == allPlayer[i].searchedPlayerId)
                        .Select((p, pjg, g) => g.type)
                        .Distinct()
                        .ToListAsync();
                    if (relatedGame.Contains(gameNames[gameType]))
                    {
                        ans.Add(allPlayer[i]);
                    }
                }

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
            public string? playerName { get; set; }
        }
        public class relatedPlayer
        {
            public string? playerName { get; set; }
            public string? playerPhoto { get; set; }
            public string? type { get; set; }
        }

        public class eventData
        {
            public string? seasonName { get; set; }
            public int? appearance { get; set; }
            public int? goal { get; set; }
            public int? shoot { get; set; }
            public int? pass { get; set; }
            public int? assist { get; set; }
            public int? red { get; set; }
            public int? yellow { get; set; }

        }
        public class getPlayerDetailVal
        {
            public int? team_id { get; set; }
            public int? player_id { get; set; }
            public string? club { get; set; }
            public string? enName { get; set; }
            public string? photo { get; set; }
            public string? position { get; set; }
            public string? number { get; set; }
            public string? nationality { get; set; }
            public string? age { get; set; }
            public string? height { get; set; }
            public string? dominantFoot { get; set; }

            public List<relatedPlayer>? relatedPlayer { get; set; }
            public List<eventData>? eventData { get; set; }

        }
        [HttpPost]
        public async Task<getPlayerDetailVal> getPlayerDetail([FromBody] getPlayerDetailPara json)
        {
            Console.WriteLine("--------------------------getPlayerDetail--------------------------");
            ORACLEconn ORACLEConnectTry = new ORACLEconn();
            ORACLEConnectTry.getConn();
            try
            {
                SqlSugarScope sqlORM = ORACLEConnectTry.sqlORM;
                getPlayerDetailVal ans = new getPlayerDetailVal();
                string? playerName = json.playerName;
                Console.WriteLine("player name is " + playerName);

                var temp = await sqlORM.Queryable<Players>()
                    .LeftJoin<TeamOwnPlayer>((p, top) => p.player_id == top.player_id)
                    .LeftJoin<Team>((p, top, t) => t.team_id == top.team_id)
                    .Where((p, top, t) => p.chineseName == playerName)
                    .Select((p, top, t) => new getPlayerDetailVal
                    {
                        team_id = top.team_id,
                        player_id = top.player_id,
                        photo = p.photo,
                        club = t.chinesename,
                        enName = t.enname,
                        position = p.type,
                        number = p.playerNumber,
                        nationality = p.country,
                        age = p.age,
                        height = p.height,
                        dominantFoot = p.foot
                    })
                    .ToListAsync();


                //更新列表属性
                if (temp.Count() != 0)
                {
                    ans = temp[0];
                    ans.relatedPlayer = new List<relatedPlayer>();

                    //首先更新相关球员
                    var players =await sqlORM.Queryable<Players>()
                        .LeftJoin<TeamOwnPlayer>((p, top) => p.player_id == top.player_id)
                        .LeftJoin<Players>((p, top, pp) => top.player_id == pp.player_id)
                        .Where((p, top, pp) => top.team_id == ans.team_id && pp.chineseName != playerName)
                        .Select((p, top, pp) => new relatedPlayer
                        {
                            playerName = pp.chineseName,
                            playerPhoto = pp.photo,
                            type = pp.type,
                        })
                        .Take(12)
                        .ToListAsync();
                    ans.relatedPlayer = players;
                    Console.WriteLine("related players count = " + ans.relatedPlayer.Count().ToString());

                    //更新赛季信息
                    ans.eventData = new List<eventData>();


                    for (int i = 2020; i < 2024; i++)
                    {
                        string startTimeStr = i.ToString() + "-07-01";
                        DateTime startTime = DateTime.Parse(startTimeStr);
                        string endTimeStr = (i + 1).ToString() + "-06-30";
                        DateTime endTime = DateTime.Parse(endTimeStr);

                        string evenName = (i - 2000).ToString() + "/" + (i - 1999).ToString();
                        Console.WriteLine("evenName = " + evenName.ToString());

                        var tempEvent = await sqlORM.Queryable<Players>()
                            .LeftJoin<PlayerJoinGame>((p, pjg) => p.player_id == pjg.player_id)
                            .LeftJoin<Game>((p, pjg, g) => pjg.game_id == g.game_id)
                            .Where((p, pjg, g) => p.chineseName == playerName && g.startTime.Value < endTime && g.startTime.Value > startTime)
                            .Select((p, pjg, g) => new eventData
                            {
                                seasonName = evenName,
                                appearance = SqlFunc.AggregateCount(pjg.goal),
                                goal = SqlFunc.AggregateSum(pjg.goal),
                                shoot = SqlFunc.AggregateSum(pjg.shoot),
                                pass = SqlFunc.AggregateSum(pjg.pass),
                                assist = SqlFunc.AggregateSum(pjg.assist),
                                red = SqlFunc.AggregateSum(pjg.red),
                                yellow = SqlFunc.AggregateSum(pjg.yellow)
                            })
                            .ToListAsync();

                        if (tempEvent[0].appearance == null) tempEvent[0].appearance = 0;
                        if (tempEvent[0].goal == null) tempEvent[0].goal = 0;
                        if (tempEvent[0].shoot == null) tempEvent[0].shoot = 0;
                        if (tempEvent[0].pass == null) tempEvent[0].pass = 0;
                        if (tempEvent[0].assist == null) tempEvent[0].assist = 0;
                        if (tempEvent[0].red == null) tempEvent[0].red = 0;
                        if (tempEvent[0].yellow == null) tempEvent[0].yellow = 0;


                        ans.eventData.Add(tempEvent[0]);



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




        //lq特供
        public class showRecentGamesVal
        {
            public string? status { get; set; }
            public string? homeTeamName { get; set; }
            public string? guestTeamName { get; set; }
            public int? guestScore { get; set; }
            public int? homeScore { get; set; }
            public string? gameUid { get; set; }
            public int? homeTeam { get; set; }
            public int? guestTeam { get; set; }
            public string? gameName { get; set; }
            public string? gameTime { get; set; }
        }
        [HttpGet]
        public async Task<List<showRecentGamesVal>> showRecentGames()
        {
            Console.WriteLine("--------------------------showRecentGames--------------------------");
            ORACLEconn ORACLEConnectTry = new ORACLEconn();
            ORACLEConnectTry.getConn();
            try
            {
                SqlSugarScope sqlORM = ORACLEConnectTry.sqlORM;
                List<showRecentGamesVal> ans = new List<showRecentGamesVal>();
                List<string> gameNames = new List<string> { "英超", "西甲", "意甲", "德甲", "法甲", "中超" };

                for (int i = 0; i < 6; i++)
                {
                    DBconn orm = new DBconn();
                    List<showRecentGamesVal> temp=await orm.Db.Queryable<Game>()
                        .LeftJoin<Team>((g, home) => g.homeTeam == home.team_id)
                        .LeftJoin<Team>((g, home, guest) => g.guestTeam == guest.team_id)
                        .Where((g, home, guest) => ((g.type == gameNames[i]) && g.status == "Played"))
                        .OrderBy((g, home, guest) => g.startTime, OrderByType.Desc)
                        .Take(6)
                        .Select((g, home, guest) => new showRecentGamesVal
                        {
                            gameName = g.type,
                            gameTime = g.startTime.Value.ToString("yyyy-mm-dd"),
                            gameUid = g.game_id.ToString(),
                            homeTeamName = home.chinesename,
                            homeTeam = home.team_id,
                            guestTeam = guest.team_id,
                            guestTeamName = guest.chinesename,
                            status = g.status
                        })
                        .ToListAsync();
                    ans.AddRange(temp);
                }

                //ans = await sqlORM.Queryable<Game>()
                //    .LeftJoin<Team>((g, home) => g.homeTeam == home.team_id)
                //    .LeftJoin<Team>((g, home, guest) => g.guestTeam == guest.team_id)
                //    .Where((g, home, guest) => ((gameType != 0 && g.type == gameNames[gameType]) || gameType == 0) && g.status == "Played")
                //    .OrderBy((g, home, guest) => g.startTime, OrderByType.Desc)
                //    .Take(6)
                //    .Select((g, home, guest) => new showRecentGamesVal
                //    {
                //        gameName = g.type,
                //        gameTime = g.startTime.Value.ToString("yyyy-mm-dd"),
                //        gameUid = g.game_id.ToString(),
                //        homeTeamName = home.chinesename,
                //        homeTeam = home.team_id,
                //        guestTeam = guest.team_id,
                //        guestTeamName = guest.chinesename,
                //        status = g.status
                //    })
                //    .ToListAsync();

                Console.WriteLine("ansCnt = " + ans.Count().ToString());



                // 接下来对比赛进行比分筛选
                for (int i = 0; i < ans.Count(); i++)
                {
                    int? game_id = int.Parse(ans[i].gameUid);
                    int? homeTeam = ans[i].homeTeam;
                    int? guestTeam = ans[i].guestTeam;
                    //Console.WriteLine("homeTeam is " + ans[i].homeTeam);
                    //Console.WriteLine("guestTeam is " + ans[i].guestTeam);

                    ans[i].homeScore = await sqlORM.Queryable<TeamOwnPlayer>()
                        .LeftJoin<PlayerJoinGame>((top, pjg) => top.team_id == homeTeam && top.player_id == pjg.player_id && pjg.game_id == game_id)
                        .SumAsync((top, pjg) => pjg.goal);

                    if (ans[i].homeScore == null)
                    {
                        ans[i].homeScore = 0;
                    }


                    ans[i].guestScore = await sqlORM.Queryable<TeamOwnPlayer>()
                        .LeftJoin<PlayerJoinGame>((top, pjg) => top.team_id == guestTeam && top.player_id == pjg.player_id && pjg.game_id == game_id)
                        .SumAsync((top, pjg) => pjg.goal);

                    if (ans[i].guestScore == null)
                    {
                        ans[i].guestScore = 0;
                    }


                }

                Console.WriteLine("recent games count = "+ans.Count);

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


