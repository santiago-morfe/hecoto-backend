using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hecotoBackend.Models
{
    public class LaboratoryInvitationsModel : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required][MaxLength(255)] public required string CodeLink { get; set; } // Corregido a PascalCase
        [Required] public DateTime Expiration { get; set; }

        // Relación
        [ForeignKey("LaboratoryId")] public int LaboratoryId { get; set; }
        public LaboratoriesModel? Laboratory { get; set; }
    }
}