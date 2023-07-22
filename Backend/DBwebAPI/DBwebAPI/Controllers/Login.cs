using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using DBwebAPI.Models;
using DBwebAPI.Controllers;
using Newtonsoft.Json.Linq;
using System.Security.Principal;
using Newtonsoft.Json;

namespace DBwebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class Login : ControllerBase
    {
        public class LoginRequest
        {
            public string Account { get; set; }
            public string Password { get; set; }
        }
        [HttpPost] 
        public async Task<string> LoginPassword([FromBody] LoginRequest json)
        {
            Console.WriteLine("GET Login!");
            ORACLEconn  ORACLEConnectTry=new ORACLEconn();

            //提取参数

            string account = json.Account;
            string passwordHash = json.Password;
            Console.WriteLine("account="+ account);
            Console.WriteLine("passwordHash= "+passwordHash);
            //string securityQ = jsonParams["securityQ"];
            //string securityAnsHash = jsonParams["securityAnsHash"];

            if (ORACLEConnectTry.getConn() == true) {
                try
                {
                    SqlSugarClient sqlORM = ORACLEConnectTry.sqlORM;
                    //进行用户查询
                    List<Usr> tempUsr = new List<Usr>();
                    tempUsr = await sqlORM.Queryable<Usr>().Where(it => it.userName == account
                    && it.userPassword == passwordHash)
                        .ToListAsync();
                    //判断用户是否存在
                    if (tempUsr.Count() == 0)
                    {
                        Console.WriteLine("登录失败");
                        return "Fail";//用户账户或密码错误
                    }
                    else
                    {
                        Console.WriteLine("登录成功");
                        createToken tempToken= new createToken();
                        string token = tempToken.createTokenFun(account, passwordHash);
                        return token;
                    }

                }
                catch (Exception)
                {
                    return "UNKNOWN";//位置错误
                }
            }
            else
            {
                return "Fail_Access";//连接数据库失败
            }

        }
    }
}
