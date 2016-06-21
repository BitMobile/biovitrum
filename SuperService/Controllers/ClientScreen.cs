using System;
using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;
using Test.Components;

namespace Test
{
    public class ClientScreen : Screen
    {
        private DbRecordset _client;
        private WebMapGoogle _map;
        private TopInfoComponent _topInfoComponent;

        public override void OnLoading()
        {
            DConsole.WriteLine("Client onloading");
            _topInfoComponent = new TopInfoComponent(this)
            {
                ExtraLayoutVisible = false,
                HeadingTextView = {Text = Translator.Translate("client")},
                LeftButtonImage = {Source = ResourceManager.GetImage("topheading_back")},
                RightButtonImage = {Source = ResourceManager.GetImage("topheading_edit")}
            };

            _map = (WebMapGoogle) GetControl("MapClient", true);
            _map.AddMarker((string) _client["Description"], (double) _client["Latitude"], (double) _client["Longitude"],
                "red");

            DConsole.WriteLine("Client end");
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

        internal DbRecordset GetCurrentClient()
        {
            object clientId;
            if (!BusinessProcess.GlobalVariables.TryGetValue("clientId", out clientId))
            {
                DConsole.WriteLine("Can't find current client ID, going to crash");
            }
            _client = DBHelper.GetClientByID((string) clientId);
            return _client;
        }

        /// <summary>
        /// Проверяет строку на то, что она null, пустая 
        /// или представляет пробельный символ
        /// </summary>
        /// <param name="item">Строка для проверки</param>
        /// <returns>True если строка пустая, null или 
        ///     пробельный символ.
        /// </returns>
        internal bool IsNotEmptyString(string item)
        {
            return !(string.IsNullOrEmpty(item) && string.IsNullOrWhiteSpace(item));
        }

        internal DbRecordset GetContacts()
        {
            object clientContacts;
            if (!BusinessProcess.GlobalVariables.TryGetValue("clientId",out clientContacts))
            {
                DConsole.WriteLine("Can't find current clientId, i'm crash.");
            }
            DbRecordset items = DBHelper.GetContactsByClientID((string)clientContacts);
            DConsole.WriteLine((string)clientContacts);
            return items;
        }

        internal void Call_OnClick(object sender, EventArgs e)
        {
            VerticalLayout callClientLayout = (VerticalLayout)sender;
            Phone.Call(callClientLayout.Id);
        }

        internal DbRecordset GetEquipments()
        {
            object clientContacts;
            if (!BusinessProcess.GlobalVariables.TryGetValue("clientId", out clientContacts))
            {
                DConsole.WriteLine("Can't find current clientId, i'm crash.");
            }

            DbRecordset equipment = DBHelper.GetEquipmentByClientID((string) clientContacts);
            return equipment;
        }
    }
}