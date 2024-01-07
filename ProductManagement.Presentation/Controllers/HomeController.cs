using Microsoft.AspNetCore.Mvc;
using ProductManagement.Presentation.Models;
using ProductManagement.Presentation.Models.ViewModels;
using ProductManagement.Presentation.Utilities;
using System.Diagnostics;

namespace ProductManagement.Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var res = await ProviderHelper.Get("https://localhost:7092/api/Message");
            return View(res);
        }

        public async Task<IActionResult> Contact()
        {
            ViewBag.Message = "";
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Contact(MessageCreateViewModel model)
        {
            var res = await ProviderHelper.Post("https://localhost:7092/api/Message", model);
            ViewBag.Message = "Teşekkürler mesajınız gönderildi" + res.fullName + "mesaj idsi ;" + res.id;

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}