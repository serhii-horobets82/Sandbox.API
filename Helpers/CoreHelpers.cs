namespace Evoflare.API.Helpers
{
    public static class CoreHelpers
    {
        public static bool SettingHasValue(string setting)
        {
            var normalizedSetting = setting?.ToLowerInvariant();
            return !string.IsNullOrWhiteSpace(normalizedSetting) &&
                !normalizedSetting.Equals("secret") &&
                !normalizedSetting.Equals("replace");
        }
    }
}