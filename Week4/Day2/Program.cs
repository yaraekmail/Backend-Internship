// Provides ASP.NET Core Identity user and role types.
using Microsoft.AspNetCore.Identity;

// Provides Entity Framework Core functionality.
using Microsoft.EntityFrameworkCore;

// Provides JWT authentication configuration.
using Microsoft.AspNetCore.Authentication.JwtBearer;

// Provides classes used to validate JWTs.
using Microsoft.IdentityModel.Tokens;

// Provides text encoding utilities for the signing key.
using System.Text;


// Create the application builder.
var builder = WebApplication.CreateBuilder(args);


// Add OpenAPI support to the application.
builder.Services.AddOpenApi();


// Register the existing Training Management DbContext.
// This connects the application to the SQL Server database.
builder.Services.AddDbContext<TrainingManagementDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));


// Register ASP.NET Core Identity.
// IdentityUser represents the application user.
// IdentityRole represents roles such as Admin or Employee.
// Entity Framework Core is used to store Identity data in the database.
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<TrainingManagementDbContext>();


// Read the JWT configuration from appsettings.Development.json.
var jwtKey = builder.Configuration["Jwt:Key"];
var jwtIssuer = builder.Configuration["Jwt:Issuer"];
var jwtAudience = builder.Configuration["Jwt:Audience"];


// Make sure the JWT signing key exists before starting the application.
// Without a signing key, the application cannot create or validate JWTs.
if (string.IsNullOrWhiteSpace(jwtKey))
{
    throw new InvalidOperationException("JWT signing key is not configured.");
}


// Register JWT Bearer Authentication.
// This tells ASP.NET Core how to validate JWT tokens
// that clients send with protected requests.
builder.Services.AddAuthentication(options =>
{
    // Set JWT Bearer as the default authentication scheme.
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

    // Use JWT Bearer when the application needs to challenge
    // an unauthenticated request.
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    // Configure the rules used to validate incoming JWT tokens.
options.TokenValidationParameters = new TokenValidationParameters
{
    // Validate that the token was issued by the expected application.
    ValidateIssuer = true,
    ValidIssuer = jwtIssuer,

    // Validate that the token is intended for the expected audience.
    ValidateAudience = true,
    ValidAudience = jwtAudience,

    // Validate the JWT signature using the configured secret key.
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = new SymmetricSecurityKey(
        Encoding.UTF8.GetBytes(jwtKey)),

    // Reject the token when its expiration time has passed.
    ValidateLifetime = true,

    // Do not allow the default 5-minute expiration tolerance.
    // This makes the expiry test happen exactly at the token's expiration time.
    ClockSkew = TimeSpan.Zero
};
});


// Register authorization services.
// Authorization works together with [Authorize]
// to protect endpoints from unauthenticated users.
builder.Services.AddAuthorization();


// Register MVC controllers so ASP.NET Core can discover
// and execute controller-based API endpoints.
builder.Services.AddControllers();


// Build the application.
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Enable the OpenAPI endpoint during development.
    app.MapOpenApi();
}


// Redirect HTTP requests to HTTPS.
app.UseHttpsRedirection();


// Enable Authentication middleware.
// This checks the JWT when a request contains authentication information.
app.UseAuthentication();


// Enable Authorization middleware.
// This applies authorization rules such as [Authorize].
app.UseAuthorization();


// Map controller endpoints such as Login and protected API endpoints.
app.MapControllers();


// Start the application.
app.Run();