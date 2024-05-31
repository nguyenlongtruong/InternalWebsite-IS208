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
    public class MeetingsController : Controller
    {
        private readonly WebsiteNoiBoCongTyContext _context;

        public MeetingsController(WebsiteNoiBoCongTyContext context)
        {
            _context = context;
        }

        // GET: Meetings
        public async Task<IActionResult> Index()
        {
            String? id = HttpContext.Session.GetString("AccountId");
            var account = await _context.Account.FirstOrDefaultAsync(acc => acc.AccountId.ToString() == id);

            var websiteNoiBoCongTyContext = _context.Meeting
                .Include(m => m.Department)
                .Include(m => m.Room)
                .Where(m=>m.DepartmentId == account.DepartmentId);
            return View(await websiteNoiBoCongTyContext.ToListAsync());
        }

        // GET: Meetings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Meeting == null)
            {
                return NotFound();
            }

            var meeting = await _context.Meeting
                .Include(m => m.Department)
                .Include(m => m.Room)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (meeting == null)
            {
                return NotFound();
            }

            return View(meeting);
        }
    }
}
