using System.Collections.Generic;

namespace Fiorello.Models
{
    public class Position
    {
        public int Id { get; set; }
        public string PositionName { get; set; }
        public List<Expert> Experts { get; set; }
    }
}
