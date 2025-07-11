/**
 * File: IAuthService.cs
 * Description: Interface định nghĩa các chức năng xử lý đăng nhập cho AuthService.
 * Created by: Thành
 * Created on: 2025-07-10
 */

using PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs.response;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Models.Services.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponseDto?> LoginAsync(string identifier, string password);
    }
}