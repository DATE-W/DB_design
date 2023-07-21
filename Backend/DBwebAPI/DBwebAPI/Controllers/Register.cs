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
            Console.WriteLine("GET Register!");
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
                    Console.WriteLine("user_id= "+ user_id);
                    Console.WriteLine("userName= "+ usr.userName);
                    Console.WriteLine("userPassword= " + usr.userPassword);
                    Console.WriteLine("userAccount= "+ usr.userAccount);
                    Console.WriteLine("userSecQue= "+ usr.userSecQue);
                    Console.WriteLine("userSecAns= " + usr.userSecAns);
                    Console.WriteLine("createDateTime= " + usr.createDateTime);
                    int count = await sqlOrm.Insertable(usr).ExecuteCommandAsync();
                    if (count == 1)
                    {
                        Console.WriteLine("注册成功");
                        return "Success";
                    }
                    else
                    {
                        Console.WriteLine("注册失败");
                        return "Fail";//用户注册的账户已存在
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("未知错误");
                    System.Console.WriteLine(ex.Message);
                    return "UNKNOWN";//未知错误
                }
            }
            else
            {
                return "Fail_Access";//连接数据库失败
            }
        }
    }
}
