using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;

using CardiacPatientMonitoring.Api.Data;

var builder = WebApplication.CreateBuilder(args);

// Registers the EF Core database context with SQL Server.
builder.Services.AddDbContext<CardiacPatientMonitoringDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// Registers ASP.NET Core Identity for user management and authentication.
builder.Services.AddIdentityCore<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<CardiacPatientMonitoringDbContext>()
    .AddSignInManager();

// Reads JWT settings from User Secrets or other configuration providers.
var jwtKey = builder.Configuration["Jwt:Key"]
    ?? throw new InvalidOperationException("JWT Key is not configured.");

var jwtIssuer = builder.Configuration["Jwt:Issuer"]
    ?? throw new InvalidOperationException("JWT Issuer is not configured.");

var jwtAudience = builder.Configuration["Jwt:Audience"]
    ?? throw new InvalidOperationException("JWT Audience is not configured.");

// Configures JWT Bearer authentication.
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = jwtIssuer,

        ValidateAudience = true,
        ValidAudience = jwtAudience,

        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtKey)),

        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

// Adds authorization services for protected endpoints and roles.
builder.Services.AddAuthorization();

// Adds controller support.
builder.Services.AddControllers();

// Registers all FluentValidation validators from the current assembly.
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

// Adds OpenAPI support.
builder.Services.AddOpenApi();

// Adds Swagger generation.
builder.Services.AddSwaggerGen(options =>
{
    // Defines JWT Bearer authentication for Swagger.
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter your JWT token."
    });

    // Applies the Bearer authentication scheme to Swagger requests.
    options.AddSecurityRequirement(document =>
        new OpenApiSecurityRequirement
        {
            [new OpenApiSecuritySchemeReference("Bearer", document)] =
                new List<string>()
        });
});

var app = builder.Build();

// Enables Swagger JSON and Swagger UI.
app.UseSwagger();
app.UseSwaggerUI();

// Creates a service scope for initializing Identity data.
using (var scope = app.Services.CreateScope())
{
    // Gets the RoleManager service from dependency injection.
    var roleManager = scope.ServiceProvider
        .GetRequiredService<RoleManager<IdentityRole>>();

    // Gets the UserManager service from dependency injection.
    var userManager = scope.ServiceProvider
        .GetRequiredService<UserManager<IdentityUser>>();

    // Creates the default User and Admin roles.
    await IdentitySeeder.SeedRolesAsync(roleManager);

    // Creates the default Admin test user.
    await IdentitySeeder.SeedAdminAsync(userManager);
}

// Enables OpenAPI in development.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Redirects HTTP requests to HTTPS.
app.UseHttpsRedirection();

// Enables authentication before authorization.
app.UseAuthentication();

// Enables authorization.
app.UseAuthorization();

// Maps controller endpoints.
app.MapControllers();
// Create a service scope to access the database context.
using (var scope = app.Services.CreateScope())
{
    // Get the database context from dependency injection.
    var context = scope.ServiceProvider.GetRequiredService<CardiacPatientMonitoringDbContext>();

    // Seed initial patients and vital-sign data.
    await DbSeeder.SeedAsync(context);
}
app.Run();
// Makes the Program class accessible to the integration test project.
public partial class Program
{
}