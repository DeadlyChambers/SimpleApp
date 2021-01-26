using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleAPI.Data;
using SimpleAPI.DataAccess;
using System;
using System.Linq;

namespace SimpleAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                try
                {
                    var db = scope.ServiceProvider.GetRequiredService<FootballContext>();
                    var migs = db.Database.GetPendingMigrations();
                    if (migs.Any())
                        db.Database.Migrate();
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    Logger.Debug(ex.Message);
                    //Looks like a pre-existing db will fail...sort of a pain
                }
            }
            //using (var scope = host.Services.CreateScope())
            //{
            //    try
            //    {
            //        var db = scope.ServiceProvider.GetRequiredService<IdentityContext>();
            //        var migs = db.Database.GetPendingMigrations();
            //        if (migs.Any())
            //            db.Database.Migrate();
            //        db.SaveChanges();
            //    }
            //    catch (Exception ex)
            //    {
            //        //Looks like a pre-existing db will fail...sort of a pain
            //    }
            //}

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
