using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace hecotoBackend.Models
{
    public class ActivityResponsesModel : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName = "jsonb")]
        public required JsonDocument UserResponse { get; set; }

        [Required]
        [Precision(18, 2)]
        public decimal Score { get; set; }

        // Relaciones
        [ForeignKey("ActivityId")]
        public int ActivityId { get; set; }
        public ActivitiesModel? Activity { get; set; }

        [ForeignKey("UserId")] public int UserId { get; set; }
        public UsersModel? User { get; set; }
    }
}