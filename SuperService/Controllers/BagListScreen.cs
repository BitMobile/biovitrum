using System;
using System.Net;
using System.Text;
using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;
using Test.Components;

namespace Test
{
    public class BagListScreen : Screen
    {
        private TabBarComponent _tabBarComponent;

        public override void OnLoading()
        {
            _tabBarComponent = new TabBarComponent(this);
            DConsole.WriteLine("BagListScreen init");
        }

        internal void TabEventsButton_OnClick(object sender, EventArgs eventArgs)
        {
            _tabBarComponent.Events_OnClick(sender, eventArgs);
            DConsole.WriteLine("Bag Events");

        }

        internal void TabBagButton_OnClick(object sender, EventArgs eventArgs)
        {
            //_tabBarComponent.Bag_OnClick(sender, eventArgs);
            DConsole.WriteLine("Bag Bag");

        }

        internal void TabClientsButton_OnClick(object sender, EventArgs eventArgs)
        {
            _tabBarComponent.Clients_OnClick(sender, eventArgs);
            DConsole.WriteLine("Bag Clients");

        }

        internal void TabSettingsButton_OnClick(object sender, EventArgs eventArgs)
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
