using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;
using System;
using Test.Components;

namespace Test
{
    public class PhotoScreen : Screen
    {
        private TopInfoComponent _topInfoComponent;

        public override void OnLoading()
        {
            _topInfoComponent = new TopInfoComponent(this)
            {
                Header = Translator.Translate("photo"),
                LeftButtonControl = new Image { Source = ResourceManager.GetImage("topheading_back") },
                RightButtonControl = new Image { Visible = false }
            };
        }

        internal string GetResourceImage(string tag)
        {
            return ResourceManager.GetImage(tag);
        }

        internal void TopInfo_LeftButton_OnClick(object sender, EventArgs eventArgs)
        {
            Navigation.Back();
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