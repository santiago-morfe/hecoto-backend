using System.ComponentModel.DataAnnotations;

namespace hecotoBackend.DTOs
{
    public class ProfileDto
    {
        public required  string Id { get; set; }
        public required  string Name { get; set; }
    }
}