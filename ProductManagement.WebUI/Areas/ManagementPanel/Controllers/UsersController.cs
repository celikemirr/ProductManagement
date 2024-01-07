using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Entities.Models;

namespace ProductManagement.WebUI.Areas.ManagementPanel.Controllers {
    [Authorize]
    [Area("ManagementPanel")]
    public class UsersController : Controller {
        private readonly ProductManagementContext _context = new ProductManagementContext();

        // GET: ManagementPanel/Users
        public async Task<IActionResult> Index() {
            return View(await _context.Users.Where(x => x.Status == true).ToListAsync());
        }
         

        // GET: ManagementPanel/Users/Create
        public IActionResult Create() {
            return View();
        }

        // POST: ManagementPanel/Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( User user) {
            if (ModelState.IsValid) {
                user.Status = true;
                user.CreatedDate = DateTime.Now;
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: ManagementPanel/Users/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            if (id == null || _context.Users == null) {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null) {
                return NotFound();
            }
            return View(user);
        }

        // POST: ManagementPanel/Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(User user) { 
            if (ModelState.IsValid) {
                try {
                    var editUser = await _context.Users.FindAsync(user.Id);
                    editUser.Status = user.Status;
                    editUser.Email = user.Email;
                    editUser.LastName = user.LastName;
                    editUser.FirstName = user.FirstName;
                    editUser.Password = user.Password;
                    editUser.Phone = user.Phone;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException) {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: ManagementPanel/Users/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            if (id == null || _context.Users == null) {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null) {
                return NotFound();
            }

            return View(user);
        }

        // POST: ManagementPanel/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            if (_context.Users == null) {
                return Problem("Entity set 'ProductManagementContext.Users'  is null.");
            }
            var user = await _context.Users.FindAsync(id);
            user.Status = false; 

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
         
    }
}
