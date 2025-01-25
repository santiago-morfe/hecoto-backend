// modelo para el manejo de los tokens
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hecotoBackend.Models
{
    public class JwtTokensModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public required string RefreshToken { get; set; }
        [Required]
        public DateTime Expiration { get; set; }
        [Required]
        public bool IsValidate { get; set; }

        [ForeignKey("UserId")]
        public required int UserId { get; set; }

        public UsersModel? User { get; set; }
    }
}