using System;
using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;
using Test.Components;

namespace Test
{
    public class ClientScreen : Screen
    {
        private TopComponent _topComponent;

        public override void OnLoading()
        {
            _topComponent = new TopComponent(this);
            _topComponent.TopHeadingTextView.Text = Translator.Translate("client");
            _topComponent.TopRightButtonImage.Source = @"Image\top_ico_edit.png";
        }

        internal void Top_LeftButton_OnClick(object sender, EventArgs e)
        {
            _topComponent.LeftButton_OnClick("ViewEvent");
        }

        internal void Top_RightButton_OnClick(object sender, EventArgs e)
        {
            //_topComponent.RightButton_OnClick();
            DConsole.WriteLine("Some Action");
        }

    }
}