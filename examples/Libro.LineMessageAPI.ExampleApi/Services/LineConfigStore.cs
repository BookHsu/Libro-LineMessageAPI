using Libro.LineMessageAPI.ExampleApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace Libro.LineMessageAPI.ExampleApi.Services
{
    /// <summary>
    /// Line 設定與事件的記憶體存放
    /// </summary>
    public sealed class LineConfigStore
    {
        private readonly object gate = new object();
        private LineConfig? config;
        private readonly List<WebhookEventRecord> events = new List<WebhookEventRecord>();

        /// <summary>
        /// 更新 Line 設定
        /// </summary>
        /// <param name="newConfig">新設定</param>
        public void Update(LineConfig newConfig)
        {
            // 使用 lock 確保執行緒安全
            lock (gate)
            {
                config = newConfig;
            }
        }

        /// <summary>
        /// 取得目前設定
        /// </summary>
        /// <returns>Line 設定</returns>
        public LineConfig? Get()
        {
            // 使用 lock 確保資料一致
            lock (gate)
            {
                return config;
            }
        }

        /// <summary>
        /// 新增 webhook 事件
        /// </summary>
        /// <param name="record">事件記錄</param>
        public void AddEvent(WebhookEventRecord record)
        {
            // 使用 lock 確保執行緒安全
            lock (gate)
            {
                events.Insert(0, record);
                if (events.Count > 200)
                {
                    events.RemoveRange(200, events.Count - 200);
                }
            }
        }

        /// <summary>
        /// 取得事件清單
        /// </summary>
        /// <returns>事件清單</returns>
        public IReadOnlyList<WebhookEventRecord> GetEvents()
        {
            // 回傳副本避免外部修改
            lock (gate)
            {
                return events.ToList();
            }
        }
    }
}



