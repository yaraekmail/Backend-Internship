using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// Handles authentication-related operations such as user registration.
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;

    // UserManager is provided by ASP.NET Core Dependency Injection.
    public AuthController(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
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
}