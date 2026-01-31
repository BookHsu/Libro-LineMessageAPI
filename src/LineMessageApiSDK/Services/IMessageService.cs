using LineMessageApiSDK.LineMessageObject;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LineMessageApiSDK.Services
{
    /// <summary>
    /// 訊息服務介面
    /// </summary>
    public interface IMessageService
    {
        /// <summary>
        /// 取得使用者上傳的檔案內容
        /// </summary>
        /// <param name="messageId">訊息 ID</param>
        /// <returns>檔案內容</returns>
        byte[] GetMessageContent(string messageId);

        /// <summary>
        /// 取得使用者上傳的檔案內容（非同步）
        /// </summary>
        /// <param name="messageId">訊息 ID</param>
        /// <returns>檔案內容</returns>
        Task<byte[]> GetMessageContentAsync(string messageId);

        /// <summary>
        /// 回覆訊息
        /// </summary>
        /// <param name="replyToken">回覆 Token</param>
        /// <param name="message">訊息內容</param>
        /// <returns>結果字串</returns>
        string SendReplyMessage(string replyToken, params Message[] message);

        /// <summary>
        /// 回覆訊息（非同步）
        /// </summary>
        /// <param name="replyToken">回覆 Token</param>
        /// <param name="message">訊息內容</param>
        /// <returns>結果字串</returns>
        Task<string> SendReplyMessageAsync(string replyToken, params Message[] message);

        /// <summary>
        /// 推播訊息
        /// </summary>
        /// <param name="toId">接收者 ID</param>
        /// <param name="message">訊息內容</param>
        /// <returns>結果字串</returns>
        string SendPushMessage(string toId, params Message[] message);

        /// <summary>
        /// 推播訊息（非同步）
        /// </summary>
        /// <param name="toId">接收者 ID</param>
        /// <param name="message">訊息內容</param>
        /// <returns>結果字串</returns>
        Task<string> SendPushMessageAsync(string toId, params Message[] message);

        /// <summary>
        /// 群發訊息
        /// </summary>
        /// <param name="toIds">接收者 ID 清單</param>
        /// <param name="message">訊息內容</param>
        /// <returns>結果字串</returns>
        string SendMulticastMessage(List<string> toIds, params Message[] message);

        /// <summary>
        /// 群發訊息（非同步）
        /// </summary>
        /// <param name="toIds">接收者 ID 清單</param>
        /// <param name="message">訊息內容</param>
        /// <returns>結果字串</returns>
        Task<string> SendMulticastMessageAsync(List<string> toIds, params Message[] message);
    }
}
