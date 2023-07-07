using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using DBwebAPI.Models;
using DBwebAPI.Controllers;

namespace DBwebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Register : ControllerBase
    {
        [HttpPost]
        public async Task<bool> normalRegisterAsync(string account, string password, string userName)
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
                    usr.userName = userName;
                    usr.userPassword = password;
                    usr.account = account;
                    usr.createDateTime = DateTime.Now.ToString();
                    int count = await sqlOrm.Insertable(usr).ExecuteCommandAsync();
                    if (count == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
