using Microsoft.Extensions.Hosting;
using Oracle.ManagedDataAccess.Types;
using SqlSugar;
using System.Data;
using System.Security.Policy;

namespace DBwebAPI.Models
{
    public class Usr
    {
        [SugarColumn(IsPrimaryKey = true)]
        public int user_id { get; set; }
        public string userName { get; set; }
        public string userPassword { get; set; }
        public string userAccount { get; set; }
        public int isBanned { get; set; } = 0;
        public int? userPoint { get; set; } = 0;
        public int? themeType { get; set; }
        //public byte[]? avatar { get; set; }
        public DateTime? signDate { get; set; }
        public DateTime createDateTime { get; set; }
        public string userSecQue { get; set; }
        public string userSecAns { get; set; }
    }
    public class UserFavouriteTeam
    {
        [SugarColumn(IsPrimaryKey = true)]
        public int user_id { get; set; }
        public int team_id { get; set; }
        public DateTime modifyDateTime { get; set; }
    }
    public class Follow
    {
        [SugarColumn(IsPrimaryKey = true)]
        public int follower_id { get; set; }
        public int follow_id { get; set; }
        public DateTime createDateTime { get; set; }
    }
    public class FollowPost
    {
        [SugarColumn(IsPrimaryKey = true)]
        public int user_id { get; set; }
        public int post_id { get; set; }
        public DateTime createDateTime { get; set; }
    }
    public class ApprovePost
    {
        [SugarColumn(IsPrimaryKey = true)]
        public int user_id { get; set; }
        public int post_id { get; set; }
        public DateTime createDateTime { get; set; }
    }
    public class Checkins
    {
        [SugarColumn(IsPrimaryKey = true)]
        public int user_id { get; set;}
        public DateTime sign_in_date { get; set;}
    }
}
