using System;
using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;
using Test.Components;

namespace Test
{
    public class MapScreen : Screen
    {
        private static WebMapGoogle _map;
        private TopInfoComponent _topInfoComponent;
        private bool isClientScreen = Convert.ToBoolean("False");

        public override void OnLoading()
        {
            _map = (WebMapGoogle) GetControl("Map", true);
            _topInfoComponent = new TopInfoComponent(this)
            {
                ExtraLayoutVisible = false,
                HeadingTextView = {Text = Translator.Translate("map")},
                RightButtonImage = {Visible = false}
            };
            DConsole.WriteLine("MapScreen");
        }

        public override void OnShow()
        {
        }

        internal void TopInfo_LeftButton_OnClick(object sender, EventArgs e)
        {
            DConsole.WriteLine("Back to screen .....");
            BusinessProcess.DoBack();
        }

        internal void TopInfo_RightButton_OnClick(object sender, EventArgs e)
        {
        }

        internal void TopInfo_Arrow_OnClick(object sender, EventArgs e)
        {
        }

        internal string GetResourceImage(string tag)
        {
            return ResourceManager.GetImage(tag);
        }
    }

    public enum MapScreenStates
    {
        EventList,
        Client,
    }
}