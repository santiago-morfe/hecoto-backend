using System.ComponentModel.DataAnnotations;

namespace hecotoBackend.Models
{
    public class Test
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }
    }
}