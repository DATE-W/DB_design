using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using DBwebAPI.Models;
using DBwebAPI.Controllers;
using Newtonsoft.Json.Linq;
using System.Security.Principal;
using Newtonsoft.Json;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace DBwebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserTokenController : ControllerBase
    {
        [HttpPost]
        public IActionResult UserToken()
        {
            try
            {
                Console.WriteLine("Get TokenTest");
                // ������ͷ�л�ȡ���ݵ�JWT����
                string authorizationHeader = Request.Headers["Authorization"].FirstOrDefault();
                //��֤ Authorization ����ͷ�Ƿ���� JWT ����
                if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer"))
                {
                    return BadRequest(new { ok = "δ�ṩ��Ч��JWT" });
                }
                //
                string jwtToken = authorizationHeader.Substring("Bearer ".Length).Trim();
                // ��֤������JWT����
                var handler = new JwtSecurityTokenHandler();
                var tokenS = handler.ReadJwtToken(jwtToken);
                // ��ȡJWT�����е�claims��Ϣ
                string account = tokenS.Claims.FirstOrDefault(claim => claim.Type == "account")?.Value;
                string password = tokenS.Claims.FirstOrDefault(claim => claim.Type == "password").Value;
                // ���ش����ƻ�ȡ���˻������롣
                Console.WriteLine("account= " + account + "   Password=" + password);
                return Ok(new { ok = "yes" });
            }
            catch (Exception ex)
            {
                // �����������ȡ�����г����κ��쳣������һ��������Ӧ��
                Console.WriteLine("��ȡ����ʱ��������" + ex.Message);
                return BadRequest(new { ok = "��ȡ����ʱ��������" + ex.Message });
            }
        }
    }
}
