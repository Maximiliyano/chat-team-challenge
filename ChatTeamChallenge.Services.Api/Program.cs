using System.Net;
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

                webBuilder.ConfigureKestrel(options =>
                {
                    options.Listen(IPAddress.Any, Convert.ToInt32(Environment.GetEnvironmentVariable("PORT")));
                });
                
                webBuilder.UseStartup<Startup>();
            });
}