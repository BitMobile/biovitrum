using System;
using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;
using Test.Components;

namespace Test
{
    public class ClientScreen : Screen
    {
        private TopInfoComponent _topInfoComponent;

        public override void OnLoading()
        {
            DConsole.WriteLine("Client onloading");
            _topInfoComponent = new TopInfoComponent(this)
            {
                ExtraLayoutVisible = false,
                HeadingTextView = {Text = Translator.Translate("client")},
                LeftButtonImage = {Source = @"Image\top_back.png"},
                RightButtonImage = {Source = @"Image\top_ico_edit.png" }
            };
            DConsole.WriteLine("Client end");
        }

        internal void TopInfo_LeftButton_OnClick(object sender, EventArgs e)
        {
            BusinessProcess.DoAction("ViewEvent");
        }

        internal void TopInfo_RightButton_OnClick(object sender, EventArgs e)
        {
            
            DConsole.WriteLine("Some Action");
        }

    }
}