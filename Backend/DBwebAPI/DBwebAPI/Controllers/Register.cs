using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using DBwebAPI.Models;
using DBwebAPI.Controllers;

namespace DBwebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class Register : ControllerBase  
    {
        public class RegisterRequest
        {
            public string Account { get; set; }
            public string Password { get; set; }
            public string UserName { get; set; }
            public string UserSecQue { get; set; }
            public string UserSecAns { get; set; }
        }
        [HttpPost]
        public async Task<string> normalRegisterAsync([FromBody] RegisterRequest registerRequest)
        {
            ORACLEconn ORACLEConnectTry = new ORACLEconn();
            if (ORACLEConnectTry.getConn() == true)
            {
                try
                {
                    SqlSugarClient sqlOrm = ORACLEConnectTry.sqlORM;
                    //进行用户查询

                    int user_id = sqlOrm.Queryable<Usr>().Max(it => it.user_id) + 1;
                    Usr usr = new Usr();
                    usr.user_id = user_id;
                    usr.userName = registerRequest.UserName;
                    usr.userPassword = registerRequest.Password;
                    usr.userAccount = registerRequest.Account;
                    usr.userSecQue = registerRequest.UserSecQue;
                    usr.userSecAns = registerRequest.UserSecAns;
                    usr.createDateTime = DateTime.Now;
                    int count = await sqlOrm.Insertable(usr).ExecuteCommandAsync();
                    if (count == 1)
                    {
                        return "Success";
                    }
                    else
                    {
                        return "error code=2";//用户注册的账户已存在
                    }
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex.Message);
                    return "error code=114514";//未知错误
                }
            }
            else
            {
                return "error code=0";//连接数据库失败
            }
        }
    }
}
