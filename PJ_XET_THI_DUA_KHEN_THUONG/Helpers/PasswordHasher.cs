namespace PJ_XET_THI_DUA_KHEN_THUONG.Helpers
{
    public static class PasswordHasher
    {
        public static bool Verify(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }
}
