using Microsoft.Extensions.Hosting;
using Oracle.ManagedDataAccess.Types;
using SqlSugar;
using System.Data;
using System.Security.Policy;


namespace DBwebAPI.Models
{

    public class Team
    {
        [SugarColumn(IsPrimaryKey = true)]
        public int team_id { get; set; }
        public string? teamName { get; set; }
        public string? city { get; set; }
        public int? coach_id { get; set; }
        public string? manager { get; set; }
        public string? boss { get; set; }
    }

    public class Coach
    {
        [SugarColumn(IsPrimaryKey = true)]
        public int coach_id { get; set; }
        public string? coachName { get; set; }
        public int? coachYear { get; set; }
    }

    public class Game
    {
        [SugarColumn(IsPrimaryKey = true)]
        public int game_id { get; set; }
        public int? homeTeam { get; set; }
        public int? guestTeam { get; set; }
        public string? gameName { get; set; }
        public int? gameType { get; set; }
        public int? status { get; set; }
        [SugarColumn(SqlParameterDbType = System.Data.DbType.Date)]
        public DateTime? dateTime { get; set; }
        public string? city { get; set; }
        public string? mainReferee { get; set; }
        public int? homeScore { get; set; }
        public int? guestScore { get; set; }
    }


}
