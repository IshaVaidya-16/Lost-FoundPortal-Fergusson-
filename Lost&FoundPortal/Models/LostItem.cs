using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lost_FoundPortal.Models
{
    [Table("lost_items")]//mapping to table
    public class LostItem
    {
        [Key] //primary key
        [Column("lost_id")]//mapping to column
        public int LostId { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("item_name")]
        public string ItemName { get; set; }

        [Column("description")]
        public string? Description { get; set; }

        [Column("location_lost")]
        public string? LocationLost { get; set; }

        [Column("date_lost")]
        public DateTime? DateLost { get; set; }

        [Column("image_path")]
        public string? ImagePath { get; set; }

        [Column("status")]
        public string Status { get; set; }

        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }
    }
}