using GoogleMaps.LocationServices;
using Microsoft.Extensions.Configuration;
using SolarAnalysisLogic.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SolarAnalysisLogic
{
    public class GeolocationInfo
    {
        static HttpClient client = new HttpClient();
        public GeolocationInfo(IConfiguration config)
        {
            Config = config;
            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
        }

        public IConfiguration Config { get; }

        

        public static (double lat, double lon) GetLatLong(string address, string key)
        {
            var locationService = new GoogleLocationService(key);
            var point = locationService.GetLatLongFromAddress(address);
            var lat = point.Latitude;
            var lon = point.Longitude;

            return (lat, lon);
        }

        public static async Task<SunModel> GetDayLengthSeconds(double lat, double lon, string date)
        {
            string url =  "https://api.sunrise-sunset.org/json?lat="+lat+"&lng="+lon+"&date="+date+"&formatted=0";


            using (HttpResponseMessage response = await client.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    SunResultModel result = await response.Content.ReadAsAsync<SunResultModel>();
                    return result.Results;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
                
            }
            
        }
    }
}
