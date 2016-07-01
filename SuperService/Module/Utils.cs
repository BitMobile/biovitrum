using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Security.Policy;

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

            lat1 = Convert.ToDouble(lat1);
            lon1 = Convert.ToDouble(lon1);
            lat2 = Convert.ToDouble(lat2);
            lon2 = Convert.ToDouble(lon2);
            const double r = 6371;
            var f1 = lat1 * Math.PI / 180;
            var f2 = lat2 * Math.PI / 180;
            var deltaf = f2 - f1;
            var deltal = (lon2 - lon1) * Math.PI / 180;

            var a = Math.Pow(Math.Sin(deltaf / 2), 2)
                       + Math.Cos(f1) * Math.Cos(f2) * Math.Pow(Math.Sin(deltal / 2), 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var result = r * c;

            return Math.Abs(result * 1000);
        }

        /// <summary>
        ///    Преобразует входящую строку к виду, который помесится в указанное количество строк интерфейса
        /// </summary>
        /// <param name="str">Строка для красивого обрезания</param>
        /// <param name="outputLineLength">Длина одной строки в интерфейсе</param>
        /// <param name="outputLinesAmount">Количество строк в интерфейсе</param>
        /// <returns></returns>
        public static string CutForUIOutput(this string str, int outputLineLength, int outputLinesAmount)
        {
            var res = str.Substring(0, Math.Min(str.Length, outputLineLength * outputLinesAmount));
            if (str.Length > outputLineLength * outputLinesAmount)
            {
                res = res.Substring(0, outputLineLength * outputLinesAmount - 3) + "...";
            }
            return res;
        }
    }
}