using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Routing;
using Microsoft.Framework.DependencyInjection;
//using Microsoft.Framework.Configuration;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.ConfigurationModel.Json;
using Microsoft.Data.Entity;
using Rest.Api.Models;
using Serilog;
using Microsoft.Framework.Logging;

namespace Rest.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }
        public Startup(IHostingEnvironment env)
        {
            // Setup configuration sources.
            var configuration = new Configuration()
                .AddJsonFile("config.json")
                .AddJsonFile($"config.{env.EnvironmentName}.json", optional: true);

            configuration.AddEnvironmentVariables();
            Configuration = configuration;

            // logging config
            Log.Logger = new LoggerConfiguration()
        #if DNXCORE50
              .WriteTo.TextWriter(Console.Out)
        #else
              .WriteTo.Trace()
        #endif
              .CreateLogger();
        }

        // This method gets called by a runtime.
        // Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.Configure<AppSettings>(x => Configuration.GetSubKey("AppSettings"));
            services.AddEntityFramework().AddSqlite()
                .AddDbContext<Db>(options => options.UseSqlite(Configuration.Get("Db")));
            services.AddLogging();
            services.AddScoped<Db, Db>();

            services.AddCors();
        }

        // Configure is called after ConfigureServices is called.
        // Configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerfactory)
        {
            app.UseStaticFiles();
            app.UseMvc();
            app.UseCors(policy => policy.WithOrigins("http://example.com"));

            loggerfactory.AddSerilog();
        }
    }
}
