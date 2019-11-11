using System;
using System.Collections.Generic;
using appTurismoIqq.Modelo.Mapa;
using Xamarin.Essentials;
using Xamarin.Forms.GoogleMaps;

namespace appTurismoIqq.Helpers
{
    public class GoogleMapsDirectionsHelper
    {
        public static void SetRoutes(Xamarin.Forms.GoogleMaps.Map map, GoogleDirections googleDirections)
        {
            if (googleDirections.routes.Count > 0)
            {
                string encodedPoints = googleDirections.routes[0].overview_polyline.points;

                var lstDecodedPoints = GetPolylinePoints(encodedPoints);
                var latLngPoints = new Position[lstDecodedPoints.Count];
                int index = 0;

                var polylineoption = new Xamarin.Forms.GoogleMaps.Polyline();
                polylineoption.StrokeColor = Xamarin.Forms.Color.Red;

                foreach (Location loc in lstDecodedPoints)
                {
                    latLngPoints[index++] = new Position(loc.Latitude, loc.Longitude);
                    polylineoption.Positions.Add(new Position(loc.Latitude, loc.Longitude));
                }

                map.Polylines.Add(polylineoption);

                UpdateCamera(map, latLngPoints[0]);
            }
        }

        private static List<Location> GetPolylinePoints(string encodedPoints)
        {
            if (string.IsNullOrEmpty(encodedPoints))
            {
                return null;
            }

            var poly = new List<Location>();
            char[] polylinechars = encodedPoints.ToCharArray();
            int index = 0;

            int currentLat = 0;
            int currentLng = 0;
            int next5bits;
            int sum;
            int shifter;

            try
            {
                while (index < polylinechars.Length)
                {
                    sum = 0;
                    shifter = 0;
                    do
                    {
                        next5bits = (int)polylinechars[index++] - 63;
                        sum |= (next5bits & 31) << shifter;
                        shifter += 5;
                    } while (next5bits >= 32 && index < polylinechars.Length);

                    if (index >= polylinechars.Length)
                        break;

                    currentLat += (sum & 1) == 1 ? ~(sum >> 1) : (sum >> 1);

                    sum = 0;
                    shifter = 0;
                    do
                    {
                        next5bits = (int)polylinechars[index++] - 63;
                        sum |= (next5bits & 31) << shifter;
                        shifter += 5;
                    } while (next5bits >= 32 && index < polylinechars.Length);

                    if (index >= polylinechars.Length && next5bits >= 32)
                        break;

                    currentLng += (sum & 1) == 1 ? ~(sum >> 1) : (sum >> 1);
                    Location p = new Location()
                    {
                        Latitude = Convert.ToDouble(currentLat) / 100000.0,
                        Longitude = Convert.ToDouble(currentLng) / 100000.0
                    };

                    poly.Add(p);
                }
            }
            catch
            {
            }
            return poly;
        }

        private static void UpdateCamera(Xamarin.Forms.GoogleMaps.Map map, Position pos)
        {
            CameraPosition cameraPosition = new CameraPosition(pos, 12, 10);
            CameraUpdate cameraUpdate = CameraUpdateFactory.NewCameraPosition(cameraPosition);
            map.AnimateCamera(cameraUpdate);
        }
    }
}
