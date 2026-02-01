namespace LineMessageApi.ExampleApi;

public sealed class LineChannelOptions
{
    public const string SectionName = "LineChannel";

    public string? ChannelAccessToken { get; init; }

    public string? ChannelSecret { get; init; }
}
