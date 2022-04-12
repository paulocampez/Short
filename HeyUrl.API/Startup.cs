using HeyUrl.Urls.Application;
using HeyUrl.Urls.Application.Interface;
using HeyUrl.Urls.Data;
using HeyUrl.Urls.Data.Repository;
using HeyUrl.Urls.Domain.Interfaces;
using JsonApiDotNetCore.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace HeyUrl.API
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
            services.AddDbContext<UrlContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default"), sqlServerOptions =>
            {
                sqlServerOptions.MigrationsAssembly(Assembly.GetAssembly(typeof(UrlContext)).FullName);
                sqlServerOptions.EnableRetryOnFailure(5);
            }));
            services.AddJsonApi<UrlContext>();
            services.AddScoped<IUrlRepository, UrlRepository>();
            services.AddScoped<IUrlService, UrlService>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HeyUrl.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(options => options.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin());
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HeyUrl.API v1"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseJsonApi();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<UrlContext>();
                context.Database.Migrate();
            }
        }
    }
}
