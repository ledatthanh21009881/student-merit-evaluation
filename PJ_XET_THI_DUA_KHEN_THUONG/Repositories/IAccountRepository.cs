/**
 * File: IAccountRepository.cs
 * Description: Định nghĩa các phương thức truy vấn tài khoản để phục vụ đăng nhập.
 * Created by: Thành
 * Created on: 2025-07-10
 */

using PJ_XET_THI_DUA_KHEN_THUONG.Models.Entities;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Models.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        Task<Accounts?> GetByUsernameAsync(string username);
        Task<Accounts?> GetStudentAccountByMSSVAsync(string mssv);
    }
}
