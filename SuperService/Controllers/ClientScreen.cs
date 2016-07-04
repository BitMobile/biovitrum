using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;
using BitMobile.DbEngine;
using System;
using System.Collections.Generic;
using Test.Components;
using Test.Entities.Catalog;
using DbRecordset = BitMobile.ClientModel3.DbRecordset;

namespace Test
{
    public class ClientScreen : Screen
    {
        private DbRecordset _client;
        private string _clientId;
        private WebMapGoogle _map;
        private TopInfoComponent _topInfoComponent;

        public override void OnLoading()
        {
            DConsole.WriteLine("Client onloading");
            _topInfoComponent = new TopInfoComponent(this)
            {
                ExtraLayoutVisible = false,
                HeadingTextView = { Text = Translator.Translate("client") },
                LeftButtonImage = { Source = ResourceManager.GetImage("topheading_back") },
                RightButtonImage = { Source = ResourceManager.GetImage("topheading_edit") }
            };

            _map = (WebMapGoogle)GetControl("MapClient", true);
            _map.AddMarker((string)_client["Description"], (double)_client["Latitude"], (double)_client["Longitude"],
                "red");

            DConsole.WriteLine("Client end");
        }

        public override void OnShow()
        {
            GPS.StartTracking();
        }

        internal void TopInfo_LeftButton_OnClick(object sender, EventArgs e)
        {
            Navigation.Back();
        }

        internal void TopInfo_RightButton_OnClick(object sender, EventArgs e)
        {
            Navigation.Move("EditContactScreen");
        }

        internal void TopInfo_Arrow_OnClick(object sender, EventArgs e)
        {
            _topInfoComponent.Arrow_OnClick(sender, e);
        }

        internal string GetResourceImage(string tag)
        {
            return ResourceManager.GetImage(tag);
        }

        internal void GoToAddContact_OnClick(object sender, EventArgs e)
        {
            Navigation.Move("AddContactScreen");
        }

        internal void GoToEditContact_OnClick(object sender, EventArgs e)
        {
            Navigation.Move("EditContactScreen");
        }

        internal void EquipmentLayout_OnClick(object sender, EventArgs e)
        {
            var layout = (VerticalLayout)sender;
            var dictionary = new Dictionary<string, object>()
            {
                {Parameters.IdEquipmentId,layout.Id }
            };
            Navigation.Move("EquipmentScreen", dictionary);
        }

        internal DbRecordset GetCurrentClient()
        {
            object clientId;
            if (!BusinessProcess.GlobalVariables.TryGetValue(Parameters.IdClientId, out clientId))
            {
                DConsole.WriteLine("Can't find current client ID, going to crash");
            }
            _clientId = (string)clientId;
            _client = DBHelper.GetClientByID(_clientId);
            return _client;
        }

        /// <summary>
        ///     Проверяет строку на то, что она null, пустая
        ///     или представляет пробельный символ
        /// </summary>
        /// <param name="item">Строка для проверки</param>
        /// <returns>
        ///     True если строка пустая, null или
        ///     пробельный символ.
        /// </returns>
        internal bool IsNotEmptyString(string item)
        {
            return !(string.IsNullOrEmpty(item) && string.IsNullOrWhiteSpace(item));
        }

        internal DbRecordset GetContacts()
        {
            object clientContacts;
            if (!BusinessProcess.GlobalVariables.TryGetValue(Parameters.IdClientId, out clientContacts))
            {
                DConsole.WriteLine("Can't find current clientId, i'm crash.");
            }

            var items = DBHelper.GetContactsByClientID((string)clientContacts);

            return items;
        }

        internal void Call_OnClick(object sender, EventArgs e)
        {
            var callClientLayout = (VerticalLayout)sender;
            Phone.Call(callClientLayout.Id);
        }

        internal DbRecordset GetEquipments()
        {
            object clientContacts;
            if (!BusinessProcess.GlobalVariables.TryGetValue(Parameters.IdClientId, out clientContacts))
            {
                DConsole.WriteLine("Can't find current clientId, i'm crash.");
            }

            var equipment = DBHelper.GetEquipmentByClientID((string)clientContacts);
            return equipment;
        }

        internal void GoToMapScreen_OnClick(object sender, EventArgs e)
        {
            DConsole.WriteLine($"{nameof(GoToMapScreen_OnClick)} Start");
            var dictionary = new Dictionary<string, object>
            {
                {Parameters.IdScreenStateId, MapScreenStates.ClientScreen},
                {Parameters.IdClientId, _clientId}
            };
            BusinessProcess.GlobalVariables.Remove(Parameters.IdScreenStateId);
            BusinessProcess.GlobalVariables.Remove(Parameters.IdClientId);
            BusinessProcess.GlobalVariables[Parameters.IdScreenStateId] = MapScreenStates.ClientScreen;
            BusinessProcess.GlobalVariables[Parameters.IdClientId] = _clientId;

            DConsole.WriteLine($"{nameof(GoToMapScreen_OnClick)} end");
            Navigation.Move("MapScreen", dictionary);
        }

        internal void ContactLayout_OnClick(object sender, EventArgs eventArgs)
        {
            var id = ((HorizontalLayout)sender).Id;
            var contact = DBHelper.GetContactById(id);
            var contacts = new Contacts((DbRef)contact["Id"])
            {
                DeletionMark = (bool)contact["DeletionMark"],
                Description = (string)contact["Description"],
                Code = (string)contact["Code"],
                Position = (string)contact["Position"],
                Tel = (string)contact["Tel"],
                EMail = (string)contact["EMail"]
            };

            DConsole.WriteLine("мыло контакта =" + contacts.EMail);
            Navigation.Move("ContactScreen", new Dictionary<string, object>
            {
                [Parameters.Contact] = contacts
            });
        }

        internal string GetConstLenghtString(string item)
        {
            return item.Length > 40 ? item.Substring(0, 40) : item;
        }

        internal string GetDistance()
        {
            var distanceInKm =
                Utils.GetDistance(GPS.CurrentLocation.Latitude, GPS.CurrentLocation.Longitude,
                    (double)_client["Latitude"], (double)_client["Longitude"]) / 1000;
            return
                $"{Math.Round(distanceInKm, 2)}" +
                $" {Translator.Translate("uom_distance")}";
        }
    }
}