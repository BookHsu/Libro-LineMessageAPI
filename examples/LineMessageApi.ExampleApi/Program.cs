using LineMessageApi.ExampleApi;
using LineMessageApiSDK;
using Microsoft.Extensions.Options;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// 綁定 Line Channel 設定（由 appsettings 或環境變數提供）
builder.Services.Configure<LineChannelOptions>(
    builder.Configuration.GetSection(LineChannelOptions.SectionName));

// 設定 API JSON 反序列化選項（包含 enum string 與大小寫不敏感）
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

// 註冊 Line SDK，並啟用全部模組供範例 API 使用
builder.Services.AddSingleton(sp =>
{
    var options = sp.GetRequiredService<IOptions<LineChannelOptions>>().Value;
    var token = options.ChannelAccessToken ?? string.Empty;

    return new LineSdkBuilder(token)
        .UseWebhook() // Webhook 驗證模組
        .UseWebhookEndpoints() // Webhook Endpoint 管理
        .UseBot() // Bot 與群組/對話資訊
        .UseBroadcast() // Broadcast / Narrowcast
        .UseMessageValidation() // 訊息驗證
        .UseRichMenu() // Rich Menu 管理
        .UseInsight() // Insights 查詢
        .UseAudience() // Audience 管理
        .UseAccountLink() // Account Link
        .UseMessages() // 訊息收發
        .UseProfiles() // 使用者/成員檔案
        .UseGroups() // 群組/多人對話
        .Build();
});

var app = builder.Build();

app.UseHttpsRedirection();

app.MapGet("/", () => Results.Ok(new { message = "LineMessageApi Example API" }));

app.MapGet("/health", () =>
    Results.Ok(new { status = "ok", utc = DateTimeOffset.UtcNow }));
app.MapControllers();

app.Run();
