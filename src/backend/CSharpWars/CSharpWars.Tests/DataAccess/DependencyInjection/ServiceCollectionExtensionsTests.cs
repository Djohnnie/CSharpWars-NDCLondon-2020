using CSharpWars.Common.DependencyInjection;
using CSharpWars.DataAccess;
using CSharpWars.DataAccess.DependencyInjection;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CSharpWars.Tests.DataAccess.DependencyInjection
{
    public class ServiceCollectionExtensionsTests
    {
        [Fact]
        public void ConfigureDataAccess_Should_Register_Scoped_DbContext()
        {
            // Arrange
            var serviceCollection = new ServiceCollection();
            serviceCollection.ConfigureDataAccess();
            serviceCollection.ConfigurationHelper(c =>
            {
                c.ArenaSize = 10;
                c.ConnectionString = "";
            });
            var provider = serviceCollection.BuildServiceProvider();

            // Act
            var dbContext1 = provider.GetService<CSharpWarsDbContext>();
            var dbContext2 = provider.GetService<CSharpWarsDbContext>();

            // Assert
            dbContext1.Should().BeOfType<CSharpWarsDbContext>();
            dbContext2.Should().BeOfType<CSharpWarsDbContext>();
            dbContext1.Should().Be(dbContext2);
        }
    }
}