using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace hecotoBackend.Models
{
    public class BattlesModel : BaseModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required][MaxLength(255)] public required string Name { get; set; }
    
    [Column(TypeName = "jsonb")]
    public required JsonDocument Content { get; set; }
    
    [Column(TypeName = "jsonb")]
    public required JsonDocument Response { get; set; }

    // Relaciones
    [ForeignKey("ArenaId")] public int ArenaId { get; set; }
    public ArenasModel? Arena { get; set; }

    [ForeignKey("ActivityTypeId")] public int ActivityTypeId { get; set; }
    public ActivityTypesModel? ActivityType { get; set; }
}
}