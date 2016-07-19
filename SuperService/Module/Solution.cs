using BitMobile.ClientModel3;
using System.Collections.Generic;

namespace Test
{
    public class Solution : Application
    {
        public override void OnCreate()
        {
            DConsole.WriteLine("DB init...");
            DBHelper.Init();
            DConsole.WriteLine("Settings init...");
            Settings.Init();
            DConsole.WriteLine("Loading first screen");
            Navigation.Move(nameof(EditServicesOrMaterialsScreen), new Dictionary<string, object>
            {
                ["lineId"] = "@ref[Document_Event_ServicesMaterials]:420f3e9c-9d3e-e611-80ee-485b39d77350",
                ["value"] = 2
            });
        }
    }
}