using Microsoft.EntityFrameworkCore;
using StudentManagementApi.Models;

var builder = WebApplication.CreateBuilder(args); 
// Tạo WebApplicationBuilder: đọc config (appsettings, env vars, command line),
// khởi tạo DI container (builder.Services), logging, và web host defaults.

// Add services
builder.Services.AddControllers();               // Đăng ký MVC controllers (API controllers)
builder.Services.AddEndpointsApiExplorer();      // Dùng để khám phá endpoints cho Swagger/OpenAPI
builder.Services.AddSwaggerGen();                // Thêm Swashbuckle (Swagger generator) cho API doc

// EF Core
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// Lấy chuỗi kết nối từ appsettings.json (hoặc env vars)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));
// Đăng ký AppDbContext với DI container, dùng SQL Server provider.
// (Cần using Microsoft.EntityFrameworkCore và package Microsoft.EntityFrameworkCore.SqlServer)

// Optional: enable CORS for testing
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
});
// Đăng ký chính sách CORS tên "AllowAll" cho phép mọi origin/header/method (chỉ nên dùng cho dev)

// Build app (tạo WebApplication từ builder)
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();      // Middleware: sinh swagger.json
    app.UseSwaggerUI();    // Middleware: giao diện swagger (UI)
}

app.UseHttpsRedirection();   // Middleware: redirect HTTP -> HTTPS
app.UseCors("AllowAll");     // Áp dụng CORS policy (phải gọi trước MapControllers)
app.UseAuthorization();      // Middleware: kiểm tra [Authorize] (cần AddAuthentication nếu có auth)
app.MapControllers();        // Bật routing cho các controller với attribute routing

// (Thiếu app.Run(); để start host — nhớ gọi app.Run() ở cuối)


app.Run();
