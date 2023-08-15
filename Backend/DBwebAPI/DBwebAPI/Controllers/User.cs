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
        }
        [HttpGet]
        public async Task<IActionResult> profile()
        {
            try
            {
                ORACLEconn ORACLEConnectTry = new ORACLEconn();
                ORACLEConnectTry.getConn();
                SqlSugarClient sqlORM = ORACLEConnectTry.sqlORM;
                Console.WriteLine("Get profile");
                // ������ͷ�л�ȡ���ݵ�JWT����
                string authorizationHeader = Request.Headers["Authorization"].FirstOrDefault();
                //��֤ Authorization ����ͷ�Ƿ���� JWT ����
                if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer"))
                {
                    Console.WriteLine("δ�ṩ��Ч��JWT");
                    return BadRequest(new { ok = "no",value="δ�ṩ��Ч��JWT" });
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
                
                int user_id=tempUsr.FirstOrDefault().user_id;
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
                int UFT_id =tmpUFT_id.Count()!=0? tmpUFT_id.FirstOrDefault().team_id:0;
                List<Team> tmpUFT = new List<Team>();
                tmpUFT = await sqlORM.Queryable<Team>().Where(it => it.team_id == UFT_id)
                    .ToListAsync();
                String UFT = tmpUFT.Count() != 0 ? tmpUFT.FirstOrDefault().teamName : "���޴˶�";
                //������
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
                profileJson reponse=new profileJson();
                reponse.username = userName;
                reponse.uft = UFT;
                reponse.approval_num = approvalNum;
                reponse.follower_num = follower_num;
                reponse.follow_num = follow_num;
                return Ok(new CustomResponse { ok = "yes", value = reponse });
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while retrieving the total count of posts��" + ex.Message);
                return BadRequest(new { error = "An error occurred while retrieving the total count of posts." });
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
                ORACLEConnectTry.getConn();
                SqlSugarClient sqlORM = ORACLEConnectTry.sqlORM;
                Console.WriteLine("Get modifyteam");
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
                if(tmpteam.Count() == 0)
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
                if(tmpUFT.Count()==0){
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
                public String type;
                public String title;
                public String contains;
                public String name;
                public String author;
                public int post_id;
                public DateTime datetime;
                public String comment;
                public String follower_name;
            }
        }
        [HttpPost]
        public async Task<IActionResult> userAction()
        {
            try
            {
                Console.WriteLine("Get userAction0");
                ORACLEconn ORACLEConnectTry = new ORACLEconn();
                ORACLEConnectTry.getConn();
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

                // ���� ActionJson ʵ��
                ActionJson actionJson = new ActionJson();

                //��ͬ����
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

                    actionJson.actions.Add(tmpac);
                }
                //�ղ�����
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
                Console.WriteLine("���ݿ����" + ex.Message);
                return BadRequest(new { error = "���ݿ����" });
            }
        }
        [HttpPost]
        public async Task<IActionResult> userPoint()
        {
            try
            {
                Console.WriteLine("Get userPoint1");
                ORACLEconn ORACLEConnectTry = new ORACLEconn();
                ORACLEConnectTry.getConn();
                SqlSugarClient sqlORM = ORACLEConnectTry.sqlORM;
                Console.WriteLine("Get userPoint");
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
                return Ok(new { userpoint = tempUsr.FirstOrDefault().userPoint });
            }
            catch (Exception ex)
            {
                Console.WriteLine("���ݿ����" + ex.Message);
                return BadRequest(new { error = "���ݿ����" });
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
                Console.WriteLine("Get userPointAction0");
                ORACLEconn ORACLEConnectTry = new ORACLEconn();
                ORACLEConnectTry.getConn();
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

                // ���� ActionJson ʵ��
                PointJson pointJson = new PointJson();

                //��ͬ����
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
                //�ղ�����
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
                // �� actionJson.actions ���鰴�� datetime ��������
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
    }
}