using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fiorello.Models
{
    public class Position
    {
        public int Id { get; set; }
        [Required]
        public string PositionName { get; set; }
        public List<Expert> Experts { get; set; }
        public bool IsDeactive { get; set; }
    }
}
