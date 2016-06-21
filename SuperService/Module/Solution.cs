using System.Collections.Generic;
using BitMobile.ClientModel3;
using Test.Entities.Catalog;

namespace Test
{
    public class Solution : Application
    {
        public override void OnCreate()
        {
            DConsole.WriteLine("DB init...");
            DBHelper.Init();
            DConsole.WriteLine("Loading first screen");
            Navigation.Move("EquipmentScreen");
        }
    }
}