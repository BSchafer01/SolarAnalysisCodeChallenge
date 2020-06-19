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
        [Range(10, 5280, ErrorMessage = "Please enter a building width between 10 ft and 5280 ft")]
        public int width { get; set; }
        [Required]
        [Range(10, 5280, ErrorMessage ="Please enter a building height between 10 ft and 5280 ft.")]
        public int height { get; set; }
        [Required(ErrorMessage ="Please enter a unique name for this building")]
        public string label { get; set; }
        public int riseLocation { get; set; }
        public int setLocation { get; set; }
        public bool seeSunSet { get; set; }
        public bool seeSunRise { get; set; }
        public double morningSunTime { get; set; }
        public double afternoonSunTime { get; set; }
    }
}
