using Microsoft.Extensions.Configuration;
using SolarAnalysisBlazorUI.Models;
using SolarAnalysisLogic;
using SolarAnalysisLogic.Models;
using Syncfusion.Blazor.Charts;
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
        private SfChart chartObj;

        private AddressDaylightModel address = new AddressDaylightModel();


        private void HandleValidSubmit()
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
            chartObj.RefreshLiveData();
        }

        private void ClearList()
        {
            buildings.Clear();
            building = new DisplayBuildingModel();
            ChartData.Clear();
            chartObj.RefreshLiveData();
        }

        private async Task HandleAddress()
        {
            var latlon = GeolocationInfo.GetLatLong(address.address, "AIzaSyCTNhOzSCncbXoOLfcBwl0aOtXpAAI");
            address.lat = latlon.lat;
            address.lon = latlon.lon;
            var sunTime = await GeolocationInfo.GetDayLengthSeconds(address.lat, address.lon, address.date.ToShortDateString());

            address.timeSeconds = sunTime.day_length;
        }
    }
}
