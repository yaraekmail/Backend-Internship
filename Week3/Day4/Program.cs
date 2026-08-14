using Microsoft.EntityFrameworkCore;


// إنشاء WebApplication Builder.
// هذا المسؤول عن تجهيز إعدادات وخدمات التطبيق قبل تشغيله.
var builder = WebApplication.CreateBuilder(args);

// إضافة خدمة OpenAPI للتطبيق.
// تسمح لنا بعرض توثيق الـ API واختبار الـ endpoints.
builder.Services.AddOpenApi();

// تسجيل خدمات الـ Controllers.
// هذا يخبر ASP.NET Core أن المشروع يحتوي على Controllers
// مثل ParticipantsController وأنه يجب اكتشافها وتشغيلها.
builder.Services.AddControllers();

// تسجيل DbContext داخل Dependency Injection.
// وربطه بقاعدة بيانات SQL Server باستخدام Connection String
// الموجودة في appsettings.json.
builder.Services.AddDbContext<TrainingManagementDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// بناء تطبيق الـ WebApplication بعد الانتهاء من إعداد الخدمات.
var app = builder.Build();

// إعداد OpenAPI في بيئة التطوير فقط.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// إعادة توجيه الطلبات من HTTP إلى HTTPS.
app.UseHttpsRedirection();

// تفعيل الـ Controllers وربط HTTP requests بالـ Controller المناسب.
// مثال:
// POST /api/participants
// سيتم توجيهه إلى ParticipantsController.
app.MapControllers();

// بيانات تجريبية موجودة افتراضيًا في مشروع ASP.NET Core.
// هذه البيانات مرتبطة بالـ WeatherForecast التجريبي.
var summaries = new[]
{
    "Freezing",
    "Bracing",
    "Chilly",
    "Cool",
    "Mild",
    "Warm",
    "Balmy",
    "Hot",
    "Sweltering",
    "Scorching"
};

// Endpoint تجريبي لإرجاع بيانات الطقس.
// هذا موجود من قالب مشروع ASP.NET Core الأصلي.
app.MapGet("/weatherforecast", () =>
{
    // إنشاء 5 سجلات تجريبية للطقس.
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            // إنشاء تاريخ لكل يوم قادم.
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),

            // إنشاء درجة حرارة عشوائية.
            Random.Shared.Next(-20, 55),

            // اختيار وصف عشوائي لدرجة الحرارة.
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();

    // إرجاع بيانات الطقس إلى الـ Client.
    return forecast;
})
// إعطاء اسم للـ endpoint.
.WithName("GetWeatherForecast");

// تشغيل التطبيق وانتظار HTTP requests.
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