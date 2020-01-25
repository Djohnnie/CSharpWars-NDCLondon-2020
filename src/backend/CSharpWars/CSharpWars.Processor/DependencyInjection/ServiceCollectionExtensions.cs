using CSharpWars.Common.DependencyInjection;
using CSharpWars.Logic.DependencyInjection;
using CSharpWars.Processor.Middleware;
using CSharpWars.Processor.Middleware.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CSharpWars.Processor.DependencyInjection
{
    /// <summary>
    /// Static class containing an extension method to configure
    /// dependency injection for the CSharpWars.Processor project.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Extension method to configure dependency injection for the CSharpWars.Processor project.
        /// </summary>
        /// <param name="services">The dependency injection container to add configuration to.</param>
        public static void ConfigureScriptProcessor(this IServiceCollection services)
        {
            services.ConfigureCommon();
            services.ConfigureLogic();
            services.AddSingleton<IBotScriptCache, BotScriptCache>();
            services.AddScoped<IMiddleware, Middleware.Middleware>();
            services.AddScoped<IPreprocessor, Preprocessor>();
            services.AddScoped<IProcessor, Middleware.Processor>();
            services.AddScoped<IPostprocessor, Postprocessor>();
            services.AddScoped<IBotScriptCompiler, BotScriptCompiler>();
        }
    }
}