using Fiorello.Models;
using System.Collections.Generic;

namespace Fiorello.VIewsModel
{
    public class HomeVM
    {
        public List<Category> Categories { get; set; }
        public List<Product> Products { get; set; }
        public List<Slider> Sliders { get; set; }
        public SliderInfo SliderInfo { get; set; }
        public About About { get; set; }
        public List<AboutInfo> AboutInfo { get; set; }
        public List<Expert> Experts { get; set; }
        public List<Position> Positions { get; set; }
    }
}
