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
            //    // ������ͷ�л�ȡ���ݵ�JWT����
            //    string authorizationHeader = Request.Headers["Authorization"].FirstOrDefault();
            //    //��֤ Authorization ����ͷ�Ƿ���� JWT ����
            //    if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer"))
            //    {
            //        return BadRequest(new { ok = "δ�ṩ��Ч��JWT" });
            //    }
            //    //
            //    string jwtToken = authorizationHeader.Substring("Bearer ".Length).Trim();
            //    // ��֤������JWT����
            //    var handler = new JwtSecurityTokenHandler();
            //    var tokenS = handler.ReadJwtToken(jwtToken);
            //    // ��ȡJWT�����е�claims��Ϣ
            //    string account = tokenS.Claims.FirstOrDefault(claim => claim.Type == "account")?.Value;
            //    string authority = tokenS.Claims.FirstOrDefault(claim => claim.Type == "authority").Value;
            //    // ���ش����ƻ�ȡ���˻������롣
            //    Console.WriteLine("account= " + account + "   Password=" + authority);
            //    return Ok(new { ok = "yes" });
            //}
            //catch (Exception ex)
            //{
            //    // �����������ȡ�����г����κ��쳣������һ��������Ӧ��
            //    Console.WriteLine("��ȡ����ʱ��������" + ex.Message);
            //    return BadRequest(new { ok = "��ȡ����ʱ��������" + ex.Message });
            //}
            //string secretKey = "f47b558d-7654-458c-99f2-13b190ef0199";
            //var tokenHandler = new JwtSecurityTokenHandler();
            //var key = Encoding.ASCII.GetBytes(secretKey);
            //var tokenS = tokenHandler.ReadJwtToken(token);
            //string alg = tokenS.Header.Alg;//��ȡ���㷨
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
