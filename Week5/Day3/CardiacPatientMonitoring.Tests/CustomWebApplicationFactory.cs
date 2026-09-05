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
            // Removes the original DbContext registration.
            services.RemoveAll<CardiacPatientMonitoringDbContext>();

            // Removes the original SQL Server DbContext options.
            services.RemoveAll<DbContextOptions<CardiacPatientMonitoringDbContext>>();

            // Removes the configuration that adds SQL Server to the DbContext.
            services.RemoveAll<IDbContextOptionsConfiguration<CardiacPatientMonitoringDbContext>>();

            // Adds the DbContext again, but this time for testing.
            services.AddDbContext<CardiacPatientMonitoringDbContext>(options =>
            {
                // Uses an in-memory database instead of SQL Server.
                options.UseInMemoryDatabase("CardiacPatientMonitoringTestDb");
            });
        });
    }
}