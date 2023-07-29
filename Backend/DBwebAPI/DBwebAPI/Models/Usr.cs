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
        public int? userPoint { get; set; }
        public int? themeType { get; set; }
        //public byte[]? avatar { get; set; }
        public DateTime? signDate { get; set; }
        public DateTime createDateTime { get; set; }
        public string userSecQue { get; set; }
        public string userSecAns { get; set; }
    }
    public class Posts
    {
        public int post_id { get; set; }
        public DateTime publishDateTime { get; set; }
        public string contains { get; set; }
        public int isBanned { get; set; }
        //public byte[] pictureBox;
        public int? approvalNum { get; set; }
        public int? disapprovalNum { get; set; }
        public int? favouriteNum { get; set; }
    }
    public class PublishPost
    {
        public int post_id { get; set; }
        public int user_id { get; set; }
    }
    public class Tag
    {
        public int post_id { get; set; }
        public string tagName { get; set; }
    }
}
