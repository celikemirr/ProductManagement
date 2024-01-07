using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProductManagement.WebUI.Areas.ManagementPanel.Controllers {
    [Authorize]
    [Area("ManagementPanel")]
    public class DashboardController : Controller {
        public IActionResult Index() {
            return View();
        }
    }
}
