using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebsiteNoiBoCongTy.Models
{
    public class Account
    {
        [Key]
        public int AccountId { get; set; }
        public string? Username { get; set; }
        public string? Fullname { get; set; }

        [RegularExpression(@"^.{6,}$", ErrorMessage = "Mật khẩu chứa ít nhất 6 kí tự")]
        public string? Password { get; set; }
        public string? Gender { get; set; }
        public DateTime Birthday { get; set; }

        public string? Position { get; set; }

        public string? PhotoURL { get; set; }

        public string? PhoneNumber { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }

        public ICollection<News>? AuthoredNews { get; set; }
        public ICollection<Notification>? AuthoredNotifications { get; set; }

    }
}
