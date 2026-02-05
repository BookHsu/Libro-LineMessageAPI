using Libro.LineMessageApi;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace Libro.LineMessageApi.Extensions;

/// <summary>
/// Line SDK DI/Options 擴充
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// 使用既有的 LineChannelOptions 註冊 LineSdk（預設只啟用 Messages 模組）
    /// </summary>
    public static IServiceCollection AddLineSdk(this IServiceCollection services)
    {
        return services.AddLineSdk(builder => builder.UseMessages());
    }

    /// <summary>
    /// 綁定設定檔並註冊 LineSdk（預設只啟用 Messages 模組）
    /// </summary>
    public static IServiceCollection AddLineSdk(
        this IServiceCollection services,
        IConfiguration configuration,
        string sectionName = LineChannelOptions.SectionName)
    {
        services.Configure<LineChannelOptions>(configuration.GetSection(sectionName));
        return services.AddLineSdk();
    }

    /// <summary>
    /// 使用既有的 LineChannelOptions 註冊 LineSdk（可自訂模組）
    /// </summary>
    public static IServiceCollection AddLineSdk(
        this IServiceCollection services,
        Action<LineSdkBuilder> configureBuilder)
    {
        if (configureBuilder is null)
        {
            throw new ArgumentNullException(nameof(configureBuilder));
        }

        services.AddSingleton(sp =>
        {
            var options = sp.GetRequiredService<IOptions<LineChannelOptions>>().Value;
            var token = options.ChannelAccessToken;
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new InvalidOperationException("LineChannel:ChannelAccessToken is not configured.");
            }

            var builder = new LineSdkBuilder(token);
            configureBuilder(builder);
            return builder.Build();
        });

        return services;
    }

    /// <summary>
    /// 綁定設定檔並註冊 LineSdk（可自訂模組）
    /// </summary>
    public static IServiceCollection AddLineSdk(
        this IServiceCollection services,
        IConfiguration configuration,
        Action<LineSdkBuilder> configureBuilder,
        string sectionName = LineChannelOptions.SectionName)
    {
        services.Configure<LineChannelOptions>(configuration.GetSection(sectionName));
        return services.AddLineSdk(configureBuilder);
    }

    /// <summary>
    /// 以程式碼設定 options 並註冊 LineSdk（可自訂模組）
    /// </summary>
    public static IServiceCollection AddLineSdk(
        this IServiceCollection services,
        Action<LineChannelOptions> configureOptions,
        Action<LineSdkBuilder> configureBuilder)
    {
        if (configureOptions is null)
        {
            throw new ArgumentNullException(nameof(configureOptions));
        }

        services.Configure(configureOptions);
        return services.AddLineSdk(configureBuilder);
    }
}

