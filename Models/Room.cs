using System.ComponentModel.DataAnnotations;

namespace WebsiteNoiBoCongTy.Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string RoomCode { get; set; }
    }
}
