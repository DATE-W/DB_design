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
                ORACLEconn ORACLEConnectTry = new ORACLEconn();
                ORACLEConnectTry.getConn();
                SqlSugarClient sqlORM = ORACLEConnectTry.sqlORM;
                Console.WriteLine("Get NewPost");
                // ������ͷ�л�ȡ���ݵ�JWT����
                string authorizationHeader = Request.Headers["Authorization"].FirstOrDefault();
                //��֤ Authorization ����ͷ�Ƿ���� JWT ����
                if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer"))
                {
                    Console.WriteLine("δ�ṩ��Ч��JWT");
                    return BadRequest(new { ok = "δ�ṩ��Ч��JWT" });
                }
                //
                string jwtToken = authorizationHeader.Substring("Bearer ".Length).Trim();
                // ��֤������JWT����
                var handler = new JwtSecurityTokenHandler();
                var tokenS = handler.ReadJwtToken(jwtToken);
                // ��ȡJWT�����е�claims��Ϣ
                string account = tokenS.Claims.FirstOrDefault(claim => claim.Type == "account")?.Value;
                List<Usr> tempUsr = new List<Usr>();
                tempUsr = await sqlORM.Queryable<Usr>().Where(it => it.userAccount == account)
                    .ToListAsync();
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
                string[] tags = json.tags;

                //�½�post
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
                //�½�PublishPost
                PublishPost publishPost = new PublishPost
                {
                    user_id = tempUsr.FirstOrDefault().user_id,
                    post_id = post.post_id
                };

                int count1 = await sqlORM.Insertable(post).ExecuteCommandAsync();
                int count2 = await sqlORM.Insertable(publishPost).ExecuteCommandAsync();
                //�½�tag
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
                    Console.WriteLine("���ӷ����ɹ�");
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
                        Console.WriteLine("���ӷ���ʧ��");
                    else if (count2 == 0)
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
        [HttpGet]
        public async Task<IActionResult> GetPostNum()
        {
            try
            {
                ORACLEconn ORACLEConnectTry = new ORACLEconn();
                ORACLEConnectTry.getConn();
                SqlSugarClient sqlORM = ORACLEConnectTry.sqlORM;
                Console.WriteLine("Get GetPostNum");
                // Get the total count of posts from the database using SqlSugar's Queryable.Count method.
                int totalCount = await sqlORM.Queryable<Posts>().CountAsync();
                Console.WriteLine("PostsNum��" + totalCount);
                // Return the total count as a JSON response.
                return Ok(new { totalPostsCount = totalCount });
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while retrieving the total count of posts��" + ex.Message);
                return BadRequest(new { error = "An error occurred while retrieving the total count of posts." });
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetPostbyOrder([FromBody] GetPostInfo json)
        {
            try
            {
                ORACLEconn ORACLEConnectTry = new ORACLEconn();
                ORACLEConnectTry.getConn();
                SqlSugarClient sqlORM = ORACLEConnectTry.sqlORM;
                Console.WriteLine("Get GetPostbyPages");
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
                        title = post.title
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
        }
        [HttpPost]
        public async Task<IActionResult> PostInfo([FromBody] needpostinfo json)
        {
            try
            {
                ORACLEconn ORACLEConnectTry = new ORACLEconn();
                ORACLEConnectTry.getConn();
                SqlSugarClient sqlORM = ORACLEConnectTry.sqlORM;
                Console.WriteLine("Get PostInfo");
                int post_id = json.post_id;
                // ������ͷ�л�ȡ���ݵ�JWT����
                string authorizationHeader = Request.Headers["Authorization"].FirstOrDefault();
                //��֤ Authorization ����ͷ�Ƿ���� JWT ����
                if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer"))
                {
                    Console.WriteLine("δ�ṩ��Ч��JWT");
                    return BadRequest(new { ok = "no", value = "δ�ṩ��Ч��JWT" });
                }
                //
                string jwtToken = authorizationHeader.Substring("Bearer ".Length).Trim();
                // ��֤������JWT����
                var handler = new JwtSecurityTokenHandler();
                var tokenS = handler.ReadJwtToken(jwtToken);
                // ��ȡJWT�����е�claims��Ϣ
                string account = tokenS.Claims.FirstOrDefault(claim => claim.Type == "account")?.Value;
                List<Usr> tempUsr = new List<Usr>();
                tempUsr = await sqlORM.Queryable<Usr>().Where(it => it.userAccount == account)
                    .ToListAsync();
                //�ж��û��Ƿ����
                if (tempUsr.Count() == 0)
                {
                    Console.WriteLine("�û�������");
                    return Ok(new CustomResponse { ok = "no", value = "������û���Ϣ" });//�û��˻����������
                }
                Console.WriteLine("�û���Ϣ��ȷ");

                Console.WriteLine("post_id: " + post_id);
                Console.WriteLine("user_id: " + tempUsr.FirstOrDefault().user_id);
                //�ҵ�post
                List<Posts> tempPosts = new List<Posts>();
                tempPosts = await sqlORM.Queryable<Posts>().Where(it => it.post_id == post_id)
                    .ToListAsync();
                //�ж������Ƿ����
                if (tempPosts.Count() == 0)
                {
                    Console.WriteLine("���Ӳ�����");
                    return Ok(new CustomResponse { ok = "no", value = "���Ӳ�����" });
                }
                //�ҵ�������ID
                List<PublishPost> tempPublicshPosts = new List<PublishPost>();
                tempPublicshPosts = await sqlORM.Queryable<PublishPost>().Where(it => it.post_id == post_id)
                    .ToListAsync();
                //�ҵ�������
                List<Usr> PostUsr = new List<Usr>();
                PostUsr = await sqlORM.Queryable<Usr>().Where(it => it.user_id == tempPublicshPosts.FirstOrDefault().user_id)
                    .ToListAsync();
                //�ҵ�����
                List<Comments> PostComments = new List<Comments>();
                PostComments = await sqlORM.Queryable<Comments>().Where(it => it.post_id == post_id)
                    .ToListAsync();

                Sentpostinfo sentpostinfo = new Sentpostinfo();
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
                    //�ҵ�������username
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
                return Ok(sentpostinfo);
            }
            catch (Exception ex)
            {
                Console.WriteLine("errorResponse: " + ex.Message);
                return Ok(new CustomResponse { ok = "no", value = "���ݿ����Ӵ���" });//�û��˻����������
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
                ORACLEconn ORACLEConnectTry = new ORACLEconn();
                ORACLEConnectTry.getConn();
                SqlSugarClient sqlORM = ORACLEConnectTry.sqlORM;
                Console.WriteLine("Get NewComment");
                int post_id = json.post_id;
                string contains = json.contains;
                // ������ͷ�л�ȡ���ݵ�JWT����
                string authorizationHeader = Request.Headers["Authorization"].FirstOrDefault();
                //��֤ Authorization ����ͷ�Ƿ���� JWT ����
                if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer"))
                {
                    Console.WriteLine("δ�ṩ��Ч��JWT");
                    return BadRequest(new { ok = "no", value = "δ�ṩ��Ч��JWT" });
                }
                //
                string jwtToken = authorizationHeader.Substring("Bearer ".Length).Trim();
                // ��֤������JWT����
                var handler = new JwtSecurityTokenHandler();
                var tokenS = handler.ReadJwtToken(jwtToken);
                // ��ȡJWT�����е�claims��Ϣ
                string account = tokenS.Claims.FirstOrDefault(claim => claim.Type == "account")?.Value;
                List<Usr> tempUsr = new List<Usr>();
                tempUsr = await sqlORM.Queryable<Usr>().Where(it => it.userAccount == account)
                    .ToListAsync();
                //�ж��û��Ƿ����
                if (tempUsr.Count() == 0)
                {
                    Console.WriteLine("�û�������");
                    return Ok(new CustomResponse { ok = "no", value = "������û���Ϣ" });//�û��˻����������
                }
                Console.WriteLine("�û���Ϣ��ȷ");

                Console.WriteLine("post_id: " + post_id);
                Console.WriteLine("user_id: " + tempUsr.FirstOrDefault().user_id);
                Console.WriteLine("contains: " + contains);
                //�ҵ�post
                List<Posts> tempPosts = new List<Posts>();
                tempPosts = await sqlORM.Queryable<Posts>().Where(it => it.post_id == post_id)
                    .ToListAsync();
                //�ж������Ƿ����
                if (tempPosts.Count() == 0)
                {
                    Console.WriteLine("���Ӳ�����");
                    return Ok(new CustomResponse { ok = "no", value = "���Ӳ�����" });
                }
                //�ҵ�������ID
                List<PublishPost> tempPublicshPosts = new List<PublishPost>();
                tempPublicshPosts = await sqlORM.Queryable<PublishPost>().Where(it => it.post_id == post_id)
                    .ToListAsync();
                //��ȡ�µ�comment_id
                int comment_id = sqlORM.Queryable<Comments>().Max(it => it.comment_id) + 1;
                //����json�ļ�
                //�½�post
                Comments comment = new Comments
                {
                    comment_id = comment_id,
                    publishDateTime = DateTime.Now,
                    contains = contains,
                    user_id = tempUsr.FirstOrDefault().user_id,
                    post_id = post_id
                };
               
                int count = await sqlORM.Insertable(comment).ExecuteCommandAsync();
                if(count > 0) { return Ok(new CustomResponse { ok = "yes", value = "�����ɹ�" }); }
                else {  return Ok(new CustomResponse { ok="no", value = "����ʧ��" }); }
            }
            catch (Exception ex)
            {
                Console.WriteLine("errorResponse: " + ex.Message);
                return Ok(new CustomResponse { ok = "no", value = "���ݿ����Ӵ���" });//�û��˻����������
            }
        }
    }
}