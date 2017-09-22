using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace DinnerTime.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
#if DEBUG
                .UseUrls("http://*:5000")
#else
                .UseUrls("http://*:80")
#endif
                .UseStartup<Startup>()
                .Build();
    }
}
