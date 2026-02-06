using System.Text.RegularExpressions;
using System.Xml;

if (args.Length != 2)
{
    Console.Error.WriteLine("Usage: XmlEnglishifier <source-xml> <target-xml>");
    return 1;
}

var sourcePath = args[0];
var targetPath = args[1];

if (!File.Exists(sourcePath))
{
    Console.Error.WriteLine($"Source XML not found: {sourcePath}");
    return 2;
}

var doc = new XmlDocument();
doc.Load(sourcePath);

var members = doc.SelectNodes("/doc/members/member");
if (members is null)
{
    Console.Error.WriteLine("Invalid XML format: /doc/members/member not found.");
    return 3;
}

foreach (XmlNode member in members)
{
    var nameAttr = member.Attributes?["name"]?.Value;
    if (string.IsNullOrWhiteSpace(nameAttr))
    {
        continue;
    }

    var summary = BuildSummaryFromSource(member, nameAttr);

    var summaryNode = member.SelectSingleNode("summary");
    if (summaryNode is null)
    {
        summaryNode = doc.CreateElement("summary");
        member.AppendChild(summaryNode);
    }

    summaryNode.RemoveAll();
    summaryNode.InnerText = summary;

    RemoveNode(member, "param");
    RemoveNode(member, "typeparam");
    RemoveNode(member, "returns");
    RemoveNode(member, "remarks");
    RemoveNode(member, "example");
    RemoveNode(member, "exception");
    RemoveNode(member, "value");
}

var outputDir = Path.GetDirectoryName(targetPath);
if (!string.IsNullOrWhiteSpace(outputDir))
{
    Directory.CreateDirectory(outputDir);
}

var settings = new XmlWriterSettings
{
    Indent = true,
    IndentChars = "  ",
    NewLineChars = "\n",
    NewLineHandling = NewLineHandling.Replace
};

using (var writer = XmlWriter.Create(targetPath, settings))
{
    doc.Save(writer);
}

Console.WriteLine($"Converted: {sourcePath} -> {targetPath}");
return 0;

static string BuildSummaryFromSource(XmlNode member, string memberName)
{
    var sourceSummary = GetSourceSummary(member);
    if (!string.IsNullOrWhiteSpace(sourceSummary))
    {
        if (ContainsCjk(sourceSummary))
        {
            var translated = TranslateChineseSummary(sourceSummary, memberName);
            if (!string.IsNullOrWhiteSpace(translated))
            {
                return PolishSummary(translated);
            }
        }
        else
        {
            return PolishSummary(EnsureSentence(sourceSummary));
        }
    }

    return BuildSummary(memberName);
}

static void RemoveNode(XmlNode parent, string nodeName)
{
    var nodes = parent.SelectNodes(nodeName);
    if (nodes is null)
    {
        return;
    }

    foreach (XmlNode node in nodes)
    {
        parent.RemoveChild(node);
    }
}

static string BuildSummary(string memberName)
{
    if (memberName.Length < 3 || memberName[1] != ':')
    {
        return "API member.";
    }

    var kind = memberName[0];
    var body = memberName[2..];

    return kind switch
    {
        'T' => BuildTypeSummary(ExtractTypeName(body)),
        'P' => BuildPropertySummary(ExtractMemberName(body)),
        'F' => PolishSummary($"Stores the {ToWords(ExtractMemberName(body)).ToLowerInvariant()} value."),
        'E' => PolishSummary($"Raised when {ToWords(ExtractMemberName(body)).ToLowerInvariant()} is triggered."),
        'M' => BuildMethodSummary(body),
        _ => "API member."
    };
}

static string GetSourceSummary(XmlNode member)
{
    var summaryNodes = member.SelectNodes("summary");
    if (summaryNodes is null || summaryNodes.Count == 0)
    {
        return string.Empty;
    }

    foreach (XmlNode node in summaryNodes)
    {
        var text = NormalizeWhitespace(node.InnerText);
        if (!string.IsNullOrWhiteSpace(text))
        {
            return text;
        }
    }

    return string.Empty;
}

static string BuildMethodSummary(string body)
{
    var method = ExtractMemberName(body);
    var typeName = ExtractTypeNameForMethod(body);
    var containingType = ExtractContainingTypeForMethod(body);
    var isAsync = method.EndsWith("Async", StringComparison.Ordinal);
    var baseMethod = isAsync ? method[..^5] : method;
    var phrase = ToWords(baseMethod);
    var manual = GetManualMethodSummary(containingType, baseMethod);

    if (baseMethod == "#ctor")
    {
        return $"Initializes a new instance of {typeName}.";
    }

    string summary;
    if (!string.IsNullOrWhiteSpace(manual))
    {
        summary = manual;
    }
    else if (StartsWith(baseMethod, "Get"))
    {
        summary = BuildVerbObjectSummary("Retrieves", "the", baseMethod[3..], "value");
    }
    else if (StartsWith(baseMethod, "Set"))
    {
        summary = BuildVerbObjectSummary("Sets", "the", baseMethod[3..], "value");
    }
    else if (StartsWith(baseMethod, "Create"))
    {
        summary = BuildVerbObjectSummary("Creates", "a", baseMethod[6..], "resource");
    }
    else if (StartsWith(baseMethod, "Delete"))
    {
        summary = BuildVerbObjectSummary("Deletes", "the", baseMethod[6..], "resource");
    }
    else if (StartsWith(baseMethod, "Update"))
    {
        summary = BuildVerbObjectSummary("Updates", "the", baseMethod[6..], "resource");
    }
    else if (StartsWith(baseMethod, "Bulk"))
    {
        summary = BuildVerbObjectSummary("Performs a bulk operation for", "the", baseMethod[4..], "resource");
    }
    else if (StartsWith(baseMethod, "Send"))
    {
        summary = BuildVerbObjectSummary("Sends", "a", baseMethod[4..], "message");
    }
    else if (StartsWith(baseMethod, "Validate") || StartsWith(baseMethod, "Varidate"))
    {
        var rest = StartsWith(baseMethod, "Validate") ? baseMethod[8..] : baseMethod[8..];
        summary = BuildVerbObjectSummary("Validates", "the", rest, "data");
    }
    else if (StartsWith(baseMethod, "Upload"))
    {
        summary = BuildVerbObjectSummary("Uploads", "the", baseMethod[6..], "content");
    }
    else if (StartsWith(baseMethod, "Download"))
    {
        summary = BuildVerbObjectSummary("Downloads", "the", baseMethod[8..], "content");
    }
    else if (StartsWith(baseMethod, "Issue"))
    {
        summary = BuildVerbObjectSummary("Issues", "a", baseMethod[5..], "token");
    }
    else if (StartsWith(baseMethod, "Link"))
    {
        summary = BuildVerbObjectSummary("Links", "the", baseMethod[4..], "resource");
    }
    else if (StartsWith(baseMethod, "Unlink"))
    {
        summary = BuildVerbObjectSummary("Unlinks", "the", baseMethod[6..], "resource");
    }
    else if (StartsWith(baseMethod, "Cancel"))
    {
        summary = BuildVerbObjectSummary("Cancels", "the", baseMethod[6..], "operation");
    }
    else if (StartsWith(baseMethod, "Build"))
    {
        summary = BuildVerbObjectSummary("Builds", "the", baseMethod[5..], "resource");
    }
    else if (StartsWith(baseMethod, "Use"))
    {
        summary = BuildVerbObjectSummary("Enables", "the", baseMethod[3..], "feature");
    }
    else if (StartsWith(baseMethod, "With"))
    {
        summary = BuildVerbObjectSummary("Configures", "the", baseMethod[4..], "option");
    }
    else if (StartsWith(baseMethod, "Enable"))
    {
        summary = BuildVerbObjectSummary("Enables", "the", baseMethod[6..], "feature");
    }
    else if (StartsWith(baseMethod, "Disable"))
    {
        summary = BuildVerbObjectSummary("Disables", "the", baseMethod[7..], "feature");
    }
    else if (StartsWith(baseMethod, "Add"))
    {
        summary = BuildVerbObjectSummary("Adds", "the", baseMethod[3..], "service");
    }
    else if (StartsWith(baseMethod, "Post"))
    {
        summary = BuildVerbObjectSummary("Sends a POST request to", "the", baseMethod[4..], "API endpoint");
    }
    else if (StartsWith(baseMethod, "Put"))
    {
        summary = BuildVerbObjectSummary("Sends a PUT request to", "the", baseMethod[3..], "API endpoint");
    }
    else if (StartsWith(baseMethod, "Read"))
    {
        summary = BuildVerbObjectSummary("Reads", "the", baseMethod[4..], "content");
    }
    else if (StartsWith(baseMethod, "Write"))
    {
        summary = BuildVerbObjectSummary("Writes", "the", baseMethod[5..], "content");
    }
    else if (StartsWith(baseMethod, "Serialize"))
    {
        summary = "Serializes value.";
    }
    else if (StartsWith(baseMethod, "Deserialize"))
    {
        summary = "Deserializes value.";
    }
    else
    {
        summary = $"Performs {phrase.ToLowerInvariant()}.";
    }

    // Async methods intentionally use the same summary style as sync methods.
    return PolishSummary(summary);
}

static string TranslateChineseSummary(string summary, string memberName)
{
    var text = summary.Trim();
    text = Regex.Replace(text, @"\s+", " ");

    if (text.StartsWith("表示", StringComparison.Ordinal))
    {
        var entity = text
            .Replace("表示", string.Empty, StringComparison.Ordinal)
            .Replace("類別", string.Empty, StringComparison.Ordinal)
            .Replace("列舉", "enum", StringComparison.Ordinal)
            .Replace("介面", "interface", StringComparison.Ordinal)
            .Replace("結構", "struct", StringComparison.Ordinal)
            .Replace("。", string.Empty, StringComparison.Ordinal)
            .Trim();

        if (string.IsNullOrWhiteSpace(entity))
        {
            entity = ExtractMemberName(memberName[2..]);
        }

        return $"Represents {entity}.";
    }

    if (text.StartsWith("初始化", StringComparison.Ordinal))
    {
        return "Initializes a new instance.";
    }

    if (text.StartsWith("取得或設定", StringComparison.Ordinal))
    {
        return BuildPropertyFromChinese(text, "Specifies");
    }

    if (text.StartsWith("是否", StringComparison.Ordinal) ||
        text.StartsWith("指出是否", StringComparison.Ordinal))
    {
        return BuildPropertyFromChinese(text, "Indicates whether");
    }

    if (text.StartsWith("取得", StringComparison.Ordinal))
    {
        return BuildActionFromChinese(text, "Retrieves");
    }

    if (text.StartsWith("設定", StringComparison.Ordinal))
    {
        return BuildActionFromChinese(text, "Sets");
    }

    if (text.StartsWith("建立", StringComparison.Ordinal))
    {
        return BuildActionFromChinese(text, "Creates");
    }

    if (text.StartsWith("更新", StringComparison.Ordinal))
    {
        return BuildActionFromChinese(text, "Updates");
    }

    if (text.StartsWith("刪除", StringComparison.Ordinal))
    {
        return BuildActionFromChinese(text, "Deletes");
    }

    if (text.StartsWith("驗證", StringComparison.Ordinal))
    {
        return BuildActionFromChinese(text, "Validates");
    }

    if (text.StartsWith("測試", StringComparison.Ordinal))
    {
        return BuildActionFromChinese(text, "Tests");
    }

    if (text.StartsWith("傳送", StringComparison.Ordinal) ||
        text.StartsWith("發送", StringComparison.Ordinal))
    {
        return BuildActionFromChinese(text, "Sends");
    }

    if (text.StartsWith("上傳", StringComparison.Ordinal))
    {
        return BuildActionFromChinese(text, "Uploads");
    }

    if (text.StartsWith("下載", StringComparison.Ordinal))
    {
        return BuildActionFromChinese(text, "Downloads");
    }

    if (text.StartsWith("連結", StringComparison.Ordinal))
    {
        return BuildActionFromChinese(text, "Links");
    }

    if (text.StartsWith("解除連結", StringComparison.Ordinal))
    {
        return BuildActionFromChinese(text, "Unlinks");
    }

    if (text.StartsWith("取消", StringComparison.Ordinal))
    {
        return BuildActionFromChinese(text, "Cancels");
    }

    return BuildSummary(memberName);
}

static string BuildPropertyFromChinese(string text, string prefix)
{
    var target = CleanupChineseTail(text
        .Replace("取得或設定", string.Empty, StringComparison.Ordinal)
        .Replace("指出是否", string.Empty, StringComparison.Ordinal)
        .Replace("是否", string.Empty, StringComparison.Ordinal));

    if (string.IsNullOrWhiteSpace(target))
    {
        target = "value";
    }

    var translated = TranslateChineseNoun(target).Trim();
    if (prefix == "Indicates whether")
    {
        return $"{prefix} {translated}.";
    }

    return $"{prefix} the {translated}.";
}

static string BuildActionFromChinese(string text, string verb)
{
    var target = text;
    var actionWords = new[]
    {
        "取得或設定", "取得", "設定", "建立", "更新", "刪除", "驗證", "測試",
        "傳送", "發送", "上傳", "下載", "連結", "解除連結", "取消"
    };

    foreach (var word in actionWords)
    {
        if (target.StartsWith(word, StringComparison.Ordinal))
        {
            target = target[word.Length..];
            break;
        }
    }

    target = CleanupChineseTail(target);
    if (string.IsNullOrWhiteSpace(target))
    {
        target = "the requested resource";
    }

    var translated = TranslateChineseNoun(target).Trim();
    return $"{verb} {translated}.";
}

static string CleanupChineseTail(string value)
{
    var text = value
        .Replace("資料", "資料", StringComparison.Ordinal)
        .Replace("。", string.Empty, StringComparison.Ordinal)
        .Replace("。", string.Empty, StringComparison.Ordinal)
        .Replace("為", string.Empty, StringComparison.Ordinal)
        .Trim();

    text = Regex.Replace(text, @"\s+", " ");
    return text;
}

static string TranslateChineseNoun(string text)
{
    var result = text;
    var map = new Dictionary<string, string>(StringComparer.Ordinal)
    {
        ["機器人資訊"] = "bot information",
        ["Webhook 端點"] = "webhook endpoint",
        ["簽章"] = "webhook signature",
        ["簽名"] = "webhook signature",
        ["頻道存取權杖"] = "channel access token",
        ["頻道密鑰"] = "channel secret",
        ["使用者資料"] = "user data",
        ["使用者設定檔"] = "user profile",
        ["群組成員 IDs"] = "group member IDs",
        ["群組成員數"] = "group member count",
        ["房間成員 IDs"] = "room member IDs",
        ["房間成員數"] = "room member count",
        ["圖文選單"] = "rich menu",
        ["圖文選單別名"] = "rich menu alias",
        ["受眾群組"] = "audience group",
        ["訊息內容"] = "message content",
        ["訊息傳送成效"] = "message delivery insight data",
        ["好友數"] = "follower insight data",
        ["受眾屬性"] = "demographic insight data",
        ["HTTP 用戶端"] = "HTTP client",
        ["JSON 序列化器"] = "JSON serializer",
        ["SDK"] = "LINE SDK",
        ["ID"] = "ID",
        ["IDs"] = "IDs"
    };

    foreach (var pair in map)
    {
        result = result.Replace(pair.Key, pair.Value, StringComparison.Ordinal);
    }

    if (ContainsCjk(result))
    {
        // Fallback: keep unknown Chinese phrase but mark as target text.
        result = $"the requested data ({result})";
    }
    else if (!result.StartsWith("the ", StringComparison.OrdinalIgnoreCase) &&
             !result.StartsWith("a ", StringComparison.OrdinalIgnoreCase) &&
             !result.StartsWith("an ", StringComparison.OrdinalIgnoreCase))
    {
        result = $"the {result}";
    }

    return result;
}

static string NormalizeWhitespace(string text)
{
    return Regex.Replace(text ?? string.Empty, "\\s+", " ").Trim();
}

static string EnsureSentence(string text)
{
    var normalized = NormalizeWhitespace(text);
    if (string.IsNullOrWhiteSpace(normalized))
    {
        return normalized;
    }

    return normalized.EndsWith(".", StringComparison.Ordinal) ? normalized : normalized + ".";
}

static bool ContainsCjk(string text)
{
    foreach (var c in text)
    {
        if (c >= '\u4E00' && c <= '\u9FFF')
        {
            return true;
        }
    }

    return false;
}

static string BuildVerbObjectSummary(string verb, string article, string rawObject, string fallbackObject)
{
    var obj = ToWords(rawObject).ToLowerInvariant().Trim();
    if (string.IsNullOrWhiteSpace(obj))
    {
        obj = fallbackObject;
    }

    if (string.IsNullOrWhiteSpace(article))
    {
        return $"{verb} {obj}.";
    }

    return $"{verb} {article} {obj}.";
}

static string? GetManualMethodSummary(string containingType, string method)
{
    var normalized = method.Replace("_", "", StringComparison.Ordinal);
    var normalizedType = containingType.Replace("_", "", StringComparison.Ordinal);

    if ((normalizedType.EndsWith(".DefaultHttpClientProvider", StringComparison.Ordinal) ||
         normalizedType.EndsWith(".IHttpClientProvider", StringComparison.Ordinal)) &&
        normalized == "GetClient")
    {
        return "Retrieves an HTTP client instance.";
    }

    if ((normalizedType.EndsWith(".DefaultHttpClientSyncAdapterFactory", StringComparison.Ordinal) ||
         normalizedType.EndsWith(".IHttpClientSyncAdapterFactory", StringComparison.Ordinal)) &&
        normalized == "Create")
    {
        return "Creates an HTTP client sync adapter.";
    }

    if ((normalizedType.EndsWith(".HttpClientSyncAdapter", StringComparison.Ordinal) ||
         normalizedType.EndsWith(".IHttpClientSyncAdapter", StringComparison.Ordinal)) &&
        normalized == "GetString")
    {
        return "Retrieves response content as a string.";
    }

    if ((normalizedType.EndsWith(".HttpClientSyncAdapter", StringComparison.Ordinal) ||
         normalizedType.EndsWith(".IHttpClientSyncAdapter", StringComparison.Ordinal)) &&
        normalized == "GetByteArray")
    {
        return "Retrieves response content as a byte array.";
    }

    if (normalizedType.EndsWith(".ServiceCollectionExtensions", StringComparison.Ordinal) &&
        normalized == "AddLineSdk")
    {
        return "Registers LINE SDK services.";
    }

    return normalized switch
    {
        "TestWebhookEndpoint" => "Checks the webhook endpoint configuration.",
        "LeaveRoomOrGroup" => "Leaves a room or group.",
        "BulkLinkRichMenu" => "Links a rich menu to multiple users.",
        "BulkUnlinkRichMenu" => "Unlinks a rich menu from multiple users.",
        "GetBotInfo" => "Retrieves bot profile information.",
        "GetFollower" => "Retrieves follower insight data.",
        "GetDemographic" => "Retrieves demographic insight data.",
        "GetMessageDelivery" => "Retrieves message delivery insight data.",
        "ValidateSignature" => "Verifies the webhook signature.",
        "VaridateSignature" => "Verifies the webhook signature.",
        _ => null
    };
}

static string BuildTypeSummary(string typeName)
{
    if (string.IsNullOrWhiteSpace(typeName))
    {
        return "Represents an API type.";
    }

    if (typeName.StartsWith("I", StringComparison.Ordinal) &&
        typeName.Length > 1 &&
        char.IsUpper(typeName[1]))
    {
        return PolishSummary($"Defines the contract for {ToWords(typeName[1..]).ToLowerInvariant()}.");
    }

    if (typeName.EndsWith("Extensions", StringComparison.Ordinal))
    {
        if (typeName == "ServiceCollectionExtensions")
        {
            return "Provides extension methods for registering LINE SDK services.";
        }

        return PolishSummary($"Provides extension methods for {ToWords(typeName[..^10]).ToLowerInvariant()}.");
    }

    if (typeName.EndsWith("Api", StringComparison.Ordinal))
    {
        return PolishSummary($"Provides API operations for {ToWords(typeName[..^3]).ToLowerInvariant()}.");
    }

    if (typeName.EndsWith("Service", StringComparison.Ordinal))
    {
        return PolishSummary($"Provides service operations for {ToWords(typeName[..^7]).ToLowerInvariant()}.");
    }

    if (typeName.EndsWith("Builder", StringComparison.Ordinal))
    {
        return PolishSummary($"Builds and configures {ToWords(typeName[..^7]).ToLowerInvariant()}.");
    }

    if (typeName.EndsWith("Options", StringComparison.Ordinal))
    {
        return PolishSummary($"Specifies configuration options for {ToWords(typeName[..^7]).ToLowerInvariant()}.");
    }

    if (typeName.EndsWith("Request", StringComparison.Ordinal) ||
        typeName.EndsWith("Response", StringComparison.Ordinal) ||
        typeName.EndsWith("Message", StringComparison.Ordinal) ||
        typeName.EndsWith("Result", StringComparison.Ordinal) ||
        typeName.EndsWith("Summary", StringComparison.Ordinal) ||
        typeName.EndsWith("Profile", StringComparison.Ordinal) ||
        typeName.EndsWith("Context", StringComparison.Ordinal))
    {
        return PolishSummary($"Defines the data model for {ToWords(typeName).ToLowerInvariant()}.");
    }

    return PolishSummary($"Defines the type for {ToWords(typeName).ToLowerInvariant()}.");
}

static string BuildPropertySummary(string memberName)
{
    var words = ToWords(memberName).ToLowerInvariant().Trim();
    if (string.IsNullOrWhiteSpace(words))
    {
        return "Specifies the value.";
    }

    if (words.StartsWith("is ") || words.StartsWith("has ") || words.StartsWith("can "))
    {
        return PolishSummary($"Indicates whether {words}.");
    }

    return PolishSummary($"Specifies the {words}.");
}

static string PolishSummary(string summary)
{
    var result = summary;

    result = Regex.Replace(result, @"\bbot info\b", "bot information", RegexOptions.IgnoreCase);
    result = Regex.Replace(result, @"\buser datas\b", "user data", RegexOptions.IgnoreCase);
    result = Regex.Replace(result, @"\bmuticast\b", "multicast", RegexOptions.IgnoreCase);
    result = Regex.Replace(result, @"\bvaridate\b", "validate", RegexOptions.IgnoreCase);

    result = Regex.Replace(result, @"\bhttp\b", "HTTP", RegexOptions.IgnoreCase);
    result = Regex.Replace(result, @"\bjson\b", "JSON", RegexOptions.IgnoreCase);
    result = Regex.Replace(result, @"\bsdk\b", "SDK", RegexOptions.IgnoreCase);
    result = Regex.Replace(result, @"\bapi\b", "API", RegexOptions.IgnoreCase);
    result = Regex.Replace(result, @"\bline sdk\b", "LINE SDK", RegexOptions.IgnoreCase);

    result = Regex.Replace(result, @"\bids\b", "IDs", RegexOptions.IgnoreCase);
    result = Regex.Replace(result, @"\bid\b", "ID", RegexOptions.IgnoreCase);

    // Clean up accidental duplicate "type" suffixes from enum-like names.
    result = Regex.Replace(result, @"\btype type\b", "type", RegexOptions.IgnoreCase);

    return result;
}

static bool StartsWith(string text, string prefix)
{
    return text.StartsWith(prefix, StringComparison.Ordinal);
}

static string ExtractTypeName(string body)
{
    var noGeneric = body.Split('{')[0];
    var withoutArgs = noGeneric.Split('(')[0];
    var hashIndex = withoutArgs.IndexOf('#');
    var core = hashIndex >= 0 ? withoutArgs[..hashIndex] : withoutArgs;
    var lastDot = core.LastIndexOf('.');
    var typeName = lastDot >= 0 ? core[(lastDot + 1)..] : core;
    var tick = typeName.IndexOf('`');
    if (tick >= 0)
    {
        typeName = typeName[..tick];
    }

    return typeName;
}

static string ExtractTypeNameForMethod(string body)
{
    var methodToken = body.Split('(')[0];
    var lastDot = methodToken.LastIndexOf('.');
    if (lastDot < 0)
    {
        return "the current type";
    }

    var typePath = methodToken[..lastDot];
    var typeLastDot = typePath.LastIndexOf('.');
    return typeLastDot >= 0 ? typePath[(typeLastDot + 1)..] : typePath;
}

static string ExtractContainingTypeForMethod(string body)
{
    var methodToken = body.Split('(')[0];
    var lastDot = methodToken.LastIndexOf('.');
    if (lastDot < 0)
    {
        return string.Empty;
    }

    return methodToken[..lastDot];
}

static string ExtractMemberName(string body)
{
    var token = body.Split('(')[0];
    var lastDot = token.LastIndexOf('.');
    if (lastDot < 0)
    {
        return token;
    }

    return token[(lastDot + 1)..];
}

static string ToWords(string name)
{
    if (string.IsNullOrWhiteSpace(name))
    {
        return name;
    }

    var normalized = name.Replace("_", " ");
    normalized = Regex.Replace(normalized, "([a-z0-9])([A-Z])", "$1 $2");
    normalized = Regex.Replace(normalized, "\\s+", " ").Trim();
    return normalized;
}
