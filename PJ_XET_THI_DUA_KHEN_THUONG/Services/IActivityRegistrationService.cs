using System.Threading.Tasks;
using PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs.request;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Services
{
    public interface IActivityRegistrationService
{
    Task RegisterAsync(ActivityRegistrationRequest request);
    Task UnregisterAsync(ActivityRegistrationRequest request);
    Task<bool> IsRegisteredAsync(ActivityRegistrationRequest request);
}
}
