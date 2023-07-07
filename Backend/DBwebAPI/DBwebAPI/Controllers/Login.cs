using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using DBwebAPI.Models;
using DBwebAPI.Controllers;

namespace DBwebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Login : ControllerBase
    {
        [HttpGet] 
        public string LoginConcroller(string account, string password)
        {
            ORACLEconn  ORACLEConnectTry=new ORACLEconn();
            if (ORACLEConnectTry.getConn() == true) {
                try
                {
                    SqlSugarClient sqlORM = ORACLEConnectTry.sqlORM;
                    //进行用户查询
                    List<Usr> tempUsr = new List<Usr>();
                    tempUsr = sqlORM.Queryable<Usr>().Where(it => it.account == account && it.userPassword == password).ToList();
                    //int shit = sqlORM.Queryable<Usr>().Max(it => it.user_id);
                    //return $"{shit}";
                    //判断用户是否存在
                    if (tempUsr.Count() == 0) return "id或密码错误";
                    else
                    {
                        createToken tempToken= new createToken();
                        string token = tempToken.createTokenFun(account, password);
                        return token;
                    }

                }
                catch (Exception)
                {
                    return "error";
                }
            }
            else
            {
                return "连接数据库失败";
            }

        }
    }
}
