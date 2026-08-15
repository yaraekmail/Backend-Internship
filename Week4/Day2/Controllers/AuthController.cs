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

    // IConfiguration reads settings such as the JWT key, issuer, and audience.
    private readonly IConfiguration _configuration;


    // ASP.NET Core Dependency Injection provides these services automatically.
    public AuthController(
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
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


        // Create claims containing information about the authenticated user.
        var claims = new[]
        {
            // Store the user's ID in the standard JWT "sub" (subject) claim.
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),

            // Store the user's email in the JWT.
            new Claim(ClaimTypes.Email, user.Email!)
        };


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
//             Set a very short expiration time temporarily so we can test token expiry.
// expires: DateTime.UtcNow.AddMinutes(1),

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

}