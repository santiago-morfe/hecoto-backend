using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hecotoBackend.Models
{
    [PrimaryKey(nameof(UserId), nameof(MedalId))]
    public class UserMedalsModel : BaseModel
    {
        [ForeignKey("UserId")] public int UserId { get; set; }
        public UsersModel? User { get; set; }

        [ForeignKey("MedalId")] public int MedalId { get; set; }
        public MedalsModel? Medal { get; set; }
    }
}