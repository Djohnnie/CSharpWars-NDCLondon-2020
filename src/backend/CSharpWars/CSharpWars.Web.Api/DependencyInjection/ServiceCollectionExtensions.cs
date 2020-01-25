using System.IO;
using System.Reflection;
using CSharpWars.Common.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using CSharpWars.Logic.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace CSharpWars.Web.Api.DependencyInjection
{
    /// <summary>
    /// Static class containing an extension method to configure
    /// dependency injection for the CSharp.Web.Api project.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Extension method to configure dependency injection for the CSharp.Web.Api project.
        /// </summary>
        /// <param name="services">The dependency injection container to add configuration to.</param>
        public static void ConfigureWebApi(this IServiceCollection services)
        {
            services.ConfigureCommon();
            services.ConfigureLogic();
            services.AddMemoryCache();
            services.AddMvc().AddNewtonsoftJson();
            services.AddCors(o => o.AddPolicy("AllowAll", builder =>
            {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            }));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CSharpWars", Version = "v1" });
                c.IncludeXmlComments(Path.ChangeExtension(Assembly.GetEntryAssembly().Location, "xml"));
            });
        }
    }
}