using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hecotoBackend.Models
{
    public class MedalsModel : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required][MaxLength(255)] public required string Type { get; set; }
        [MaxLength(255)] public string? Rank { get; set; }
        [Required][MaxLength(255)] public required string Name { get; set; }
    }
}