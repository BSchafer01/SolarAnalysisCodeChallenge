using SolarAnalysisLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolarAnalysisLogic
{
    public class SolarTimeCalculator
    {

        public static List<IBuildingModel> CalculateSunTime(List<IBuildingModel> buildings, int daylightSeconds)
        {
            double timePerDeg = daylightSeconds / (double)180;
            int maxHeight = MaxBuildingHeight(buildings);
            foreach (var building in buildings)
            {

                var builds = buildings.Select((x, index) => new { index, x });
                int bIndex = builds.Where(x => x.x.label == building.label).First().index;

                List<double> morningAngle = new List<double>();
                List<double> afternoonAngle = new List<double>();
                double maxMorning = 0;
                double maxAfternoon = 0;

                foreach (var b in builds)
                {
                    if (b.x.label != building.label)
                    {
                        if (b.x.height >= building.height)
                        {
                            
                            if (b.index < bIndex)
                            {
                                morningAngle.Add(maxMorning);
                                afternoonAngle.Add(AngleNotCovered(building, building.setLocation, b.x, b.x.setLocation));
                            }
                            else
                            {
                                afternoonAngle.Add(maxAfternoon);
                                morningAngle.Add(AngleNotCovered(building, building.riseLocation, b.x, b.x.riseLocation));
                            }
                        }
                        else
                        {
                            morningAngle.Add(maxMorning);
                            afternoonAngle.Add(maxAfternoon);
                        }
                    }
                    else
                    {
                        morningAngle.Add(maxMorning);
                        afternoonAngle.Add(maxAfternoon);
                    }
                    
                }
                maxMorning = morningAngle.Max();
                maxAfternoon = afternoonAngle.Max();
                building.morningSunTime = timePerDeg * (90 - maxMorning);
                building.afternoonSunTime = timePerDeg * (90 - maxAfternoon);

            }

            return buildings;
        }

        public static double AngleNotCovered(IBuildingModel lowBuilding, int lowBuildingPosition, IBuildingModel highBuilding, int highBuildingPosition)
        {
            double angle = 0;
            bool covered = true;

            while (covered)
            {
                angle += 0.1;
                double radians = angle * (Math.PI / 180);
                double result = Math.Tan(radians);

                double shadowLength = (highBuilding.height - lowBuilding.height) / result;

                covered = shadowLength >= (lowBuildingPosition + lowBuilding.width) - (highBuildingPosition + highBuilding.width);
            }

            return angle;
        }

        public static int MaxBuildingHeight(List<IBuildingModel> buildings)
        {
            int maxHeight = 0;
            foreach (var building in buildings)
            {
                if (building.height > maxHeight)
                {

                    maxHeight = building.height;
                }
            }

            return maxHeight;
        }

    }
}
