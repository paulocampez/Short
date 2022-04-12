using hey_url_challenge_code_dotnet.Bll;
using hey_url_challenge_code_dotnet.Bll.Interface;
using HeyUrlChallengeCodeDotnet.Data;
using JsonApiDotNetCore.Configuration;
using Microsoft.AspNetCore.Builder;

using JsonApiDotNetCore.Serialization;
using JsonApiDotNetCore.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Linq;

namespace HeyUrlChallengeCodeDotnet
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddBrowserDetection();
            services.AddControllersWithViews();
            Environment.SetEnvironmentVariable("API_URL", Configuration["Api:ApiUrl"]);
            services.AddScoped<IUrlBLL, UrlBLL>();
            services.AddMvc(options =>
            {
                var formatter = options.InputFormatters.OfType<SystemTextJsonInputFormatter>().FirstOrDefault();
                formatter.SupportedMediaTypes.Add("application/vnd.api+json");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
         
            using var scope = app.ApplicationServices.CreateScope();
        }
    }
}
