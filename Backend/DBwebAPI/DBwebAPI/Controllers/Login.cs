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
        [HttpGet] 
        public async Task<string> LoginConcrollerAsync([FromBody]JObject json)
        {
            Console.WriteLine("GET Login!");
            ORACLEconn  ORACLEConnectTry=new ORACLEconn();
            Console.WriteLine("Conn success!");

            //提取参数
            var jsonStr = JsonConvert.SerializeObject(json);
            var jsonParams = JsonConvert.DeserializeObject<dynamic>(jsonStr);
            string username = jsonParams["username"];
            string passwordHash = jsonParams["passwordHash"];
            Console.WriteLine("json get success!");
            //string securityQ = jsonParams["securityQ"];
            //string securityAnsHash = jsonParams["securityAnsHash"];

            if (ORACLEConnectTry.getConn() == true) {
                try
                {
                    SqlSugarClient sqlORM = ORACLEConnectTry.sqlORM;
                    //进行用户查询
                    List<Usr> tempUsr = new List<Usr>();
                    tempUsr = await sqlORM.Queryable<Usr>().Where(it => it.userName == username 
                    && it.userPassword == passwordHash)
                        .ToListAsync();
                    //判断用户是否存在
                    if (tempUsr.Count() == 0)
                    {
                        return "error code=1";//用户账户或密码错误
                    }
                    else
                    {
                        createToken tempToken= new createToken();
                        string token = tempToken.createTokenFun(username, passwordHash);
                        return token;
                    }

                }
                catch (Exception)
                {
                    return "error code=114514";//位置错误
                }
            }
            else
            {
                return "wyh是大傻逼";//连接数据库失败
            }

        }
    }
}
