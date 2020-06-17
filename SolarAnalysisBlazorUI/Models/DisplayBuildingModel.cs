using SolarAnalysisLogic.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SolarAnalysisBlazorUI.Models
{
    public class DisplayBuildingModel : IBuildingModel
    {        
        [Required]
        public int width { get; set; }
        [Required]
        public int height { get; set; }
        [Required]
        public string label { get; set; }
        public int riseLocation { get; set; }
        public int setLocation { get; set; }
        public bool seeSunSet { get; set; }
        public bool seeSunRise { get; set; }
        public double sunTime { get; set; }
    }
}
