using BitMobile.ClientModel3;
using System.Collections.Generic;
using Test.Catalog;

namespace Test
{
    public class Solution : Application
    {
        public override void OnCreate()
        {
            DConsole.WriteLine("DB init...");
            DBHelper.Init();
            DConsole.WriteLine("Loading first screen");
            Navigation.Move("EditContactScreen", new Dictionary<string, object>
            {
                ["contact"] = new Contacts()
                {
                    Description = "Один одиныч",
                    Position = "Президентыч",
                    Tel = "+1 (234) 567-89-00",
                    EMail = "omg@example.huy"
                }
            });
        }
    }
}