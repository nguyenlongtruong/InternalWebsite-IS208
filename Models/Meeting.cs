using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebsiteNoiBoCongTy.Models
{
    public class Meeting
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Subject { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }

        [ForeignKey("Room")]
        public int RoomId { get; set; }
        public Room? Room { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }

    }
}
