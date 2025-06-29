using PJ_XET_THI_DUA_KHEN_THUONG.Helpers;
using PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs;
using PJ_XET_THI_DUA_KHEN_THUONG.Models.Entities;
using PJ_XET_THI_DUA_KHEN_THUONG.Repositories;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _repo;
        private readonly ITokenGenerator _token;

        public AuthService(IAuthRepository repo, ITokenGenerator token)
        {
            _repo = repo;
            _token = token;
        }

        public async Task<LoginResponseDto?> AuthenticateAsync(LoginRequestDto request)
        {
            // 1. Thử tìm bằng username (admin, gvcn1, ...)
            var user = await _repo.GetUserByUsernameAsync(request.Username);

            if (user == null)
            {
                // 2. Nếu không có, thử tìm sinh viên theo mã số
                var student = await _repo.GetStudentByStudentCodeAsync(request.Username);
                if (student == null || student.User == null)
                    return null;

                user = student.User;

                // 3. Kiểm tra password
                if (!user.IsActive || !PasswordHasher.Verify(request.Password, user.PasswordHash))
                    return null;

                // 4. Trả thông tin đăng nhập sinh viên
                return new LoginResponseDto
                {
                    UserId = user.UserId,
                    RoleId = user.RoleId,
                    FullName = student.LastName + " " + student.FirstName,
                    Token = _token.Generate(user),
                    Avatar = student.Avatar,
                    ClassId = student.ClassId,
                    IsClassLeader = await _repo.IsClassLeaderAsync(student.StudentId),
                    FacultyId = student.FacultyId
                };
            }
            else
            {
                // 5. Nếu là user bình thường (admin, gvcn...), kiểm tra password
                if (!user.IsActive || !PasswordHasher.Verify(request.Password, user.PasswordHash))
                    return null;    

                return new LoginResponseDto
                {
                    UserId = user.UserId,
                    RoleId = user.RoleId,
                    FullName = user.Username,
                    Token = _token.Generate(user),
                    Avatar = null,
                    ClassId = null,
                    IsClassLeader = false,
                    FacultyId = null
                };
            }
        }
    }
}
