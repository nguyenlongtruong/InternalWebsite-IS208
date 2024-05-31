using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebsiteNoiBoCongTy.Data;
using WebsiteNoiBoCongTy.Models;

namespace WebsiteNoiBoCongTy.Controllers
{
    public class NotificationsController : Controller
    {
        private readonly WebsiteNoiBoCongTyContext _context;

        public NotificationsController(WebsiteNoiBoCongTyContext context)
        {
            _context = context;
        }

        // GET: Notifications
        public async Task<IActionResult> Index()
        {
            String id = HttpContext.Session.GetString("AccountId");
            var websiteNoiBoCongTyContext = _context.Notification.Include(n => n.Account).Where(n => n.AccountId.ToString() == id);
            return View(await websiteNoiBoCongTyContext.ToListAsync());
        }

        // GET: Notifications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Notification == null)
            {
                return NotFound();
            }

            var notification = await _context.Notification
                .Include(n => n.Account)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (notification == null)
            {
                return NotFound();
            }

            return View(notification);
        }
    }
}
