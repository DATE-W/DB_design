using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NetTaste;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DBwebAPI
{
    public class createToken
    {
        public string createTokenFun(string account, string password)
        {
            DateTime utcNow = DateTime.UtcNow;
            string key = "f47b558d-7654-458c-99f2-13b190ef0199";
            SecurityKey securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
            // token中的claims用于储存自定义信息，如登录之后的用户id等
            var claims = new[]
                {
                new Claim("account", account),
                new Claim("password", password)
        };
            JwtSecurityToken jwtToken = new JwtSecurityToken(
                            //issuer: "fan",
                            //audience: "audi~~!",
                            claims: claims
                            //notBefore: utcNow,
                            //expires: utcNow.AddYears(1),
                            //signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
                            );

            string token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return token;
        }
    }
}
