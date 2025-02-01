using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hecotoBackend.Models
{
    public class TermsAndConditionsModel : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required][MaxLength(255)] public required string Version { get; set; }
        [Required] public required string Content { get; set; }
        [Required] public DateTime EffectiveDate { get; set; }
    }
}