using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;
using System;
using System.Net;
using System.Text;
using Test.Components;

namespace Test
{
    public class SettingsScreen : Screen
    {
        private TabBarComponent _tabBarComponent;
        private TopInfoComponent _topInfoComponent;

        // TODO: Тут будет экран настроек

        public override void OnLoading()
        {
            DConsole.WriteLine("SettingsScreen init");

            _topInfoComponent = new TopInfoComponent(this)
            {
                Header = Translator.Translate("settings"),
            };

            _tabBarComponent = new TabBarComponent(this);
        }

        internal void TopInfo_LeftButton_OnClick(object sender, EventArgs e)
        {
        }

        internal void TopInfo_RightButton_OnClick(object sender, EventArgs e)
        {
        }

        internal void TopInfo_Arrow_OnClick(object sender, EventArgs e)
        {
        }

        internal void TabBarFirstTabButton_OnClick(object sender, EventArgs eventArgs)
        {
            _tabBarComponent.Events_OnClick(sender, eventArgs);
            DConsole.WriteLine("Settings Events");
        }

        internal void TabBarSecondTabButton_OnClick(object sender, EventArgs eventArgs)
        {
            _tabBarComponent.Bag_OnClick(sender, eventArgs);
            DConsole.WriteLine("Settings Bag");
        }

        internal void TabBarThirdButton_OnClick(object sender, EventArgs eventArgs)
        {
            _tabBarComponent.Clients_OnClick(sender, eventArgs);
            DConsole.WriteLine("Settings Clients");
        }

        internal void TabBarFourthButton_OnClick(object sender, EventArgs eventArgs)
        {
            //_tabBarComponent.Settings_OnClick(sender, eventArgs);
            DConsole.WriteLine("Settings Settings");
        }

        internal string GetResourceImage(string tag)
        {
            return ResourceManager.GetImage(tag);
        }
    }
}