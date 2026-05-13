using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lost_FoundPortal.Models
{
    [Table("found_items")]
    public class FoundItem
    {
        [Key]
        [Column("found_id")]
        public int FoundId { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("item_name")]
        public string ItemName { get; set; }

        [Column("description")]
        public string? Description { get; set; }

        [Column("location_found")]
        public string LocationFound { get; set; }

        [Column("date_found")]
        public DateTime? DateFound { get; set; }

        [Column("image_path")]
        public string? ImagePath { get; set; }

        [Column("status")]
        public string? Status { get; set; }

        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }
    }
}