using CardiacPatientMonitoring.Api.Controllers;
using CardiacPatientMonitoring.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
// Provides ASP.NET Core action result types such as OkObjectResult.
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

// Contains unit tests for the AuthController.
public class AuthControllerTests
{
    // Creates a mock UserManager so the test does not use the real Identity database.
    private readonly Mock<UserManager<IdentityUser>> _mockUserManager;

    // Creates a mock SignInManager so the test does not perform real password validation.
    private readonly Mock<SignInManager<IdentityUser>> _mockSignInManager;
    // Stores test JWT configuration values used when creating the controller.
    private readonly IConfiguration _configuration;
    // Creates the AuthController using the mocked dependencies.
    private AuthController CreateController()
    {
        return new AuthController(
            _mockUserManager.Object,
            _mockSignInManager.Object,
            _configuration);
    }


    [Fact]
    public async Task Login_WhenCredentialsAreValid_ReturnsOkWithToken()
    {
        // Arrange: create a fake user that the mock UserManager will return.
        var user = new IdentityUser
        {
            Id = Guid.NewGuid().ToString(),
            Email = "test@example.com",
            UserName = "test@example.com"
        };

        // Tell the mock UserManager to return the fake user for this email.
        _mockUserManager
            .Setup(manager => manager.FindByEmailAsync("test@example.com"))
            .ReturnsAsync(user);

        // Tell the mock SignInManager that the password is correct.
        _mockSignInManager
            .Setup(manager => manager.CheckPasswordSignInAsync(
                user,
                "Password123!",
                false))
            .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);

        // Tell the mock UserManager that this user has the User role.
        _mockUserManager
            .Setup(manager => manager.GetRolesAsync(user))
            .ReturnsAsync(new List<string> { "User" });

        // Create the controller using the mocked dependencies.
        var controller = CreateController();

        // Create the login request with the test credentials.
        var request = new LoginRequest
        {
            Email = "test@example.com",
            Password = "Password123!"
        };

        // Act: call the Login endpoint directly.
        var result = await controller.Login(request);

        // Assert: verify that the login returned HTTP 200 OK.
        var okResult = Assert.IsType<Microsoft.AspNetCore.Mvc.OkObjectResult>(result);

        // Verify that the response contains a JWT token.
        Assert.NotNull(okResult.Value);
    }




    public AuthControllerTests()
    {
        // UserManager has many dependencies, so Moq needs mocked dependencies to create it.
        _mockUserManager = new Mock<UserManager<IdentityUser>>(
            Mock.Of<IUserStore<IdentityUser>>(),
            null!,
            null!,
            null!,
            null!,
            null!,
            null!,
            null!,
            null!);

        // Creates the SignInManager mock using the mocked UserManager.
        _mockSignInManager = new Mock<SignInManager<IdentityUser>>(
            _mockUserManager.Object,
            Mock.Of<IHttpContextAccessor>(),
            Mock.Of<IUserClaimsPrincipalFactory<IdentityUser>>(),
            null!,
            null!,
            null!,
            null!);
        // Creates test JWT settings without using the real application configuration.
        _configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["Jwt:Key"] = "ThisIsATestSecretKeyThatIsLongEnough123!",
                ["Jwt:Issuer"] = "CardiacPatientMonitoringTest",
                ["Jwt:Audience"] = "CardiacPatientMonitoringTestUsers"
            })
            .Build();
    }
}