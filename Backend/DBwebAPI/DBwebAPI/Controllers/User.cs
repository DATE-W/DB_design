using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using DBwebAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using static DBwebAPI.Controllers.LoginController;
using static DBwebAPI.Controllers.ForumController;
using static DBwebAPI.Controllers.UserController;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System;
using Dm;
using System.Reflection.Metadata;

namespace DBwebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        public class profileJson {
            public String username { get; set; }
            public String uft { get; set; }
            public int follower_num { get; set; }
            public int follow_num { get; set; }
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
                    Console.WriteLine("���ݿ�����ʧ��");
                    return BadRequest("���ݿ�����ʧ��");
                };
                SqlSugarClient sqlORM = ORACLEConnectTry.sqlORM;
                Console.WriteLine("--------------------------Get profile--------------------------");
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

                int user_id = tempUsr.FirstOrDefault().user_id;
                //�û���
                String userName = tempUsr.FirstOrDefault().userName;
                Console.WriteLine("�û���Ϣ��ȷ");
                //��ע��
                List<Follow> tmpFollowers = new List<Follow>();
                tmpFollowers = await sqlORM.Queryable<Follow>().Where(it => it.follower_id == user_id)
                    .ToListAsync();
                int follower_num = tmpFollowers.Count();
                //��˿��
                List<Follow> tmpFollows = new List<Follow>();
                tmpFollows = await sqlORM.Queryable<Follow>().Where(it => it.follow_id == user_id)
                    .ToListAsync();
                int follow_num = tmpFollows.Count();
                //����
                List<UserFavouriteTeam> tmpUFT_id = new List<UserFavouriteTeam>();
                tmpUFT_id = await sqlORM.Queryable<UserFavouriteTeam>().Where(it => it.user_id == user_id)
                    .ToListAsync();
                int UFT_id = tmpUFT_id.Count() != 0 ? tmpUFT_id.FirstOrDefault().team_id : 0;
                List<Team> tmpUFT = new List<Team>();
                tmpUFT = await sqlORM.Queryable<Team>().Where(it => it.team_id == UFT_id)
                    .ToListAsync();
                String UFT = tmpUFT.Count() != 0 ? tmpUFT.FirstOrDefault().teamName : "���޴˶�";
                //������
                List<PublishPost> tmpPP = new List<PublishPost>();
                tmpPP = await sqlORM.Queryable<PublishPost>().Where(it => it.user_id == user_id)
                    .ToListAsync();
                int approvalNum = 0;
                foreach (var pp in tmpPP)
                {
                    List<Posts> tmpPost = new List<Posts>();
                    tmpPost = await sqlORM.Queryable<Posts>().Where(it => it.post_id == pp.post_id)
                        .ToListAsync();
                    approvalNum += tmpPost.Count() != 0 ? tmpPost.FirstOrDefault().approvalNum : 0;
                }
                profileJson response = new profileJson();
                response.username = userName;
                response.uft = UFT;
                response.approval_num = approvalNum;
                response.follower_num = follower_num;
                response.follow_num = follow_num;
                response.account = account;
                return Ok(new CustomResponse { ok = "yes", value = response });
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred��" + ex.Message);
                return BadRequest(new { error = "An error occurred." });
            }
        }
        [HttpPost]
        public async Task<IActionResult> modifyprofile([FromBody] profileJson json)
        {
            try
            {
                ORACLEconn ORACLEConnectTry = new ORACLEconn();
                if (!ORACLEConnectTry.getConn())
                {
                    Console.WriteLine("���ݿ�����ʧ��");
                    return BadRequest("���ݿ�����ʧ��");
                };
                SqlSugarClient sqlORM = ORACLEConnectTry.sqlORM;
                Console.WriteLine("--------------------------Get modifyprofile--------------------------");
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

                Usr tmpU = tempUsr.FirstOrDefault();
                int count = 0, count0 = 0;
                int user_id = tempUsr.FirstOrDefault().user_id;
                if (json.username != null)
                {
                    //�û���
                    tmpU.userName = json.username;
                }
                count0 += 1;
                count += await sqlORM.Updateable(tmpU).ExecuteCommandAsync();
                //����
                Team tmpTeam;
                UserFavouriteTeam tmpUFT;
                if (json.uft != null)
                {
                    tmpTeam = await sqlORM.Queryable<Team>()
                                .Where(it => it.teamName == json.uft)
                                .FirstAsync();
                    tmpUFT = await sqlORM.Queryable<UserFavouriteTeam>()
                                .Where(it => it.user_id == user_id)
                                .FirstAsync();
                    if (tmpUFT != null)
                    {
                        tmpUFT.team_id = tmpTeam.team_id;
                        count0 += 1;
                        count += await sqlORM.Updateable(tmpUFT).ExecuteCommandAsync();
                    }
                }
                if (count0 == count)
                {
                    Console.WriteLine("�޸ĳɹ�");
                    return Ok(new { ok = "yes" });
                }
                else
                {
                    Console.WriteLine("�޸�ʧ��");
                    return Ok(new { ok = "no" });
                }
                //return Ok(new CustomResponse { ok = "yes", value = response });
            }
            catch (Exception ex)
            {
                Console.WriteLine("errorResponse: " + ex.Message);
                return Ok(new CustomResponse { ok = "no", value = "���ݿ����Ӵ���" });//�û��˻����������
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
                    Console.WriteLine("���ݿ�����ʧ��");
                    return BadRequest("���ݿ�����ʧ��");
                };
                SqlSugarClient sqlORM = ORACLEConnectTry.sqlORM;
                Console.WriteLine("--------------------------Get modifyteam--------------------------");
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

                int user_id = tempUsr.FirstOrDefault().user_id;
                //���Ҷ���id
                int team_id;
                List<Team> tmpteam = new List<Team>();
                tmpteam = await sqlORM.Queryable<Team>().Where(it => it.teamName == json.teamname)
                    .ToListAsync();
                if (tmpteam.Count() == 0)
                {
                    return Ok(new CustomResponse { ok = "no", value = "���޴˶�" });//���޴˶�
                }
                else
                {
                    team_id = tmpteam.FirstOrDefault().team_id;
                }
                List<UserFavouriteTeam> tmpUFT = new List<UserFavouriteTeam>();
                tmpUFT = await sqlORM.Queryable<UserFavouriteTeam>().Where(it => it.user_id == user_id)
                    .ToListAsync();
                int count;
                if (tmpUFT.Count() == 0) {
                    //�½���ϵ
                    UserFavouriteTeam tmp = new UserFavouriteTeam();
                    tmp.user_id = user_id;
                    tmp.team_id = team_id;
                    count = await sqlORM.Insertable(tmp).ExecuteCommandAsync();
                }
                else
                {
                    //�޸Ĺ�ϵ
                    tmpUFT.FirstOrDefault().team_id = team_id;
                    // �������ݿ��еĹ�ϵ
                    count = await sqlORM.Updateable(tmpUFT.FirstOrDefault()).ExecuteCommandAsync();
                }
                if (count > 0)
                {
                    return Ok(new CustomResponse { ok = "ok", value = "�����޸ĳɹ�" });
                }
                else
                {
                    return Ok(new CustomResponse { ok = "no", value = "�����޸�ʧ��" });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("errorResponse: " + ex.Message);
                return Ok(new CustomResponse { ok = "no", value = "���ݿ����Ӵ���" });//�û��˻����������
            }
        }
        public class ActionJson
        {
            public List<action> actions { get; set; } = new List<action>();
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
                    Console.WriteLine("���ݿ�����ʧ��");
                    return BadRequest("���ݿ�����ʧ��");
                };
                SqlSugarClient sqlORM = ORACLEConnectTry.sqlORM;
                Console.WriteLine("--------------------------Get userAction--------------------------");
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
                int user_id = tempUsr.FirstOrDefault().user_id;
                Console.WriteLine("�û���Ϣ��ȷ");

                // ���� ActionJson ʵ��
                ActionJson actionJson = new ActionJson();

                //��������
                List<PublishPost> tmpPP = new List<PublishPost>();
                tmpPP = await sqlORM.Queryable<PublishPost>().Where(it => it.user_id == user_id)
                    .ToListAsync();
                foreach (var ap in tmpPP)
                {
                    List<Posts> tmpp = new List<Posts>();
                    tmpp = await sqlORM.Queryable<Posts>().Where(it => it.post_id == ap.post_id)
                        .ToListAsync();
                    List<Usr> tmpusr = new List<Usr>();
                    tmpusr = await sqlORM.Queryable<Usr>().Where(it => it.user_id == ap.user_id)
                        .ToListAsync();
                    ActionJson.action tmpac = new ActionJson.action();
                    tmpac.title = tmpp.FirstOrDefault().title;
                    tmpac.contains = tmpp.FirstOrDefault().contains;
                    tmpac.datetime = tmpp.FirstOrDefault().publishDateTime;
                    tmpac.type = "publish";
                    tmpac.author = tmpusr.FirstOrDefault().userName;
                    tmpac.post_id = ap.post_id;

                    actionJson.actions.Add(tmpac);
                }
                //��ͬ����
                List<LikePost> tmpAP = new List<LikePost>();
                tmpAP = await sqlORM.Queryable<LikePost>().Where(it => it.user_id == user_id)
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

                    actionJson.actions.Add(tmpac);
                }
                //�ղ�����
                List<CollectPost> tmpFP = new List<CollectPost>();
                tmpFP = await sqlORM.Queryable<CollectPost>().Where(it => it.user_id == user_id)
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

                    actionJson.actions.Add(tmpac);
                }
                //��������
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
                    actionJson.actions.Add(tmpac);
                }
                //��ע�û�
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
                    actionJson.actions.Add(tmpac);
                }
                // �� actionJson.actions ���鰴�� datetime ��������
                actionJson.actions = actionJson.actions.OrderByDescending(a => a.datetime).ToList();
                return Ok(actionJson);
            }
            catch (Exception ex)
            {
                Console.WriteLine("DB error��" + ex.Message);
                return BadRequest(new { error = "DB error" });
            }
        }
        [HttpPost]
        public async Task<IActionResult> userPoint()
        {
            try
            {
                ORACLEconn ORACLEConnectTry = new ORACLEconn();
                if (!ORACLEConnectTry.getConn())
                {
                    Console.WriteLine("���ݿ�����ʧ��");
                    return BadRequest("���ݿ�����ʧ��");
                };
                SqlSugarClient sqlORM = ORACLEConnectTry.sqlORM;
                Console.WriteLine("--------------------------Get userPoint--------------------------");
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
                int user_id = tempUsr.FirstOrDefault().user_id;
                Console.WriteLine("Point:" + tempUsr.FirstOrDefault().userPoint);
                return Ok(new { userpoint = tempUsr.FirstOrDefault().userPoint });
            }
            catch (Exception ex)
            {
                Console.WriteLine("DB error��" + ex.Message);
                return BadRequest(new { error = "DB error" });
            }
        }
        public class PointJson
        {
            public List<point> points { get; set; } = new List<point>();
            public class point
            {
                public String type;
                public DateTime datetime;
            }
        }
        [HttpPost]
        public async Task<IActionResult> userPointAction()
        {
            try
            {
                ORACLEconn ORACLEConnectTry = new ORACLEconn();
                if (!ORACLEConnectTry.getConn())
                {
                    Console.WriteLine("���ݿ�����ʧ��");
                    return BadRequest("���ݿ�����ʧ��");
                };
                SqlSugarClient sqlORM = ORACLEConnectTry.sqlORM;
                Console.WriteLine("--------------------------Get userAction--------------------------");
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
                int user_id = tempUsr.FirstOrDefault().user_id;
                Console.WriteLine("�û���Ϣ��ȷ");

                // ���� ActionJson ʵ��
                PointJson pointJson = new PointJson();


                //��������
                List<PublishPost> tmpPP = new List<PublishPost>();
                tmpPP = await sqlORM.Queryable<PublishPost>().Where(it => it.user_id == user_id)
                    .ToListAsync();
                foreach (var ap in tmpPP)
                {
                    List<Posts> tmpp = new List<Posts>();
                    tmpp = await sqlORM.Queryable<Posts>().Where(it => it.post_id == ap.post_id)
                        .ToListAsync();
                    PointJson.point tmpac = new PointJson.point();
                    tmpac.datetime = tmpp.FirstOrDefault().publishDateTime;
                    tmpac.type = "publish";
                    pointJson.points.Add(tmpac);
                }
                //��ͬ����
                List<LikePost> tmpAP = new List<LikePost>();
                tmpAP = await sqlORM.Queryable<LikePost>().Where(it => it.user_id == user_id)
                    .ToListAsync();
                foreach (var ap in tmpAP)
                {
                    PointJson.point tmpac = new PointJson.point();
                    tmpac.datetime = ap.createDateTime;
                    tmpac.type = "like";
                    pointJson.points.Add(tmpac);
                }
                //�ղ�����
                List<CollectPost> tmpFP = new List<CollectPost>();
                tmpFP = await sqlORM.Queryable<CollectPost>().Where(it => it.user_id == user_id)
                    .ToListAsync();
                foreach (var ap in tmpFP)
                {

                    PointJson.point tmpac = new PointJson.point();
                    tmpac.datetime = ap.createDateTime;
                    tmpac.type = "collect";
                    pointJson.points.Add(tmpac);
                }
                //��������
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
                //��ע�û�
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
                // �� points ���鰴�� datetime ��������
                pointJson.points = pointJson.points.OrderByDescending(a => a.datetime).ToList();
                List<String> response = new List<String>();
                // �� pointJson.points ���Ƹ� response
                foreach (var point in pointJson.points)
                {
                    response.Add(point.type);
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine("���ݿ����" + ex.Message);
                return BadRequest(new { error = "���ݿ����" });
            }
        }
        public class NoticeJson 
        {
            public DateTime[] notice_dates { get; set; }
            public String[] notice_people { get; set; }
            public String[] notice_contents { get; set; }
        }
        public class NoticeClass
        {
            public  DateTime dateTime { get; set; }
            public String people { get; set; }
            public String content { get; set; }
        }
        //֪ͨ
        [HttpPost]
        public async Task<IActionResult> Notice()
        {
            try
            {
                ORACLEconn ORACLEConnectTry = new ORACLEconn();
                if (!ORACLEConnectTry.getConn())
                {
                    Console.WriteLine("���ݿ�����ʧ��");
                    return BadRequest("���ݿ�����ʧ��");
                };
                SqlSugarClient sqlORM = ORACLEConnectTry.sqlORM;
                Console.WriteLine("--------------------------Get userAction--------------------------");
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
                int user_id = tempUsr.FirstOrDefault().user_id;
                Console.WriteLine("�û���Ϣ��ȷ");

                // ���� NoticeClass ʵ��
                List<NoticeClass> noticeList = new List<NoticeClass>();

                //��õ���
                List<Posts> posts = await sqlORM.Queryable<Posts, PublishPost>((p, pp) => p.post_id == pp.post_id)
                    .Where((p, pp) => pp.user_id == user_id)
                    .Select((p, pp) => p)
                    .ToListAsync();
                List<LikePost> approvePosts = await sqlORM.Queryable<LikePost, Posts>((ap, p) => ap.post_id == p.post_id)
                    .Where(ap => posts.Select(post => post.post_id).Contains(ap.post_id))
                    .Select((ap, p) => ap)
                    .ToListAsync();
                foreach (var approvePost in approvePosts)
                {
                    Usr matchingUser = await sqlORM.Queryable<Usr>().SingleAsync(u => u.user_id == approvePost.user_id);

                    if (matchingUser != null)
                    {
                        NoticeClass notice = new NoticeClass
                        {
                            dateTime = approvePost.createDateTime,
                            people = matchingUser.userName,
                            content = "like"
                        };
                        noticeList.Add(notice);
                    }
                }

                //����ղ�
                List<Posts> posts2 = await sqlORM.Queryable<Posts, PublishPost>((p, pp) => p.post_id == pp.post_id)
                    .Where((p, pp) => pp.user_id == user_id)
                    .Select((p, pp) => p)
                    .ToListAsync();
                List<CollectPost> followPosts = await sqlORM.Queryable<CollectPost, Posts>((ap, p) => ap.post_id == p.post_id)
                    .Where(ap => posts2.Select(post => post.post_id).Contains(ap.post_id))
                    .Select((ap, p) => ap)
                    .ToListAsync();
                foreach (var a in followPosts)
                {
                    Usr matchingUser = await sqlORM.Queryable<Usr>().SingleAsync(u => u.user_id == a.user_id);

                    if (matchingUser != null)
                    {
                        NoticeClass notice = new NoticeClass
                        {
                            dateTime = a.createDateTime,
                            people = matchingUser.userName,
                            content = "collect"
                        };
                        noticeList.Add(notice);
                    }
                }
                //��������
                List<Posts> posts3 = await sqlORM.Queryable<Posts, PublishPost>((p, pp) => p.post_id == pp.post_id)
                    .Where((p, pp) => pp.user_id == user_id)
                    .Select((p, pp) => p)
                    .ToListAsync();
                List<Comments> commentPosts = await sqlORM.Queryable<Comments, Posts>((ap, p) => ap.post_id == p.post_id)
                    .Where(ap => posts3.Select(post => post.post_id).Contains(ap.post_id))
                    .Select((ap, p) => ap)
                    .ToListAsync();
                foreach (var a in commentPosts)
                {
                    Usr matchingUser = await sqlORM.Queryable<Usr>().SingleAsync(u => u.user_id == a.user_id);

                    if (matchingUser != null)
                    {
                        NoticeClass notice = new NoticeClass
                        {
                            dateTime = a.publishDateTime,
                            people = matchingUser.userName,
                            content = "comment"
                        };
                        noticeList.Add(notice);
                    }
                }

                // �� points ���鰴�� datetime ��������
                noticeList = noticeList.OrderByDescending(a => a.dateTime).ToList();

                NoticeJson response = new NoticeJson();
                List<DateTime> timeList = new List<DateTime>();
                List<String> peopleList = new List<string>();
                List<String> contentList = new List<string>();
                foreach (var notice in noticeList)
                {
                    timeList.Add(notice.dateTime);
                    peopleList.Add(notice.people);
                    contentList.Add(notice.content);
                }
                response.notice_people = peopleList.ToArray();
                response.notice_dates = timeList.ToArray();
                response.notice_contents = contentList.ToArray();
                return Ok(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine("���ݿ����" + ex.Message);
                return BadRequest(new { error = "���ݿ����" });
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
                    Console.WriteLine("���ݿ�����ʧ��");
                    return BadRequest("���ݿ�����ʧ��");
                };
                SqlSugarClient sqlORM = ORACLEConnectTry.sqlORM;
                Console.WriteLine("--------------------------Get userAction--------------------------");
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
                int user_id = tempUsr.FirstOrDefault().user_id;
                Console.WriteLine("�û���Ϣ��ȷ");

                DateTime dateTime = DateTime.Now;
                Checkins checkins = new Checkins();
                checkins.user_id = user_id;
                checkins.sign_in_date = dateTime;
                int count = await sqlORM.Insertable(checkins).ExecuteCommandAsync();
                if(count > 0)
                {
                    Console.WriteLine("checkin success");
                    Console.WriteLine("userid:  "+user_id+"  checkinTime:   "+dateTime);
                    return Ok(new CustomResponse { ok = "yes", value = "ǩ���ɹ�" });
                }
                else
                {
                    Console.WriteLine("checkin fail");
                    return Ok(new CustomResponse { ok = "yes", value = "ǩ��ʧ��" });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("���ݿ����" + ex.Message);
                return BadRequest(new { error = "���ݿ����" });
            }
        }
        [HttpPost]
        public async Task<IActionResult> UserCheckTime()
        {
            try
            {
                Console.WriteLine("--------------------------Get UserCheckin--------------------------");
                ORACLEconn ORACLEConnectTry = new ORACLEconn();
                if (!ORACLEConnectTry.getConn())
                {
                    Console.WriteLine("���ݿ�����ʧ��");
                    return BadRequest("���ݿ�����ʧ��");
                };
                SqlSugarClient sqlORM = ORACLEConnectTry.sqlORM;
                Console.WriteLine("Get userAction");
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
                int user_id = tempUsr.FirstOrDefault().user_id;
                Console.WriteLine("�û���Ϣ��ȷ");

                List<DateTime> times = new List<DateTime>();
                List<Checkins> checkins = new List<Checkins>();
                checkins = await sqlORM.Queryable<Checkins>().Where(it => it.user_id == user_id)
                    .ToListAsync();
                foreach (var check in checkins)
                {
                    times.Add(check.sign_in_date);
                }
                DateTime[] timesArray = times.ToArray();
                return Ok(timesArray);
            }
            catch (Exception ex)
            {
                Console.WriteLine("���ݿ����" + ex.Message);
                return BadRequest(new { error = "���ݿ����" });
            }
        }
        private class Followed
        {
            public int user_id { get; set; }
            public String userName { get; set; }
        }
        //��ע���б�
        [HttpPost]
        public async Task<IActionResult> following()
        {
            try
            {
                ORACLEconn ORACLEConnectTry = new ORACLEconn();
                if (!ORACLEConnectTry.getConn())
                {
                    Console.WriteLine("���ݿ�����ʧ��");
                    return BadRequest("���ݿ�����ʧ��");
                };
                SqlSugarClient sqlORM = ORACLEConnectTry.sqlORM;
                Console.WriteLine("--------------------------Get following--------------------------");
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
                int user_id = tempUsr.FirstOrDefault().user_id;
                Console.WriteLine("�û���Ϣ��ȷ");
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
               
                return Ok(followed);
            }
            catch (Exception ex)
            {
                Console.WriteLine("���ݿ����" + ex.Message);
                return BadRequest(new { error = "���ݿ����" });
            }
        }
        //��˿���б�
        [HttpPost]
        public async Task<IActionResult> fansList()
        {
            try
            {
                ORACLEconn ORACLEConnectTry = new ORACLEconn();
                if (!ORACLEConnectTry.getConn())
                {
                    Console.WriteLine("���ݿ�����ʧ��");
                    return BadRequest("���ݿ�����ʧ��");
                };
                SqlSugarClient sqlORM = ORACLEConnectTry.sqlORM;
                Console.WriteLine("--------------------------Get fansList--------------------------");
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
                int user_id = tempUsr.FirstOrDefault().user_id;
                Console.WriteLine("�û���Ϣ��ȷ");
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

                return Ok(followed);
            }
            catch (Exception ex)
            {
                Console.WriteLine("���ݿ����" + ex.Message);
                return BadRequest(new { error = "���ݿ����" });
            }
        }
    }
}