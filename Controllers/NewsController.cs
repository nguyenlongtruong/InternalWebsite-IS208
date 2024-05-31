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
    public class NewsController : Controller
    {
        private readonly WebsiteNoiBoCongTyContext _context;

        public NewsController(WebsiteNoiBoCongTyContext context)
        {
            _context = context;
        }

        // GET: News
        public async Task<IActionResult> Index()
        {
            var websiteNoiBoCongTyContext = _context.News.Include(n => n.Author);
            return View(await websiteNoiBoCongTyContext.ToListAsync());
        }

        // GET: News/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.News == null)
            {
                return NotFound();
            }

            var news = await _context.News
                .Include(n => n.Author)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }
    }
}
