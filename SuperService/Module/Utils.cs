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
        ///    Преобразует входящую строку к виду, который помесится в указанное количество строк интерфейса
        /// </summary>
        /// <param name="outputLineLength"> длина строк в символах</param>
        /// <param name="outputLinesAmount"> количество строк</param>
        /// <returns></returns>
        public static string CutForUIOutput(this String str, int outputLineLength, int outputLinesAmount)
        {
            //пока это заглушка, т.к. алгоритм обрезки использовал рекурсию которая не поддерживается. 

            var res =  str.Substring(0, Math.Min(str.Length, outputLineLength * outputLinesAmount));
            if(str.Length > outputLineLength * outputLinesAmount)
            {
                res = res.Substring(0, outputLineLength*outputLinesAmount - 3) + "...";
            }
            return res;
        }

    }
}