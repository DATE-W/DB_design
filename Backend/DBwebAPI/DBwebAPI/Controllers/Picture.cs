using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using SqlSugar;
using System.Linq.Expressions;
using DBwebAPI;

namespace DBwebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class Picture : ControllerBase
    {
        //验证文件后缀名合法性
        public class FileUtility
        {
            public static bool IsFileExtensionValid(IFormFile file, string[] allowedExtensions)
            {
                var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
                return allowedExtensions.Contains(fileExtension);
            }
        }
        [HttpPost]
        public async Task<IActionResult> SaveFile(IFormFile file)
        {
            Console.WriteLine("接口访问成功");
            string[] allowedExtensions = { ".jpg", ".png" };
            if(file == null || file.Length == 0)
            {
                Console.WriteLine("未接收到有效文件");
                return BadRequest(new CustomResponse { ok = "no", value = "未接收到有效图片" });
            }
            else
            {
                Stream stream = file.OpenReadStream();
                MD5 md5 = MD5.Create();
                SHA256 sha256 = SHA256.Create();
                string fileName;
                var hash = sha256.ComputeHash(stream);
                if (hash == null)
                {
                    Console.WriteLine("哈希错了，怪");
                    return BadRequest(new CustomResponse { ok = "no", value = "服务器内部错误" });
                }
                else
                {
                    // 将哈希值转换为16进制字符串
                    StringBuilder strb = new StringBuilder();
                    foreach (byte b in hash)
                    {
                        strb.Append(b.ToString("x2")); // 使用"x2"格式化将字节转换为16进制
                    }
                    fileName = strb.ToString();
                }
                string filePath = "/home/ubuntu/DataBase/test/";
                string extension = Path.GetExtension(file.FileName);
                filePath += fileName + extension;
                Stream saveStream = new FileStream(filePath, FileMode.Create);
                try
                {
                    await file.CopyToAsync(saveStream);
                    Console.WriteLine("成功");
                    return Ok(new CustomResponse { ok = "yes", value = fileName + extension });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return Ok(new CustomResponse { ok = "no", value = fileName});
                }
            }

            
        }
    }
}
