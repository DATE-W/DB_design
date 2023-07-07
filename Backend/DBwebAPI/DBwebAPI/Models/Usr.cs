using SqlSugar;
using System.Reflection.Metadata;
using System.Security.Policy;

namespace DBwebAPI.Models
{ 
    public class Usr
    {
        [SugarColumn(IsPrimaryKey = true)]
        public int user_id { get; set; }
        public string userName { get; set; }
        public string userPassword { get; set; }
        public int isBanned { get; set; }
        public int? userPoint { get; set; }
        public int? themeType { get; set; }
        public string? avatar { get; set; }
        public string? signDate { get; set; }
        public string createDateTime { get; set; }
        public string account { get; set; }
    }
}
