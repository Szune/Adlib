using Adlib.Database.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Adlib.Database
{
    public class Program
    {
        public static void Main(string[] args) => CreateHostBuilder(args).Build().Run();

        public static IHostBuilder CreateHostBuilder(string[] args)
            => Host.CreateDefaultBuilder(args)
                .ConfigureServices((c, sp) => 
                    sp.AddDbContext<AdContext>(
                    opt=>
                        opt.UseSqlite(c.Configuration.GetConnectionString("AdDb"))));
    }
}