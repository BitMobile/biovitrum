using BitMobile.ClientModel3;
using System;

namespace Test
{
    public class Solution : Application
    {
        public override void OnCreate()
        {
            //            DConsole.WriteLine("DB init...");
            //            DBHelper.Init();
            //            DConsole.WriteLine("Loading first screen");
            //            Navigation.Move("AuthScreen");
            DConsole.WriteLine("Started");
            try
            {
                DConsole.WriteLine(
                    $"{"Сбербанк, \tцентр обслуживания клиентов,  \n\r    новый офис".CutForUIOutput(20, 2)}");
            }
            catch (Exception e)
            {
                DConsole.WriteLine($"{e.GetType().FullName}:{e.Message}");
                DConsole.WriteLine($"{e.StackTrace}");
            }
        }
    }
}