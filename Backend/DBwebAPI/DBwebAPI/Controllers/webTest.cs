using DBwebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;

namespace DBwebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class webTest : Controller
    {
        [HttpGet]
        public string webGet([FromQuery]string test)
        {
            Console.WriteLine("webGet access!");
            return test;

        }
    }
}
