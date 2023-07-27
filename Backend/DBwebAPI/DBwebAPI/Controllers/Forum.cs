using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using DBwebAPI.Models;
using DBwebAPI.Controllers;
using Newtonsoft.Json.Linq;
using System.Security.Principal;
using Newtonsoft.Json;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using static DBwebAPI.Controllers.LoginController;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;

namespace DBwebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ForumController : ControllerBase
    {
        public class NewPostJson
        {
            public string title { get; set; }
            public string contains { get; set; }
            //public string[] tags { get; set; }
        }
        [HttpPost]
        public async Task<IActionResult> NewPost([FromBody] NewPostJson json)
        {
            try
            {
                ORACLEconn ORACLEConnectTry = new ORACLEconn();
                ORACLEConnectTry.getConn();
                SqlSugarClient sqlORM = ORACLEConnectTry.sqlORM;
                Console.WriteLine("Get NewPost");
                // ������ͷ�л�ȡ���ݵ�JWT����
                string authorizationHeader = Request.Headers["Authorization"].FirstOrDefault();
                Console.WriteLine("0");
                //��֤ Authorization ����ͷ�Ƿ���� JWT ����
                if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer"))
                {
                    Console.WriteLine("δ�ṩ��Ч��JWT");
                    return BadRequest(new { ok = "δ�ṩ��Ч��JWT" });
                }
                Console.WriteLine("1");
                //
                string jwtToken = authorizationHeader.Substring("Bearer ".Length).Trim();
                // ��֤������JWT����
                var handler = new JwtSecurityTokenHandler();
                var tokenS = handler.ReadJwtToken(jwtToken);
                // ��ȡJWT�����е�claims��Ϣ
                string account = tokenS.Claims.FirstOrDefault(claim => claim.Type == "account")?.Value;
                string password = tokenS.Claims.FirstOrDefault(claim => claim.Type == "password").Value;
                Console.WriteLine("2");
                List<Usr> tempUsr = new List<Usr>();
                tempUsr = await sqlORM.Queryable<Usr>().Where(it => it.userAccount == account
                && it.userPassword == password)
                    .ToListAsync();
                Console.WriteLine("3");
                //�ж��û��Ƿ����
                if (tempUsr.Count() == 0)
                {
                    Console.WriteLine("�û�������");
                    return Ok(new CustomResponse { ok = "no", value = "������û���Ϣ" });//�û��˻����������
                }
                Console.WriteLine("�û���Ϣ��ȷ");
                //��ȡ�µ�Post_id
                int post_id = sqlORM.Queryable<Posts>().Max(it => it.post_id) + 1; 
                //����json�ļ�
                String title = json.title;
                String contains = json.contains;

                Console.WriteLine("4");
                //List<string> ListTags = new List<string>(json.tags);
                //�½�post
                Posts post = new Posts
                {
                    post_id = post_id,
                    publishDateTime = DateTime.Now,
                    contains = contains,
                    isBanned = 0,
                    approvalNum = 0,
                    disapprovalNum = 0,
                    favouriteNum = 0
                };
                Console.WriteLine("5");
                //�½�PublishPost
                PublishPost publishPost = new PublishPost
                {
                    user_id = tempUsr.FirstOrDefault().user_id,
                    post_id = post.post_id
                };
                //�½�tag
                /*
                foreach (string tagstr in ListTags)
                {
                    Console.WriteLine("tag:" + tagstr);
                    //Tag tag = new Tag
                    //{
                    //    post_id = post_id,
                    //    tagName = tagstr
                    //};
                }*/
                Console.WriteLine("post_id= " + post.post_id);
                Console.WriteLine("publishDateTime= " + post.publishDateTime);
                Console.WriteLine("contains= " + post.contains);
                Console.WriteLine("isBanned= " + post.isBanned);
                Console.WriteLine("approvalNum= " + post.approvalNum);
                Console.WriteLine("disapprovalNum= " + post.disapprovalNum);
                Console.WriteLine("favouriteNum= " + post.favouriteNum);
                Console.WriteLine("6");
                int count1 = await sqlORM.Insertable(post).ExecuteCommandAsync();
                int count2 = await sqlORM.Insertable(publishPost).ExecuteCommandAsync();
                //int count3 = await sqlORM.Insertable(publishPost).ExecuteCommandAsync();
                if (count1 == 1 && count2==1)
                {
                    Console.WriteLine("���ӷ����ɹ�");
                    Console.WriteLine("post_id= " + post.post_id);
                    Console.WriteLine("publishDateTime= " + post.publishDateTime);
                    Console.WriteLine("contains= " + post.contains);
                    Console.WriteLine("isBanned= " + post.isBanned);
                    Console.WriteLine("approvalNum= " + post.approvalNum);
                    Console.WriteLine("disapprovalNum= " + post.disapprovalNum);
                    Console.WriteLine("favouriteNum= " + post.favouriteNum);
                    return Ok(new CustomResponse { ok = "yes", value = "Success" });
                }
                else
                {
                    if(count1==0)
                        Console.WriteLine("���ӷ���ʧ��");
                    else if(count2==0)
                        Console.WriteLine("publishPost����ʧ��");
                    //else if (count3 == 0)
                    //    Console.WriteLine("Tag����ʧ��");
                    return Ok(new CustomResponse { ok = "no", value = "Fail" });
                }
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