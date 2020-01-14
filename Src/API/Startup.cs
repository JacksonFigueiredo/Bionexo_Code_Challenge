using API.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.Net.Http.Headers;
using API.Business;
using API.Business.Interfaces;
using API.Repository.Interfaces;
using API.Repository.Implementation;

namespace API
{
    public class Startup

    {
        private readonly ILogger _logger;
        public IConfiguration _configuration { get; }
        public IHostingEnvironment _environment { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment environment, ILogger<Startup> logger)
        {
            _configuration = configuration;
            _environment = environment;
            _logger = logger;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = _configuration["SQLServerConnection:DefaultConnection"];
            services.AddDbContext<SqlContext>(options => options.UseSqlServer(connectionString));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddApiVersioning(option => option.ReportApiVersions = true);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new Info
                    {
                        Title = "Bionexo - Monitoring Framework API 1.0 - Challenge",
                        Version = "v1"
                    });

            });

            services.AddScoped<IBussinessLayer, BussinessImplementation>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepositoryImplementation<>));
        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");
            app.UseRewriter(option);

            _ = app.UseMvc(routes =>
              {
                  routes.MapRoute(
                      name: "DefaultApi",
                      template: "{controller=Values}/{id?}");
              });
        }
    }
}
