using System;
using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;
using Test.Components;

namespace Test
{
    public class BagListScreen : Screen
    {
        private TabBarComponent _tabBarComponent;
        private TopInfoComponent _topInfoComponent;

        public override void OnLoading()
        {
            DConsole.WriteLine("BagListScreen init");

            _topInfoComponent = new TopInfoComponent(this)
            {
                HeadingTextView = {Text = Translator.Translate("bag")},
                LeftButtonImage = {Source = ResourceManager.GetImage("baglistscreen_busket")},
                RightButtonImage = {Source = ResourceManager.GetImage("baglistscreen_plus")},
                ExtraLayoutVisible = false
            };

            _tabBarComponent = new TabBarComponent(this);
        }

        internal void TopInfo_LeftButton_OnClick(object sender, EventArgs e)
        {
            BusinessProcess.DoAction("RequestHistory");
        }

        internal void TopInfo_RightButton_OnClick(object sender, EventArgs e)
        {
            // TODO: Добавление заявки на материал
        }

        internal void TopInfo_Arrow_OnClick(object sender, EventArgs e)
        {
        }

        internal void TabBarFirstTabButton_OnClick(object sender, EventArgs eventArgs)
        {
            _tabBarComponent.Events_OnClick(sender, eventArgs);
            DConsole.WriteLine("Bag Events");
        }

        internal void TabBarSecondTabButton_OnClick(object sender, EventArgs eventArgs)
        {
            //_tabBarComponent.Bag_OnClick(sender, eventArgs);
            DConsole.WriteLine("Bag Bag");
        }

        internal void TabBarThirdButton_OnClick(object sender, EventArgs eventArgs)
        {
            _tabBarComponent.Clients_OnClick(sender, eventArgs);
            DConsole.WriteLine("Bag Clients");
        }

        internal void TabBarFourthButton_OnClick(object sender, EventArgs eventArgs)
        {
            _tabBarComponent.Settings_OnClick(sender, eventArgs);
            DConsole.WriteLine("Bag Settings");
        }

        internal string GetResourceImage(string tag)
        {
            return ResourceManager.GetImage(tag);
        }
    }
}