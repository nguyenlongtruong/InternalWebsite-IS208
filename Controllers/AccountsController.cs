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
    public class AccountsController : Controller
    {
        private readonly WebsiteNoiBoCongTyContext _context;

        public AccountsController(WebsiteNoiBoCongTyContext context)
        {
            _context = context;
        }

        // GET: Accounts/Details/5
        public async Task<IActionResult> Details()
        {
            String id = HttpContext.Session.GetString("AccountId");
            if (id == null || _context.Account == null)
            {
                return NotFound();
            }

            var account = await _context.Account
                .Include(a => a.Department)
                .FirstOrDefaultAsync(m => m.AccountId.ToString() == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // GET: Accounts/Edit/5
        public async Task<IActionResult> Edit()
        {
            String id = HttpContext.Session.GetString("AccountId");
            if (id == null || _context.Account == null)
            {
                return NotFound();
            }

            var account = await _context.Account.FirstOrDefaultAsync(
                a => a.AccountId.ToString() == id);
            if (account == null)
            {
                return NotFound();
            }
            ViewData["DepartmentId"] = new SelectList(_context.Department, "DepartmentId", "Name", account.DepartmentId);
            return View(account);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("AccountId,Username,Fullname,Password,Gender, PhotoURL,Birthday,Position,PhoneNumber,DepartmentId")] Account account)
        {
            String id = HttpContext.Session.GetString("AccountId");
            if (id != account.AccountId.ToString())
            {
                return NotFound();
            }
            ViewData["DepartmentId"] = new SelectList(_context.Set<Department>(), "DepartmentId", "Name", account.DepartmentId);
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(account);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountExists(account.AccountId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return View(account);
            }
            return View(account);
        }



        private bool AccountExists(int id)
        {
          return (_context.Account?.Any(e => e.AccountId == id)).GetValueOrDefault();
        }

        //======================LOGIN=======================
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var account = await _context.Account.FirstOrDefaultAsync(c => c.Username == username && c.Password == password);
            if (account == null)
            {
                return View();
            }
            HttpContext.Session.SetString("AccountId", account.AccountId.ToString());
            return RedirectToAction("Index", "Home");
        }
    }
}
