using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
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
        public class CustomResponse
        {
            public string ok { get; set; }
            public object value { get; set; }
        }
        public class GetNewsRequest
        {
            public int num { get; set; }
            public string matchTag { get; set; }
            public string propertyTag { get; set; }
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
                    SqlSugarClient sqlORM = ORACLEConnectTry.sqlORM;
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
            Console.WriteLine("GET Login!");
            ORACLEconn ORACLEConnectTry = new ORACLEconn();
            int num = json.num;
            string mtag = json.matchTag;
            string ptag = json.propertyTag;
            if(ORACLEConnectTry.getConn() == true)
            {
                try
                {
                    SqlSugarClient sqlORM = ORACLEConnectTry.sqlORM;
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
                        news = await sqlORM.Queryable<News>().LeftJoin<NewsHavePicture>((n, nhp) => n.news_id == nhp.news_id).Take(num).ToListAsync();
                        ret = new List<NewsWithPicture>();
                        for (int i = 0; i < news.Count; i++)
                        {
                            List<string> pictureRoutes = await sqlORM.Queryable<News>().
                                LeftJoin<NewsHavePicture>((n, nhp) => n.news_id == nhp.news_id).
                                Where(n => n.news_id == news[i].news_id).
                                Select((n, nhp) => nhp.pictureRoute).
                                ToListAsync();
                            ret.Add(new NewsWithPicture { newsBody = news[i], pictureRoutes = pictureRoutes });
                        }
                    }
                    else if(mtag != "" && ptag == "")
                    {
                        news = await sqlORM.Queryable<News>().LeftJoin<NewsHavePicture>((n, nhp) => n.news_id == nhp.news_id).Where(n => n.matchTag == mtag).Take(num).ToListAsync();
                        ret = new List<NewsWithPicture>();
                        for (int i = 0; i < news.Count; i++)
                        {
                            List<string> pictureRoutes = await sqlORM.Queryable<News>().
                                LeftJoin<NewsHavePicture>((n, nhp) => n.news_id == nhp.news_id).
                                Where(n => n.news_id == news[i].news_id).
                                Select((n, nhp) => nhp.pictureRoute).
                                ToListAsync();
                            ret.Add(new NewsWithPicture { newsBody = news[i], pictureRoutes = pictureRoutes });
                        }
                    }
                    else if(mtag == "" && ptag != "")
                    {
                        news = await sqlORM.Queryable<News>().LeftJoin<NewsHavePicture>((n, nhp) => n.news_id == nhp.news_id).Where(n => n.propertyTag == ptag).Take(num).ToListAsync();
                        ret = new List<NewsWithPicture>();
                        for (int i = 0; i < news.Count; i++)
                        {
                            List<string> pictureRoutes = await sqlORM.Queryable<News>().
                                LeftJoin<NewsHavePicture>((n, nhp) => n.news_id == nhp.news_id).
                                Where(n => n.news_id == news[i].news_id).
                                Select((n, nhp) => nhp.pictureRoute).
                                ToListAsync();
                            ret.Add(new NewsWithPicture { newsBody = news[i], pictureRoutes = pictureRoutes });
                        }
                    }
                    else
                    {
                        news = await sqlORM.Queryable<News>().LeftJoin<NewsHavePicture>((n, nhp) => n.news_id == nhp.news_id).Where(n => n.matchTag == mtag && n.propertyTag == ptag).Take(num).ToListAsync();
                        ret = new List<NewsWithPicture>();
                        for (int i = 0; i < news.Count; i++)
                        {
                            List<string> pictureRoutes = await sqlORM.Queryable<News>().
                                LeftJoin<NewsHavePicture>((n, nhp) => n.news_id == nhp.news_id).
                                Where(n => n.news_id == news[i].news_id).
                                Select((n, nhp) => nhp.pictureRoute).
                                ToListAsync();
                            ret.Add(new NewsWithPicture { newsBody = news[i], pictureRoutes = pictureRoutes });
                        }
                    }
                    return Ok(new CustomResponse { ok = "yes", value = ret });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return Ok(new CustomResponse { ok = "no", value = "UNKNOWN" }); // Internal server error
                }
            }
            else { return Ok(new CustomResponse { ok = "no", value = "ConnectionFailed" }); };
        }
    }
}
