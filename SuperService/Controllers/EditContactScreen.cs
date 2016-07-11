using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;
using System;
using Test.Components;

namespace Test
{
    internal class EditContactScreen : Screen
    {
        private TopInfoComponent _topInfoComponent;

        public override void OnLoading()
        {
            _topInfoComponent = new TopInfoComponent(this)
            {
                Header = Translator.Translate("contact"),
                LeftButtonControl = new TextView(Translator.Translate("cancel")),
                RightButtonControl = new TextView(Translator.Translate("save")),
                ArrowVisible = false
            };
        }

        internal void TopInfo_LeftButton_OnClick(object sender, EventArgs e)
        {
            Navigation.Back();
        }

        internal void TopInfo_RightButton_OnClick(object sender, EventArgs e)
        {
            // TODO
        }

        internal void TopInfo_Arrow_OnClick(object sender, EventArgs e)
        {
            _topInfoComponent.Arrow_OnClick(sender, e);
        }

        internal string GetResourceImage(string tag)
        {
            return ResourceManager.GetImage(tag);
        }

        internal void Back_OnClick(object sender, EventArgs e)
        {
            Navigation.Back();
        }
    }
}