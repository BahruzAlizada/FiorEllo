using System.ComponentModel.DataAnnotations;

namespace Fiorello.Models
{
    public class SliderInfo
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }
}
