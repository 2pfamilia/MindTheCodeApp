using MindTheCodeApp.Models.AuthModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace MindTheCodeApp.Models.AuthModels
{
    [Table("User_Roles")]
    public class UserRole
    {
        [Key, Column("role_id"), NotNull, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoleId { get; set; }

        [Required, Column("code"), NotNull, StringLength(6)]
        public string? Code { get; set; }

        [Required, Column("title"), NotNull, StringLength(50, MinimumLength = 3)]
        public string? Title { get; set; }

        [Required, Column("description"), AllowNull, StringLength(200)]
        public string? Description { get; set; }

        [Column("date_created"), NotNull]
        public DateTime DateCreated { get; set; } = DateTime.Now;

        public ICollection<User>? Users { get; set; }
    }
}
