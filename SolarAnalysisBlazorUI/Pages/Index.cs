using Microsoft.Extensions.Configuration;
using SolarAnalysisBlazorUI.Models;
using SolarAnalysisLogic;
using SolarAnalysisLogic.Models;
using Syncfusion.Blazor.Charts;
using Syncfusion.Blazor.DropDowns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolarAnalysisBlazorUI.Pages
{
    public partial class Index
    {
        private DisplayBuildingModel building = new DisplayBuildingModel();
        private List<DisplayBuildingModel> buildings = new List<DisplayBuildingModel>();
        private List<ChartDataModel> ChartData = new List<ChartDataModel>();
        private List<BarChartModel> barData = new List<BarChartModel>();
        private SfChart chartObj;
        private SfChart barObj;


        private AddressDaylightModel address = new AddressDaylightModel() { date = DateTime.Today};


        private async Task HandleValidSubmit()
        {
            List<IBuildingModel> send = new List<IBuildingModel>();
            List<IBuildingModel> rec = new List<IBuildingModel>();
            DisplayBuildingModel existingBuilding = buildings.Where(x => x.label == building.label).FirstOrDefault();
            if (existingBuilding != null)
            {
                existingBuilding.height = building.height;
                existingBuilding.width = building.width;
            }
            else
            {
                buildings.Add(new DisplayBuildingModel { height = building.height, label = building.label, width = building.width });
            }

            foreach (var b in buildings)
            {
                send.Add(b);
            }
            rec = SunsetSunriseIdentifier.SunsetCheck(send);
            buildings.Clear();
            ChartData.Clear();
            foreach (var r in rec)
            {
                buildings.Add(new DisplayBuildingModel { height = r.height, label = r.label, width = r.width, seeSunRise = r.seeSunRise, seeSunSet = r.seeSunSet, riseLocation = r.riseLocation, setLocation = r.setLocation });
                if (r.seeSunRise && r.seeSunSet)
                {
                    ChartData.Add(new ChartDataModel
                    {
                        xValue = r.setLocation,
                        sunriseSunset = r.height,
                        sunrise = 0,
                        sunset = 0,
                        noSun = 0
                    });
                }
                else if (r.seeSunRise)
                {
                    ChartData.Add(new ChartDataModel
                    {
                        xValue = r.setLocation,
                        sunriseSunset = 0,
                        sunrise = r.height,
                        sunset = 0,
                        noSun = 0
                    });

                }
                else if (r.seeSunSet)
                {
                    ChartData.Add(new ChartDataModel
                    {
                        xValue = r.setLocation,
                        sunriseSunset = 0,
                        sunrise = 0,
                        sunset = r.height,
                        noSun = 0
                    });
                }
                else
                {
                    ChartData.Add(new ChartDataModel
                    {
                        xValue = r.setLocation,
                        sunriseSunset = 0,
                        sunrise = 0,
                        sunset = 0,
                        noSun = r.height
                    });
                }
                ChartData.Add(new ChartDataModel
                {
                    xValue = r.setLocation + r.width,
                    sunriseSunset = 0,
                    sunrise = 0,
                    sunset = 0,
                    noSun = 0
                });

            }
            if (address.timeSeconds > 0)
            {
                await HandleAddress();
            }

            await chartObj.RefreshLiveData();
        }

        private void ClearList()
        {
            buildings.Clear();
            building = new DisplayBuildingModel();
            barData.Clear();
            ChartData.Clear();
            barObj.RefreshLiveData();
            chartObj.RefreshLiveData();
        }

        private async Task HandleAddress()
        {
            var latlon = GeolocationInfo.GetLatLong(address.address, "REDACTED");
            address.lat = latlon.lat;
            address.lon = latlon.lon;
            var sunTime = await GeolocationInfo.GetDayLengthSeconds(address.lat, address.lon, address.date.ToShortDateString());

            address.timeSeconds = sunTime.day_length;

            List<IBuildingModel> iBuildings = buildings.ToList<IBuildingModel>();


            iBuildings = SolarTimeCalculator.CalculateSunTime(iBuildings, address.timeSeconds);
            buildings.Clear();
            barData.Clear();
            foreach (var iBuild in iBuildings)
            {
                buildings.Add(new DisplayBuildingModel
                {
                    label = iBuild.label,
                    height = iBuild.height,
                    width = iBuild.width,
                    riseLocation = iBuild.riseLocation,
                    setLocation = iBuild.setLocation,
                    seeSunRise = iBuild.seeSunRise, 
                    seeSunSet = iBuild.seeSunSet,
                    afternoonSunTime = iBuild.afternoonSunTime,
                    morningSunTime = iBuild.morningSunTime
                });
                barData.Add(new BarChartModel { label = iBuild.label, morningSun = iBuild.morningSunTime, afternoonSun = iBuild.afternoonSunTime });
            }

            await barObj.RefreshLiveData();

        }

    }
}
