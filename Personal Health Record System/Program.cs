using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Personal_Health_Record_System.Data;

var builder = WebApplication.CreateBuilder(args);

// 加入資料庫連線設定
builder.Services.AddDbContext<Personal_Health_Record_SystemContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Personal_Health_Record_SystemContext")
    ?? throw new InvalidOperationException("Connection string 'Personal_Health_Record_SystemContext' not found.")));

// 加入 Session 服務
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // 設定 Session 逾時時間
    options.Cookie.HttpOnly = true; // 防止 JavaScript 存取
    options.Cookie.IsEssential = true; // 確保 Session Cookie 會隨請求一起傳送
});

// 加入控制器與視圖
builder.Services.AddControllersWithViews();

var app = builder.Build();

// 配置 HTTP 請求管道
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// 啟用 Session
app.UseSession();

app.UseAuthorization();

// 設定預設路由
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
