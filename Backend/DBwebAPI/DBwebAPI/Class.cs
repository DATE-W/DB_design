using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace DBwebAPI
{
    public class JwtTokenGenerator
    {
        private readonly RSA _privateKey;
        private readonly RSA _publicKey;

        public JwtTokenGenerator()
        {
            // 生成 RSA 密钥对
            using (RSA rsa = RSA.Create())
            {
                _privateKey = rsa;
                _publicKey = rsa;
            }
        }

        public string GenerateToken()
        {
            // 创建 JWT Token 的 Claims
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, "user-id"),
            // 添加其他自定义的 Claims
        };

            // 创建签名凭据
            var signingCredentials = new SigningCredentials(
                new RsaSecurityKey(_privateKey),
                SecurityAlgorithms.RsaSha256
            );

            // 创建 JWT Token
            var token = new JwtSecurityToken(
                issuer: "your-issuer",
                audience: "your-audience",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1), // 设置过期时间
                signingCredentials: signingCredentials
            );

            // 使用 JwtSecurityTokenHandler 生成 Token 字符串
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }

        public bool ValidateToken(string token)
        {
            // 创建 Token 验证参数
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new RsaSecurityKey(_publicKey),
                ValidateIssuer = true,
                ValidIssuer = "your-issuer",
                ValidateAudience = true,
                ValidAudience = "your-audience",
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            try
            {
                // 使用 JwtSecurityTokenHandler 验证 Token
                var tokenHandler = new JwtSecurityTokenHandler();
                tokenHandler.ValidateToken(token, validationParameters, out _);

                // Token 通过验证
                return true;
            }
            catch (SecurityTokenException)
            {
                // Token 验证失败
                return false;
            }
        }
    }
}
