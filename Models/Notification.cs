using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebsiteNoiBoCongTy.Models
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Content { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        [Required]
        public DateTime PublishDate { get; set; }

        [ForeignKey("Account")]
        public int AccountId { get; set; }
        public Account? Account { get; set; }
    }
}
