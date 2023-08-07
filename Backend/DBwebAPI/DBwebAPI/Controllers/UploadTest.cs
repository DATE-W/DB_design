using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DBwebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadTest : ControllerBase
    {
        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage([FromForm] IFormFile image)
        {
            // 处理上传的文件
            if (image != null && image.Length > 0)
            {
                // 执行将文件保存到服务器文件系统的逻辑
                // 可以使用image.OpenReadStream()获取文件流，然后将其保存到指定的位置
            }

            return Ok("文件上传成功");
        }

    }
}
