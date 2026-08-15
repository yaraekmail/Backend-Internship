using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

// Create the application builder.
var builder = WebApplication.CreateBuilder(args);

// Add OpenAPI support to the application.
builder.Services.AddOpenApi();
// Register MVC controllers so ASP.NET Core can discover and use controllers.
builder.Services.AddControllers();
// Register the existing TrainingManagementDbContext.
// The connection string is read from appsettings.json.
builder.Services.AddDbContext<TrainingManagementDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// Register ASP.NET Core Identity.
// IdentityUser represents the application user,
// while IdentityRole represents roles such as Admin or Employee.
// Identity uses TrainingManagementDbContext to store
// users, roles, and related Identity data in SQL Server.
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<TrainingManagementDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Redirect HTTP requests to HTTPS.
app.UseHttpsRedirection();
// Map controller endpoints such as /api/Auth/register.
app.MapControllers();
app.Run();