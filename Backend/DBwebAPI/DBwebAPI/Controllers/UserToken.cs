using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using DBwebAPI.Models;
using DBwebAPI.Controllers;
using Newtonsoft.Json.Linq;
using System.Security.Principal;
using Newtonsoft.Json;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace DBwebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserTokenController : ControllerBase
    {
        
        [HttpPost]
        public async Task<string>  createToken(string account,string authority)
        {
            string token = new CreateToken().createTokenFun(account, authority);
            return token;
        }
        [HttpPost]
        public IActionResult UserToken(string token)
        {
            //try
            //{
            //    Console.WriteLine("Get TokenTest");
            //    // 从请求头中获取传递的JWT令牌
            //    string authorizationHeader = Request.Headers["Authorization"].FirstOrDefault();
            //    //验证 Authorization 请求头是否包含 JWT 令牌
            //    if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer"))
            //    {
            //        return BadRequest(new { ok = "未提供有效的JWT" });
            //    }
            //    //
            //    string jwtToken = authorizationHeader.Substring("Bearer ".Length).Trim();
            //    // 验证并解析JWT令牌
            //    var handler = new JwtSecurityTokenHandler();
            //    var tokenS = handler.ReadJwtToken(jwtToken);
            //    // 获取JWT令牌中的claims信息
            //    string account = tokenS.Claims.FirstOrDefault(claim => claim.Type == "account")?.Value;
            //    string authority = tokenS.Claims.FirstOrDefault(claim => claim.Type == "authority").Value;
            //    // 返回从令牌获取的账户和密码。
            //    Console.WriteLine("account= " + account + "   Password=" + authority);
            //    return Ok(new { ok = "yes" });
            //}
            //catch (Exception ex)
            //{
            //    // 如果在令牌提取过程中出现任何异常，返回一个错误响应。
            //    Console.WriteLine("提取令牌时发生错误：" + ex.Message);
            //    return BadRequest(new { ok = "提取令牌时发生错误：" + ex.Message });
            //}
            //string secretKey = "f47b558d-7654-458c-99f2-13b190ef0199";
            //var tokenHandler = new JwtSecurityTokenHandler();
            //var key = Encoding.ASCII.GetBytes(secretKey);
            //var tokenS = tokenHandler.ReadJwtToken(token);
            //string alg = tokenS.Header.Alg;//读取出算法
            //Console.WriteLine(alg);
            var valid = new ValidateToken();
            try
            {
                bool pass = valid.ValidateJwtToken(token);
                if(pass)
                {
                    return Ok(new { ok = "yes" });
                }
                else
                {
                    return Ok(new { ok = "no" });
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
