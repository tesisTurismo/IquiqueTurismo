using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using appTurismoIqq.Modelo.Mapa;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace appTurismoIqq.Servicios
{
    public class GoogleDirectionsClient
    {
        private static string GOOGLE_KEY = "AIzaSyA-0qzPhnpVdMnaEssPXvobqNeilQdUbw0";

        public async static Task<GoogleDirections> GetDirections(Location origin, Location dest, List<Location> markerPoints)
        {
            string str_origin = "origin=" + origin.Latitude.ToString(new CultureInfo("en-US")) + "," + origin.Longitude.ToString(new CultureInfo("en-US"));

            string str_dest = "destination=" + dest.Latitude.ToString(new CultureInfo("en-US")) + "," + dest.Longitude.ToString(new CultureInfo("en-US"));

            string sensor = "sensor=false";

            String waypoints = "";

            for (int i = 0; i < markerPoints.Count; i++)
            {
                Location point = markerPoints[i];

                if (i == 0)
                    waypoints = "waypoints=";

                waypoints += point.Latitude.ToString(new CultureInfo("en-US")) + "," + point.Longitude.ToString(new CultureInfo("en-US")) + "|";
            }

            // Building the parameters to the web service
            String parameters = str_origin + "&" + str_dest + "&" + sensor + "&" + waypoints;

            // Output format
            String output = "json";

            // Building the url to the web service
            String url = "https://maps.googleapis.com/maps/api/directions/" + output + "?" + parameters;

            url += "&key=" + GOOGLE_KEY;


            using (var client = new HttpClient())
            {
                var jsonResponse = string.Empty;
                HttpResponseMessage response = null;

                response = client.GetAsync(url).Result;

                jsonResponse = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<GoogleDirections>(jsonResponse);
            }
        }
    }
}
