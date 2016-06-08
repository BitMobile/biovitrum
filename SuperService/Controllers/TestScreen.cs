using System;
using System.Collections.Generic;
using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;

namespace Test
{
    public class TestScreen : Screen
    {
        private DockLayout _rootLayout;

        public override void OnLoading()
        {
            bool test = true;
            DConsole.WriteLine($"{test}");
            if (test)
                DConsole.WriteLine("test = true");
            else
                DConsole.WriteLine("test = false");

            test = Convert.ToBoolean(test);
            DConsole.WriteLine($"{test}");
            if (test)
                DConsole.WriteLine("test = true");
            else
                DConsole.WriteLine("test = false");
        }

        internal string GetResourceImage(string tag)
        {
            return ResourceManager.GetImage(tag);
        }

        internal void Button_OnClick(object sender, EventArgs eventArgs)
        {
        }
    }
}