using ChatTeamChallenge.Testing.Common.Helpers;

namespace ChatTeamChallenge.Services.Api;

public static class Program
{
    public static void Main(string[] args) =>
        CreateWebHostBuilder(args).Build().Run();

    private static IHostBuilder CreateWebHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.ConfigureAppConfiguration(config => config
                    .AddJsonFile(ConfigurationHelper.GetConfigFileName(), false, false));

                webBuilder.UseUrls("http://*:8080");
                
                webBuilder.UseKestrel(options =>
                {
                    options.ListenAnyIP(8080);
                });

                webBuilder.UseIIS();
                
                webBuilder.UseStartup<Startup>();
            });
}