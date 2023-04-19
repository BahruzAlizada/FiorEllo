using System.ComponentModel.DataAnnotations;

namespace Fiorello.Models
{
    public class AboutInfo
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public bool IsDeactive { get; set; }
    }
}
