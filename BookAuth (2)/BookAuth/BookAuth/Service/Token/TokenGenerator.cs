using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BookAuth.Service.Token
{
    public class TokenGenerator : ITokenGenerator
    {
        public string GenerateToken(string email, string role)
        {
            var claims = new[]
            {
                new Claim("email", email),
                new Claim("role", role)
            };

            // Generate a key with at least 256 bits
            using var provider = new RNGCryptoServiceProvider();
            var keyBytes = new byte[32]; // 32 bytes = 256 bits
            provider.GetBytes(keyBytes);

            var key = new SymmetricSecurityKey(keyBytes);
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "bookauth",
                audience: "customerapi",
                claims: claims,
                expires: DateTime.Now.AddMinutes(20),
                signingCredentials: creds
            );

            var response = new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            };

            return JsonConvert.SerializeObject(response);
        }
    }
}
