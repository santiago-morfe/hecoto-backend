using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hecotoBackend.Models
{
    public class LaboratoriesModel : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required][MaxLength(255)] public required string Name { get; set; }
        [Required] public required string Description { get; set; }
        [Required] public bool IsPublic { get; set; }

        // Relaciones
        [ForeignKey("CreatedBy")] public int CreatedBy { get; set; }
        public UsersModel? Creator { get; set; }
        public ICollection<ClassesModel>? Classes { get; set; }
    }
}