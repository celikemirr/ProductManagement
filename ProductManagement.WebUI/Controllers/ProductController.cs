using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Entities.Models;

namespace ProductManagement.WebUI.Controllers {
    public class ProductController : Controller {
        ProductManagementContext _context = new ProductManagementContext();
        public IActionResult Index() {
            var products = _context.Products.Where(x => x.Status == true).ToList();
            return View(products);
        }
        public IActionResult Details(int? id) {
            var product = _context.Products.Include("ProductDetail").FirstOrDefault(x => x.Id == id && x.Status == true);
            if (product != null) {
                product.ProductDetail.NumberOfViews++;
                _context.SaveChanges();
                return View(product);

            }
            return NotFound();
        }
    }
}
