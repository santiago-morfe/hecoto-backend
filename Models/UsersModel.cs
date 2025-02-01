using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hecotoBackend.Models
{
    public class UsersModel : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        [MinLength(3)]
        [RegularExpression(@"^[a-zA-Z0-9]+$")]
        public required string UserName { get; set; }

        [Required]
        [MaxLength(255)]
        public required string PasswordHash { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(255)]
        public required string Email { get; set; }

        public ICollection<RefreshTokensModel>? RefreshTokens { get; set; }
        public ICollection<MedalsModel>? Medals { get; set; }
    }
}