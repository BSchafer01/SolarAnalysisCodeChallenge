using SolarAnalysisLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolarAnalysisLogic
{
    public class SunsetSunriseIdentifier
    {

        public static List<IBuildingModel> SunsetCheck(List<IBuildingModel> buildings)
        {
            List<IBuildingModel> building = new List<IBuildingModel>();


            int maxHeight = 0;
            int sunsetLocation = 0;
            int sunriseLocation = 0;
            buildings.Reverse();
            foreach (var b in buildings)
            {
                if (b.height > maxHeight)
                {
                    b.seeSunRise = true;
                    maxHeight = b.height;
                }
                else
                {
                    b.seeSunRise = false;
                }
                b.riseLocation = sunriseLocation;
                sunriseLocation += b.width + 5;
            }
            buildings.Reverse();
            maxHeight = 0;
            foreach (var b in buildings)
            {
                if (b.height > maxHeight)
                {
                    b.seeSunSet = true;
                    
                    maxHeight = b.height;
                }
                else
                {
                    b.seeSunSet = false;
                }
                b.setLocation = sunsetLocation;
                sunsetLocation += b.width + 5;
                building.Add(b);
            }


            return building;
        }
    }
}
