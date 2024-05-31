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
    public class ManageMeetingsController : Controller
    {
        private readonly WebsiteNoiBoCongTyContext _context;

        public ManageMeetingsController(WebsiteNoiBoCongTyContext context)
        {
            _context = context;
        }

        // GET: ManageMeetings
        public async Task<IActionResult> Index()
        {
            var websiteNoiBoCongTyContext = _context.Meeting.Include(m => m.Department).Include(m => m.Room);
            return View(await websiteNoiBoCongTyContext.ToListAsync());
        }

        // GET: ManageMeetings/Details/5
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

        // GET: ManageMeetings/Create
        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Department, "DepartmentId", "Name");
            ViewData["RoomId"] = new SelectList(_context.Room, "Id", "RoomCode");
            return View();
        }

        // POST: ManageMeetings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Subject,StartTime,EndTime,RoomId,DepartmentId")] Meeting meeting)
        {
            if (ModelState.IsValid)
            {
                // Lấy danh sách tài khoản có phòng ban tương ứng với cuộc họp được tạo
                var accountsInDepartment = await _context.Account
                    .Where(a => a.DepartmentId == meeting.DepartmentId)
                    .ToListAsync();

                // Tạo thông báo
                foreach (var account in accountsInDepartment)
                {
                    Room room = await _context.Room.FirstOrDefaultAsync(r => r.Id == meeting.RoomId);
                    var notification = new Notification
                    {
                        Title = $"Cuộc họp mới: {meeting.Subject}",
                        Content = $"Tại phòng : {room.RoomCode} vào : {meeting.StartTime}",
                        CreateDate = DateTime.Now,
                        PublishDate = DateTime.Now,
                        AccountId = account.AccountId,
                        Account = account
                    };

                    _context.Notification.Add(notification);
                    await _context.SaveChangesAsync();
                }
                _context.Add(meeting);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["DepartmentId"] = new SelectList(_context.Department, "DepartmentId", "Name", meeting.DepartmentId);
            ViewData["RoomId"] = new SelectList(_context.Room, "Id", "RoomCode", meeting.RoomId);
            return View(meeting);
        }

        // GET: ManageMeetings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Meeting == null)
            {
                return NotFound();
            }

            var meeting = await _context.Meeting.FindAsync(id);
            if (meeting == null)
            {
                return NotFound();
            }
            ViewData["DepartmentId"] = new SelectList(_context.Department, "DepartmentId", "Name", meeting.DepartmentId);
            ViewData["RoomId"] = new SelectList(_context.Room, "Id", "RoomCode", meeting.RoomId);
            return View(meeting);
        }

        // POST: ManageMeetings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Subject,StartTime,EndTime,RoomId,DepartmentId")] Meeting meeting)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(meeting);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MeetingExists(meeting.Id))
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
            ViewData["DepartmentId"] = new SelectList(_context.Department, "DepartmentId", "Name", meeting.DepartmentId);
            ViewData["RoomId"] = new SelectList(_context.Room, "Id", "RoomCode", meeting.RoomId);
            return View(meeting);
        }

        // GET: ManageMeetings/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: ManageMeetings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Meeting == null)
            {
                return Problem("Entity set 'WebsiteNoiBoCongTyContext.Meeting'  is null.");
            }
            var meeting = await _context.Meeting.FindAsync(id);
            if (meeting != null)
            {
                _context.Meeting.Remove(meeting);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MeetingExists(int id)
        {
          return (_context.Meeting?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
