using System;
using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;

namespace Test.Components
{
    public class TabEventsComponent
    {
        private readonly Screen _parentScreen;
        private TextView _textView;
        private Image _image;

        public TabEventsComponent(Screen parentScreen)
        {
            _parentScreen = parentScreen;
            SwitchScreen(parentScreen);
        }

        internal void SwitchScreen(Screen parentScreen)
        {
            DConsole.WriteLine(BusinessProcess.CurrentNode.Attributes?["Name"].Value);
            switch (BusinessProcess.CurrentNode.Attributes?["Name"].Value)
            {
                case "EventList":
                    _textView = (TextView)_parentScreen.GetControl("TextViewEvents", true);
                    _image = (Image)parentScreen.GetControl("ImgEvents", true);

                    _textView.CssClass = "TabTextViewActive";
                    _image.Source = @"Image\_Components\TabBar\EventsActive.png";
                    break;

                case "Bag":
                    _textView = (TextView)parentScreen.GetControl("TextViewBag", true);
                    _image = (Image)parentScreen.GetControl("ImgBag", true);

                    _textView.CssClass = "TabTextViewActive";
                    _image.Source = @"Image\_Components\TabBar\BagActive.png";
                    break;

                case "ClientList":
                    _textView = (TextView)parentScreen.GetControl("TextViewClients", true);
                    _image = (Image)parentScreen.GetControl("ImgClients", true);

                    _textView.CssClass = "TabTextViewActive";
                    _image.Source = @"Image\_Components\TabBar\ClientsActive.png";
                    break;

                case "Settings":
                    _textView = (TextView)parentScreen.GetControl("TextViewSettings", true);
                    _image = (Image)parentScreen.GetControl("ImgSettings", true);

                    _textView.CssClass = "TabTextViewActive";
                    _image.Source = @"Image\_Components\TabBar\SettingsActive.png";
                    break;
            }
        }

        internal void Events_OnClick(object sender, EventArgs e)
        {
            BusinessProcess.DoAction("EventList");
        }

        internal void Bag_OnClick(object sender, EventArgs e)
        {
            BusinessProcess.DoAction("Bag");
        }

        internal void Clients_OnClick(object sender, EventArgs e)
        {
            BusinessProcess.DoAction("ClientList");
        }

        internal void Settings_OnClick(object sender, EventArgs e)
        {
            BusinessProcess.DoAction("Settings");
        }
    }
}
