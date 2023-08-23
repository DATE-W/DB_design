using Microsoft.Extensions.Hosting;
using Oracle.ManagedDataAccess.Types;
using SqlSugar;
using System.Data;
using System.Security.Policy;


namespace DBwebAPI.Models
{

    //public class Team
    //{
    //    [SugarColumn(IsPrimaryKey = true)]
    //    public int team_id { get; set; }
    //    public string? teamName { get; set; }
    //    public string? city { get; set; }
    //    public int? coach_id { get; set; }
    //    public string? manager { get; set; }
    //    public string? boss { get; set; }
    //    public string? teamLogo { get; set; }
    //}

    public class Team
    {
        [SugarColumn(IsPrimaryKey = true)]
        public int team_id { get; set; }
        public string? chinesename { get; set; }
        public string? logo { get; set; }
        public string? enname { get; set; }
        public string? city { get; set; }
        public string? coach { get; set; }
        public int? foundedyear { get; set; }
    }

    //public class Coach
    //{
    //    [SugarColumn(IsPrimaryKey = true)]
    //    public int coach_id { get; set; }
    //    public string? coachName { get; set; }
    //    public int? coachYear { get; set; }
    //}

    //public class Game
    //{
    //    [SugarColumn(IsPrimaryKey = true)]
    //    public int game_id { get; set; }
    //    public int? homeTeam { get; set; }
    //    public int? guestTeam { get; set; }
    //    public string? gameName { get; set; }
    //    public int? gameType { get; set; }
    //    public int? status { get; set; }
    //    [SugarColumn(SqlParameterDbType = System.Data.DbType.Date)]
    //    public DateTime? dateTime { get; set; }
    //    public string? city { get; set; }
    //    public string? mainReferee { get; set; }
    //    public int? homeScore { get; set; }
    //    public int? guestScore { get; set; }
    //}

    public class Players
    {
        [SugarColumn(IsPrimaryKey = true)]
        public int? player_id { get; set; }

        public string? chineseName { get; set; }
        public string? enName { get; set; }
        public string? photo { get; set; }
        public string? country { get; set; }
        public string? height { get; set; }
        public string? type { get; set; }
        public string? age { get; set; }
        public string? playerNumber { get; set; }
        public string? foot { get; set; }
    }

    public class TeamOwnPlayer
    {
        [SugarColumn(IsPrimaryKey = true)]
        public int? team_id { get; set; }

        [SugarColumn(IsPrimaryKey = true)]
        public int? player_id { get; set; }
    }

    public class Game
    {
        [SugarColumn(IsPrimaryKey = true)]
        public int? game_id { get; set; }

        public int? homeTeam { get; set; }
        public int? guestTeam { get; set; }
        public string? type { get; set; }
        public DateTime? startTime { get; set; }
        public string? status { get; set; }
        public string? liveUrl { get; set; }
    }


    public class PlayerJoinGame
    {
        [SugarColumn(IsPrimaryKey = true)]
        public int? game_id { get; set; }

        [SugarColumn(IsPrimaryKey = true)]
        public int? player_id { get; set; }

        public int? minutes { get; set; }
        public int? goal { get; set; }
        public int? assist { get; set; }
        public int? red { get; set; }
        public int? yellow { get; set; }
        public int? shoot { get; set; }
        public int? target { get; set; }
        public int? surpass { get; set; }
        public int? surpassSuccess { get; set; }
        public int? fouled { get; set; }
        public int? pass { get; set; }
        public int? passRate { get; set; }
        public int? tackle { get; set; }
        public int? intercept { get; set; }
        public int? foul { get; set; }
        public int? lost { get; set; }
        public int? surpassed { get; set; }
    }


}
