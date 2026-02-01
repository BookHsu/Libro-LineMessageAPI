using LineMessageApi.ExampleApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<LineChannelOptions>(
    builder.Configuration.GetSection(LineChannelOptions.SectionName));
builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapGet("/", () => Results.Ok(new { message = "LineMessageApi Example API" }));

app.MapGet("/health", () =>
    Results.Ok(new { status = "ok", utc = DateTimeOffset.UtcNow }));
app.MapControllers();

app.Run();
