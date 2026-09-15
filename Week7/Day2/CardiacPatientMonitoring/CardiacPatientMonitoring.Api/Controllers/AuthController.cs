using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CardiacPatientMonitoring.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using CardiacPatientMonitoring.Api.Data;
using CardiacPatientMonitoring.Api.Entities;
using Microsoft.EntityFrameworkCore;
namespace CardiacPatientMonitoring.Api.Controllers;
// Handles user registration and login.
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IConfiguration _configuration;
    private readonly CardiacPatientMonitoringDbContext _context;
    public AuthController(
       UserManager<IdentityUser> userManager,
       SignInManager<IdentityUser> signInManager,
       IConfiguration configuration,
       CardiacPatientMonitoringDbContext context)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
        _context = context;
    }

    // Registers a new user.
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var existingUser = await _userManager.FindByEmailAsync(request.Email);

        if (existingUser is not null)
        {
            return Conflict(new { message = "Email is already registered." });
        }
        // Starts a transaction for creating the Identity user and Patient together.
        await using var transaction = await _context.Database.BeginTransactionAsync();

        var user = new IdentityUser
        {
            UserName = request.Email,
            Email = request.Email
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            return BadRequest(new
            {
                message = "Registration failed.",
                errors = result.Errors.Select(e => e.Description)
            });
        }
        // Creates the Patient record linked to the new Identity user.
        var patient = new Patient
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            DateOfBirth = request.DateOfBirth,
            Gender = request.Gender,
            Email = request.Email,
            UserId = user.Id
        };

        _context.Patients.Add(patient);
        // New users receive the standard Patient role.
        await _userManager.AddToRoleAsync(user, "Patient");

        // Saves the Patient record to the database.
        await _context.SaveChangesAsync();

        // Commits the transaction after both records are saved successfully.
        await transaction.CommitAsync();

        return Ok(new
        {
            message = "User registered successfully."
        });
    }

    // Authenticates a user and returns a JWT token.
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null)
        {
            return Unauthorized(new { message = "Invalid email or password." });
        }

        var result = await _signInManager.CheckPasswordSignInAsync(
            user,
            request.Password,
            false);

        if (!result.Succeeded)
        {
            return Unauthorized(new { message = "Invalid email or password." });
        }

        var roles = await _userManager.GetRolesAsync(user);
        // Finds the Patient linked to the logged-in Identity user.
        var patient = await _context.Patients
            .FirstOrDefaultAsync(p => p.UserId == user.Id);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id),
            new(JwtRegisteredClaimNames.Email, user.Email!),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())

    };
        // Adds the Patient ID to the JWT when the user is linked to a Patient.
        if (patient is not null)
        {
            claims.Add(new Claim("patientId", patient.Id.ToString()));
        }

        // Adds the user's roles to the JWT claims.
        claims.AddRange(
            roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var key = _configuration["Jwt:Key"]
            ?? throw new InvalidOperationException("JWT Key is not configured.");

        var issuer = _configuration["Jwt:Issuer"]
            ?? throw new InvalidOperationException("JWT Issuer is not configured.");

        var audience = _configuration["Jwt:Audience"]
            ?? throw new InvalidOperationException("JWT Audience is not configured.");

        var signingKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(key));

        var credentials = new SigningCredentials(
            signingKey,
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(15),
            signingCredentials: credentials);

        return Ok(new
        {
            token = new JwtSecurityTokenHandler().WriteToken(token),
            expiration = token.ValidTo
        });
    }
}