using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// إضافة خدمة OpenAPI للتطبيق.
builder.Services.AddOpenApi();

// تسجيل DbContext داخل Dependency Injection.
// وربطه بقاعدة بيانات SQL Server باستخدام Connection String.
builder.Services.AddDbContext<TrainingManagementDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// إعداد OpenAPI في بيئة التطوير.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// إعادة توجيه الطلبات من HTTP إلى HTTPS.
app.UseHttpsRedirection();

// بيانات تجريبية موجودة افتراضيًا في مشروع ASP.NET Core.
var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild",
    "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

// Endpoint تجريبي لإرجاع بيانات الطقس.
app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();

    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

// Record يمثل بيانات الطقس التجريبية.
record WeatherForecast(
    DateOnly Date,
    int TemperatureC,
    string? Summary)
{
    // تحويل درجة الحرارة من Celsius إلى Fahrenheit.
    public int TemperatureF =>
        32 + (int)(TemperatureC / 0.5556);
}