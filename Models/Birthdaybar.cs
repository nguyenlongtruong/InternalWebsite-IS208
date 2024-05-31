using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using WebsiteNoiBoCongTy.Data;

namespace WebsiteNoiBoCongTy.Models
{
    public class Birthdaybar : ViewComponent
    {
        private readonly WebsiteNoiBoCongTyContext _context;

        public Birthdaybar(WebsiteNoiBoCongTyContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
   
            if (HttpContext.Request.Path == "/" || HttpContext.Request.Path == "/Home/Index")
            {
                DateTime dateTime = DateTime.Now;
                Account? account = _context.Account.FirstOrDefault(acc => acc.Birthday.Day.Equals(dateTime.Day) && acc.Birthday.Month.Equals(dateTime.Month));
                return View(account);
            }
            return Content(""); 
        }
    }
}
