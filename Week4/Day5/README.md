Day 5 — Securing the API
Overview

Day 5 focused on hardening the Training Management API by implementing the required security configurations and verifying them through practical testing.

During this day, we completed the following tasks:

Configured rate limiting with a stricter policy for the login endpoint.
Configured a named CORS policy with a specific allowed origin.
Enabled HTTPS redirection and HSTS.
Reviewed the codebase for raw SQL queries and confirmed that no matching raw SQL usage exists.
Tested the implemented security configurations using Postman where applicable.
Task 1 — Rate Limiting
Implementation

We used ASP.NET Core's built-in rate limiting functionality and configured two named policies in Program.cs:

LoginPolicy

The login endpoint was given a stricter limit:

5 requests per minute
options.AddFixedWindowLimiter("LoginPolicy", limiterOptions =>
{
    limiterOptions.PermitLimit = 5;
    limiterOptions.Window = TimeSpan.FromMinutes(1);
    limiterOptions.QueueLimit = 0;
});

The policy was applied to the login endpoint in Controllers/AuthController.cs:

[EnableRateLimiting("LoginPolicy")]
[HttpPost("login")]
public async Task<IActionResult> Login(LoginRequest request)
GeneralPolicy

A less restrictive policy was configured for general endpoints:

100 requests per minute
options.AddFixedWindowLimiter("GeneralPolicy", limiterOptions =>
{
    limiterOptions.PermitLimit = 100;
    limiterOptions.Window = TimeSpan.FromMinutes(1);
    limiterOptions.QueueLimit = 0;
});

This policy was applied to the participant GetById endpoint in Controllers/ParticipantsController.cs:

[EnableRateLimiting("GeneralPolicy")]
[HttpGet("{id}")]
public async Task<IActionResult> GetById(int id)

We also configured the application to return 429 Too Many Requests when a rate limit is exceeded:

options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
Testing

We tested both rate-limiting policies using Postman.

For the login endpoint, repeated requests were sent to:

POST http://localhost:5030/api/auth/login

After exceeding the configured limit, the API returned:

429 Too Many Requests

The general policy was also applied to:

GET http://localhost:5030/api/participants/1
Related Files
Program.cs
Controllers/AuthController.cs
Controllers/ParticipantsController.cs
Task 2 — CORS
Implementation

We configured a named CORS policy in Program.cs:

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("https://myapp.com")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

The policy is named:

AllowFrontend

and the configured allowed origin is:

https://myapp.com

The policy was then enabled in the middleware pipeline:

app.UseCors("AllowFrontend");
Testing

We tested the CORS configuration by sending requests with different origins.

The configured origin:

https://myapp.com

was used as the allowed origin.

We also tested a different origin that was not included in the configured policy.

CORS enforcement is performed by browsers. Postman was used to send requests and inspect the resulting request/response behavior.

Related File
Program.cs
Task 3 — HTTPS Redirection & HSTS
Implementation

We enabled HSTS and HTTPS redirection in the middleware pipeline in Program.cs:

app.UseHsts();


app.UseHttpsRedirection();

The local application URLs are configured in Properties/launchSettings.json:

http://localhost:5030
https://localhost:7135

This completed the HTTPS and HSTS configuration required by the Day 5 task.

Related Files
Program.cs
Properties/launchSettings.json
Task 4 — SQL Injection Prevention Review
Implementation

For this task, we reviewed the C# codebase to check whether raw SQL queries were being used.

We searched all C# files using:

Get-ChildItem -Recurse -Filter *.cs | Select-String -Pattern "FromSql|ExecuteSql|SqlQuery"

The command returned no results.

Therefore, no matching usage of:

FromSql
ExecuteSql
SqlQuery

was found in the project.

The database operations we implemented use Entity Framework Core and LINQ, for example in Controllers/ParticipantsController.cs:

var participant = await _context.Participants
    .FirstOrDefaultAsync(p => p.Id == id);

No raw SQL query was added as part of Day 5.

Final Day 5 Implementation

The main security-related changes are located in:

Program.cs

Contains:

Rate limiting policies
CORS policy
HTTPS redirection
HSTS
Rate limiter middleware
CORS middleware
Controllers/AuthController.cs

Contains:

Login endpoint
LoginPolicy rate limiting
Controllers/ParticipantsController.cs

Contains:

Protected participant endpoints
GeneralPolicy rate limiting
Existing authorization rules
Properties/launchSettings.json

Contains:

Local HTTP URL
Local HTTPS URL
Testing Summary

The Day 5 implementation was verified through the following tests:

Task	Test	Result
Rate Limiting	Login endpoint	429 Too Many Requests after exceeding the limit
Rate Limiting	General endpoint	General rate-limit policy configured
CORS	Allowed origin	Tested
CORS	Disallowed origin	Tested
HTTPS	HTTPS configuration	Configured
HSTS	HSTS middleware	Configured
SQL Injection	Raw SQL search	No matching raw SQL usage found
Day 5 Checklist
 Configure LoginPolicy
 Configure GeneralPolicy
 Apply stricter rate limiting to login
 Apply general rate limiting to a general endpoint
 Configure 429 Too Many Requests
 Test rate limiting using Postman
 Configure named AllowFrontend CORS policy
 Configure the allowed origin
 Test allowed and disallowed origins
 Enable HTTPS redirection
 Enable HSTS
 Review the codebase for raw SQL
 Confirm no matching raw SQL usage was found
Day 5 Result

By the end of Day 5, the Training Management API had the required hardening configurations implemented:

Rate limiting with different limits for login and general endpoints.
A named CORS policy with a specific allowed origin.
HTTPS redirection.
HSTS.
A codebase review confirming that no matching raw SQL usage was present.