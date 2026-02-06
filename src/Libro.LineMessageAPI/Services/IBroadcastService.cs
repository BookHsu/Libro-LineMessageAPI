using Libro.LineMessageApi.SendMessage;
using Libro.LineMessageApi.Types;
using System.Threading.Tasks;

namespace Libro.LineMessageApi.Services
{
    /// <summary>
    /// Broadcast / Narrowcast 服務
    /// </summary>
    public interface IBroadcastService
    {
        /// <summary>
        /// 發送 Broadcast
        /// </summary>
        bool SendBroadcast(BroadcastMessage message);

        /// <summary>
        /// 發送 Broadcast
        /// </summary>
        Task<bool> SendBroadcastAsync(BroadcastMessage message);

        /// <summary>
        /// 發送 Narrowcast
        /// </summary>
        bool SendNarrowcast(NarrowcastMessage message);

        /// <summary>
        /// 發送 Narrowcast
        /// </summary>
        Task<bool> SendNarrowcastAsync(NarrowcastMessage message);

        /// <summary>
        /// 取得 Narrowcast 進度
        /// </summary>
        NarrowcastProgressResponse GetNarrowcastProgress(string requestId);

        /// <summary>
        /// 取得 Narrowcast 進度
        /// </summary>
        Task<NarrowcastProgressResponse> GetNarrowcastProgressAsync(string requestId);
    }
}

