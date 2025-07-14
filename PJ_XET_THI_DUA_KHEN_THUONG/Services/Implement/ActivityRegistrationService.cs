using System.Threading.Tasks;
using PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs.request;
using PJ_XET_THI_DUA_KHEN_THUONG.Models.Entities;
using PJ_XET_THI_DUA_KHEN_THUONG.Repositories;
using PJ_XET_THI_DUA_KHEN_THUONG.Services;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Services.Implement
{
    public class ActivityRegistrationService : IActivityRegistrationService
{
    private readonly IActivityRegistrationRepository _repository;

    public ActivityRegistrationService(IActivityRegistrationRepository repository)
    {
        _repository = repository;
    }

    public async Task RegisterAsync(ActivityRegistrationRequest request)
    {
        var exists = await _repository.GetByIdAsync(request.ActivityId, request.UserId);
        if (exists != null)
            throw new Exception("User already registered for this activity.");

        var registration = new ActivityRegistration
        {
            ActivityId = request.ActivityId,
            UserID = request.UserId,
            RegisteredAt = DateTime.UtcNow
        };
        await _repository.AddAsync(registration);
    }

    public async Task UnregisterAsync(ActivityRegistrationRequest request)
    {
        await _repository.DeleteAsync(request.ActivityId, request.UserId);
    }

    public async Task<bool> IsRegisteredAsync(ActivityRegistrationRequest request)
    {
        var reg = await _repository.GetByIdAsync(request.ActivityId, request.UserId);
        return reg != null;
    }
}}
