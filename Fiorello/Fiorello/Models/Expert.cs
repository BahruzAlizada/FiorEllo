using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fiorello.Models
{
    public class Expert
    {
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }
        public string Image { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
        public Position Position { get; set; }
        public int PositionId { get; set; }
        public bool IsDeactive { get; set; }
    }
}
