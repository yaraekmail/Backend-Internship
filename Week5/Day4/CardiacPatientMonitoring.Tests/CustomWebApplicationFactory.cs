// Provides access to the API DbContext.
using CardiacPatientMonitoring.Api.Data;

// Provides ASP.NET Core hosting and testing features.
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

// Provides Entity Framework Core features.
using Microsoft.EntityFrameworkCore;

// Provides EF Core infrastructure services.
using Microsoft.EntityFrameworkCore.Infrastructure;

// Provides dependency injection features.
using Microsoft.Extensions.DependencyInjection;

// Provides RemoveAll<T>() for removing registered services.
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CardiacPatientMonitoring.Tests;

// Creates a test version of the API.
public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    // Configures the API specifically for integration testing.
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        // Changes the services used by the test version of the API.
        builder.ConfigureServices(services =>
        {
            // Removes the original database context registration.
            services.RemoveAll<CardiacPatientMonitoringDbContext>();

            // Removes the original SQL Server database options.
            services.RemoveAll<DbContextOptions<CardiacPatientMonitoringDbContext>>();

            // Removes the SQL Server provider configuration.
            services.RemoveAll<IDbContextOptionsConfiguration<CardiacPatientMonitoringDbContext>>();

            // Adds the database context again using an in-memory database.
            services.AddDbContext<CardiacPatientMonitoringDbContext>(options =>
            {
                // Uses a separate in-memory database for testing.
                options.UseInMemoryDatabase("CardiacPatientMonitoringDay4TestDb");
            });
        });
    }
}