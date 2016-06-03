using System;
using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;
using Test.Components;
using System.Collections;

namespace Test
{
    public class ClientListScreen : Screen
    {
        private TabBarComponent _tabBarComponent;
        private TopInfoComponent _topInfoComponent;

        public override void OnLoading()
        {
           
            DConsole.WriteLine("ClientListScreen init");

            _topInfoComponent = new TopInfoComponent(this)
            {
                HeadingTextView = { Text = Translator.Translate("clients") },
                LeftButtonImage = { Visible = false },
                RightButtonImage = { Visible = false },
                ExtraLayoutVisible = false
            };

            _tabBarComponent = new TabBarComponent(this);
        }

        internal void TabEventsButton_OnClick(object sender, EventArgs eventArgs)
        {
            _tabBarComponent.Events_OnClick(sender, eventArgs);
            DConsole.WriteLine("Clients Events");
        }

        internal void TabBagButton_OnClick(object sender, EventArgs eventArgs)
        {
            _tabBarComponent.Bag_OnClick(sender, eventArgs);
            DConsole.WriteLine("Clients Bag");
        }

        internal void TabClientsButton_OnClick(object sender, EventArgs eventArgs)
        {
            //_tabBarComponent.Clients_OnClick(sender, eventArgs);
            DConsole.WriteLine("Clients Clients");
        }

        internal void TabSettingsButton_OnClick(object sender, EventArgs eventArgs)
        {
            _tabBarComponent.Settings_OnClick(sender, eventArgs);
            DConsole.WriteLine("Clients Settings");
        }

        internal string GetResourceImage(string tag)
        {
            return ResourceManager.GetImage(tag);
        }

        internal string GetResourceComponent(string tag)
        {
            return ResourceManager.GetComponent(tag);
        }

        internal void ClientLayout_OnClick(object sender, EventArgs eventArgs)
        {
            DConsole.WriteLine("ClientLayout_OnClick " + ((VerticalLayout)sender).Id);
            // TODO: Передача Id конкретной таски
            BusinessProcess.GlobalVariables["clientId"] = ((VerticalLayout)sender).Id;
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
