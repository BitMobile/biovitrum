using System;
using System.Collections.Generic;

namespace Test
{
    public static class Utils
    {
        public static object GetValueOrDefault(this IDictionary<string, object> dictionary, string key,
            object @default = null)
        {
            object res;
            return dictionary.TryGetValue(key, out res) ? res : @default;
        }

        /// <summary>
        /// Distance in meters
        /// </summary>
        /// <returns></returns>
        public static double GetDistance(double lat1, double lon1, double lat2, double lon2)
        {
            //Haversine formula:
            //a = sin²(Δf/2) + cos f1 ⋅ cos f2 ⋅ sin²(Δl/2)
            //c = 2 ⋅ atan2( √a, √(1−a) )
            //d = R ⋅ c
            //where	f is latitude, l is longitude, R is earth’s radius (mean radius = 6,371km);
            //note that angles need to be in radians to pass to trig functions!

            const double r = 6371;
            double f1 = lat1 * Math.PI / 180;
            double f2 = lat2 * Math.PI / 180;
            double deltaf = f2 - f1;
            double deltal = (lon2 - lon1) * Math.PI / 180;

            double a = Math.Pow(Math.Sin(deltaf / 2), 2)
                + Math.Cos(f1) * Math.Cos(f2) * Math.Pow(Math.Sin(deltal / 2), 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double result = r * c;

            return Math.Abs(result * 1000);
        }
    }
}