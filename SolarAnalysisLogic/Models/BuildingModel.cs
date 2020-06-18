using System;
using System.Collections.Generic;
using System.Text;

namespace SolarAnalysisLogic.Models
{
    public class BuildingModel : IBuildingModel
    {
        public int height { get; set; }
        public int width { get; set; }
        public string label { get; set; }
        public int riseLocation { get; set; }
        public int setLocation { get; set; }
        public bool seeSunSet { get; set; }
        public bool seeSunRise { get; set; }
        public double morningSunTime { get; set; }
        public double afternoonSunTime { get; set; }

    }
}
