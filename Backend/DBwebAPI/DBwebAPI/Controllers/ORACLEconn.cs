using DBwebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;

namespace DBwebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ORACLEconn : Controller
    {
        public SqlSugarClient sqlORM = null;
        [HttpGet]
        public bool getConn()
        {
            Console.WriteLine("get access!");

            //创建数据库连接
            DBconn dbconn = new DBconn();
            //打开ORM
            dbconn.Conn();
            
            sqlORM = dbconn.sqlORM;

            if (sqlORM != null) return true;
            else return false;

        }
    }
}
