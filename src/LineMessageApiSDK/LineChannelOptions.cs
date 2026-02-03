namespace LineMessageApiSDK;

/// <summary>
/// LINE Channel 設定（Access Token / Secret）
/// </summary>
public sealed class LineChannelOptions
{
    /// <summary>
    /// 預設設定節點名稱：LineChannel
    /// </summary>
    public const string SectionName = "LineChannel";

    /// <summary>
    /// Channel Access Token
    /// </summary>
    public string ChannelAccessToken { get; init; } = string.Empty;

    /// <summary>
    /// Channel Secret
    /// </summary>
    public string ChannelSecret { get; init; } = string.Empty;
}
