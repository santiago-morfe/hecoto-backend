using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hecotoBackend.Models
{
    public class PermissionsModel : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // Relaciones
        [ForeignKey("UserId")] public int UserId { get; set; }
        public UsersModel? User { get; set; }

        [ForeignKey("LaboratoryId")] public int LaboratoryId { get; set; }
        public LaboratoriesModel? Laboratory { get; set; }

        [ForeignKey("PermissionTypeId")] public int PermissionTypeId { get; set; }
        public PermissionTypesModel? PermissionType { get; set; }
    }
}