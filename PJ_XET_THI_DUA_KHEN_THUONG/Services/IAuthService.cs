using PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Services
{
    public interface IAuthService
    {
        Task<LoginResponseDto?> AuthenticateAsync(LoginRequestDto request);

    }
}
