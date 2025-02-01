using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hecotoBackend.Models
{
    public class ArenasModel : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required][MaxLength(255)] public required string Name { get; set; }

        [ForeignKey("ColiseumId")] public int ColiseumId { get; set; }
        public ColiseumsModel? Coliseum { get; set; }
    }
}