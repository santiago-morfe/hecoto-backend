using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hecotoBackend.Models
{
    [PrimaryKey(nameof(ColiseumId), nameof(TagId))]
public class TagsColiseumModel : BaseModel
{
    [ForeignKey("ColiseumId")] public int ColiseumId { get; set; }
    public ColiseumsModel? Coliseum { get; set; }

    [ForeignKey("TagId")] public int TagId { get; set; }
    public TagsModel? Tag { get; set; }
}
}