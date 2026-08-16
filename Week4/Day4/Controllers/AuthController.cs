using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// Provides JWT classes used to create the token.
using System.IdentityModel.Tokens.Jwt;

// Provides Claim classes used to store information about the user.
using System.Security.Claims;

// Provides classes used to create the JWT signature.
using Microsoft.IdentityModel.Tokens;

// Provides UTF-8 encoding for the JWT secret key.
using System.Text;

// Provides the [Authorize] attribute used to protect API endpoints.
using Microsoft.AspNetCore.Authorization;


// Handles authentication-related operations such as user registration and login.
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    // UserManager manages Identity users and is used for registration.
    private readonly UserManager<IdentityUser> _userManager;

    // SignInManager verifies the submitted login credentials.
    private readonly SignInManager<IdentityUser> _signInManager;

    // RoleManager manages application roles such as User and Admin.
    private readonly RoleManager<IdentityRole> _roleManager;

    // IConfiguration reads settings such as the JWT key, issuer, and audience.
    private readonly IConfiguration _configuration;


    // ASP.NET Core Dependency Injection provides these services automatically.
    public AuthController(
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager,
        RoleManager<IdentityRole> roleManager,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _configuration = configuration;
    }


    // Registers a new user.
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        // Create a new Identity user using the email from the request.
        var user = new IdentityUser
        {
            UserName = request.Email,
            Email = request.Email
        };

        // CreateAsync validates the user and password,
        // hashes the password, and saves the user if valid.
        var result = await _userManager.CreateAsync(user, request.Password);

        // Return validation errors if registration fails.
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        // Return success after the user is created.
        return Ok(new
        {
            message = "User registered successfully."
        });
    }

    // Creates the User and Admin roles if they do not already exist.
    [HttpPost("create-roles")]
    public async Task<IActionResult> CreateRoles()
    {
        // Define the roles required by the application.
        var roles = new[] { "User", "Admin" };

        // Check each role and create it only if it does not already exist.
        foreach (var role in roles)
        {
            // Check whether the role already exists in ASP.NET Core Identity.
            if (!await _roleManager.RoleExistsAsync(role))
            {
                // Create the role in the Identity database.
                await _roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        // Return success after ensuring both roles exist.
        return Ok(new
        {
            message = "User and Admin roles are ready."
        });
    }

    // Assigns existing users to the required roles.
    [HttpPost("assign-roles")]
    public async Task<IActionResult> AssignRoles()
    {
        // Find the user that should receive the User role.
        var user = await _userManager.FindByEmailAsync("testuser@gmail.com");

        // Find the user that should receive the Admin role.
        var admin = await _userManager.FindByEmailAsync("yara@gmail.com");

        // Make sure both users exist before assigning roles.
        if (user == null || admin == null)
        {
            return NotFound("One or both users were not found.");
        }

        // Add the first user to the User role.
        await _userManager.AddToRoleAsync(user, "User");

        // Add the second user to the Admin role.
        await _userManager.AddToRoleAsync(admin, "Admin");

        // Return success after assigning both roles.
        return Ok(new
        {
            message = "Roles assigned successfully."
        });
    }

    // Changes the password of an existing user.
    // This endpoint is temporary and is being used for our Day 3 training.
    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword()
    {
        // Find Yara's existing Identity user by email.
        var user = await _userManager.FindByEmailAsync("yara@gmail.com");

        // If the user does not exist, return 404 Not Found.
        if (user == null)
        {
            return NotFound("User not found.");
        }

        // Create a temporary password-reset token for this user.
        // Identity generates this token so the password can be changed safely.
        var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

        // Define the new password that we will use for testing.
        var newPassword = "YaraPassword123!";

        // Use Identity to validate and hash the new password,
        // then store the resulting password hash for this user.
        var result = await _userManager.ResetPasswordAsync(
            user,
            resetToken,
            newPassword);

        // If the password does not satisfy Identity's password rules,
        // return the validation errors.
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        // Return success after the password has been changed.
        return Ok(new
        {
            message = "Password changed successfully."
        });
    }



    // Logs in an existing user and returns a JWT when the credentials are valid.
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        // Find the existing Identity user using the submitted email.
        var user = await _userManager.FindByEmailAsync(request.Email);

        // If the email does not belong to an existing user, reject the login.
        if (user == null)
        {
            return Unauthorized();
        }

        // Check whether the submitted password matches the stored password hash.
        var result = await _signInManager.CheckPasswordSignInAsync(
            user,
            request.Password,
            lockoutOnFailure: false);

        // If the password is incorrect, reject the login attempt.
        if (!result.Succeeded)
        {
            return Unauthorized();
        }


        // Get all roles assigned to the authenticated user.
        var roles = await _userManager.GetRolesAsync(user);

        // Create the basic claims for the JWT.
        var claims = new List<Claim>
{
    // Store the user's ID in the standard JWT "sub" claim.
    new Claim(JwtRegisteredClaimNames.Sub, user.Id),

    // Store the user's email in the JWT.
    new Claim(ClaimTypes.Email, user.Email!)
};

        // Add each user role as a role claim inside the JWT.
        foreach (var role in roles)
        {
            // Role claims allow [Authorize(Roles = "...")]
            // to check the user's role later.
            claims.Add(new Claim(ClaimTypes.Role, role));
        }


        // Read the secret key used to sign the JWT.
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));


        // Create signing credentials using the secret key and HMAC-SHA256.
        var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256);


        // Create the JWT itself.
        var token = new JwtSecurityToken(
            // Identify the application that issued the token.
            issuer: _configuration["Jwt:Issuer"],

            // Identify the application for which the token is intended.
            audience: _configuration["Jwt:Audience"],

            // Add the user's claims to the token payload.
            claims: claims,

            // Make the token expire after 15 minutes.
            expires: DateTime.UtcNow.AddMinutes(15),

            // Sign the token using our secret key.
            signingCredentials: credentials
        );


        // Convert the JWT object into the string sent to the client.
        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);


        // Return the generated JWT to the client.
        return Ok(new
        {
            token = tokenString
        });
    }


    // This endpoint is protected by JWT authentication.
    // A valid and non-expired JWT must be provided to access it.
    [Authorize]
    [HttpGet("protected")]
    public IActionResult Protected()
    {
        // This response is returned only after the JWT has been successfully validated.
        return Ok(new
        {
            message = "You accessed a protected endpoint successfully."
        });
    }
    // This endpoint can only be accessed by authenticated users
    // who have the Admin role.
    [Authorize(Roles = "Admin")]
    [HttpGet("admin-only")]
    public IActionResult AdminOnly()
    {
        // This response is returned only when the JWT is valid
        // and the user has the Admin role.
        return Ok(new
        {
            message = "Welcome Admin. You can access this endpoint."
        });
    }

}