using LineMessageApi.ExampleApi.Hubs;
using LineMessageApi.ExampleApi.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// 設定 API JSON 序列化，讓 enum 以字串輸出
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

// 註冊 SignalR 以推送 webhook 事件
builder.Services.AddSignalR();

// 註冊記憶體設定與事件存放
builder.Services.AddSingleton<LineConfigStore>();

var app = builder.Build();

app.UseHttpsRedirection();

// 啟用預設檔案與靜態檔案服務
app.UseDefaultFiles();
app.UseStaticFiles();

app.MapGet("/health", () =>
    Results.Ok(new { status = "ok", utc = DateTimeOffset.UtcNow }));

// 註冊 API 控制器
app.MapControllers();

// 註冊 SignalR Hub
app.MapHub<LineWebhookHub>("/hubs/line-webhook");

app.Run();
