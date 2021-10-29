using System.IO;
using MatBlazor;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using raspberry_mqqt_motion_alarm.Data;

using raspberry_mqqt_motion_alarm.Services.Implementations;
using raspberry_mqqt_motion_alarm.Services.Interfaces;


namespace raspberry_mqqt_motion_alarm
{
    public class Startup
    {

        private readonly string contentRoot;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            contentRoot = configuration.GetValue<string>(WebHostDefaults.ContentRootKey);


        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddSingleton<ILiteDbContext, LiteDbContext>();
            services.AddTransient<IZoneService, ZoneService>();
            services.AddTransient<IMotionService, MotionService>();



            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "raspberry_mqqt_motion_alarm", Version = "v1" });
            });

            services.AddMatBlazor();

            services.Configure<RazorPagesOptions>(options => options.RootDirectory = "/Frontend/Pages");
            services.AddRazorPages();
            services.AddServerSideBlazor();

            services.AddDataProtection()
                .PersistKeysToFileSystem(new DirectoryInfo(Path.Combine(this.contentRoot))); 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IZoneService zoneService)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "raspberry_mqqt_motion_alarm v1"));
        
            zoneService.Init();

            app.UseRouting();

            app.UseAuthorization();

            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
