/**
 * File: AuthService.cs
 * Description: Cài đặt IAuthService, xử lý logic đăng nhập và sinh JWT.
 * Created by: Thành
 * Created on: 2025-07-10
 */

using PJ_XET_THI_DUA_KHEN_THUONG.Models.Entities;
using PJ_XET_THI_DUA_KHEN_THUONG.Models.Repositories.Interfaces;
using PJ_XET_THI_DUA_KHEN_THUONG.Models.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs.response;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Models.Services.Implement
{
    public class AuthService : IAuthService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IConfiguration _config;

        public AuthService(IAccountRepository accountRepository, IConfiguration config)
        {
            _accountRepository = accountRepository;
            _config = config;
        }

        public async Task<LoginResponseDto?> LoginAdminAsync(string username, string password)
        {
            var account = await _accountRepository.GetByUsernameAsync(username);

            if (account == null || !BCrypt.Net.BCrypt.Verify(password, account.PasswordHash))
                return null;

            return GenerateToken(account);
        }

        public async Task<LoginResponseDto?> LoginStudentAsync(string mssv, string password)
        {
            var account = await _accountRepository.GetStudentAccountByMSSVAsync(mssv);

            if (account == null || !BCrypt.Net.BCrypt.Verify(password, account.PasswordHash))
                return null;

            return GenerateToken(account);
        }

        private LoginResponseDto GenerateToken(Accounts account)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]!);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("userId", account.UserID.ToString()),
                    new Claim("roleId", account.RoleID.ToString()),
                    new Claim("fullName", $"{account.User.LastName} {account.User.FirstName}"),
                    new Claim("roleName", account.Role.RoleName)
                }),
                Expires = DateTime.UtcNow.AddHours(6),
                Issuer = _config["Jwt:Issuer"],
                Audience = _config["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new LoginResponseDto
            {
                Token = tokenHandler.WriteToken(token),
                FullName = $"{account.User.LastName} {account.User.FirstName}",
                RoleId = account.RoleID,
                RoleName = account.Role.RoleName,
                UserId = account.UserID
            };
        }
    }
}
