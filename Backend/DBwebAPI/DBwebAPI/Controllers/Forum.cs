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
using static DBwebAPI.Controllers.ForumController;
using System.ComponentModel;
using System.Xml.Linq;

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
            public string[] tags { get; set; }
        }
        public class GetPostInfo
        {
            public int page { get; set; }
            public int count { get; set; }
            public string tag { get; set; }
        }
        public class PostInfoJson
        {
            public int post_id { get; set; }
            public string title { get; set; }
            public string contains { get;set; }
            public int approvalNum { get; set; }
            public int collectNum { get; set; }
        }
        public class PostsJson
        {
            public string ok { get; set; }
            public PostInfoJson[] postInfoJsons { get; set; }
        }
        public class PagesJson
        {
            public int page { get; set; }
            public int count { get; set; }
        }
        [HttpPost]
        public async Task<IActionResult> NewPost([FromBody] NewPostJson json)
        {
            try
            {
                Console.WriteLine("--------------------------Get NewPost--------------------------");
                ORACLEconn ORACLEConnectTry = new ORACLEconn();
                if (!ORACLEConnectTry.getConn()) {
                    Console.WriteLine("数据库连接失败");
                    return BadRequest("数据库连接失败");
                };
                SqlSugarClient sqlORM = ORACLEConnectTry.sqlORM;
                
                // 从请求头中获取传递的JWT令牌
                string authorizationHeader = Request.Headers["Authorization"].FirstOrDefault();
                //验证 Authorization 请求头是否包含 JWT 令牌
                if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer"))
                {
                    Console.WriteLine("未提供有效的JWT");
                    return BadRequest(new { ok = "未提供有效的JWT" });
                }
                //
                string jwtToken = authorizationHeader.Substring("Bearer ".Length).Trim();
                // 验证并解析JWT令牌
                var handler = new JwtSecurityTokenHandler();
                var tokenS = handler.ReadJwtToken(jwtToken);
                // 获取JWT令牌中的claims信息
                string account = tokenS.Claims.FirstOrDefault(claim => claim.Type == "account")?.Value;
                List<Usr> tempUsr = new List<Usr>();
                tempUsr = await sqlORM.Queryable<Usr>().Where(it => it.userAccount == account)
                    .ToListAsync();
                //判断用户是否存在
                if (tempUsr.Count() == 0)
                {
                    Console.WriteLine("用户不存在");
                    return Ok(new CustomResponse { ok = "no", value = "错误的用户信息" });//用户账户或密码错误
                }
               //获取新的Post_id
                int post_id = sqlORM.Queryable<Posts>().Max(it => it.post_id) + 1;
                //解析json文件
                String title = json.title;
                String contains = json.contains;
                string[] tags = json.tags;

                //新建post
                Posts post = new Posts
                {
                    post_id = post_id,
                    title = title,
                    publishDateTime = DateTime.Now,
                    contains = contains,
                    isBanned = 0,
                    approvalNum = 0,
                    disapprovalNum = 0,
                    favouriteNum = 0
                };
                //新建PublishPost
                PublishPost publishPost = new PublishPost
                {
                    user_id = tempUsr.FirstOrDefault().user_id,
                    post_id = post.post_id
                };

                int count1 = await sqlORM.Insertable(post).ExecuteCommandAsync();
                int count2 = await sqlORM.Insertable(publishPost).ExecuteCommandAsync();
                // Update the point
                var updateResult = await sqlORM.Updateable<Usr>()
                    .SetColumns(u => new Usr { userPoint = u.userPoint+10 })
                    .Where(u => u.userAccount == account)
                    .ExecuteCommandAsync();
                Console.WriteLine("发帖 积分+10");
                //新建tag
                foreach (string tagstr in tags)
                {
                    Console.WriteLine("tag= " + tagstr);
                    Tag tag = new Tag
                    {
                        post_id = post_id,
                        tagName = tagstr
                    };
                    await sqlORM.Insertable(tag).ExecuteCommandAsync();
                }
                if (count1 == 1 && count2 == 1)
                {
                    Console.WriteLine("帖子发布成功");
                    Console.WriteLine("post_id= " + post.post_id);
                    Console.WriteLine("publishDateTime= " + post.publishDateTime);
                    Console.WriteLine("title= " + post.title);
                    Console.WriteLine("contains= " + post.contains);
                    Console.WriteLine("isBanned= " + post.isBanned);
                    Console.WriteLine("approvalNum= " + post.approvalNum);
                    Console.WriteLine("disapprovalNum= " + post.disapprovalNum);
                    Console.WriteLine("favouriteNum= " + post.favouriteNum);
                    return Ok(new CustomResponse { ok = "yes", value = "Success" });
                }
                else
                {
                    if (count1 == 0)
                        Console.WriteLine("帖子发布失败");
                    else if (count2 == 0)
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
        [HttpGet]
        public async Task<IActionResult> GetPostNum()
        {
            try
            {
                Console.WriteLine("--------------------------Get GetPostNum--------------------------");
                ORACLEconn ORACLEConnectTry = new ORACLEconn();
                if (!ORACLEConnectTry.getConn())
                {
                    Console.WriteLine("数据库连接失败");
                    return BadRequest("数据库连接失败");
                };
                SqlSugarClient sqlORM = ORACLEConnectTry.sqlORM;
               
                // Get the total count of posts from the database using SqlSugar's Queryable.Count method.
                int totalCount = await sqlORM.Queryable<Posts>().CountAsync();
                Console.WriteLine("PostsNum：" + totalCount);
                // Return the total count as a JSON response.
                return Ok(new { totalPostsCount = totalCount });
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while retrieving the total count of posts：" + ex.Message);
                return BadRequest(new { error = "An error occurred while retrieving the total count of posts." });
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetPostbyOrder([FromBody] GetPostInfo json)
        {
            try
            {
                Console.WriteLine("--------------------------Get GetPostbyOrder--------------------------");
                ORACLEconn ORACLEConnectTry = new ORACLEconn();
                if (!ORACLEConnectTry.getConn())
                {
                    Console.WriteLine("数据库连接失败");
                    return BadRequest("数据库连接失败");
                };
                SqlSugarClient sqlORM = ORACLEConnectTry.sqlORM;
                
                int count = json.count;
                int page = json.page;
                string tag = json.tag;
                int startIndex = (page - 1) * count;
                int endIndex = startIndex + count;

                Console.WriteLine("page: " + page);
                Console.WriteLine("count: " + count);
                List<Posts> allPosts = new List<Posts>();
                List<Posts> filteredPosts = new List<Posts>();
                List<Posts> posts = new List<Posts>();
                List<PostInfoJson> postInfoJsons = new List<PostInfoJson>();
                if (!tag.Equals("ALL"))
                {
                    List<Tag> allTags = await sqlORM.Queryable<Tag>().Where(it => true).ToListAsync();
                    foreach (Tag t in allTags)
                    {
                        Console.WriteLine("Post_id: " + t.post_id + "   Tag: " + t.tagName);
                    }
                    List<int> matchingPostIds = allTags.Where(t => t.tagName.Equals(tag)).Select(t => t.post_id).ToList();
                    filteredPosts = await sqlORM.Queryable<Posts>().Where(post => matchingPostIds.Contains(post.post_id))
                            .OrderByDescending(post => post.publishDateTime).ToListAsync();
                    IEnumerable<Posts> postsForPage = filteredPosts.Skip(startIndex).Take(count);
                    postInfoJsons = postsForPage.Select(post => new PostInfoJson
                    {
                        post_id = post.post_id,
                        title = post.title,
                        contains = post.contains,
                        approvalNum = post.approvalNum,
                        collectNum = post.favouriteNum
                    }).ToList();
                }
                else
                {
                    allPosts = await sqlORM.Queryable<Posts>().Where(it => true).OrderByDescending(post => post.publishDateTime).ToListAsync();
                    // Retrieve the posts for the current page
                    IEnumerable<Posts> postsForPage = allPosts.Skip(startIndex).Take(count);
                    // Map the posts to the desired PostInfoJson format
                    postInfoJsons = postsForPage.Select(post => new PostInfoJson
                    {
                        post_id = post.post_id,
                        title = post.title
                    }).ToList();
                }
                PostsJson response = new PostsJson
                {
                    ok = "yes",
                    postInfoJsons = postInfoJsons.ToArray()
                };
                Console.WriteLine("success");
                return Ok(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine("errorResponse: " + ex.Message);
                PostsJson errorResponse = new PostsJson
                {
                    ok = "no",
                    postInfoJsons = new PostInfoJson[0]
                };
                return Ok(errorResponse);
            }
        }
        public class needpostinfo
        {
            public int post_id { get; set; }
        }
        public class Comment
        {
            public string userName { get; set; }
            public string contains { get; set; }
            public DateTime publishDateTime { get; set; }
        }
        public class Sentpostinfo
        {
            public string ok { get; set; }
            public string name { get; set; }
            public string title { get; set; }
            public string contains { get; set; }
            public DateTime publishDateTime { get; set; }
            public int approvalNum { get; set; }
            public Comment[] comments { get; set; }
            public int islike { get; set; }
            public int iscollect { get; set; }
            public int isfollow { get; set; }
        }
        [HttpPost]
        public async Task<IActionResult> PostInfo([FromBody] needpostinfo json)
        {
            try
            {
                Console.WriteLine("--------------------------Get PostInfo--------------------------");
                ORACLEconn ORACLEConnectTry = new ORACLEconn();
                if (!ORACLEConnectTry.getConn())
                {
                    Console.WriteLine("数据库连接失败");
                    return BadRequest("数据库连接失败");
                };
                SqlSugarClient sqlORM = ORACLEConnectTry.sqlORM;
                
                int post_id = json.post_id;
                // 从请求头中获取传递的JWT令牌
                string authorizationHeader = Request.Headers["Authorization"].FirstOrDefault();
                //验证 Authorization 请求头是否包含 JWT 令牌
                if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer"))
                {
                    Console.WriteLine("未提供有效的JWT");
                    return BadRequest(new { ok = "no", value = "未提供有效的JWT" });
                }
                //
                string jwtToken = authorizationHeader.Substring("Bearer ".Length).Trim();
                // 验证并解析JWT令牌
                var handler = new JwtSecurityTokenHandler();
                var tokenS = handler.ReadJwtToken(jwtToken);
                // 获取JWT令牌中的claims信息
                string account = tokenS.Claims.FirstOrDefault(claim => claim.Type == "account")?.Value;
                List<Usr> tempUsr = new List<Usr>();
                tempUsr = await sqlORM.Queryable<Usr>().Where(it => it.userAccount == account)
                    .ToListAsync();
                //判断用户是否存在
                if (tempUsr.Count() == 0)
                {
                    Console.WriteLine("用户不存在");
                    return Ok(new CustomResponse { ok = "no", value = "错误的用户信息" });//用户账户或密码错误
                }

                Console.WriteLine("post_id: " + post_id);
                Console.WriteLine("user_id: " + tempUsr.FirstOrDefault().user_id);
                int user_id = tempUsr.FirstOrDefault().user_id;
                //找到post
                List<Posts> tempPosts = new List<Posts>();
                tempPosts = await sqlORM.Queryable<Posts>().Where(it => it.post_id == post_id)
                    .ToListAsync();
                //判断帖子是否存在
                if (tempPosts.Count() == 0)
                {
                    Console.WriteLine("帖子不存在");
                    return Ok(new CustomResponse { ok = "no", value = "帖子不存在" });
                }
                //找到发帖人ID
                List<PublishPost> tempPublicshPosts = new List<PublishPost>();
                tempPublicshPosts = await sqlORM.Queryable<PublishPost>().Where(it => it.post_id == post_id)
                    .ToListAsync();
                //找到发帖人
                List<Usr> PostUsr = new List<Usr>();
                PostUsr = await sqlORM.Queryable<Usr>().Where(it => it.user_id == tempPublicshPosts.FirstOrDefault().user_id)
                    .ToListAsync();
                //找到关注
                List<Follow> follow = new List<Follow>();
                follow = await sqlORM.Queryable<Follow>().Where(it => it.follow_id == PostUsr.FirstOrDefault().user_id && it.follower_id == user_id)
                    .ToListAsync();
                //找到评论
                List<Comments> PostComments = new List<Comments>();
                PostComments = await sqlORM.Queryable<Comments>().Where(it => it.post_id == post_id)
                    .ToListAsync();

                Sentpostinfo sentpostinfo = new Sentpostinfo();
                //找到Like
                List<LikePost> tempLike = new List<LikePost>();
                tempLike = await sqlORM.Queryable<LikePost>().Where(it => it.post_id == post_id && it.user_id == tempUsr.FirstOrDefault().user_id)
                    .ToListAsync();
                //找到collect
                List<CollectPost> tempCollect = new List<CollectPost>();
                tempCollect = await sqlORM.Queryable<CollectPost>().Where(it => it.post_id == post_id && it.user_id == tempUsr.FirstOrDefault().user_id)
                    .ToListAsync();
                sentpostinfo.iscollect= tempCollect.Count!=0? 1 : 0;
                sentpostinfo.islike = tempLike.Count != 0? 1: 0;
                sentpostinfo.isfollow = follow.Count != 0 ? 1 : 0;
                sentpostinfo.ok = "yes";
                sentpostinfo.name = PostUsr.FirstOrDefault().userName;//string
                sentpostinfo.title = tempPosts.FirstOrDefault().title;//string
                sentpostinfo.contains = tempPosts.FirstOrDefault().contains;//string
                sentpostinfo.publishDateTime = tempPosts.FirstOrDefault().publishDateTime;//DataTime
                sentpostinfo.approvalNum = (int)tempPosts.FirstOrDefault().approvalNum;//int
                sentpostinfo.comments = new Comment[0];
                List<Comment> commentsList = new List<Comment>();
                foreach (var comment in PostComments)
                {
                    //找到评论人username
                    List<Usr> ComUsr = new List<Usr>();
                    ComUsr = await sqlORM.Queryable<Usr>().Where(it => it.user_id == comment.user_id)
                        .ToListAsync();
                    Comment tmpComment = new Comment();
                    tmpComment.userName = ComUsr.First().userName;
                    tmpComment.contains = comment.contains;
                    tmpComment.publishDateTime = comment.publishDateTime;
                    Console.WriteLine();
                    Console.WriteLine("userName: " + tmpComment.userName);
                    Console.WriteLine("contains: " + tmpComment.contains);
                    Console.WriteLine("publishDateTime: " + tmpComment.publishDateTime);
                    commentsList.Add(tmpComment);
                }
                sentpostinfo.comments = commentsList.ToArray();

                Console.WriteLine("name: " + sentpostinfo.name);
                Console.WriteLine("title: " + sentpostinfo.title);
                Console.WriteLine("contains: " + sentpostinfo.contains);
                Console.WriteLine("publishDateTime: " + sentpostinfo.publishDateTime);
                Console.WriteLine("approvalNum: " + sentpostinfo.approvalNum);
                Console.WriteLine("Like: " + sentpostinfo.islike);
                Console.WriteLine("Collect: " + sentpostinfo.iscollect);
                return Ok(sentpostinfo);
            }
            catch (Exception ex)
            {
                Console.WriteLine("errorResponse: " + ex.Message);
                return Ok(new CustomResponse { ok = "no", value = "数据库连接错误" });//用户账户或密码错误
            }
        }
        public class newComment
        {
            public int post_id { get; set; }
            public string contains { get; set; }
        }
        [HttpPost]
        public async Task<IActionResult> NewComment([FromBody] newComment json)
        {
            try
            {
                Console.WriteLine("--------------------------Get NewComment--------------------------");
                ORACLEconn ORACLEConnectTry = new ORACLEconn();
                if (!ORACLEConnectTry.getConn())
                {
                    Console.WriteLine("数据库连接失败");
                    return BadRequest("数据库连接失败");
                };
                SqlSugarClient sqlORM = ORACLEConnectTry.sqlORM;
                
                int post_id = json.post_id;
                string contains = json.contains;
                // 从请求头中获取传递的JWT令牌
                string authorizationHeader = Request.Headers["Authorization"].FirstOrDefault();
                //验证 Authorization 请求头是否包含 JWT 令牌
                if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer"))
                {
                    Console.WriteLine("未提供有效的JWT");
                    return BadRequest(new { ok = "no", value = "未提供有效的JWT" });
                }
                //
                string jwtToken = authorizationHeader.Substring("Bearer ".Length).Trim();
                // 验证并解析JWT令牌
                var handler = new JwtSecurityTokenHandler();
                var tokenS = handler.ReadJwtToken(jwtToken);
                // 获取JWT令牌中的claims信息
                string account = tokenS.Claims.FirstOrDefault(claim => claim.Type == "account")?.Value;
                List<Usr> tempUsr = new List<Usr>();
                tempUsr = await sqlORM.Queryable<Usr>().Where(it => it.userAccount == account)
                    .ToListAsync();
                //判断用户是否存在
                if (tempUsr.Count() == 0)
                {
                    Console.WriteLine("用户不存在");
                    return Ok(new CustomResponse { ok = "no", value = "错误的用户信息" });//用户账户或密码错误
                }

                Console.WriteLine("post_id: " + post_id);
                Console.WriteLine("user_id: " + tempUsr.FirstOrDefault().user_id);
                Console.WriteLine("contains: " + contains);
                //找到post
                List<Posts> tempPosts = new List<Posts>();
                tempPosts = await sqlORM.Queryable<Posts>().Where(it => it.post_id == post_id)
                    .ToListAsync();
                //判断帖子是否存在
                if (tempPosts.Count() == 0)
                {
                    Console.WriteLine("帖子不存在");
                    return Ok(new CustomResponse { ok = "no", value = "帖子不存在" });
                }
                //找到评论人ID
                List<PublishPost> tempPublicshPosts = new List<PublishPost>();
                tempPublicshPosts = await sqlORM.Queryable<PublishPost>().Where(it => it.post_id == post_id)
                    .ToListAsync();
                //获取新的comment_id
                int comment_id = sqlORM.Queryable<Comments>().Max(it => it.comment_id) + 1;
                //解析json文件
                //新建post
                Comments comment = new Comments
                {
                    comment_id = comment_id,
                    publishDateTime = DateTime.Now,
                    contains = contains,
                    user_id = tempUsr.FirstOrDefault().user_id,
                    post_id = post_id
                };
                // Update the point
                var updateResult = await sqlORM.Updateable<Usr>()
                    .SetColumns(u => new Usr { userPoint = u.userPoint + 3 })
                    .Where(u => u.userAccount == account)
                    .ExecuteCommandAsync();
                Console.WriteLine("评论 积分+3");
                int count = await sqlORM.Insertable(comment).ExecuteCommandAsync();
                if(count > 0) { return Ok(new CustomResponse { ok = "yes", value = "发布成功" }); }
                else {  return Ok(new CustomResponse { ok="no", value = "发布失败" }); }
            }
            catch (Exception ex)
            {
                Console.WriteLine("errorResponse: " + ex.Message);
                return Ok(new CustomResponse { ok = "no", value = "数据库连接错误" });//用户账户或密码错误
            }
        }
        public class LikeJson
        {
            public int post_id { get; set; }
        }
       
        [HttpPost]
        public async Task<IActionResult> Like([FromBody] LikeJson json)
        {
            try
            {
                Console.WriteLine("--------------------------Get Like--------------------------");
                ORACLEconn ORACLEConnectTry = new ORACLEconn();
                if (!ORACLEConnectTry.getConn())
                {
                    Console.WriteLine("数据库连接失败");
                    return BadRequest("数据库连接失败");
                };
                SqlSugarClient sqlORM = ORACLEConnectTry.sqlORM;

                int post_id = json.post_id;
                // 从请求头中获取传递的JWT令牌
                string authorizationHeader = Request.Headers["Authorization"].FirstOrDefault();
                //验证 Authorization 请求头是否包含 JWT 令牌
                if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer"))
                {
                    Console.WriteLine("未提供有效的JWT");
                    return BadRequest(new { ok = "no", value = "未提供有效的JWT" });
                }
                //
                string jwtToken = authorizationHeader.Substring("Bearer ".Length).Trim();
                // 验证并解析JWT令牌
                var handler = new JwtSecurityTokenHandler();
                var tokenS = handler.ReadJwtToken(jwtToken);
                // 获取JWT令牌中的claims信息
                string account = tokenS.Claims.FirstOrDefault(claim => claim.Type == "account")?.Value;
                List<Usr> tempUsr = new List<Usr>();
                tempUsr = await sqlORM.Queryable<Usr>().Where(it => it.userAccount == account)
                    .ToListAsync();
                //判断用户是否存在
                if (tempUsr.Count() == 0)
                {
                    Console.WriteLine("用户不存在");
                    return Ok(new CustomResponse { ok = "no", value = "错误的用户信息" });//用户账户或密码错误
                }

                Console.WriteLine("post_id: " + post_id);
                Console.WriteLine("user_id: " + tempUsr.FirstOrDefault().user_id);
                //找到post
                List<Posts> tempPosts = new List<Posts>();
                tempPosts = await sqlORM.Queryable<Posts>().Where(it => it.post_id == post_id)
                    .ToListAsync();
                //判断帖子是否存在
                if (tempPosts.Count() == 0)
                {
                    Console.WriteLine("帖子不存在");
                    return Ok(new CustomResponse { ok = "no", value = "帖子不存在" });
                }
                //找到Like
                List<LikePost> tempLike = new List<LikePost>();
                tempLike = await sqlORM.Queryable<LikePost>().Where(it => it.post_id == post_id && it.user_id == tempUsr.FirstOrDefault().user_id)
                    .ToListAsync();
                //未点赞
                if (tempLike.Count == 0)
                {
                    LikePost like = new LikePost();
                    like.user_id = tempUsr.FirstOrDefault().user_id;
                    like.post_id=post_id;
                    like.createDateTime = DateTime.Now;
                    int count = await sqlORM.Insertable(like).ExecuteCommandAsync();
                    tempPosts.FirstOrDefault().approvalNum++;
                    // Update the point
                    var updateResult = await sqlORM.Updateable<Usr>()
                        .SetColumns(u => new Usr { userPoint = u.userPoint + 3 })
                        .Where(u => u.userAccount == account)
                        .ExecuteCommandAsync();
                    Console.WriteLine("点赞 积分+1");
                    int updateCount = await sqlORM.Updateable(tempPosts.FirstOrDefault()).ExecuteCommandAsync();
                    if (count* updateCount > 0) { Console.WriteLine("like success"); return Ok(new CustomResponse { ok = "yes", value = "点赞成功" }); }
                    else { Console.WriteLine("like fail"); return Ok(new CustomResponse { ok = "no", value = "点赞失败" });  }
                }
                else
                {
                    // 已点赞，执行取消点赞逻辑
                    tempPosts.FirstOrDefault().approvalNum--;
                    int updateCount = await sqlORM.Updateable(tempPosts.FirstOrDefault()).ExecuteCommandAsync();
                    int deletedCount = await sqlORM.Deleteable<LikePost>(tempLike.FirstOrDefault()).ExecuteCommandAsync();
                    if (deletedCount* updateCount > 0)
                    {
                        Console.WriteLine("delike success");
                        return Ok(new CustomResponse { ok = "yes", value = "取消点赞成功" });
                    }
                    else
                    {
                        Console.WriteLine("delike fail");
                        return Ok(new CustomResponse { ok = "no", value = "取消点赞失败" });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("errorResponse: " + ex.Message);
                return Ok(new CustomResponse { ok = "no", value = "数据库连接错误" });//用户账户或密码错误
            }
        }
        public class CollectJson
        {
            public int post_id { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> collect([FromBody] LikeJson json)
        {
            try
            {
                Console.WriteLine("--------------------------Get collect--------------------------");
                ORACLEconn ORACLEConnectTry = new ORACLEconn();
                if (!ORACLEConnectTry.getConn())
                {
                    Console.WriteLine("数据库连接失败");
                    return BadRequest("数据库连接失败");
                };
                SqlSugarClient sqlORM = ORACLEConnectTry.sqlORM;
                
                int post_id = json.post_id;
                // 从请求头中获取传递的JWT令牌
                string authorizationHeader = Request.Headers["Authorization"].FirstOrDefault();
                //验证 Authorization 请求头是否包含 JWT 令牌
                if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer"))
                {
                    Console.WriteLine("未提供有效的JWT");
                    return BadRequest(new { ok = "no", value = "未提供有效的JWT" });
                }
                //
                string jwtToken = authorizationHeader.Substring("Bearer ".Length).Trim();
                // 验证并解析JWT令牌
                var handler = new JwtSecurityTokenHandler();
                var tokenS = handler.ReadJwtToken(jwtToken);
                // 获取JWT令牌中的claims信息
                string account = tokenS.Claims.FirstOrDefault(claim => claim.Type == "account")?.Value;
                List<Usr> tempUsr = new List<Usr>();
                tempUsr = await sqlORM.Queryable<Usr>().Where(it => it.userAccount == account)
                    .ToListAsync();
                //判断用户是否存在
                if (tempUsr.Count() == 0)
                {
                    Console.WriteLine("用户不存在");
                    return Ok(new CustomResponse { ok = "no", value = "错误的用户信息" });//用户账户或密码错误
                }

                Console.WriteLine("post_id: " + post_id);
                Console.WriteLine("user_id: " + tempUsr.FirstOrDefault().user_id);
                //找到post
                List<Posts> tempPosts = new List<Posts>();
                tempPosts = await sqlORM.Queryable<Posts>().Where(it => it.post_id == post_id)
                    .ToListAsync();
                //判断帖子是否存在
                if (tempPosts.Count() == 0)
                {
                    Console.WriteLine("帖子不存在");
                    return Ok(new CustomResponse { ok = "no", value = "帖子不存在" });
                }
                //找到Collect
                List<CollectPost> tempCollect = new List<CollectPost>();
                tempCollect = await sqlORM.Queryable<CollectPost>().Where(it => it.post_id == post_id && it.user_id == tempUsr.FirstOrDefault().user_id)
                    .ToListAsync();
                //未收藏
                if (tempCollect.Count == 0)
                {
                    CollectPost like = new CollectPost();
                    like.user_id = tempUsr.FirstOrDefault().user_id;
                    like.post_id = post_id;
                    like.createDateTime = DateTime.Now;
                    int count = await sqlORM.Insertable(like).ExecuteCommandAsync();
                    tempPosts.FirstOrDefault().favouriteNum++;
                    // Update the point
                    var updateResult = await sqlORM.Updateable<Usr>()
                        .SetColumns(u => new Usr { userPoint = u.userPoint + 3 })
                        .Where(u => u.userAccount == account)
                        .ExecuteCommandAsync();
                    Console.WriteLine("收藏 积分+1");
                    int updateCount = await sqlORM.Updateable(tempPosts.FirstOrDefault()).ExecuteCommandAsync();
                    if (count * updateCount > 0) { Console.WriteLine("collect success"); return Ok(new CustomResponse { ok = "yes", value = "收藏成功" }); }
                    else { Console.WriteLine("collect fail"); return Ok(new CustomResponse { ok = "no", value = "收藏失败" }); }
                }
                else
                {
                    // 已收藏，执行取消收藏逻辑
                    tempPosts.FirstOrDefault().favouriteNum--;
                    int updateCount = await sqlORM.Updateable(tempPosts.FirstOrDefault()).ExecuteCommandAsync();
                    int deletedCount = await sqlORM.Deleteable<CollectPost>(tempCollect.FirstOrDefault()).ExecuteCommandAsync();
                    if (deletedCount * updateCount > 0)
                    {
                        Console.WriteLine("decollect success");
                        return Ok(new CustomResponse { ok = "yes", value = "取消收藏成功" });
                    }
                    else
                    {
                        Console.WriteLine("decollect fail");
                        return Ok(new CustomResponse { ok = "no", value = "取消收藏失败" });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("errorResponse: " + ex.Message);
                return Ok(new CustomResponse { ok = "no", value = "数据库连接错误" });//用户账户或密码错误
            }
        }
        public class followJosn
        {
            public int post_id { get; set; }
        }
        [HttpPost]
        public async Task<IActionResult> follow([FromBody] followJosn json)
        {
            try
            {
                Console.WriteLine("--------------------------Get Follow--------------------------");
                ORACLEconn ORACLEConnectTry = new ORACLEconn();
                if (!ORACLEConnectTry.getConn())
                {
                    Console.WriteLine("数据库连接失败");
                    return BadRequest("数据库连接失败");
                };
                SqlSugarClient sqlORM = ORACLEConnectTry.sqlORM;
                int post_id = json.post_id;
                // 从请求头中获取传递的JWT令牌
                string authorizationHeader = Request.Headers["Authorization"].FirstOrDefault();
                //验证 Authorization 请求头是否包含 JWT 令牌
                if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer"))
                {
                    Console.WriteLine("error JWT");
                    return BadRequest(new { ok = "no", value = "未提供有效的JWT" });
                }
                //
                string jwtToken = authorizationHeader.Substring("Bearer ".Length).Trim();
                // 验证并解析JWT令牌
                var handler = new JwtSecurityTokenHandler();
                var tokenS = handler.ReadJwtToken(jwtToken);
                // 获取JWT令牌中的claims信息
                string account = tokenS.Claims.FirstOrDefault(claim => claim.Type == "account")?.Value;
                List<Usr> tempUsr = new List<Usr>();
                tempUsr = await sqlORM.Queryable<Usr>().Where(it => it.userAccount == account)
                    .ToListAsync();
                //判断用户是否存在
                if (tempUsr.Count() == 0)
                {
                    Console.WriteLine("error account");
                    return Ok(new CustomResponse { ok = "no", value = "错误的用户信息" });//用户账户或密码错误
                }

                Console.WriteLine("post_id: " + post_id);
                Console.WriteLine("user_id: " + tempUsr.FirstOrDefault().user_id);
                int user_id = tempUsr.FirstOrDefault().user_id;
                //找到post
                List<Posts> tempPosts = new List<Posts>();
                tempPosts = await sqlORM.Queryable<Posts>().Where(it => it.post_id == post_id)
                    .ToListAsync();
                //判断帖子是否存在
                if (tempPosts.Count() == 0)
                {
                    Console.WriteLine("no such post");
                    return Ok(new CustomResponse { ok = "no", value = "no such post" });
                }
                //找到发帖人ID
                List<PublishPost> tempPublicshPosts = new List<PublishPost>();
                tempPublicshPosts = await sqlORM.Queryable<PublishPost>().Where(it => it.post_id == post_id)
                    .ToListAsync();
                //找到发帖人
                List<Usr> PostUsr = new List<Usr>();
                PostUsr = await sqlORM.Queryable<Usr>().Where(it => it.user_id == tempPublicshPosts.FirstOrDefault().user_id)
                    .ToListAsync();
                Follow follow = new Follow();
                follow.follow_id = PostUsr.FirstOrDefault().user_id;
                follow.follower_id = user_id;
                follow.createDateTime = DateTime.Now;

                Follow searchF = await sqlORM.Queryable<Follow>().Where(it=>it.follower_id == follow.follower_id && it.follow_id==follow.follow_id).FirstAsync();
                int count = 0;
                if (searchF != null)
                {
                    count = await sqlORM.Deleteable(searchF).ExecuteCommandAsync();
                    if (count > 0)
                    {
                        Console.WriteLine("defollow success");
                        return Ok(new CustomResponse { ok = "yes", value = "success" });
                    }
                    else
                    {
                        Console.WriteLine("defollow success");
                        return Ok(new CustomResponse { ok = "no", value = "fail" });
                    }
                }
                else
                {
                    count = await sqlORM.Insertable(follow).ExecuteCommandAsync();
                    if (count > 0)
                    {
                        Console.WriteLine("follow success");
                        return Ok(new CustomResponse { ok = "yes", value = "success" });
                    }
                    else
                    {
                        Console.WriteLine("follow success");
                        return Ok(new CustomResponse { ok = "no", value = "fail" });
                    }
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("errorResponse: " + ex.Message);
                return Ok(new CustomResponse { ok = "no", value = "DB connect fail" });
            }
        }
        public class reportJson
        {
            public int post_id { get; set; }
            public string descriptions { get; set; }
        }
        [HttpPost]
        public async Task<IActionResult> report([FromBody] reportJson json)
        {
            try
            {
                Console.WriteLine("--------------------------Get reports--------------------------");
                ORACLEconn ORACLEConnectTry = new ORACLEconn();
                if (!ORACLEConnectTry.getConn())
                {
                    Console.WriteLine("数据库连接失败");
                    return BadRequest("数据库连接失败");
                };
                SqlSugarClient sqlORM = ORACLEConnectTry.sqlORM;
                
                int post_id = json.post_id;
                String descriptions = json.descriptions;
                // 从请求头中获取传递的JWT令牌
                string authorizationHeader = Request.Headers["Authorization"].FirstOrDefault();
                //验证 Authorization 请求头是否包含 JWT 令牌
                if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer"))
                {
                    Console.WriteLine("error JWT");
                    return BadRequest(new { ok = "no", value = "未提供有效的JWT" });
                }
                //
                string jwtToken = authorizationHeader.Substring("Bearer ".Length).Trim();
                // 验证并解析JWT令牌
                var handler = new JwtSecurityTokenHandler();
                var tokenS = handler.ReadJwtToken(jwtToken);
                // 获取JWT令牌中的claims信息
                string account = tokenS.Claims.FirstOrDefault(claim => claim.Type == "account")?.Value;
                List<Usr> tempUsr = new List<Usr>();
                tempUsr = await sqlORM.Queryable<Usr>().Where(it => it.userAccount == account)
                    .ToListAsync();
                //判断用户是否存在
                if (tempUsr.Count() == 0)
                {
                    Console.WriteLine("error account");
                    return Ok(new CustomResponse { ok = "no", value = "错误的用户信息" });//用户账户或密码错误
                }

                Console.WriteLine("post_id: " + post_id);
                Console.WriteLine("user_id: " + tempUsr.FirstOrDefault().user_id);
                int user_id = tempUsr.FirstOrDefault().user_id;
                //找到post
                List<Posts> tempPosts = new List<Posts>();
                tempPosts = await sqlORM.Queryable<Posts>().Where(it => it.post_id == post_id)
                    .ToListAsync();
                //判断帖子是否存在
                if (tempPosts.Count() == 0)
                {
                    Console.WriteLine("no such post");
                    return Ok(new CustomResponse { ok = "no", value = "no such post" });
                }
                //找到发帖人ID
                List<PublishPost> tempPublicshPosts = new List<PublishPost>();
                tempPublicshPosts = await sqlORM.Queryable<PublishPost>().Where(it => it.post_id == post_id)
                    .ToListAsync();
                //找到发帖人
                List<Usr> PostUsr = new List<Usr>();
                PostUsr = await sqlORM.Queryable<Usr>().Where(it => it.user_id == tempPublicshPosts.FirstOrDefault().user_id)
                    .ToListAsync();
                Reports reports = new Reports();
                reports.reporter_id = user_id;
                reports.post_id = post_id;
                //reports.reportee_id = PostUsr.FirstOrDefault().user_id;
                reports.report_time=DateTime.Now;
                reports.status = 0;
                reports.descriptions = descriptions;
                int count = await sqlORM.Insertable(reports).ExecuteCommandAsync();
                if (count > 0)
                {
                    Console.WriteLine("reporter_id:"+ reports.reporter_id);
                    Console.WriteLine("post_id:"+ post_id);
                    //Console.WriteLine("reportee_id:" + reports.reportee_id);
                    Console.WriteLine("descriptions:" + descriptions);
                    Console.WriteLine("reports success");
                    return Ok(new CustomResponse { ok = "yes", value = "success" });
                }
                else
                {
                    Console.WriteLine("follow success");
                    return Ok(new CustomResponse { ok = "no", value = "fail" });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("errorResponse: " + ex.Message);
                return Ok(new CustomResponse { ok = "no", value = "DB connect fail" });
            }
        }
    }
   
}