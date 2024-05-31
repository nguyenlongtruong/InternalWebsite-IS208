using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebsiteNoiBoCongTy.Data;
using WebsiteNoiBoCongTy.Models;

namespace WebsiteNoiBoCongTy.Controllers.Admin
{
    public class ManageNewsController : Controller
    {
        private readonly WebsiteNoiBoCongTyContext _context;

        public ManageNewsController(WebsiteNoiBoCongTyContext context)
        {
            _context = context;
        }

        // GET: ManageNews
        public async Task<IActionResult> Index()
        {
            var websiteNoiBoCongTyContext = _context.News.Include(n => n.Author);
            return View(await websiteNoiBoCongTyContext.ToListAsync());
        }

        // GET: ManageNews/Details/5
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

        // GET: ManageNews/Create
        public IActionResult Create()
        {
            ViewData["AccountId"] = new SelectList(_context.Account, "AccountId", "Fullname");
            return View();
        }

        // POST: ManageNews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NewsTitle,Content,CreateDate,AccountId")] News news)
        {
            if (ModelState.IsValid)
            {
                var accountList = _context.Account.ToList();
                foreach(Account acc in accountList)
                {
                    Notification notification = new Notification
                    {
                        Title = $"Tin tức mới ",
                        Content = $"{news.NewsTitle}",
                        AccountId = acc.AccountId,
                        CreateDate = DateTime.Now,
                        PublishDate = DateTime.Now,
                    };

                    _context.Notification.Add(notification);
                }

                _context.Add(news);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountId"] = new SelectList(_context.Account, "AccountId", "Fullname", news.AccountId);
            return View(news);
        }

        // GET: ManageNews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.News == null)
            {
                return NotFound();
            }

            var news = await _context.News.FindAsync(id);
            if (news == null)
            {
                return NotFound();
            }
            ViewData["AccountId"] = new SelectList(_context.Account, "AccountId", "Fullname", news.AccountId);
            return View(news);
        }

        // POST: ManageNews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NewsTitle,Content,CreateDate,AccountId")] News news)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(news);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewsExists(news.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountId"] = new SelectList(_context.Account, "AccountId", "Fullname", news.AccountId);
            return View(news);
        }

        // GET: ManageNews/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: ManageNews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.News == null)
            {
                return Problem("Entity set 'WebsiteNoiBoCongTyContext.News'  is null.");
            }
            var news = await _context.News.FindAsync(id);
            if (news != null)
            {
                _context.News.Remove(news);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NewsExists(int id)
        {
          return (_context.News?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
