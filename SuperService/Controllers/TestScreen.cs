using System;
using System.Collections.Generic;
using BitMobile.ClientModel3.UI;

namespace Test
{
    public class TestScreen : Screen
    {
        private DockLayout _rootLayout;

        public override void OnLoading()
        {
            _rootLayout = (DockLayout) Controls[0];
        }

        internal string GetResourceImage(string tag)
        {
            return ResourceManager.GetImage(tag);
        }

        internal void Button_OnClick(object sender, EventArgs eventArgs)
        {
            BusinessProcess.DoAction("ToApplication", new Dictionary<string, object>
            {
                {"test", "lol"},
                {"foo", "bar"},
                {"bool", false},
                {"int", 42}
            });
        }
    }
}