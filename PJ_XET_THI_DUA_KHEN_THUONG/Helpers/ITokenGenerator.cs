using PJ_XET_THI_DUA_KHEN_THUONG.Models.Entities;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Helpers
{
    public interface ITokenGenerator
    {
        string Generate(User user);
    }
}
