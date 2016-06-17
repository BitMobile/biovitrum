using System;
using System.Collections;
using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;
using Test.Components;

namespace Test
{
    public class EventListScreen : Screen
    {
        private TabBarComponent _tabBarComponent;
        private TopInfoComponent _topInfoComponent;

        public override void OnLoading()
        {
            DConsole.WriteLine("OnLoanding EventList");

            _tabBarComponent = new TabBarComponent(this);
            _topInfoComponent = new TopInfoComponent(this)
            {
                LeftButtonImage = {Source = ResourceManager.GetImage("topheading_filter")},
                RightButtonImage = {Source = ResourceManager.GetImage("topheading_map") },
                HeadingTextView = {Text = Translator.Translate("orders")},
                LeftExtraLayout = {CssClass = "ExtraLeftLayoutCss"},
                RightExtraLayout = {CssClass = "ExtraRightLayoutCss"}
            };

            var statistic = DBHelper.GetEventsStatistic();
            _topInfoComponent.LeftExtraLayout.AddChild(new TextView($"{statistic.DayCompleteAmout}/{statistic.DayTotalAmount}") {CssClass = "ExtraInfo"});
            _topInfoComponent.LeftExtraLayout.AddChild(new TextView(Translator.Translate("today"))
            {
                CssClass = "BottonExtraInfo"
            });
            _topInfoComponent.RightExtraLayout.AddChild(
                new TextView($"{statistic.MonthCompleteAmout}/{statistic.MonthTotalAmount}") {CssClass = "ExtraInfo"});
            _topInfoComponent.LeftExtraLayout.AddChild(new TextView(Translator.Translate("per_month"))
            {
                CssClass = "BottonExtraInfo"
            });

            DConsole.WriteLine("FillingOrderList");
        }





        internal void TopInfo_LeftButton_OnClick(object sender, EventArgs e)
        {
        }


        internal void TopInfo_Arrow_OnClick(object sender, EventArgs e)
        {
            _topInfoComponent.Arrow_OnClick(sender, e);
        }

        internal void TopInfo_RightButton_OnClick(object sender, EventArgs e)
        {
            DConsole.WriteLine("GO to map");
            BusinessProcess.DoAction("ViewMap");
        }

        internal void EventLayout_OnClick(object sender, EventArgs e)
        {
            DConsole.WriteLine("Go To View Event");
            var currentEvent = (HorizontalLayout) sender;
            BusinessProcess.GlobalVariables["currentEventId"] = currentEvent.Id;
            BusinessProcess.DoAction("ViewEvent");
        }


        // TabBar buttons
        internal void TabBarFirstTabButton_OnClick(object sender, EventArgs eventArgs)
        {
            //_tabBarComponent.Events_OnClick(sender, eventArgs);
            DConsole.WriteLine("Settings Events");
        }

        internal void TabBarSecondTabButton_OnClick(object sender, EventArgs eventArgs)
        {
            _tabBarComponent.Bag_OnClick(sender, eventArgs);
            DConsole.WriteLine("Settings Bag");
        }

        internal void TabBarThirdButton_OnClick(object sender, EventArgs eventArgs)
        {
            _tabBarComponent.Clients_OnClick(sender, eventArgs);
            DConsole.WriteLine("Settings Clients");
        }

        internal void TabBarFourthButton_OnClick(object sender, EventArgs eventArgs)
        {
            _tabBarComponent.Settings_OnClick(sender, eventArgs);
            DConsole.WriteLine("Settings Settings");
        }

        internal IEnumerable GetEvents()
        {
            return DBHelper.GetEvents();
        }

        internal string GetResourceImage(string tag)
        {
            return ResourceManager.GetImage(tag);
        }
    }
}