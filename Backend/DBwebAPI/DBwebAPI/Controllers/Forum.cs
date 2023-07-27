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
                // 从请求头中获取传递的JWT令牌
                string authorizationHeader = Request.Headers["Authorization"].FirstOrDefault();
                Console.WriteLine("0");
                //验证 Authorization 请求头是否包含 JWT 令牌
                if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer"))
                {
                    Console.WriteLine("未提供有效的JWT");
                    return BadRequest(new { ok = "未提供有效的JWT" });
                }
                Console.WriteLine("1");
                //
                string jwtToken = authorizationHeader.Substring("Bearer ".Length).Trim();
                // 验证并解析JWT令牌
                var handler = new JwtSecurityTokenHandler();
                var tokenS = handler.ReadJwtToken(jwtToken);
                // 获取JWT令牌中的claims信息
                string account = tokenS.Claims.FirstOrDefault(claim => claim.Type == "account")?.Value;
                string password = tokenS.Claims.FirstOrDefault(claim => claim.Type == "password").Value;
                Console.WriteLine("2");
                List<Usr> tempUsr = new List<Usr>();
                tempUsr = await sqlORM.Queryable<Usr>().Where(it => it.userAccount == account
                && it.userPassword == password)
                    .ToListAsync();
                Console.WriteLine("3");
                //判断用户是否存在
                if (tempUsr.Count() == 0)
                {
                    Console.WriteLine("用户不存在");
                    return Ok(new CustomResponse { ok = "no", value = "错误的用户信息" });//用户账户或密码错误
                }
                Console.WriteLine("用户信息正确");
                //获取新的Post_id
                int post_id = sqlORM.Queryable<Posts>().Max(it => it.post_id) + 1; 
                //解析json文件
                String title = json.title;
                String contains = json.contains;

                Console.WriteLine("4");
                //List<string> ListTags = new List<string>(json.tags);
                //新建post
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
                //新建PublishPost
                PublishPost publishPost = new PublishPost
                {
                    user_id = tempUsr.FirstOrDefault().user_id,
                    post_id = post.post_id
                };
                //新建tag
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
                    Console.WriteLine("帖子发布成功");
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
                        Console.WriteLine("帖子发布失败");
                    else if(count2==0)
                        Console.WriteLine("publishPost发布失败");
                    //else if (count3 == 0)
                    //    Console.WriteLine("Tag发布失败");
                    return Ok(new CustomResponse { ok = "no", value = "Fail" });
                }
            }
            catch (Exception ex)
            {
                // 如果在令牌提取过程中出现任何异常，返回一个错误响应。
                Console.WriteLine("提取令牌时发生错误：" + ex.Message);
                return BadRequest(new { ok = "提取令牌时发生错误：" + ex.Message });
            }
        }
    }
}