using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lost_FoundPortal.Models
{
    [Table("user")]
    public class Student
    {
        [Key]
        [Column("user_id")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Full Name is required")]
        [Column("fullname")]
        public string? FullName { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [Column("address")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        [Column("email")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [Column("phone")]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "Roll No is required")]
        [Column("rollno")]
        public string? RollNo { get; set; }

        [Required(ErrorMessage = "Branch is required")]
        [Column("branch")]
        public string? Branch { get; set; }

        [Required(ErrorMessage = "Course is required")]
        [Column("course")]
        public string? Course { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [Column("password")]
        public string? Password { get; set; }

        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }
    }
}