using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hecotoBackend.Models
{
    [PrimaryKey(nameof(UserId), nameof(TermsId))]
    public class UserTermsAcceptanceModel
    {
        [ForeignKey("UserId")] public int UserId { get; set; }
        public UsersModel? User { get; set; }

        [ForeignKey("TermsId")] public int TermsId { get; set; }
        public TermsAndConditionsModel? Terms { get; set; }

        [Required] public DateTime AcceptedAt { get; set; }
    }
}