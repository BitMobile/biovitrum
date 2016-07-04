using BitMobile.ClientModel3;
using System;
using System.Collections;
using System.Collections.Generic;

// ReSharper disable LoopCanBeConvertedToQuery

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
        ///     Distance in meters
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
        ///     Преобразует входящую строку к виду, который помесится в указанное количество строк интерфейса
        /// </summary>
        /// <param name="str">Строка для красивого обрезания</param>
        /// <param name="outputLineLength">Длина одной строки в интерфейсе</param>
        /// <param name="outputLinesAmount">Количество строк в интерфейсе</param>
        /// <returns>Преобразованная строка</returns>
        public static string CutForUIOutput(this string str, int outputLineLength, int outputLinesAmount)
        {
            var split = str.Split(null);
            outputLineLength = Convert.ToInt32(outputLineLength);
            outputLinesAmount = Convert.ToInt32(outputLinesAmount);
            var words = new ArrayList();
            foreach (var word in split)
            {
                if (!string.IsNullOrWhiteSpace(word))
                    words.Add(word);
            }
            bool fitAll;
            var lines = CreateLinesFromWords(outputLineLength, outputLinesAmount, words, out fitAll);
            bool? test = fitAll;
            string res = null;
            foreach (var line in lines)
            {
                res = res == null ? (string)line : $"{res} {line}";
            }
            return (bool)test ? res : $"{res?.TrimEnd()}...";
        }

        private static ArrayList CreateLinesFromWords(int outputLineLength, int outputLinesAmount, IList words, out bool fitAll)
        {
            var lines = new ArrayList { "" };
            var lastLineNumber = 0;
            var currentWordNumber = 0;
            while (currentWordNumber < words.Count && lastLineNumber < outputLinesAmount)
            {
                var word = (string)words[currentWordNumber];
                if (((string)lines[lastLineNumber]).Length + 1 + word.Length <= outputLineLength)
                {
                    lines[lastLineNumber] = string.IsNullOrEmpty((string)lines[lastLineNumber])
                        ? word
                        : $"{lines[lastLineNumber]} {word}";
                    currentWordNumber++;
                    continue;
                }
                if (word.Length > outputLineLength)
                {
                    if (string.IsNullOrEmpty((string)lines[lastLineNumber]))
                    {
                        words[currentWordNumber] = ((string)words[currentWordNumber]).Substring(outputLineLength);
                        lines[lastLineNumber] = ((string)words[currentWordNumber]).Substring(0, outputLineLength);
                    }
                    lines.Add(string.Empty);
                    lastLineNumber++;
                }
                else
                {
                    lines.Add(word);
                    currentWordNumber++;
                    lastLineNumber++;
                }
            }
            fitAll = currentWordNumber >= words.Count;
            DConsole.WriteLine($"{fitAll} = {currentWordNumber} >= {words.Count}");
            if (lines.Count > outputLinesAmount)
                lines[lines.Count - 1] = string.Empty;

            return lines;
        }
    }
}