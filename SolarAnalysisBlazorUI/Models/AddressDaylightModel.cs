using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolarAnalysisBlazorUI.Models
{
    public class AddressDaylightModel
    {
        public string address { get; set; }
        public DateTime date { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }
        public int timeSeconds { get; set; }
    }
}
