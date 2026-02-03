using Microsoft.AspNetCore.Mvc;

namespace LineMessageApi.ExampleApi.Controllers
{
    /// <summary>
    /// 首頁控制器
    /// </summary>
    public sealed class HomeController : Controller
    {
        /// <summary>
        /// 控制台首頁
        /// </summary>
        [HttpGet("/")]
        public IActionResult Index()
        {
            // 回傳控制台頁面
            return View();
        }
    }
}
