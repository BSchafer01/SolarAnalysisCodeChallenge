using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SolarAnalysisBlazorUI.Models
{
    public class AddressDaylightModel
    {
        [Required]
        public string address { get; set; }
        [Required]
        public DateTime date { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }
        public int timeSeconds { get; set; }
    }
}
