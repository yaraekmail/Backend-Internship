// Provides ASP.NET Core Identity user and role types.
using Microsoft.AspNetCore.Identity;
// Provides ASP.NET Core's built-in rate limiting features.
using Microsoft.AspNetCore.RateLimiting;
// Provides ASP.NET Core's built-in rate limiting functionality.
using System.Threading.RateLimiting;
// Provides FluentValidation registration methods.
using FluentValidation;
// Provides ASP.NET Core FluentValidation integration methods.
using FluentValidation.AspNetCore;
// Provides Entity Framework Core functionality.
using Microsoft.EntityFrameworkCore;

// Provides JWT authentication configuration.
using Microsoft.AspNetCore.Authentication.JwtBearer;

// Provides classes used to validate JWTs.
using Microsoft.IdentityModel.Tokens;

// Provides text encoding utilities for the signing key.
using System.Text;
using System.Security.Claims;


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
// IdentityRole represents roles such as Admin or User.
// Entity Framework Core stores Identity data in SQL Server.
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<TrainingManagementDbContext>()

    // Register the default token providers.
    // These providers are required for operations such as
    // password reset and email confirmation.
    .AddDefaultTokenProviders();


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


// Register authorization policies used to control access to endpoints.
builder.Services.AddAuthorization(options =>
{
    // Define a named policy with two requirements:
    // 1. The user must have the Admin role.
    // 2. The user's email claim must be yara@gmail.com.
    options.AddPolicy("AdminWithEmailPolicy", policy =>
    {
        policy.RequireRole("Admin");
        policy.RequireClaim(ClaimTypes.Email, "yara@gmail.com");
    });
});

// Configure CORS and allow only our known frontend origin.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        // Only this frontend origin is allowed to call the API from a browser.
        policy.WithOrigins("https://myapp.com")

              // Allow the frontend to send normal HTTP headers.
              .AllowAnyHeader()

              // Allow the frontend to use any HTTP method such as GET, POST, PUT, DELETE.
              .AllowAnyMethod();
    });
});

// Register MVC controllers.
builder.Services.AddControllers();

// Configure ASP.NET Core's built-in rate limiting.
builder.Services.AddRateLimiter(options =>
{
    // Return HTTP 429 when the request limit is exceeded.
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

    // General endpoints: allow up to 100 requests per minute.
    options.AddFixedWindowLimiter("GeneralPolicy", limiterOptions =>
    {
        limiterOptions.PermitLimit = 100;
        limiterOptions.Window = TimeSpan.FromMinutes(1);
        limiterOptions.QueueLimit = 0;
    });

    // Login endpoint: allow only 5 requests per minute.
    // This stricter limit helps reduce brute-force login attempts.
    options.AddFixedWindowLimiter("LoginPolicy", limiterOptions =>
    {
        limiterOptions.PermitLimit = 5;
        limiterOptions.Window = TimeSpan.FromMinutes(1);
        limiterOptions.QueueLimit = 0;
    });
});

// Enable automatic FluentValidation integration.
// FluentValidation will run automatically during ASP.NET Core model validation.
builder.Services.AddFluentValidationAutoValidation();

// Register all validators from this application assembly.
// This allows ASP.NET Core to discover our validators automatically.
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

// Build the application.
var app = builder.Build();


// Create a scope so we can access scoped Identity services
// such as RoleManager outside of a controller.
using (var scope = app.Services.CreateScope())
{
    // Get the RoleManager service from Dependency Injection.
    var roleManager = scope.ServiceProvider
        .GetRequiredService<RoleManager<IdentityRole>>();

    // Define the roles required by the application.
    var roles = new[] { "User", "Admin" };

    // Check each role and create it only if it does not already exist.
    foreach (var role in roles)
    {
        // Check whether this role already exists in the database.
        if (!await roleManager.RoleExistsAsync(role))
        {
            // Create the role in the Identity database.
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }


    // Get the UserManager service from Dependency Injection.
    // UserManager is used to manage users and assign them to roles.
    var userManager = scope.ServiceProvider
        .GetRequiredService<UserManager<IdentityUser>>();
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Enable the OpenAPI endpoint during development.
    app.MapOpenApi();
}

// Enable HSTS to tell browsers to use HTTPS.
app.UseHsts();
// Redirect HTTP requests to HTTPS.
app.UseHttpsRedirection();
// Enable rate limiting for incoming HTTP requests.
// Apply the named CORS policy to incoming browser requests.
app.UseCors("AllowFrontend");
app.UseRateLimiter();

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