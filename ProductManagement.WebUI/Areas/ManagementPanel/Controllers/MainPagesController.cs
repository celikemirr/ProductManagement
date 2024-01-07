using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Entities.Models;
using ProductManagement.WebUI.Helpers;

namespace ProductManagement.WebUI.Areas.ManagementPanel.Controllers {
    [Authorize]
    [Area("ManagementPanel")]
    public class MainPagesController : Controller {
        private readonly IWebHostEnvironment _environment;
        private readonly ProductManagementContext _context = new 
ProductManagementContext();
        public MainPagesController(IWebHostEnvironment environment) {
            _environment = environment;
        }
        // GET: ManagementPanel/MainPages
        public async Task<IActionResult> Index() {
            var mainPage =  await _context.MainPages.FirstOrDefaultAsync();
            return View(mainPage);
        }
          
        // GET: ManagementPanel/MainPages/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            if (id == null || _context.MainPages == null) {
                return NotFound();
            }

            var mainPage = await _context.MainPages.FindAsync(id);
            if (mainPage == null) {
                return NotFound();
            }
            return View(mainPage);
        }

        // POST: ManagementPanel/MainPages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MainPage mainPage, IFormFile? img) { 
            if (ModelState.IsValid) {
                try {
                    var editMainPage = await _context.MainPages.FindAsync(mainPage.Id);
                    if (img != null) {
                        editMainPage.ImageURL = await UploadHelper.UploadImageAsync(_environment, img);
                    }
                    editMainPage.Title = mainPage.Title;
                    editMainPage.Description = mainPage.Description;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException) {  
                        throw; 
                }
                return RedirectToAction(nameof(Index));
            }
            return View(mainPage);
        }
        
    }
}
