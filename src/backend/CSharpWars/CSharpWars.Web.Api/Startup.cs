using CSharpWars.Common.DependencyInjection;
using CSharpWars.Web.Api.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using static System.Convert;
using static System.Environment;

namespace CSharpWars.Web.Api
{
    /// <summary>
    /// The ASP.NET WebApi WebHost startup configuration class.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Configures the dependency injection container.
        /// </summary>
        /// <param name="services">The dependency injection container.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigurationHelper(c =>
            {
                c.ConnectionString = GetEnvironmentVariable("CONNECTION_STRING");
                c.ArenaSize = ToInt32(GetEnvironmentVariable("ARENA_SIZE"));
            });

            services.ConfigureWebApi();
        }

        /// <summary>
        /// Configures the specified application and web-host environment.
        /// </summary>
        /// <param name="application">The application.</param>
        /// <param name="environment">The web-host environment.</param>
        public void Configure(IApplicationBuilder application, IWebHostEnvironment environment)
        {
            if (environment.IsDevelopment())
            {
                application.UseDeveloperExceptionPage();
            }

            application.UseSwagger();

            application.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            application.UseRouting();
            application.UseAuthorization();
            application.UseCors("AllowAll");
            application.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}