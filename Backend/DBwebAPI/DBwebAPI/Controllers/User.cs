using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using DBwebAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using static DBwebAPI.Controllers.LoginController;
using static DBwebAPI.Controllers.ForumController;
using static DBwebAPI.Controllers.UserController;
using System.Security.AccessControl;

namespace DBwebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        public class profileJson{
            public String username { get; set; }
            public String uft { get; set; }
            public int follower_num { get; set; }
            public int follow_num { get;set; }
            public int approval_num { get; set; }
            public String account { get; set; }
        }
        [HttpPost]
        public async Task<IActionResult> profile()
        {
            try
            {
                ORACLEconn ORACLEConnectTry = new ORACLEconn();
                if (!ORACLEConnectTry.getConn())
                {
                    Console.WriteLine("connect DB fail");
                    return BadRequest("connect DB fail");
                };
                SqlSugarClient sqlORM = ORACLEConnectTry.sqlORM;
                Console.WriteLine("Get profile");
                // 从请求头中获取传递的JWT令牌
                string authorizationHeader = Request.Headers["Authorization"].FirstOrDefault();
                //验证 Authorization 请求头是否包含 JWT 令牌
                if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer"))
                {
                    Console.WriteLine("未提供有效的JWT");
                    //return BadRequest(new { ok = "no",value="未提供有效的JWT" });
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
                int user_id = 1;
                if (tempUsr.Count() == 0)
                {
                    Console.WriteLine("no such user");
                    //return Ok(new CustomResponse { ok = "no", value = "错误的用户信息" });//用户账户或密码错误
                }
                else
                user_id = tempUsr.FirstOrDefault().user_id;
                //用户名
                String userName = tempUsr.FirstOrDefault().userName;
                Console.WriteLine("right user");
                //关注数
                List<Follow> tmpFollowers = new List<Follow>();
                tmpFollowers = await sqlORM.Queryable<Follow>().Where(it => it.follower_id == user_id)
                    .ToListAsync();
                int follower_num = tmpFollowers.Count();
                //粉丝数
                List<Follow> tmpFollows = new List<Follow>();
                tmpFollows = await sqlORM.Queryable<Follow>().Where(it => it.follow_id == user_id)
                    .ToListAsync();
                int follow_num = tmpFollows.Count();
                //主队
                List<UserFavouriteTeam> tmpUFT_id = new List<UserFavouriteTeam>();
                tmpUFT_id = await sqlORM.Queryable<UserFavouriteTeam>().Where(it => it.user_id == user_id)
                    .ToListAsync();
                int UFT_id =tmpUFT_id.Count()!=0? tmpUFT_id.FirstOrDefault().team_id:-1;
                List<Team> tmpUFT = new List<Team>();
                tmpUFT = await sqlORM.Queryable<Team>().Where(it => it.team_id == UFT_id)
                    .ToListAsync();
                String UFT = tmpUFT.Count() != 0 ? tmpUFT.FirstOrDefault().teamName : "no such team";
                //点赞数
                List<PublishPost> tmpPP = new List<PublishPost>();
                tmpPP = await sqlORM.Queryable<PublishPost>().Where(it => it.user_id == user_id)
                    .ToListAsync();
                int approvalNum = 0;
                foreach(var pp in tmpPP)
                {
                    List<Posts> tmpPost = new List<Posts>();
                    tmpPost = await sqlORM.Queryable<Posts>().Where(it => it.post_id == pp.post_id)
                        .ToListAsync();
                    approvalNum += tmpPost.Count() != 0? tmpPost.FirstOrDefault().approvalNum : 0;
                }
                profileJson response=new profileJson();
                response.username = userName;
                response.uft = UFT;
                response.approval_num = approvalNum;
                response.follower_num = follower_num;
                response.follow_num = follow_num;
                response.account = account;
                Console.WriteLine("response.username:"+ response.username);
                Console.WriteLine("response.uft:" + response.uft);
                Console.WriteLine("response.approval_num:" + response.approval_num);
                Console.WriteLine("response.follower_num:" + response.follower_num);
                Console.WriteLine("response.follow_num:" + response.follow_num);
                Console.WriteLine("response.account:" + response.account);
                return Ok(new CustomResponse { ok = "yes", value = response });
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred：" + ex.Message);
                return BadRequest(new { error = "An error occurred." });
            }
        }
        public class modifyprofileJson
        {
            public String username { get; set; }
            public String sign { get;set; }
            public String avatar { get; set; }
        }
        [HttpPost]
        public async Task<IActionResult> modifyprofile([FromBody] modifyprofileJson json)
        {
            try
            {
                ORACLEconn ORACLEConnectTry = new ORACLEconn();
                if (!ORACLEConnectTry.getConn())
                {
                    Console.WriteLine("数据库连接失败");
                    return BadRequest("数据库连接失败");
                };
                SqlSugarClient sqlORM = ORACLEConnectTry.sqlORM;
                Console.WriteLine("Get modifyprofile");
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
                Console.WriteLine("用户信息正确");

                Usr tmpU = tempUsr.FirstOrDefault();
                int count=0, count0=0;
                int user_id = tempUsr.FirstOrDefault().user_id;
                if (json.username != null)
                {
                    //用户名
                    tmpU.userName= json.username;
                }
                count0 += 1;
                count += await sqlORM.Updateable(tmpU).ExecuteCommandAsync();

                if (count0 == count)
                {
                    Console.WriteLine("modify success");
                    Console.WriteLine("-------------------------------------------------------------------------------------------");
                    return Ok(new{ ok = "yes" });
                }
                else
                {
                    Console.WriteLine("modify fail");
                    return Ok(new{ ok = "no" });
                }
                //return Ok(new CustomResponse { ok = "yes", value = response });
            }
            catch (Exception ex)
            {
                Console.WriteLine("errorResponse: " + ex.Message);
                return Ok(new CustomResponse { ok = "no", value = "数据库连接错误" });//用户账户或密码错误
            }
        }
        public class utfJson
        {
            public String teamname { get; set; }
        }
        [HttpPost]
        public async Task<IActionResult> modifyteam([FromBody] utfJson json)
        {
            try
            {
                ORACLEconn ORACLEConnectTry = new ORACLEconn();
                if (!ORACLEConnectTry.getConn())
                {
                    Console.WriteLine("数据库连接失败");
                    return BadRequest("数据库连接失败");
                };
                SqlSugarClient sqlORM = ORACLEConnectTry.sqlORM;
                Console.WriteLine("Get modifyteam");
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
                Console.WriteLine("用户信息正确");

                int user_id = tempUsr.FirstOrDefault().user_id;
                //查找队伍id
                int team_id;
                List<Team> tmpteam = new List<Team>();
                tmpteam = await sqlORM.Queryable<Team>().Where(it => it.teamName == json.teamname)
                    .ToListAsync();
                if(tmpteam.Count() == 0)
                {
                    return Ok(new CustomResponse { ok = "no", value = "查无此队" });//查无此队
                }
                else
                {
                    team_id = tmpteam.FirstOrDefault().team_id;
                }
                List<UserFavouriteTeam> tmpUFT = new List<UserFavouriteTeam>();
                tmpUFT = await sqlORM.Queryable<UserFavouriteTeam>().Where(it => it.user_id == user_id)
                    .ToListAsync();
                int count;
                if(tmpUFT.Count()==0){
                    //新建关系
                    UserFavouriteTeam tmp = new UserFavouriteTeam();
                    tmp.user_id = user_id;
                    tmp.team_id = team_id;
                    count = await sqlORM.Insertable(tmp).ExecuteCommandAsync();
                }
                else
                {
                    //修改关系
                    tmpUFT.FirstOrDefault().team_id = team_id;
                    // 更新数据库中的关系
                    count = await sqlORM.Updateable(tmpUFT.FirstOrDefault()).ExecuteCommandAsync();
                }
                if (count > 0)
                {
                    return Ok(new CustomResponse { ok = "ok", value = "主队修改成功" });
                }
                else
                {
                    return Ok(new CustomResponse { ok = "no", value = "主队修改失败" });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("errorResponse: " + ex.Message);
                return Ok(new CustomResponse { ok = "no", value = "数据库连接错误" });//用户账户或密码错误
            }
        }
        public class ActionJson
        {
           //public List<action> actions { get; set; } = new List<action>();
            public action[] actions { get; set; }
            public class action
            {
                public String type { get; set; }
                public String title { get; set; }
                public String contains { get; set; }
                public String name { get; set; }
                public String author { get; set; }
                public int post_id { get; set; }
                public DateTime datetime { get; set; }
                public String comment { get; set; }
                public String follower_name { get; set; }
            }
        }
        [HttpPost]
        public async Task<IActionResult> userAction()
        {
            try
            {
                ORACLEconn ORACLEConnectTry = new ORACLEconn();
                if (!ORACLEConnectTry.getConn())
                {
                    Console.WriteLine("数据库连接失败");
                    return BadRequest("数据库连接失败");
                };
                SqlSugarClient sqlORM = ORACLEConnectTry.sqlORM;
                Console.WriteLine("Get userAction");
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
                int user_id = tempUsr.FirstOrDefault().user_id;
                Console.WriteLine("用户信息正确");

                // 创建 ActionJson 实例
                ActionJson actionJson = new ActionJson();
                List<ActionJson.action> actionsList = new List<ActionJson.action>();
                //赞同帖子
                List<ApprovePost> tmpAP = new List<ApprovePost>();
                tmpAP = await sqlORM.Queryable<ApprovePost>().Where(it => it.user_id == user_id)
                    .ToListAsync();
                foreach (var ap in tmpAP)
                {
                    List<Posts> tmpp = new List<Posts>();
                    tmpp = await sqlORM.Queryable<Posts>().Where(it => it.post_id == ap.post_id)
                        .ToListAsync();
                    List<PublishPost> tmppp = new List<PublishPost>();
                    tmppp = await sqlORM.Queryable<PublishPost>().Where(it => it.post_id == ap.post_id)
                        .ToListAsync();
                    List<Usr> tmpusr = new List<Usr>();
                    tmpusr = await sqlORM.Queryable<Usr>().Where(it => it.user_id == tmppp.FirstOrDefault().user_id)
                        .ToListAsync();
                    ActionJson.action tmpac = new ActionJson.action();
                    tmpac.title = tmpp.FirstOrDefault().title;
                    tmpac.contains = tmpp.FirstOrDefault().contains;
                    tmpac.datetime = ap.createDateTime;
                    tmpac.type = "like";
                    tmpac.author = tmpusr.FirstOrDefault().userName;
                    tmpac.post_id = ap.post_id;

                    actionsList.Add(tmpac);
                }
                //收藏帖子
                List<FollowPost> tmpFP = new List<FollowPost>();
                tmpFP = await sqlORM.Queryable<FollowPost>().Where(it => it.user_id == user_id)
                    .ToListAsync();
                foreach (var ap in tmpFP)
                {
                    List<Posts> tmpp = new List<Posts>();
                    tmpp = await sqlORM.Queryable<Posts>().Where(it => it.post_id == ap.post_id)
                        .ToListAsync();
                    List<PublishPost> tmppp = new List<PublishPost>();
                    tmppp = await sqlORM.Queryable<PublishPost>().Where(it => it.post_id == ap.post_id)
                        .ToListAsync();
                    List<Usr> tmpusr = new List<Usr>();
                    tmpusr = await sqlORM.Queryable<Usr>().Where(it => it.user_id == tmppp.FirstOrDefault().user_id)
                        .ToListAsync();
                    ActionJson.action tmpac = new ActionJson.action();
                    tmpac.title = tmpp.FirstOrDefault().title;
                    tmpac.contains = tmpp.FirstOrDefault().contains;
                    tmpac.datetime = ap.createDateTime;
                    tmpac.type = "collect";
                    tmpac.author = tmpusr.FirstOrDefault().userName;
                    tmpac.post_id = ap.post_id;

                    actionsList.Add(tmpac);
                }
                //评论帖子
                List<Comments> tmpCP = new List<Comments>();
                tmpCP = await sqlORM.Queryable<Comments>().Where(it => it.user_id == user_id)
                    .ToListAsync();
                foreach (var ap in tmpCP)
                {
                    List<Posts> tmpp = new List<Posts>();
                    tmpp = await sqlORM.Queryable<Posts>().Where(it => it.post_id == ap.post_id)
                        .ToListAsync();
                    List<PublishPost> tmppp = new List<PublishPost>();
                    tmppp = await sqlORM.Queryable<PublishPost>().Where(it => it.post_id == ap.post_id)
                        .ToListAsync();
                    List<Usr> tmpusr = new List<Usr>();
                    tmpusr = await sqlORM.Queryable<Usr>().Where(it => it.user_id == tmppp.FirstOrDefault().user_id)
                        .ToListAsync();
                    ActionJson.action tmpac = new ActionJson.action();
                    tmpac.title = tmpp.FirstOrDefault().title;
                    tmpac.contains = tmpp.FirstOrDefault().contains;
                    tmpac.datetime = ap.publishDateTime;
                    tmpac.type = "comment";
                    tmpac.author = tmpusr.FirstOrDefault().userName;
                    tmpac.post_id = ap.post_id;
                    tmpac.comment = ap.contains;
                    actionsList.Add(tmpac);
                }
                //关注用户
                List<Follow> tmpFU = new List<Follow>();
                tmpFU = await sqlORM.Queryable<Follow>().Where(it => it.follower_id == user_id)
                    .ToListAsync();
                foreach (var ap in tmpFU)
                {
                    List<Usr> tmpusr = new List<Usr>();
                    tmpusr = await sqlORM.Queryable<Usr>().Where(it => it.user_id == ap.follow_id)
                        .ToListAsync();
                    ActionJson.action tmpac = new ActionJson.action();
                    tmpac.datetime = ap.createDateTime;
                    tmpac.type = "follow";
                    tmpac.name = tmpusr.FirstOrDefault().userName;
                    actionsList.Add(tmpac);
                }
                // 对 actionJson.actions 数组按照 datetime 降序排序
                actionsList = actionsList.OrderByDescending(a => a.datetime).ToList();
                actionJson.actions = actionsList.ToArray();
                Console.WriteLine("count:"+ actionJson.actions.Count());
                Console.WriteLine("get userAction success");
                Console.WriteLine("-------------------------------------------------------------------------------------------");
                return Ok(actionJson.actions);
            }
            catch (Exception ex)
            {
                Console.WriteLine("DB error：" + ex.Message);
                return BadRequest(new { error = "DB error" });
            }
        }
        [HttpPost]
        public async Task<IActionResult> userPoint()
        {
            try
            {
                Console.WriteLine("Get userPoint");
                ORACLEconn ORACLEConnectTry = new ORACLEconn();
                if (!ORACLEConnectTry.getConn())
                {
                    Console.WriteLine("数据库连接失败");
                    return BadRequest("数据库连接失败");
                };
                SqlSugarClient sqlORM = ORACLEConnectTry.sqlORM;
                Console.WriteLine("Get userPoint");
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
                int user_id = tempUsr.FirstOrDefault().user_id;
                Console.WriteLine("Point:"+tempUsr.FirstOrDefault().userPoint);
                return Ok(new { userpoint = tempUsr.FirstOrDefault().userPoint });
            }
            catch (Exception ex)
            {
                Console.WriteLine("DB error：" + ex.Message);
                return BadRequest(new { error = "DB error" });
            }
        }
        public class PointJson
        {
            public List<point> points { get; set; } = new List<point>();
            public class point
            {
                public String type { get; set; }
                public DateTime datetime { get; set; }
            }
        }
        [HttpPost]
        public async Task<IActionResult> userPointAction()
        {
            try
            {
                Console.WriteLine("Get userPointAction0");
                ORACLEconn ORACLEConnectTry = new ORACLEconn();
                if (!ORACLEConnectTry.getConn())
                {
                    Console.WriteLine("数据库连接失败");
                    return BadRequest("数据库连接失败");
                };
                SqlSugarClient sqlORM = ORACLEConnectTry.sqlORM;
                Console.WriteLine("Get userAction");
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
                int user_id = tempUsr.FirstOrDefault().user_id;
                Console.WriteLine("用户信息正确");

                // 创建 ActionJson 实例
                PointJson pointJson = new PointJson();

                //赞同帖子
                List<ApprovePost> tmpAP = new List<ApprovePost>();
                tmpAP = await sqlORM.Queryable<ApprovePost>().Where(it => it.user_id == user_id)
                    .ToListAsync();
                foreach (var ap in tmpAP)
                {
                    PointJson.point tmpac = new PointJson.point();
                    tmpac.datetime = ap.createDateTime;
                    tmpac.type = "like";
                    pointJson.points.Add(tmpac);
                }
                //收藏帖子
                List<FollowPost> tmpFP = new List<FollowPost>();
                tmpFP = await sqlORM.Queryable<FollowPost>().Where(it => it.user_id == user_id)
                    .ToListAsync();
                foreach (var ap in tmpFP)
                {

                    PointJson.point tmpac = new PointJson.point();
                    tmpac.datetime = ap.createDateTime;
                    tmpac.type = "collect";
                    pointJson.points.Add(tmpac);
                }
                //评论帖子
                List<Comments> tmpCP = new List<Comments>();
                tmpCP = await sqlORM.Queryable<Comments>().Where(it => it.user_id == user_id)
                    .ToListAsync();
                foreach (var ap in tmpCP)
                {
                    PointJson.point tmpac = new PointJson.point();
                    tmpac.datetime = ap.publishDateTime;
                    tmpac.type = "comment";
                    pointJson.points.Add(tmpac);
                }
                //关注用户
                List<Follow> tmpFU = new List<Follow>();
                tmpFU = await sqlORM.Queryable<Follow>().Where(it => it.follower_id == user_id)
                    .ToListAsync();
                foreach (var ap in tmpFU)
                {
                    PointJson.point tmpac = new PointJson.point();
                    tmpac.datetime = ap.createDateTime;
                    tmpac.type = "follow";
                    pointJson.points.Add(tmpac);
                }
                // 对 points 数组按照 datetime 降序排序
                pointJson.points = pointJson.points.OrderByDescending(a => a.datetime).ToList();
                List<String> response = new List<String>();
                
                // 将 pointJson.points 复制给 response
                foreach (var point in pointJson.points)
                {
                    response.Add(point.type);
                }
                String[] resString = response.ToArray();
                Console.WriteLine(resString);
                Console.WriteLine("-------------------------------------------------------------------------------------------");
                return Ok(resString);
            }
            catch (Exception ex)
            {
                Console.WriteLine("数据库错误：" + ex.Message);
                return BadRequest(new { error = "数据库错误" });
            }
        }
        [HttpPost]
        public async Task<IActionResult> UserCheckin()
        {
            try
            {
                Console.WriteLine("Get UserCheckin");
                ORACLEconn ORACLEConnectTry = new ORACLEconn();
                if (!ORACLEConnectTry.getConn())
                {
                    Console.WriteLine("数据库连接失败");
                    return BadRequest("数据库连接失败");
                };
                SqlSugarClient sqlORM = ORACLEConnectTry.sqlORM;
                Console.WriteLine("Get userAction");
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
                int user_id = tempUsr.FirstOrDefault().user_id;
                Console.WriteLine("用户信息正确");

                DateTime dateTime = DateTime.Now;
                Checkins checkins = new Checkins();
                checkins.user_id = user_id;
                checkins.sign_in_date = dateTime;
                int count = await sqlORM.Insertable(checkins).ExecuteCommandAsync();
                if(count > 0)
                {
                    Console.WriteLine("checkin success");
                    Console.WriteLine("userid："+user_id+"  checkinTime："+dateTime);
                    Console.WriteLine("-------------------------------------------------------------------------------------------");
                    return Ok(new CustomResponse { ok = "yes", value = "success" });
                }
                else
                {
                    Console.WriteLine("checkin fail");
                    return Ok(new CustomResponse { ok = "no", value = "fail" });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("数据库错误：" + ex.Message);
                return BadRequest(new { error = "数据库错误" });
            }
        }
        [HttpPost]
        public async Task<IActionResult> UserCheckTime()
        {
            try
            {
                Console.WriteLine("Get UserCheckTime");
                ORACLEconn ORACLEConnectTry = new ORACLEconn();
                if (!ORACLEConnectTry.getConn())
                {
                    Console.WriteLine("数据库连接失败");
                    return BadRequest("数据库连接失败");
                };
                SqlSugarClient sqlORM = ORACLEConnectTry.sqlORM;
                Console.WriteLine("Get userAction");
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
                int user_id = tempUsr.FirstOrDefault().user_id;
                Console.WriteLine("用户信息正确");

                List<DateTime> times = new List<DateTime>();
                List<Checkins> checkins = new List<Checkins>();
                checkins = await sqlORM.Queryable<Checkins>().Where(it => it.user_id == user_id)
                    .ToListAsync();
                foreach (var check in checkins)
                {
                    times.Add(check.sign_in_date);
                }
                DateTime[] timesArray = times.ToArray();
                Console.WriteLine("CheckinTimeCount:" + timesArray.Count());
                Console.WriteLine("-------------------------------------------------------------------------------------------");
                return Ok(timesArray);
            }
            catch (Exception ex)
            {
                Console.WriteLine("数据库错误：" + ex.Message);
                return BadRequest(new { error = "数据库错误" });
            }
        }
        private class Followed
        {
            public int user_id { get; set; }
            public String userName { get; set; }
        }
        //关注人列表
        [HttpPost]
        public async Task<IActionResult> following()
        {
            try
            {
                ORACLEconn ORACLEConnectTry = new ORACLEconn();
                if (!ORACLEConnectTry.getConn())
                {
                    Console.WriteLine("数据库连接失败");
                    return BadRequest("数据库连接失败");
                };
                SqlSugarClient sqlORM = ORACLEConnectTry.sqlORM;
                Console.WriteLine("Get following");
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
                int user_id = tempUsr.FirstOrDefault().user_id;
                Console.WriteLine("用户信息正确");
                //
                
                List<Followed> followed = new List<Followed>();
                
                List<Follow> tmpF = new List<Follow>();
                tmpF = await sqlORM.Queryable<Follow>().Where(it => it.follower_id == user_id)
                    .ToListAsync();
                foreach (var f in tmpF)
                {
                    Usr user = await sqlORM.Queryable<Usr>().Where(it => it.user_id == f.follow_id)
                        .FirstAsync();
                    Followed t = new Followed();
                    t.user_id = user.user_id;
                    t.userName =user.userName;
                    if (user != null) { followed.Add(t); };
                }
                Followed[] followArr = followed.ToArray();
                return Ok(followArr);
            }
            catch (Exception ex)
            {
                Console.WriteLine("数据库错误：" + ex.Message);
                return BadRequest(new { error = "数据库错误" });
            }
        }
        //粉丝人列表
        [HttpPost]
        public async Task<IActionResult> fansList()
        {
            try
            {
                ORACLEconn ORACLEConnectTry = new ORACLEconn();
                if (!ORACLEConnectTry.getConn())
                {
                    Console.WriteLine("数据库连接失败");
                    return BadRequest("数据库连接失败");
                };
                SqlSugarClient sqlORM = ORACLEConnectTry.sqlORM;
                Console.WriteLine("Get fansList");
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
                int user_id = tempUsr.FirstOrDefault().user_id;
                Console.WriteLine("用户信息正确");
                //

                List<Followed> followed = new List<Followed>();
                List<Follow> tmpF = new List<Follow>();
                tmpF = await sqlORM.Queryable<Follow>().Where(it => it.follow_id == user_id)
                    .ToListAsync();
                foreach (var f in tmpF)
                {
                    Usr user = await sqlORM.Queryable<Usr>().Where(it => it.user_id == f.follower_id)
                        .FirstAsync();
                    Followed t = new Followed();
                    t.user_id = user.user_id;
                    t.userName = user.userName;
                    if (user != null) { followed.Add(t); };
                }
                Followed[] followArr = followed.ToArray();
                return Ok(followArr);;
            }
            catch (Exception ex)
            {
                Console.WriteLine("数据库错误：" + ex.Message);
                return BadRequest(new { error = "数据库错误" });
            }
        }
    }
}