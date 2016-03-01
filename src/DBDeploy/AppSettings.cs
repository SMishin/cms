using Microsoft.Extensions.Configuration;

namespace DBDeploy
{
    public static class AppSettings
    {
	    public static readonly IConfiguration Configuration;

	    static AppSettings()
	    {
			ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
			configurationBuilder.AddJsonFile("appsettings.json");
			Configuration = configurationBuilder.Build();
		}
    }
}
