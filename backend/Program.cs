using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseSentry("https://f63c74ddd9b2413a914906839a71f0ed@sentry.io/1875445");
                    webBuilder.UseStartup<Startup>();
                });
        }
    }
}