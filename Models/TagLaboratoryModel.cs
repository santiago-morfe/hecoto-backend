using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hecotoBackend.Models
{
    [PrimaryKey(nameof(LaboratoryId), nameof(TagId))]
    public class TagLaboratoryModel : BaseModel
    {
        [ForeignKey("LaboratoryId")] public int LaboratoryId { get; set; }
        public LaboratoriesModel? Laboratory { get; set; }

        [ForeignKey("TagId")] public int TagId { get; set; }
        public TagsModel? Tag { get; set; }
    }
}