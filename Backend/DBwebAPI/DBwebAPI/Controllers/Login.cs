using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using DBwebAPI.Models;
using DBwebAPI.Controllers;

namespace DBwebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class Login : ControllerBase
    {
        [HttpGet] 
        public async Task<string> LoginConcrollerAsync(string account, string password)
        {
            ORACLEconn  ORACLEConnectTry=new ORACLEconn();
            if (ORACLEConnectTry.getConn() == true) {
                try
                {
                    SqlSugarClient sqlORM = ORACLEConnectTry.sqlORM;
                    //进行用户查询
                    List<Usr> tempUsr = new List<Usr>();
                    tempUsr = await sqlORM.Queryable<Usr>().Where(it => it.userAccount == account && it.userPassword == password).ToListAsync();
                    //判断用户是否存在
                    if (tempUsr.Count() == 0)
                    {
                        return "error code=1";//用户账户或密码错误
                    }
                    else
                    {
                        createToken tempToken= new createToken();
                        string token = tempToken.createTokenFun(account, password);
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
