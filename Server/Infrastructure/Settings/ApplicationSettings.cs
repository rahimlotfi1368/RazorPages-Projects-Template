namespace Infrastructure.Settings
{
    public class ApplicationSettings
    {
        public ApplicationSettings()
        {
            CultureSettings=new CultureSettings();
        }
        public static readonly string KeyName = nameof(ApplicationSettings);
        public string? Version { get; set; }
        public string[]? ActivationKeys { get; set; }
        public CultureSettings? CultureSettings { get; set; }
    }
}
