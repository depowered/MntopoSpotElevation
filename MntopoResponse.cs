using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace MntopoSpotElevation
{
    static class MntopoResponse
    {
        private static double GetElevationAtLonLat(double lon, double lat)
        {
            string url = $"http://arcgis.dnr.state.mn.us/mndnr/rest/services/elevation/DEM1m/MapServer/exts/ElevationsSOE_NET/ElevationLayers/0/GetElevationAtLonLat?lon={lon}&lat={lat}&f=pjson";
            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            using (Stream dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);
                string jsonString = reader.ReadToEnd();

                response.Close();

                // Regex is used here because there is version conflicts with the .dll refereneces used in System.Text.Json
                string pattern = @"\d*[.]\d*";
                string elevationString = Regex.Match(jsonString, pattern).Value;
                
                // cast to double
                try
                {
                    double elevation = Convert.ToDouble(elevationString);
                    return elevation;
                }

                // If the user clicks out of data extents, the server will return json containing an error code.
                // The casting will throw an error as there will not be a regex match.
                catch
                {
                    return -1;
                }
                
            }
        }

        public static double GetElevationAtLonLatMeters(double lon, double lat)
        {
            return Math.Round(GetElevationAtLonLat(lon, lat), 2);
        }

        public static double GetElevationAtLonLatFeet(double lon, double lat)
        {
            return Math.Round((GetElevationAtLonLat(lon, lat) * 3.28084), 1);
        }
    }
}
