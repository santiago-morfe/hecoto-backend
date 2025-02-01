using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace hecotoBackend.Models
{
    public class ActivitiesModel : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required][MaxLength(255)] public required string Name { get; set; }

        [Column(TypeName = "jsonb")]
        public required JsonDocument Content { get; set; } // Usar System.Text.Json

        [Column(TypeName = "jsonb")]
        public required JsonDocument Response { get; set; }

        // Relaciones
        [ForeignKey("ActivityTypeId")] public int ActivityTypeId { get; set; }
        public ActivityTypesModel? ActivityType { get; set; }

        [ForeignKey("ClassId")] public int ClassId { get; set; }
        public ClassesModel? Class { get; set; }
    }
}