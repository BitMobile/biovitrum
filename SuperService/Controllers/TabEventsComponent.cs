using System;
using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;

namespace Test
{
    public class TabEventsComponent
    {
        private Screen _parentScreen;
        public TextView _textView;
        private Image _image;

        public TabEventsComponent(Screen parentScreen)
        {
            _parentScreen = parentScreen;
            SwichScreen(parentScreen);
        }

        internal void SwichScreen(Screen parentScreen)
        {
            DConsole.WriteLine(BusinessProcess.CurrentNode.Attributes?["Name"].Value);
            switch (BusinessProcess.CurrentNode.Attributes?["Name"].Value)
            {
                case "EventList":
                    _textView = (TextView)_parentScreen.GetControl("TextViewEvents", true);
                    _image = (Image)parentScreen.GetControl("ImgEvents", true);

                    _textView.CssClass = "TabTextViewActive";
                    _image.Source = @"Image\EventsActive.png";
                    break;

                case "Bag":
                    _textView = (TextView)parentScreen.GetControl("TextViewBag", true);
                    _image = (Image)parentScreen.GetControl("ImgBag", true);

                    _textView.CssClass = "TabTextViewActive";
                    _image.Source = @"Image\BagActive.png";
                    break;

                case "ClientList":
                    _textView = (TextView)parentScreen.GetControl("TextViewClients", true);
                    _image = (Image)parentScreen.GetControl("ImgClients", true);

                    _textView.CssClass = "TabTextViewActive";
                    _image.Source = @"Image\ClientsActive.png";
                    break;

                case "Settings":
                    _textView = (TextView)parentScreen.GetControl("TextViewSettings", true);
                    _image = (Image)parentScreen.GetControl("ImgSettings", true);

                    _textView.CssClass = "TabTextViewActive";
                    _image.Source = @"Image\SettingsActive.png";
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
