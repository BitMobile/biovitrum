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
            _topInfoComponent.LeftExtraLayout.AddChild(new TextView(Translator.Translate("per_month"))
            {
                CssClass = "BottonExtraInfo"
            });

            DConsole.WriteLine("FillingOrderList");
            FillingOrderList();
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
                        Text = itemElement.StartDatePlan.Date.ToString("dddd, dd MMMM")
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
                {"screenState", MapScreenStates.EventListScreen}
            };
            DConsole.WriteLine("After");
            BusinessProcess.GlobalVariables["screenState"] = MapScreenStates.EventListScreen;
            BusinessProcess.DoAction("ViewMap", dictionary);
        }

        internal void EventLayout_OnClick(object sender, EventArgs e)
        {
            DConsole.WriteLine("Go To View Event");
            var currentEvent = (HorizontalLayout) sender;
            BusinessProcess.GlobalVariables["currentEventId"] = currentEvent.Id;
            BusinessProcess.DoAction("ViewEvent");
        }

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

        private ArrayList GetEventsFromDb()
        {
            return DBHelper.GetEvents();
        }

        internal string GetResourceImage(string tag)
        {
            return ResourceManager.GetImage(tag);
        }

        private ArrayList GetTodayEventsLocation()
        {
            var result = new ArrayList();
            DConsole.WriteLine("Start " + nameof(GetTodayEventsLocation));

            foreach (var item in _eventsList)
            {
                var itemElement = (EventListElement) item;
                DConsole.WriteLine($"{itemElement.ClientDescription} {Environment.NewLine}" +
                                   $"latitude {itemElement.Latitude} longitude {itemElement.Longitude}");
#if DEBUG
                if (itemElement.StartDatePlan.Date <= DateTime.Now.Date)
#else
                if (itemElement.StartDatePlan.Date == DateTime.Now.Date)
#endif
                {
                    DConsole.WriteLine("If");
                    var longitude = Convert.ToDouble(itemElement.Longitude);
                    var latitude = Convert.ToDouble(itemElement.Latitude);

                    DConsole.WriteLine($"{nameof(latitude)} {latitude} {nameof(longitude)} {longitude}");
                    var
                        client =
                            new @string(itemElement.ClientDescription, latitude, longitude);
                    DConsole.WriteLine("Make clientLocation");
                    result.Add(client);
                }
                else if (itemElement.StartDatePlan.Date > DateTime.Now.Date)
                {
                    DConsole.WriteLine("Exit foreach");
                    break;
                }
            }
            DConsole.WriteLine("End " + nameof(GetTodayEventsLocation));
            return result;
        }
    }

    public class @string
    {
        private GpsCoordinate _coordinate;

        public @string(string clientDescription, double latitude, double longitude,
            MapMarkerColor markerColor = MapMarkerColor.Red)
        {
            GetValue(clientDescription, latitude, longitude);
            ClientDescription = clientDescription;

            isNullCoordinate(latitude, longitude);

            MarkerColor = string.Empty;

            switch (markerColor)
            {
                case MapMarkerColor.Red:
                    MarkerColor = "red";
                    break;

                case MapMarkerColor.Green:
                    MarkerColor = "green";
                    break;

                case MapMarkerColor.Blue:
                    MarkerColor = "blue";
                    break;

                case MapMarkerColor.Orange:
                    MarkerColor = "orange";
                    break;
                case MapMarkerColor.Yellow:
                    MarkerColor = "yellow";
                    break;
            }

            DConsole.WriteLine("Make Client Ok");
        }


        public string MarkerColor { get; private set; }
        public bool IsEmpty => _coordinate.Empty;
        public bool NotEmpty => _coordinate.NotEmpty;

        public double Latitude => _coordinate.Latitude;
        public double Longitude => _coordinate.Longitude;

        public string ClientDescription { get; }

        private void GetValue(string clientDescription, double latitude, double longitude)
        {
            DConsole.WriteLine($"client Desc {clientDescription}" +
                               $"latitude {latitude} longitude {longitude}");
        }

        private void isNullCoordinate(double latitude, double longitude)
        {
            _coordinate = latitude == 0 && longitude == 0
                ? new GpsCoordinate()
                : new GpsCoordinate(latitude, longitude, DateTime.Now);
        }
    }


    //public class ClientLocation
    //{
    //    public ClientLocation(string clientDescription, System.Double latitude, System.Double longitude, MapMarkerColor markerColor = Test.MapMarkerColor.Red)
    //    {
    //        ClientDescription = clientDescription;

    //if (latitude != 0 && longitude != 0)
    //    _clientLocation = new GpsCoordinate(latitude, longitude, DateTime.Now);
    //else
    //    _clientLocation = default(GpsCoordinate);

    //        MapMarkerColor = default(string);

    //        switch (markerColor)
    //        {
    //            case Test.MapMarkerColor.Red:
    //                MapMarkerColor = "red";
    //                break;

    //            case Test.MapMarkerColor.Green:
    //                MapMarkerColor = "green";
    //                break;

    //            case Test.MapMarkerColor.Blue:
    //                MapMarkerColor = "blue";
    //                break;

    //            case Test.MapMarkerColor.Orange:
    //                MapMarkerColor = "orange";
    //                break;
    //            case Test.MapMarkerColor.Yellow:
    //                MapMarkerColor = "yellow";
    //                break;
    //        }
    //    }

    //    private GpsCoordinate _clientLocation;
    //    public string ClientDescription { get; private set; }

    //    public double Latitude => _clientLocation.Latitude;

    //    public double Longitude => _clientLocation.Longitude;

    //    public string MapMarkerColor { get; private set; }

    //    public bool IsEmpty => _clientLocation.Empty;

    //    public bool NotEmpty => _clientLocation.NotEmpty;
    //}

    public class Some
    {
        public Some(int i)
        {
            Return = i;
        }

        public int Return { get; }
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