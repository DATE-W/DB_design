using Microsoft.Extensions.Hosting;
using Oracle.ManagedDataAccess.Types;
using SqlSugar;
using System.Data;
using System.Security.Policy;

namespace DBwebAPI.Models
{
    public class Posts
    {
        [SugarColumn(IsPrimaryKey = true)]
        public int post_id { get; set; }
        public string title { get; set; }
        public DateTime publishDateTime { get; set; }
        public string contains { get; set; }
        public int isBanned { get; set; }
        //public byte[] pictureBox;
        public int approvalNum { get; set; }
        public int? disapprovalNum { get; set; }
        public int favouriteNum { get; set; }
    }
    public class PublishPost
    {
        [SugarColumn(IsPrimaryKey = true)]
        public int post_id { get; set; }
        public int user_id { get; set; }
    }
    public class PostPic
    {
        [SugarColumn(IsPrimaryKey = true)]
        public int post_id { get; set; }
        public string pic { get; set; }
    }
    public class Tag
    {
        [SugarColumn(IsPrimaryKey = true)]
        public int post_id { get; set; }
        public string tagName { get; set; }
    }
    public class Comments
    {
        [SugarColumn(IsPrimaryKey = true)]
        public int comment_id { get; set; }
        public DateTime publishDateTime { get; set; }
        public string contains { get; set; }
        public int user_id { get; set; }
        public int post_id { get; set; }
    }
    //public class Reports
    //{
    //    [SugarColumn(IsPrimaryKey = true)]
    //    public int? admin_id { get; set; }
    //    public DateTime report_time { get; set; }
    //    public int? reporter_id { get; set; }
    //    public string? descriptions { get; set; }
    //    public string? reply { get; set; }

    //    public int status { get; set; }
    //    public int? post_id { get; set; }

    //}
}
