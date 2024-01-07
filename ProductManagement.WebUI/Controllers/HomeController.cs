using Microsoft.AspNetCore.Mvc;
using ProductManagement.Entities.Models;
using ProductManagement.WebUI.Models;
using System.Diagnostics;

namespace ProductManagement.WebUI.Controllers {
    public class HomeController : Controller {
        private readonly ProductManagementContext _context = new ProductManagementContext();
        public IActionResult Index() {
            var mainPage = _context.MainPages.FirstOrDefault(x => x.Status == true);
            return View(mainPage);
        }

        public IActionResult About() {
            var about = _context.Abouts.FirstOrDefault();
            return View(about);
        }

        public IActionResult Contact() {
            ViewBag.Message = "";
            var contact = _context.Contacts.FirstOrDefault();
            return View(contact);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Contact(Message message) {
            if (ModelState.IsValid) {
                message.CreatedDate = DateTime.Now;
                message.Status = true;
                _context.Messages.Add(message);
                _context.SaveChanges();
            }
            else {
                ViewBag.Message = "Modeli bi kontrol et";
            }

         
            var contact = _context.Contacts.FirstOrDefault();
            return View(contact); 
        }

        public IActionResult Privacy() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}