using Microsoft.AspNetCore.Mvc;

namespace ProductManagement.WebUI.Areas.ManagementPanel.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
