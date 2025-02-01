using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hecotoBackend.Models
{
    [PrimaryKey(nameof(UserId), nameof(LaboratoryId))]
    public class LaboratoryParticipantsModel : BaseModel
    {
        [ForeignKey("UserId")] public int UserId { get; set; }
        public UsersModel? User { get; set; }

        [ForeignKey("LaboratoryId")] public int LaboratoryId { get; set; }
        public LaboratoriesModel? Laboratory { get; set; }
    }
}