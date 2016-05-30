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
            DConsole.WriteLine($"Latitude: {(double) _client["Latitude"]} Longitude: {(double) _client["Longitude"]}");
            DConsole.WriteLine("Client end");
        }

        internal void TopInfo_LeftButton_OnClick(object sender, EventArgs e)
        {
            BusinessProcess.DoAction("ViewEvent");
        }

        internal void TopInfo_RightButton_OnClick(object sender, EventArgs e)
        {
            BusinessProcess.DoAction("EditContact");
        }

        internal string GetResourceImage(string tag)
        {
            return ResourceManager.GetImage(tag);
        }

        internal void GoToAddContact_OnClick(object sender, EventArgs e)
        {
            BusinessProcess.DoAction("AddContact");
        }

        internal void GoToEditContact_OnClick(object sender, EventArgs e)
        {
            BusinessProcess.DoAction("EditContact");
        }

        internal DbRecordset GetCurrentClient()
        {
            object eventId;
            if (!BusinessProcess.GlobalVariables.TryGetValue("clientId", out eventId))
            {
                DConsole.WriteLine("Can't find current client ID, going to crash");
            }
            _client = DBHelper.GetClientByID((string) eventId);
            DConsole.WriteLine("Get Data");
            return _client;
        }

        internal bool IsEmptyString(string item)
        {
            return !(string.IsNullOrEmpty(item) && string.IsNullOrWhiteSpace(item));
        }
    }
}