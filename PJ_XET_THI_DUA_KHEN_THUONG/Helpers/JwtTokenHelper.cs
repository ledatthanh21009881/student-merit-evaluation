/**
 * File: JwtTokenHelper.cs
 * Description: Helper dùng để sinh JWT token từ thông tin người dùng.
 * Created by: Thành
 * Created on: 2025-07-10
 */

using PJ_XET_THI_DUA_KHEN_THUONG.Models.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Data.Helpers
{
    public static class JwtTokenHelper
    {
        public static string GenerateToken(Accounts account, IConfiguration config)
        {
            var key = Encoding.UTF8.GetBytes(config["Jwt:Key"]!);
            var tokenHandler = new JwtSecurityTokenHandler();

            var claims = new[]
            {
                new Claim("userId", account.UserID.ToString()),
                new Claim("roleId", account.RoleID.ToString()),
                new Claim("fullName", $"{account.User?.LastName ?? ""} {account.User?.FirstName ?? ""}"),
                new Claim("roleName", account.Role?.RoleName ?? "")
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(6),
                Issuer = config["Jwt:Issuer"],
                Audience = config["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
