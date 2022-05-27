namespace Infrastructure.Settings
{
    public class CultureSettings
    {
        public static readonly string KeyName = nameof(CultureSettings);
        public string? DefaultCultureName { get; set; }
        public string[]? SupportedCultureNames { get; set; }
    }
}
