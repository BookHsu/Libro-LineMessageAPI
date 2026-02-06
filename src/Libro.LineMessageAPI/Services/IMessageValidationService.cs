using Libro.LineMessageApi.SendMessage;
using System.Threading.Tasks;

namespace Libro.LineMessageApi.Services
{
    /// <summary>
    /// 訊息驗證服務
    /// </summary>
    public interface IMessageValidationService
    {
        /// <summary>
        /// 驗證 Reply 訊息
        /// </summary>
        bool ValidateReply(ReplyMessage message);

        /// <summary>
        /// 驗證 Reply 訊息
        /// </summary>
        Task<bool> ValidateReplyAsync(ReplyMessage message);

        /// <summary>
        /// 驗證 Push 訊息
        /// </summary>
        bool ValidatePush(PushMessage message);

        /// <summary>
        /// 驗證 Push 訊息
        /// </summary>
        Task<bool> ValidatePushAsync(PushMessage message);

        /// <summary>
        /// 驗證 Multicast 訊息
        /// </summary>
        bool ValidateMulticast(MulticastMessage message);

        /// <summary>
        /// 驗證 Multicast 訊息
        /// </summary>
        Task<bool> ValidateMulticastAsync(MulticastMessage message);

        /// <summary>
        /// 驗證 Broadcast 訊息
        /// </summary>
        bool ValidateBroadcast(BroadcastMessage message);

        /// <summary>
        /// 驗證 Broadcast 訊息
        /// </summary>
        Task<bool> ValidateBroadcastAsync(BroadcastMessage message);

        /// <summary>
        /// 驗證 Narrowcast 訊息
        /// </summary>
        bool ValidateNarrowcast(NarrowcastMessage message);

        /// <summary>
        /// 驗證 Narrowcast 訊息
        /// </summary>
        Task<bool> ValidateNarrowcastAsync(NarrowcastMessage message);
    }
}

