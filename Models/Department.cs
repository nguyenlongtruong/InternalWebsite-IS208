using System.ComponentModel.DataAnnotations;

namespace WebsiteNoiBoCongTy.Models
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }
        [Required]
        public string? Name { get; set; }
    }
}
