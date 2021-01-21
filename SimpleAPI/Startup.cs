using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SimpleAPI.Business.Managers;
using SimpleAPI.Data;
using SimpleAPI.DataAccess;
using SimpleAPI.DataAccess.Configuration;
using SimpleAPI.DTO;
using SimpleAPI.DTO.Profiles;
using System;
using System.Linq;

namespace SimpleAPI
{
    public class Startup : StartupDb
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddAutoMapper((serviceProvider, autoMapper) =>
            {
                autoMapper.AddProfile(new AutoMappingProfile());
                //autoMapper.UseEntityFrameworkCoreModel(serviceProvider);
            }, AppDomain.CurrentDomain.GetAssemblies());

            //services.AddRazorPages().AddRazorPagesOptions(options =>
            //{
            //    AddPageSpecificAuth(options);
            //});

            var conn = Environment.ExpandEnvironmentVariables(Configuration["Data:DefaultConnection"]);

            services.AddControllers();
            services.AddCors(options =>
            {
                options.AddPolicy(
                   name: "AllowOrigin",
                   builder =>
                   {
                       builder.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader();
                   });
            });

            services.AddSingleton(provider =>
            {
                var service = provider.GetRequiredService<ILogger<StartupLogger>>();
                return new StartupLogger(service);
            });
            var logger = services.BuildServiceProvider().GetRequiredService<StartupLogger>();
            HandleApplicationVersioning(logger);

            logger.Log($"In startup the connstring is {conn}");
            ConnectionString = conn;
            _ = services.AddDbContext<FootballContext>(options =>
                options.UseNpgsql(conn));

            _ = services.AddDbContext<IdentityContext>(options =>
                   options.UseNpgsql(conn));

           // services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<SCCUser>(options => options.SignIn.RequireConfirmedAccount = true)
               .AddEntityFrameworkStores<IdentityContext>();

            services.AddIdentityServer()
                .AddApiAuthorization<SCCUser, IdentityContext>();

            services.AddAuthentication()
                .AddIdentityServerJwt();
         //   services.AddControllersWithViews();
        //    services.AddRazorPages();
            // In production, the Angular files will be served from this directory
            //services.AddSpaStaticFiles(configuration =>
            //{
            //    configuration.RootPath = "../ClientApp/dist";
            //});

            ConfigureDI(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseMigrationsEndPoint();
            }
            else
            {

                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
         //       app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseAuthentication();

            app.UseIdentityServer();

            app.UseAuthorization();

            app.UseCors("AllowOrigin");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //app.UseEndpoints(endpoints =>
            //{
            //   endpoints.MapRazorPages();
            //    endpoints.MapControllerRoute(
            //        name: "default",
            //        pattern: "{controller}/{action=Index}/{id?}");

            //});

            //app.UseSpa(spa =>
            //{
            //    // To learn more about options for serving an Angular SPA from ASP.NET Core,
            //    // see https://go.microsoft.com/fwlink/?linkid=864501

            //    spa.Options.SourcePath = "../ClientApp";

            //    if (env.IsDevelopment())
            //    {
            //        //Uncomment if you want to run website and api at the same time. 
            //      //  spa.UseAngularCliServer(npmScript: "start");
            //    }
            //});
        }



        /// <summary>
        /// Silly method to help display Build Version, Deploy Version, and App Version that
        /// is incremented every build, is now being done by autoincrement_version.ps1
        /// </summary>
        private void HandleApplicationVersioning(StartupLogger logger)
        {
            try
            {
                var coreVersion = Configuration["Data:AppVersion"];
                AppVersion = coreVersion.ToString();
                var x = 1;

                foreach (var clArg in Environment.GetCommandLineArgs())
                {
                    if (clArg.Equals("-version", StringComparison.OrdinalIgnoreCase))
                    {
                        CICDVersion = Environment.GetCommandLineArgs()[x];
                    }
                    x++;
                }
                logger.Log($"Command Parameters passed as args {string.Join(",", Environment.GetCommandLineArgs().Select(x => x))}");
                var version = AppVersion.Split('.');
                if (string.IsNullOrEmpty(CICDVersion))
                    CICDVersion = $"{version[0]}.{version[1]}.{Configuration["Data:CICDVersion"]}";
                AppVersion = CICDVersion + "." + AppVersion.Split('.')[3];
            }
            catch (Exception e)
            {
                //don't really care about this if it fails
            }
        }

        /// <summary>
        /// Set the individual page, folders, or areas to have policies enforced for Auth
        /// </summary>
        /// <param name="options"></param>
        public void AddPageSpecificAuth(RazorPagesOptions options)
        {
            ////Admin can delete and edit
            //options.Conventions.AuthorizePage("/Teams/Delete", SCCPolicies.Updaters);
            //options.Conventions.AuthorizePage("/Teams/Edit", SCCPolicies.Updaters);
            ////Trusted can create
            //options.Conventions.AuthorizePage("/Teams/Create", SCCPolicies.Creators);
            ////Account can view
            //options.Conventions.AuthorizePage("/Teams/Details", SCCPolicies.Readers);
            //Anonymous can view all indexes
            //options.Conventions.AllowAnonymousToPage("~/Index");
            //Execpt stats

            options.Conventions.AllowAnonymousToAreaPage("Identity", "/Account/AccessDenied");
        }



        /// <summary>
        /// Setup the various custom DI impleminations/interfaces
        /// </summary>
        /// <param name="services"></param>
        private void ConfigureDI(IServiceCollection services)
        {
            services.AddTransient<IEntitiesManager<TeamDto>, BasicEntitiesManager<TeamDto>>();
            services.AddTransient<IEntitiesManager<PlayerDto>, BasicEntitiesManager<PlayerDto>>();
            services.AddTransient<IEntitiesManager<GameDto>, BasicEntitiesManager<GameDto>>();
            services.AddTransient<IMultiEntitiesManager<StatDto>, StatsManager<StatDto>>();
        }


    }

    /// <summary>
    /// Simple logger specifically for Startup logging
    /// </summary>
    public class StartupLogger
    {
        private readonly ILogger _logger;

        public StartupLogger(ILogger<StartupLogger> logger)
        {
            _logger = logger;
        }

        public void Log(string message)
        {
            _logger.LogInformation(message);
        }
    }
}
