using System;
using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;
using Test.Components;
using System.Collections;

namespace Test
{
    public class ClientListScreen : Screen
    {
        private TabEventsComponent _tabEventsComponent;

        public override void OnLoading()
        {
         //   _tabEventsComponent = new TabEventsComponent(this);
            DConsole.WriteLine("ClientListScreen init");
        }

        internal void TabEventsButton_OnClick(object sender, EventArgs eventArgs)
        {
            _tabEventsComponent.Events_OnClick(sender, eventArgs);
            DConsole.WriteLine("Clients Events");
        }

        internal void TabBagButton_OnClick(object sender, EventArgs eventArgs)
        {
            _tabEventsComponent.Bag_OnClick(sender, eventArgs);
            DConsole.WriteLine("Clients Bag");
        }

        internal void TabClientsButton_OnClick(object sender, EventArgs eventArgs)
        {
            //_tabEventsComponent.Clients_OnClick(sender, eventArgs);
            DConsole.WriteLine("Clients Clients");
        }

        internal void TabSettingsButton_OnClick(object sender, EventArgs eventArgs)
        {
            _tabEventsComponent.Settings_OnClick(sender, eventArgs);
            DConsole.WriteLine("Clients Settings");
        }

        internal string GetResourceImage(string tag)
        {
            return ResourceManager.GetImage(tag);
        }

        internal void ClientLayout_OnClick(object sender, EventArgs eventArgs)
        {
            DConsole.WriteLine("ClientLayout_OnClick " + ((Button)sender).Id);
            // TODO: Передача Id конкретной таски
            BusinessProcess.GlobalVariables["currentClientId"] = ((Button)sender).Id;
            BusinessProcess.DoAction("ViewClient");
        }


        internal IEnumerable GetClients()
        {
            DConsole.WriteLine("получение клиентов");
            var result = DBHelper.GetClients();
            DConsole.WriteLine("Получили клиентов");

            //var result2 = DBHelper.GetClients();
           // var dbEx = result2.Unload();
            //DConsole.WriteLine("in result " + dbEx.Count());

            return result;
        }
    }
}
