using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;

namespace Test
{
    public class Solution : Application
    {
        public override void OnCreate()
        {
            DConsole.WriteLine("DB init...");
            DBHelper.Init();
            DConsole.WriteLine("Starting application...");
            BusinessProcess.Init();
        }

    }
}