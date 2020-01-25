using System;
using System.Diagnostics.CodeAnalysis;
using CSharpWars.Common.Configuration.Interfaces;
using CSharpWars.Model;
using Microsoft.EntityFrameworkCore;

namespace CSharpWars.DataAccess
{
    [ExcludeFromCodeCoverage]
    public class CSharpWarsDbContext : DbContext
    {
        private readonly IConfigurationHelper _configurationHelper;
        
        public DbSet<Bot> Bots { get; set; }

        public CSharpWarsDbContext(IConfigurationHelper configurationHelper)
        {
            _configurationHelper = configurationHelper;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (string.IsNullOrEmpty(_configurationHelper.ConnectionString))
            {
                optionsBuilder.UseInMemoryDatabase($"{Guid.NewGuid()}");
            }
            else
            {
                optionsBuilder.UseSqlServer(_configurationHelper.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bot>(e =>
            {
                e.ToTable("BOTS").HasKey(x => x.Id).IsClustered(false);
                e.Property<int>("SysId").UseIdentityColumn();
                e.HasIndex("SysId").IsClustered();
            });
        }
    }
}