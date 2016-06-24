using System;
using System.Collections;
using System.Collections.Generic;
using BitMobile.Application.Tracking;
using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;
using Test.Components;

namespace Test
{
    public class EventListScreen : Screen
    {
        private ArrayList _eventsList;
        private ScrollView _svlEventList;
        private TabBarComponent _tabBarComponent;
        private TopInfoComponent _topInfoComponent;

        public override void OnLoading()
        {
            DConsole.WriteLine("OnLoanding EventList");
            _svlEventList = (ScrollView) GetControl("EventListScrollView", true);

            _eventsList = GetEventsFromDb();

            _tabBarComponent = new TabBarComponent(this);

            _topInfoComponent = new TopInfoComponent(this)
            {
                LeftButtonImage = {Source = ResourceManager.GetImage("topheading_filter")},
                RightButtonImage = {Source = ResourceManager.GetImage("topheading_map")},
                HeadingTextView = {Text = Translator.Translate("orders")},
                LeftExtraLayout = {CssClass = "ExtraLeftLayoutCss"},
                RightExtraLayout = {CssClass = "ExtraRightLayoutCss"}
            };

            var statistic = DBHelper.GetEventsStatistic();
            _topInfoComponent.LeftExtraLayout.AddChild(
                new TextView($"{statistic.DayCompleteAmout}/{statistic.DayTotalAmount}") {CssClass = "ExtraInfo"});
            _topInfoComponent.LeftExtraLayout.AddChild(new TextView(Translator.Translate("today"))
            {
                CssClass = "BottonExtraInfo"
            });
            _topInfoComponent.RightExtraLayout.AddChild(
                new TextView($"{statistic.MonthCompleteAmout}/{statistic.MonthTotalAmount}") {CssClass = "ExtraInfo"});
            _topInfoComponent.RightExtraLayout.AddChild(new TextView(Translator.Translate("per_month"))
            {
                CssClass = "BottonExtraInfo"
            });

            DConsole.WriteLine("FillingOrderList");
            FillingOrderList();
        }


        public override void OnShow()
        {
            GPS.StartTracking();
        }

        private void FillingOrderList()
        {
            if (_eventsList == null)
                return;

            var currenDate = DateTime.Now;
            var isHeaderAdded = false;
            VerticalLayout orderInfoLayout = null;


            foreach (var variable in _eventsList)
            {
                var itemElement = (EventListElement) variable;

                if (itemElement.StartDatePlan.Date <= currenDate.Date)
                {
                    if (orderInfoLayout != null)
                    {
                        orderInfoLayout.AddChild(new HorizontalLine {CssClass = "ClientHorizontalLine"});
                    }

                    FillEventList(ref isHeaderAdded, ref itemElement, ref orderInfoLayout);
                }
                else
                {
                    _svlEventList.AddChild(new HorizontalLine {CssClass = "FinalDateLine"});
                    currenDate = itemElement.StartDatePlan;
                    isHeaderAdded = false;
                    FillEventList(ref isHeaderAdded, ref itemElement, ref orderInfoLayout);
                }
            }
        }


        private void FillEventList(ref bool isHeaderAdded, ref EventListElement itemElement,
            ref VerticalLayout orderInfoRefLayout)
        {
            TextView dateText;
            HorizontalLine finalDateLine;
            HorizontalLayout eventLayout;
            VerticalLayout timeLayout;
            TextView startDatePlaneTextView;
            TextView actualStartDateTextView;
            VerticalLayout importanceLayout;
            HorizontalLayout importanceIndicatorLayout;
            VerticalLayout orderInfoLayout;
            TextView clientDescriptionTextView;
            TextView clientAdressTextView;
            TextView typeDeparturesTextView;


            if (!isHeaderAdded)
            {
                if (itemElement.StartDatePlan.Date <= DateTime.Now.Date)
                {
                    dateText = new TextView(Translator.Translate("todayUpper")) {CssClass = "DateText"};
                    finalDateLine = new HorizontalLine {CssClass = "FinalDateLine"};
                    _svlEventList.AddChild(dateText);
                    _svlEventList.AddChild(finalDateLine);
                    isHeaderAdded = true;
                }

                else
                {
                    dateText = new TextView
                    {
                        CssClass = "DateText",
                        Text = itemElement.StartDatePlan.Date.ToString("dddd, dd MMMM").ToUpper()
                    };
                    finalDateLine = new HorizontalLine {CssClass = "FinalDateLine"};
                    _svlEventList.AddChild(dateText);
                    _svlEventList.AddChild(finalDateLine);
                    isHeaderAdded = true;
                }
            }

            eventLayout = new HorizontalLayout {CssClass = "OrderInfoContainer", Id = itemElement.Id};
            eventLayout.OnClick += EventLayout_OnClick;

            timeLayout = new VerticalLayout {CssClass = "OrderTimeContainer"};
            startDatePlaneTextView = new TextView
            {
                Text = itemElement.StartDatePlan.ToString("HH:mm"),
                CssClass = "StartDatePlan"
            };

            actualStartDateTextView = new TextView
            {
                CssClass = "ActualStartDate"
            };

            if (itemElement.ActualStartDate != default(DateTime))
            {
                actualStartDateTextView.Text = (DateTime.Now - itemElement.ActualStartDate).ToString(@"hh\:mm");
            }
            else
            {
                actualStartDateTextView.Visible = false;
            }

            timeLayout.AddChild(startDatePlaneTextView);
            timeLayout.AddChild(actualStartDateTextView);

            importanceLayout = new VerticalLayout {CssClass = "ImportanceContainer"};
            importanceIndicatorLayout = new HorizontalLayout();

            switch (itemElement.ImportanceName)
            {
                case "Critical":
                    importanceIndicatorLayout.CssClass = "ImportanceIndicatorCritical";
                    break;
                case "High":
                    importanceIndicatorLayout.CssClass = "ImportanceIndicatorHigh";
                    break;
                case "Standart":
                    importanceIndicatorLayout.CssClass = "ImportanceIndicatorStandart";
                    break;
                default:
                    importanceIndicatorLayout.CssClass = "ImportanceIndicatorStandart";
                    break;
            }

            importanceLayout.AddChild(importanceIndicatorLayout);

            orderInfoLayout = new VerticalLayout {CssClass = "OrderInfo"};
            orderInfoRefLayout = orderInfoLayout;
            clientDescriptionTextView = new TextView
            {
                CssClass = "ClientDescription",
                Text = itemElement.ClientDescription
            };

            clientAdressTextView = new TextView
            {
                CssClass = "ClientAdress",
                Text = itemElement.ClientAddress
            };

            typeDeparturesTextView = new TextView
            {
                CssClass = "TypesDepartures",
                Text = itemElement.TypeDeparture
            };


            orderInfoLayout.AddChild(clientDescriptionTextView);
            orderInfoLayout.AddChild(clientAdressTextView);
            orderInfoLayout.AddChild(typeDeparturesTextView);


            eventLayout.AddChild(timeLayout);
            eventLayout.AddChild(importanceLayout);
            eventLayout.AddChild(orderInfoLayout);


            _svlEventList.AddChild(eventLayout);
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
            DConsole.WriteLine("Before dictionary");
            var dictionary = new Dictionary<string, object>
            {
                {Parameters.IdScreenStateId, MapScreenStates.EventListScreen}
            };
            DConsole.WriteLine("After");
            BusinessProcess.GlobalVariables[Parameters.IdScreenStateId] = MapScreenStates.EventListScreen;
            Navigation.Move("MapScreen", dictionary);
        }

        internal void EventLayout_OnClick(object sender, EventArgs e)
        {
            DConsole.WriteLine("Go To View Event");
            var currentEvent = (HorizontalLayout) sender;
            BusinessProcess.GlobalVariables[Parameters.IdCurrentEventId] = currentEvent.Id;
            Navigation.Move("EventScreen");
        }

        internal void TabBarFirstTabButton_OnClick(object sender, EventArgs eventArgs)
        {
            //_tabBarComponent.Events_OnClick(sender, eventArgs);
        }

        internal void TabBarSecondTabButton_OnClick(object sender, EventArgs eventArgs)
        {
            _tabBarComponent.Bag_OnClick(sender, eventArgs);
        }

        internal void TabBarThirdButton_OnClick(object sender, EventArgs eventArgs)
        {
            _tabBarComponent.Clients_OnClick(sender, eventArgs);
        }

        internal void TabBarFourthButton_OnClick(object sender, EventArgs eventArgs)
        {
            _tabBarComponent.Settings_OnClick(sender, eventArgs);
        }

        private ArrayList GetEventsFromDb()
        {
            return DBHelper.GetEvents();
        }

        internal string GetResourceImage(string tag)
        {
            return ResourceManager.GetImage(tag);
        }

    }

    public enum MapMarkerColor
    {
        Red,
        Green,
        Blue,
        Yellow,
        Orange
    }
}