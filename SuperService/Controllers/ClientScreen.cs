﻿using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;
using BitMobile.Common.Controls;
using BitMobile.DbEngine;
using System;
using System.Collections.Generic;
using Test.Catalog;
using Test.Components;
using DbRecordset = BitMobile.ClientModel3.DbRecordset;

namespace Test
{
    public class ClientScreen : Screen
    {
        private DbRecordset _client;
        private string _clientId;
        private WebMapGoogle _map;
        private TopInfoComponent _topInfoComponent;
        private string _clientDesc;

        public override void OnLoading()
        {
            DConsole.WriteLine("Client onloading");
            _topInfoComponent = new TopInfoComponent(this)
            {
                Header = Translator.Translate("client"),
                LeftButtonControl = new Image { Source = ResourceManager.GetImage("topheading_back") },
                RightButtonControl = new Image { Source = ResourceManager.GetImage("topheading_edit") },
                ArrowVisible = false
            };
            _topInfoComponent.ActivateBackButton();

            var latitude = Converter.ToDouble(_client["Latitude"]);
            var longitude = Converter.ToDouble(_client["Longitude"]);

            if (!latitude.Equals(0.0) && !longitude.Equals(0.0))
            {
                _map = (WebMapGoogle)GetControl("MapClient", true);
                _map.AddMarker((string)_client["Description"], latitude,
                    longitude, "red");
            }

            _clientDesc = GetConstLenghtString(_client["Description"].ToString());
            DConsole.WriteLine("Client end");
        }

        public override void OnShow()
        {
            BusinessProcess.GlobalVariables.Remove(Parameters.IdProfileId);
        }

        internal void TopInfo_LeftButton_OnClick(object sender, EventArgs e)
        {
            Navigation.Back();
        }

        internal void TopInfo_RightButton_OnClick(object sender, EventArgs e)
        {
            BusinessProcess.GlobalVariables[Parameters.IdClientId] = _clientId;
            Navigation.Move(nameof(ParameterListScreen));
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
            Navigation.Move("EditContactScreen", new Dictionary<string, object>
            {
                [Parameters.Contact] = new Contacts
                {
                    Id = DbRef.CreateInstance("Catalog_Contacts", Guid.NewGuid()),
                },
                [Parameters.IdClientId] = _clientId
            });
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

        internal void ContactContainerLayout_OnClick(object sender, EventArgs eventArgs)
        {
            var id = ((HorizontalLayout)((VerticalLayout)sender).Parent).Id;
            var contacts = (Contacts)DbRef.FromString(id).GetObject();

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
            var latitude = Converter.ToDouble(_client["Latitude"]);
            var longitude = Converter.ToDouble(_client["Longitude"]);
            if (latitude.Equals(0.0) && longitude.Equals(0.0)) return "NaN";
            if (Math.Abs(latitude) < 0.1 && Math.Abs(longitude) < 0.1) return "NaN";

            var coordinate = DBHelper.GetCoordinate(TimeRangeCoordinate.DefaultTimeRange);
            var lastLatitude = Converter.ToDouble(coordinate["Latitude"]);
            var lastLongitude = Converter.ToDouble(coordinate["Longitude"]);

            var distanceInKm =
                Utils.GetDistance(lastLatitude, lastLongitude,
                    latitude, longitude) / 1000;
            return
                $"{Math.Round(distanceInKm, 2)}" +
                $" {Translator.Translate("uom_distance")}";
        }

        internal bool ShowEquipment() => Settings.EquipmentEnabled;
    }
}