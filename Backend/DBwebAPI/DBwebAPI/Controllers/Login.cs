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
        [HttpPost]
        public async Task<IActionResult> LoginPassword([FromBody] LoginRequest json)
        {
            Console.WriteLine("GET Login!");
            ORACLEconn ORACLEConnectTry = new ORACLEconn();
            //提取参数

            string account = json.Account;
            string passwordHash = json.Password;
            string authority = "Normal";
            Console.WriteLine("account=" + account);
            Console.WriteLine("passwordHash= " + passwordHash);
            //string securityQ = jsonParams["securityQ"];
            //string securityAnsHash = jsonParams["securityAnsHash"];

            if (ORACLEConnectTry.getConn() == true)
            {
                try
                {
                    SqlSugarClient sqlORM = ORACLEConnectTry.sqlORM;
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
<<<<<<< Updated upstream
                        // string token = new createToken().createTokenFun(account, passwordHash, authority);
                        string token;
                        try
                        {
                            token = new CreateToken().createTokenFun(account, authority);
                            Console.WriteLine(token);
                            // 返回登录成功及JWT令牌
                            return Ok(new CustomResponse { ok = "yes", value = token });
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            return Ok(new CustomResponse { ok = "no", value = "UNKNOWN" });
                        }
=======
                        string token = new createToken().createTokenFun(account, passwordHash);
                        //更新用户上一次登录时间
                        await sqlORM.Updateable<Usr>().Where(it => it.user_id == tempUsr[0].user_id).ReSetValue(it => { it.signDate = DateTime.Now; }).ExecuteCommandAsync();
                        Console.WriteLine(token);
                        // 返回登录成功及JWT令牌
                        return Ok(new CustomResponse { ok = "yes", value = token });
>>>>>>> Stashed changes
                    }
                }
                catch (Exception)
                {
                    return Ok(new CustomResponse { ok = "no", value = "UNKNOWN" }); // Internal server error
                }
            }
            else { return Ok(new CustomResponse { ok = "no", value = "UNKNOWN" }); };
        }
    }
}
