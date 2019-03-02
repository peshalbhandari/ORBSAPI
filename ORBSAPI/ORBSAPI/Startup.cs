using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using NLog.Extensions.Logging;
using NLog.Web;
using ORBS.DAL.DataAccessLayer;
using ORBS.DAL.IDataAccessLayer;

namespace ORBSAPI
{
    public class Startup
    {

        public IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();

        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
                });

            //add singleton service to services
            services.AddSingleton<IConfiguration>(Configuration);

            //add other for DI
            services.AddTransient<IMenuCuisineDAL, MenuCuisineDAL>();
            services.AddTransient<ITableDAL, TableDAL>();


            services.AddSingleton(services);

            //add CORS to the application
            var cors = Configuration["CORS:Origin"];
            if (cors != null)
            {
                services.AddCors(x => x.AddPolicy("corsPolicy", y => y.WithOrigins(cors).AllowAnyHeader().AllowAnyMethod().AllowCredentials()));
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            env.ConfigureNLog("nlog.config");
            // make sure Chinese chars don't fk up
            // Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            //add NLog to ASP.NET Core
            loggerFactory.AddNLog();

        }
    }
}
