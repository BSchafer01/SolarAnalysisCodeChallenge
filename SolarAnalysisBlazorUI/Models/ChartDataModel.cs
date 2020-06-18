using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolarAnalysisBlazorUI.Models
{
    public class ChartDataModel
    {
        public int xValue { get; set; }
        public int sunriseSunset { get; set; }
        public int sunrise { get; set; }
        public int sunset { get; set; }
        public int noSun { get; set; }
    }
}
