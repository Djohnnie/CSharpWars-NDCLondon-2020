using Microsoft.Extensions.DependencyInjection;

namespace CSharpWars.DataAccess.DependencyInjection
{
    /// <summary>
    /// Static class containing an extension method to configure
    /// dependency injection for the CSharpWars.DataAccess project.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Extension method to configure dependency injection for the CSharpWars.DataAccess project.
        /// </summary>
        /// <param name="services">The dependency injection container to add configuration to.</param>
        public static void ConfigureDataAccess(this IServiceCollection services)
        {
            services.AddDbContext<CSharpWarsDbContext>();
        }
    }
}