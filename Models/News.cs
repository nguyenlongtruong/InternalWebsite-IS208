using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebsiteNoiBoCongTy.Models
{
    public class News
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? NewsTitle { get; set; }
        [Required]
        public string? Content { get; set; }
        public DateTime CreateDate { get; set; }

        [ForeignKey("Account")]
        public int AccountId { get; set; }
        public Account? Author { get; set; }
    }
}
