using System.ComponentModel.DataAnnotations;

namespace hecotoBackend.Models
{
    public class UsersModel : BaseModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(3)]
        [RegularExpression(@"^[a-zA-Z0-9]+$")]
        public required  string UserName { get; set; }

        [Required]
        public required  string PasswordHash  { get; set;}

        [Required]
        [EmailAddress]
        public required  string Email { get; set; }

        public JwtTokensModel? Token { get; set; }
    }
}