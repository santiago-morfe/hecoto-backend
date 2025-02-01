using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hecotoBackend.Models
{
    public class ClassesModel : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required][MaxLength(255)] public required string Name { get; set; }

        // Relación con Laboratory
        [ForeignKey("LaboratoryId")] public int LaboratoryId { get; set; }
        public LaboratoriesModel? Laboratory { get; set; }
        public ICollection<ActivitiesModel>? Activities { get; set; }
    }
}