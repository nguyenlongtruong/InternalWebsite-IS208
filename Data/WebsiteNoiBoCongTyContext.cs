using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebsiteNoiBoCongTy.Models;

namespace WebsiteNoiBoCongTy.Data
{
    public class WebsiteNoiBoCongTyContext : DbContext
    {
        public WebsiteNoiBoCongTyContext (DbContextOptions<WebsiteNoiBoCongTyContext> options)
            : base(options)
        {
        }

        public DbSet<WebsiteNoiBoCongTy.Models.Account> Account { get; set; } = default!;

        public DbSet<WebsiteNoiBoCongTy.Models.Department> Department { get; set; } = default!;

       
        public DbSet<WebsiteNoiBoCongTy.Models.News> News { get; set; } = default!;

        public DbSet<WebsiteNoiBoCongTy.Models.Notification> Notification { get; set; } = default!;


        public DbSet<WebsiteNoiBoCongTy.Models.Room> Room { get; set; } = default!;

        public DbSet<WebsiteNoiBoCongTy.Models.Meeting> Meeting { get; set; } = default!;

    }
}
