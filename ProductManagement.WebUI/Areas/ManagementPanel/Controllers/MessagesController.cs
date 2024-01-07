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
    public class MessagesController : Controller { 
        private readonly ProductManagementContext _context = new
ProductManagementContext();
        

        // GET: ManagementPanel/Messages
        public async Task<IActionResult> Index() {
            return View(await _context.Messages.ToListAsync());
        }

        // GET: ManagementPanel/Messages/Details/5
        public async Task<IActionResult> Details(int? id) {
            if (id == null || _context.Messages == null) {
                return NotFound();
            }

            var message = await _context.Messages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (message == null) {
                return NotFound();
            }

            return View(message);
        }
   
    }
}
