using Microsoft.AspNetCore.Identity;

namespace CardiacPatientMonitoring.Api.Data;

// Creates the default application roles and test admin user.
public static class IdentitySeeder
{
    // Creates the User and Admin roles if they do not already exist.
    public static async Task SeedRolesAsync(
        RoleManager<IdentityRole> roleManager)
    {
        // Defines the roles required by the application.
        string[] roles = { "User", "Admin" };

        foreach (var role in roles)
        {
            // Checks whether the role already exists.
            if (!await roleManager.RoleExistsAsync(role))
            {
                // Creates the role if it does not exist.
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }

    // Creates a default Admin test user if it does not already exist.
    public static async Task SeedAdminAsync(
        UserManager<IdentityUser> userManager)
    {
        // Defines the credentials for the local development Admin account.
        const string adminEmail = "admin@cardiac.local";
        const string adminPassword = "Admin123!";

        // Checks whether the Admin user already exists.
        var adminUser = await userManager.FindByEmailAsync(adminEmail);

        if (adminUser is null)
        {
            // Creates the Admin user.
            adminUser = new IdentityUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(
                adminUser,
                adminPassword);

            // Stops startup if the Admin user could not be created.
            if (!result.Succeeded)
            {
                var errors = string.Join(
                    "; ",
                    result.Errors.Select(error => error.Description));

                throw new InvalidOperationException(
                    $"Failed to create Admin user: {errors}");
            }
        }

        // Ensures the Admin user has the Admin role.
        if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
}