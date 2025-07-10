/**
 * File: AccountRepository.cs
 * Description: Triển khai repository xử lý truy vấn tài khoản từ CSDL.
 * Created by: Thành
 * Created on: 2025-07-10
 */

using PJ_XET_THI_DUA_KHEN_THUONG.Data;
using PJ_XET_THI_DUA_KHEN_THUONG.Models.Entities;
using PJ_XET_THI_DUA_KHEN_THUONG.Models.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Models.Repositories.Implement
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationDbContext _context;

        public AccountRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Đăng nhập cho admin, khoa, cố vấn...
        public async Task<Accounts?> GetByUsernameAsync(string username)
        {
            return await _context.Accounts
                .Include(a => a.User)
                .Include(a => a.Role)
                .FirstOrDefaultAsync(a => a.Username == username && a.IsActive);
        }

        // Sinh viên đăng nhập bằng mã sinh viên
        public async Task<Accounts?> GetStudentAccountByMSSVAsync(string mssv)
        {
            var user = await _context.Users
            .Include(u => u.Accounts!)
                .ThenInclude(a => a.Role)
            .FirstOrDefaultAsync(u => u.IdentityCode == mssv);

            return user?.Accounts.FirstOrDefault(a => a.IsActive);
        }
    }
}
