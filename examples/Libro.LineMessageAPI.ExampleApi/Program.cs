using Libro.LineMessageAPI.ExampleApi.Hubs;
using Libro.LineMessageAPI.ExampleApi.Services;
using Libro.LineMessageApi;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// 設定 MVC 與 API JSON 序列化，讓 enum 以字串輸出
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

// 註冊 SignalR 以推送 webhook 事件
builder.Services.AddSignalR();

// 綁定 LineChannel 設定（API 範例用）
builder.Services.Configure<LineChannelOptions>(
    builder.Configuration.GetSection(LineChannelOptions.SectionName));

// 註冊記憶體設定與事件存放
builder.Services.AddSingleton<LineConfigStore>();

var app = builder.Build();

app.UseHttpsRedirection();

// 啟用靜態檔案服務
app.UseStaticFiles();

app.MapGet("/health", () =>
    Results.Ok(new { status = "ok", utc = DateTimeOffset.UtcNow }));

// 註冊 API 控制器
app.MapControllers();

// 註冊 MVC 頁面路由
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// 註冊 SignalR Hub
app.MapHub<LineWebhookHub>("/hubs/line-webhook");

app.Run();





