using System;
using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;

namespace Test
{
    public class MapScreen : Screen
    {
        public override void OnLoading()
        {
            DConsole.WriteLine("MapScreen");
        }

        internal void BackButton_OnClick(object sender, EventArgs eventArgs)
        {
            BusinessProcess.DoAction("BackToEventList");
        }
    }
}