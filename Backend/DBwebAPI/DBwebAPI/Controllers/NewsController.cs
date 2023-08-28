using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using System.Linq.Expressions;
using DBwebAPI;
using DBwebAPI.Models;
using DBwebAPI.Relations;

namespace DBwebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        public class NewsWithPicture
        {
            public News newsBody { get; set; }
            public List<string>? pictureRoutes { get; set; }
        }
        public class GetNewsRequest
        {
            public int num { get; set; }
            public string matchTag { get; set; }
            public string propertyTag { get; set; }
        }
        public class SearchNewsRequest
        {
            public string keyword { get; set; }
        }
        [HttpGet] 
        public async Task<IActionResult> GetNewsNum()
        {
            Console.WriteLine("GET Login!");
            ORACLEconn ORACLEConnectTry = new ORACLEconn();
            int num;
            if (ORACLEConnectTry.getConn() == true)
            {
                try
                {
                    SqlSugarScope sqlORM = ORACLEConnectTry.sqlORM;
                    //List<News> news = await sqlORM.Queryable<News>().OrderBy(it => it.publishDateTime).ToListAsync();
                    num = await sqlORM.Queryable<News>().CountAsync();
                    return Ok(new CustomResponse { ok = "yes", value = num });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return Ok(new CustomResponse { ok = "no", value = -1 }); // Internal server error
                }
            }
            else { return Ok(new CustomResponse { ok = "no", value = -1 }); };
        }
        [HttpPost]
        public async Task<IActionResult> GetNews([FromBody] GetNewsRequest json)
        {
            Console.WriteLine("--------------------------GetNews--------------------------");
            ORACLEconn ORACLEConnectTry = new ORACLEconn();
            int num = json.num;
            string mtag = json.matchTag;
            string ptag = json.propertyTag;
            if(ORACLEConnectTry.getConn() == true)
            {
                try
                {
                    SqlSugarScope sqlORM = ORACLEConnectTry.sqlORM;
                    int sum = sqlORM.Queryable<Video>().Count();
                    if (num == -1 || num > sum)
                    {
                        num = sum;
                    }
                    //List<News> news = await sqlORM.Queryable<News>().OrderBy(it => it.publishDateTime).Take(num).ToListAsync();
                    //List<NewsWithPicture> newsWithPictures = new List<NewsWithPicture>();
                    //for(int i = 0; i < news.Count; i++)
                    //{
                    //    
                    //}
                    //var news = await sqlORM.Queryable<News>().LeftJoin<NewsHavePicture>((n, nhp)=> n.news_id == nhp.news_id).Select((n, nhp)=>new NewsWithPicture 
                    //{news_id = n.news_id, publishDateTime = n.publishDateTime, summary = n.summary, contains = n.contains, matchTag = n.matchTag, propertyTag = n.propertyTag})
                    //    .ToListAsync();
                    List<News> news = new List<News>();
                    List<NewsWithPicture> ret = new List<NewsWithPicture>();
                    if (mtag == "" && ptag == "")
                    {
                        news = await sqlORM.Queryable<News>().
                            OrderBy(n => n.publishDateTime, OrderByType.Desc).
                            Take(num).ToListAsync();
                        ret = new List<NewsWithPicture>();
                        Console.WriteLine(news.Count);

                        for (int i = 0; i < news.Count; i++)
                        {
                            int id = news[i].news_id;
                            List<string> pictureRoutes = await sqlORM.Queryable<NewsHavePicture>().
                                Where(n => n.news_id == id).
                                Select(nhp => nhp.pictureRoute).
                                ToListAsync();
                            ret.Add(new NewsWithPicture { newsBody = news[i], pictureRoutes = pictureRoutes });
                        }
                    }
                    else if(mtag != "" && ptag == "")
                    {
                        news = await sqlORM.Queryable<News>().Where(n => n.matchTag == mtag).
                            OrderBy(n => n.publishDateTime, OrderByType.Desc).Take(num).ToListAsync();
                        ret = new List<NewsWithPicture>();
                        for (int i = 0; i < news.Count; i++)
                        {
                            int id = news[i].news_id;
                            List<string> pictureRoutes = await sqlORM.Queryable<NewsHavePicture>().
                                Where(n => n.news_id == id).
                                Select(nhp => nhp.pictureRoute).
                                ToListAsync();
                            ret.Add(new NewsWithPicture { newsBody = news[i], pictureRoutes = pictureRoutes });
                        }
                    }
                    else if(mtag == "" && ptag != "")
                    {
                        news = await sqlORM.Queryable<News>().Where(n => n.propertyTag == ptag).
                            OrderBy(n => n.publishDateTime, OrderByType.Desc).Take(num).ToListAsync();
                        ret = new List<NewsWithPicture>();
                        for (int i = 0; i < news.Count; i++)
                        {
                            int id = news[i].news_id;
                            List<string> pictureRoutes = await sqlORM.Queryable<NewsHavePicture>().
                                Where(n => n.news_id == id).
                                Select(nhp => nhp.pictureRoute).
                                ToListAsync();
                            ret.Add(new NewsWithPicture { newsBody = news[i], pictureRoutes = pictureRoutes });
                        }
                    }
                    else
                    {
                        news = await sqlORM.Queryable<News>().Where(n => n.matchTag == mtag && n.propertyTag == ptag).//LeftJoin<NewsHavePicture>((n, nhp) => n.news_id == nhp.news_id).Where(n => n.matchTag == mtag && n.propertyTag == ptag).
                            OrderBy(n => n.publishDateTime, OrderByType.Desc).Take(num).ToListAsync();
                        ret = new List<NewsWithPicture>();
                        for (int i = 0; i < news.Count; i++)
                        {
                            int id = news[i].news_id;
                            List<string> pictureRoutes = await sqlORM.Queryable<NewsHavePicture>().
                                Where(n => n.news_id == id).
                                Select(nhp => nhp.pictureRoute).
                                ToListAsync();
                            ret.Add(new NewsWithPicture { newsBody = news[i], pictureRoutes = pictureRoutes });
                        }
                    }
                    return Ok(new CustomResponse { ok = "yes", value = ret });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return Ok(new CustomResponse { ok = "no", value = "服务器内部错误" }); // Internal server error
                }
            }
            else { return Ok(new CustomResponse { ok = "no", value = "数据库连接失败！" }); };
        }
        [HttpPost]
        public async Task<IActionResult> SearchNews([FromBody] SearchNewsRequest json)
        {
            Console.WriteLine("--------------------------SearchNews--------------------------");
            ORACLEconn ORACLEConnectTry = new ORACLEconn();
            string keyword = json.keyword;
            if (ORACLEConnectTry.getConn() == true)
            {
                try
                {
                    SqlSugarScope sqlORM = ORACLEConnectTry.sqlORM;
                    //Expression<Func<News, bool>> exp = Expressionable.Create<News>() //创建表达式
                    //    .AndIF(p > 0, it => it.Id == p)
                    //    .AndIF(name != null, it => it.Name == name && it.Sex == 1)
                    //    .ToExpression();//注意 这一句 不能少
                    List<News> news = await sqlORM.Queryable<News>().Where(it => (it.title.Contains(keyword) || it.summary.Contains(keyword) || it.contains.Contains(keyword))).ToListAsync();
                    Func<News, int> evaluate = x =>
                    {
                        int num = 0;
                        if (x.title.Contains(keyword))
                        {
                            num += 4;
                        }
                        if (x.summary.Contains(keyword))
                        {
                            num += 2;
                        }
                        if (x.contains.Contains(keyword))
                        {
                            num += 1;
                        }
                        return num;
                    };
                    
                    news.Sort((a, b) => 
                    { 
                        int na = evaluate(a), nb = evaluate(b);
                        return (na == nb ? 0 : (na > nb ? -1 : 1));
                    });
                    return Ok(new CustomResponse { ok = "yes", value = news});
                }
                catch
                {
                    //return new InternalError(new CustomResponse { ok = "no", value = "Internal Error" });
                    return Ok(new CustomResponse { ok = "no", value = "Internal Error" });
                }
            }
            else
            {
                //return new ServiceUnavailable(new CustomResponse { ok = "no", value = "ConnectionFailed" });
                return Ok(new CustomResponse { ok = "no", value = "ConnectionFailed" });
            }
        }
    }
}
