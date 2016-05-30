using System;
using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;
using Test.Components;
using Test;

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
                LeftButtonImage = {Source = ResourceManager.GetImage("topheading_back") },
                RightButtonImage = {Source = ResourceManager.GetImage("topheading_edit") }
            };

            DConsole.WriteLine((string)BusinessProcess.GlobalVariables["currentEventId"]);
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

        internal DbRecordset GetCurrentEvent()
        {
            object eventId;
            if (!BusinessProcess.GlobalVariables.TryGetValue("currentEventId", out eventId))
            {
                DConsole.WriteLine("Can't find current event ID, going to crash");
            }

            return DBHelper.GetEventByID((string)eventId);
        }

        internal bool IsEmptyString(string item)
        {
            return !(string.IsNullOrEmpty(item) && string.IsNullOrWhiteSpace(item));
        }
    }
}