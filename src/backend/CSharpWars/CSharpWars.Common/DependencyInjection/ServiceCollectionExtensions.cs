using System;
using Microsoft.Extensions.DependencyInjection;
using CSharpWars.Common.Configuration;
using CSharpWars.Common.Configuration.Interfaces;
using CSharpWars.Common.Helpers;
using CSharpWars.Common.Helpers.Interfaces;

namespace CSharpWars.Common.DependencyInjection
{
    /// <summary>
    /// Static class containing an extension method to configure
    /// dependency injection for the CSharpWars.Common project.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>Extension method to configure configuration.</summary>
        /// <param name="services">The dependency injection container to add configuration to.</param>
        /// <param name="configuration">The configuration.</param>
        public static void ConfigurationHelper(this IServiceCollection services, Action<IConfigurationHelper> configuration)
        {
            services.AddSingleton<IConfigurationHelper, ConfigurationHelper>(factory =>
            {
                var configurationHelper = new ConfigurationHelper();
                configuration(configurationHelper);
                return configurationHelper;
            });
        }

        /// <summary>
        /// Extension method to configure dependency injection for the CSharpWars.Common project.
        /// </summary>
        /// <param name="services">The dependency injection container to add configuration to.</param>
        public static void ConfigureCommon(this IServiceCollection services)
        {
            services.AddSingleton<IRandomHelper, RandomHelper>();
        }
    }
}