using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Entities.Models;
using ProductManagement.WebUI.Helpers;

namespace ProductManagement.WebUI.Areas.ManagementPanel.Controllers {
    [Authorize]
    [Area("ManagementPanel")]
    public class ProductsController : Controller {
        private readonly ProductManagementContext _context = new ProductManagementContext();

        private readonly IWebHostEnvironment _environment;  

        public ProductsController(IWebHostEnvironment environment) {
            _environment = environment;
        }

        // GET: ManagementPanel/Products
        public async Task<IActionResult> Index() {
            var products = await _context.Products.Include("ProductDetail").ToListAsync();
            return View(products);
        }

        // GET: ManagementPanel/Products/Details/5
        public async Task<IActionResult> Details(int? id) {
            if (id == null || _context.Products == null) {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null) {
                return NotFound();
            }

            return View(product);
        }

        // GET: ManagementPanel/Products/Create
        public IActionResult Create() {
            return View();
        }
         
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product, IFormFile? img) {
            if (ModelState.IsValid) {
                if (img!= null) {
                    product.ImageURL = await UploadHelper.UploadImageAsync(_environment, img);
                }
                product.Status = true;
                product.CreatedDate = DateTime.Now;
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: ManagementPanel/Products/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            if (id == null || _context.Products == null) {
                return NotFound();
            }

            var product = await _context.Products.Include(t => t.ProductDetail).FirstOrDefaultAsync(r => r.Id == id);
            if (product == null) {
                return NotFound();
            }
            return View(product);
        }

        // POST: ManagementPanel/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Product product, IFormFile? img) { 
            if (ModelState.IsValid) {
                try {
                    var editProduct = await _context.Products.Include(x => x.ProductDetail).FirstOrDefaultAsync(t => t.Id == product.Id);
                    if (img != null) {
                        editProduct.ImageURL = await UploadHelper.UploadImageAsync(_environment, img);
                    }

                    editProduct.Status = product.Status;
                    editProduct.Name = product.Name;
                    editProduct.Price = product.Price;
                    editProduct.ProductDetail.Description = product.ProductDetail.Description;
                    editProduct.ProductDetail.IsBestSeller = product.ProductDetail.IsBestSeller; 
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException) {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

    }
}
