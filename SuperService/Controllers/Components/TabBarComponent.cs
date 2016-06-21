﻿using System;
using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;

namespace Test.Components
{
    public class TabBarComponent
    {
        private readonly Screen _parentScreen;
        private TextView _textView;
        private Image _image;

        public TabBarComponent(Screen parentScreen)
        {
            _parentScreen = parentScreen;
            SwitchActiveTab(parentScreen);
        }

        internal void SwitchActiveTab(Screen parentScreen)
        {
            DConsole.WriteLine("Test");
            var screenName = Navigation.CurrentScreenInfo.Name;
            DConsole.WriteLine(screenName);
//        DConsole.WriteLine(screenInfo.Name);
            switch (screenName)
            {
                case "EventListScreen":
                    _textView = (TextView) _parentScreen.GetControl("TabBarFirstTabTextView", true);
                    _image = (Image) parentScreen.GetControl("TabBarFirstTabImage", true);

                    _textView.CssClass = "TabTextViewActive";
                    _image.Source = ResourceManager.GetImage("tabbar_events_active");
                    break;

                case "BagListScreen":
                    _textView = (TextView) parentScreen.GetControl("TabBarSecondTabTextView", true);
                    _image = (Image) parentScreen.GetControl("TabBarSecondTabImage", true);

                    _textView.CssClass = "TabTextViewActive";
                    _image.Source = ResourceManager.GetImage("tabbar_bag_active");
                    break;

                case "ClientListScreen":
                    _textView = (TextView) parentScreen.GetControl("TabBarThirdTabTextView", true);
                    _image = (Image) parentScreen.GetControl("TabBarThirdTabImage", true);

                    _textView.CssClass = "TabTextViewActive";
                    _image.Source = ResourceManager.GetImage("tabbar_clients_active");
                    break;

                case "SettingsScreen":
                    _textView = (TextView) parentScreen.GetControl("TabBarFourthTabTextView", true);
                    _image = (Image) parentScreen.GetControl("TabBarFourthTabImage", true);

                    _textView.CssClass = "TabTextViewActive";
                    _image.Source = ResourceManager.GetImage("tabbar_settings_active");
                    break;
            }
        }

        internal void Events_OnClick(object sender, EventArgs e)
        {
            Navigation.ModalMove("EventListScreen");
        }

        internal void Bag_OnClick(object sender, EventArgs e)
        {
            Navigation.ModalMove("BagListScreen");
        }

        internal void Clients_OnClick(object sender, EventArgs e)
        {
            Navigation.ModalMove("ClientListScreen");
        }

        internal void Settings_OnClick(object sender, EventArgs e)
        {
            Navigation.ModalMove("SettingsScreen");
        }
    }
}