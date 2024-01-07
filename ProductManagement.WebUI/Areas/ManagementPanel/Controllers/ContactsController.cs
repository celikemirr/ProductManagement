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
    public class ContactsController : Controller {
        private readonly IWebHostEnvironment _environment;
        private readonly ProductManagementContext _context = new
ProductManagementContext();
        public ContactsController(IWebHostEnvironment environment) {
            _environment = environment;
        }

        // GET: ManagementPanel/Contacts
        public async Task<IActionResult> Index() {
           return View(await _context.Contacts.FirstOrDefaultAsync());
        }

        
        // GET: ManagementPanel/Contacts/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            if (id == null || _context.Contacts == null) {
                return NotFound();
            }

            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null) {
                return NotFound();
            }
            return View(contact);
        }

        // POST: ManagementPanel/Contacts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit( Contact contact, IFormFile? img) { 
            if (ModelState.IsValid) {
                try {
                    var editContact = await _context.Contacts.FindAsync(contact.Id); 
                    if (img != null) {
                        editContact.ImageURL = await UploadHelper.UploadImageAsync(_environment, img);
                    }
                    editContact.Title = contact.Title;
                    editContact.Description = contact.Description;
                    editContact.Phone = contact.Phone;
                    editContact.Email = contact.Email;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException) {
                    if (!ContactExists(contact.Id)) {
                        return NotFound();
                    }
                    else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(contact);
        }
         
        private bool ContactExists(int id) {
            return (_context.Contacts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
