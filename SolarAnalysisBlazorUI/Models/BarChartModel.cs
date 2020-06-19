using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolarAnalysisBlazorUI.Models
{
    public class BarChartModel
    {
        public string label { get; set; }
        public double morningSun { get; set; }
        public double afternoonSun { get; set; }

    }
}
