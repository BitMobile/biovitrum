using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;
using System;
using System.Collections.Generic;
using Test.Components;

namespace Test
{
    public class TestScreen : Screen
    {
        private TopInfoComponent _topInfoComponent;

        public override void OnLoading()
        {
            DConsole.WriteLine("OnLoading?");
            try
            {
                _topInfoComponent = new TopInfoComponent(this)
                {
                    RightButtonControl = new Image() { Source = ResourceManager.GetImage("topheading_info") },
                    LeftButtonControl = new Image() { Source = ResourceManager.GetImage("topheading_back") },
                    Header = "Газпром нефть"
                };
            }
            catch (Exception e)
            {
                DConsole.WriteLine("test");
                DConsole.WriteLine(e.ToString());
            }
            DConsole.WriteLine("OnLoading end");
        }

        public override void OnShow()
        {
            DConsole.WriteLine("OnShow?");
        }

        internal string GetResourceImage(string tag)
        {
            return ResourceManager.GetImage(tag);
        }

        internal void Button_OnClick(object sender, EventArgs eventArgs)
        {
        }

        internal void TopInfo_LeftButton_OnClick(object sender, EventArgs eventArgs)
        {
        }

        internal void TopInfo_RightButton_OnClick(object sender, EventArgs eventArgs)
        {
        }

        internal void TopInfo_Arrow_OnClick(object sender, EventArgs eventArgs)
        {
            _topInfoComponent.Arrow_OnClick(sender, eventArgs);
        }
    }
}