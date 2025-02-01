using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hecotoBackend.Models
{
    public class RefreshTokensModel : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public required string RefreshToken { get; set; }

        [Required]
        public DateTime Expiration { get; set; }

        [Required]
        public bool IsValid { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public UsersModel? User { get; set; }
    }
}