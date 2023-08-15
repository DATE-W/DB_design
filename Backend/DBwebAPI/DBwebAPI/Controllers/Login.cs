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
    public class LoginController : ControllerBase
    {
        public class LoginRequest
        {
            public string Account { get; set; }
            public string Password { get; set; }
        }
        public class CustomResponse
        {
            public string ok { get; set; }
            public object value { get; set; }
        }
        [HttpPost]
        public async Task<IActionResult> LoginPassword([FromBody] LoginRequest json)
        {
            Console.WriteLine("GET Login!");
            ORACLEconn ORACLEConnectTry = new ORACLEconn();
            SqlSugarClient sqlORM = ORACLEConnectTry.sqlORM;
            //提取参数
            string account = json.Account;
            string passwordHash = json.Password;
            Console.WriteLine("account=" + account);
            Console.WriteLine("passwordHash= " + passwordHash);
            //string securityQ = jsonParams["securityQ"];
            //string securityAnsHash = jsonParams["securityAnsHash"];

            if (ORACLEConnectTry.getConn() == true)
            {
                try
                {
                    //进行用户查询
                    List<Usr> tempUsr = new List<Usr>();
                    tempUsr = await sqlORM.Queryable<Usr>().Where(it => it.userAccount == account
                    && it.userPassword == passwordHash)
                        .ToListAsync();
                    //判断用户是否存在
                    if (tempUsr.Count() == 0)
                    {
                        Console.WriteLine("登录失败");
                        return Ok(new CustomResponse { ok = "no", value = "Fail" });//用户账户或密码错误
                    }
                    else
                    {
                        Console.WriteLine("登录成功");
                        // 生成JWT令牌
                        string token = new createToken().createTokenFun(account, passwordHash);
                        Console.WriteLine(token);
                        // 返回登录成功及JWT令牌
                        return Ok(new CustomResponse { ok = "yes", value = token });
                    }
                }
                catch (Exception ex)
                {
                    return Ok(new CustomResponse { ok = "no", value = ex }); // Internal server error
                }
            }
            else { return Ok(new CustomResponse { ok = "no", value = "数据库连接失败" }); };
        }
    }
}
