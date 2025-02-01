using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace hecotoBackend.Models
{
    public class BattleResponsesModel : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName = "jsonb")]
        public required JsonDocument UserResponse { get; set; }

        [Required] public int Score { get; set; }

        // Relaciones
        [ForeignKey("BattleId")] public int BattleId { get; set; }
        public BattlesModel? Battle { get; set; }

        [ForeignKey("UserId")] public int UserId { get; set; }
        public UsersModel? User { get; set; }
    }
}