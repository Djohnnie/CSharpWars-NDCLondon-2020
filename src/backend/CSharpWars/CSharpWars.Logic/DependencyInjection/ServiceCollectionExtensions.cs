using CSharpWars.Common.DependencyInjection;
using CSharpWars.DataAccess.DependencyInjection;
using CSharpWars.Logic.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CSharpWars.Logic.DependencyInjection
{
    /// <summary>
    /// Static class containing an extension method to configure
    /// dependency injection for the CSharpWars.Logic project.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Extension method to configure dependency injection for the CSharpWars.Logic project.
        /// </summary>
        /// <param name="services">The dependency injection container to add configuration to.</param>
        public static void ConfigureLogic(this IServiceCollection services)
        {
            services.ConfigureDataAccess();
            services.ConfigureCommon();
            services.AddTransient<IArenaLogic, ArenaLogic>();
            services.AddTransient<IBotLogic, BotLogic>();
        }
    }
}