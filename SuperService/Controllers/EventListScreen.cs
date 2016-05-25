using System;
using System.Collections;
using System.Globalization;
using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;

namespace Test
{
    public class EventListScreen : Screen
    {
        private ArrayList _eventsList;
        //private VerticalLayout _vlSlideVerticalLayout;
        private ScrollView _svlEventList;
        private TopInfoComponent _topInfoComponent;

        public override void OnLoading()
        {
            //_vlSlideVerticalLayout = (VerticalLayout)GetControl("SlideVerticalLayout", true);
            _svlEventList = (ScrollView)GetControl("EventListScrollView", true);
            _eventsList = GetEventsFromDb();

            _topInfoComponent = new TopInfoComponent(this)
            {
                LeftButtonImage = { Source = @"Image\top_eventlist_filtr_button.png" },
                RightButtonImage = { Source = @"Image\top_eventlist_map_button.png" },
                HeadingTextView = { Text = Translator.Translate("orders") },
                LeftExtraLayout = { CssClass = "ExtraLeftLayoutCss" },
                RightExtraLayout = { CssClass = "ExtraRightLayoutCss" }
            };

            _topInfoComponent.LeftExtraLayout.AddChild(new TextView(@"7/9") { CssClass = "ExtraInfo" });
            _topInfoComponent.LeftExtraLayout.AddChild(new TextView(Translator.Translate("today"))
            {
                CssClass = "BottonExtraInfo"
            });
            _topInfoComponent.RightExtraLayout.AddChild(new TextView(@"14/29") { CssClass = "ExtraInfo" });
            _topInfoComponent.LeftExtraLayout.AddChild(new TextView(Translator.Translate("per_month"))
            {
                CssClass = "BottonExtraInfo"
            });
            FillingOrderList();
        }


        private void FillingOrderList()
        {
            if (_eventsList == null)
                return;

            var currenDate = DateTime.Now;
            var isHeaderAdded = false;


            foreach (var VARIABLE in _eventsList)
            {
                var itemElement = (EventListElement)VARIABLE;


                if (itemElement.StartDatePlan.Date <= currenDate.Date)
                {
                    DConsole.WriteLine("StartDatePlan = currentDate");
                   FillEventList(ref isHeaderAdded,ref itemElement,ref currenDate);
                }
                else
                {
                    DConsole.WriteLine("Not Equals");
                    currenDate = itemElement.StartDatePlan;
                    isHeaderAdded = false;
                    FillEventList(ref isHeaderAdded, ref itemElement, ref currenDate);
                }
            }

            foreach (var VARIABLE in _eventsList)
            {
                var itemElement = (EventListElement)VARIABLE;

                DConsole.WriteLine($"{itemElement.StartDatePlan.Date}");
                DConsole.WriteLine($"{itemElement.ClientDescription}");
                DConsole.WriteLine($"{itemElement.ClientAddress}");
                DConsole.WriteLine($"{itemElement.TypeDeparture}");
            }
        }


        private void FillEventList(ref bool isHeaderAdded,ref EventListElement itemElement,
            ref DateTime currentDate)
        {

            VerticalLayout dateContainer;
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
            HorizontalLine clientHorizontalLine;
            var buf = string.Empty;

            //dateContainer = new VerticalLayout() { CssClass = "DateContainer" };
            //DConsole.WriteLine("CssClass: DateContainer");

            

            if (!isHeaderAdded)
            {
                if (itemElement.StartDatePlan.Date <= DateTime.Now.Date)
                {
                    dateText = new TextView(Translator.Translate("today_eventListNode")) { CssClass = "DateText" };
                    DConsole.WriteLine("CssClass: DateText");
                    finalDateLine = new HorizontalLine { CssClass = "FinalDateLine" };
                    DConsole.WriteLine("CssClass: FinalDateLine");
                    //dateContainer.AddChild(dateText);
                    //dateContainer.AddChild(finalDateLine);
                    _svlEventList.AddChild(dateText);
                    _svlEventList.AddChild(finalDateLine);
                    isHeaderAdded = true;
                }

                else
                {
                    DConsole.WriteLine("Else");
                    dateText = new TextView
                    {
                        CssClass = "DateText",
                        Text = itemElement.StartDatePlan.Date.ToString("dddd, dd MMMM")
                    };
                    DConsole.WriteLine("CssClass: DateText");
                    finalDateLine = new HorizontalLine { CssClass = "FinalDateLine" };
                    DConsole.WriteLine("CssClass: FinalDateLine");
                    //dateContainer.AddChild(dateText);
                    //dateContainer.AddChild(finalDateLine);
                    _svlEventList.AddChild(dateText);
                    _svlEventList.AddChild(finalDateLine);
                    DConsole.WriteLine(itemElement.StartDatePlan.Date.ToString("dddd, dd MMMM"));
                    isHeaderAdded = true;
                }
            }

            eventLayout = new HorizontalLayout() { CssClass = "OrderInfoContainer" };
            DConsole.WriteLine("CssClass: OrderInfoContainer");
            eventLayout.OnClick += EventLayout_OnClick;

            timeLayout = new VerticalLayout() { CssClass = "OrderTimeContainer" };
            DConsole.WriteLine("CssClass: OrderTimeContainer");
            startDatePlaneTextView = new TextView()
            {
                Text = itemElement.StartDatePlan.ToString("HH:mm"),
                CssClass = "StartDatePlan"
            };
            DConsole.WriteLine("CssClass: StartDatePlan");

            actualStartDateTextView = new TextView()
            {
                CssClass = "ActualStartDate"
            };
            DConsole.WriteLine("CssClass: ActualStartDate");
            DConsole.WriteLine("Warning");
            if (itemElement.ActualStartDate != default(DateTime))
            {
                actualStartDateTextView.Text = (DateTime.Now - itemElement.ActualStartDate).ToString("HH:mm");
            }
            else
            {
                actualStartDateTextView.Visible = false;
            }

            timeLayout.AddChild(startDatePlaneTextView);
            timeLayout.AddChild(actualStartDateTextView);

            importanceLayout = new VerticalLayout() { CssClass = "ImportanceContainer" };
            DConsole.WriteLine("CssClass: ImportanceContainer");
            importanceIndicatorLayout = new HorizontalLayout();

            switch (itemElement.ImportanceName)
            {
                case "Critical":
                    importanceIndicatorLayout.CssClass = "ImportanceIndicatorCritical";
                    DConsole.WriteLine("CssClass: ImportanceIndicatorCritical");
                    // importanceIndicatorLayout.Refresh();
                    DConsole.WriteLine("CssClass: OK");
                    break;
                case "High":
                    importanceIndicatorLayout.CssClass = "ImportanceIndicatorHigh";
                    DConsole.WriteLine("CssClass: ImportanceIndicatorHigh");
                    //importanceIndicatorLayout.Refresh();
                    DConsole.WriteLine("CssClass: OK");
                    break;
                case "Standart":
                    importanceIndicatorLayout.CssClass = "ImportanceIndicatorStandart";
                    DConsole.WriteLine("CssClass: ImportanceIndicatorStandart");
                    //importanceIndicatorLayout.Refresh();
                    DConsole.WriteLine("CssClass: OK");
                    break;
            }

            importanceLayout.AddChild(importanceIndicatorLayout);

            orderInfoLayout = new VerticalLayout() { CssClass = "OrderInfo" };
            clientDescriptionTextView = new TextView()
            {
                CssClass = "ClientDescription",
                Text = itemElement.ClientDescription
            };
            DConsole.WriteLine("CssClass: ClientDescription");
            clientAdressTextView = new TextView()
            {
                CssClass = "ClientAdress",
                Text = itemElement.ClientAddress
            };
            DConsole.WriteLine("CssClass: ClientAdress");
            typeDeparturesTextView = new TextView()
            {
                CssClass = "TypesDepartures",
                Text = itemElement.TypeDeparture
            };
            DConsole.WriteLine("CssClass: TypesDepartures");

            orderInfoLayout.AddChild(clientDescriptionTextView);
            orderInfoLayout.AddChild(clientAdressTextView);
            orderInfoLayout.AddChild(typeDeparturesTextView);


            eventLayout.AddChild(timeLayout);
            eventLayout.AddChild(importanceLayout);
            eventLayout.AddChild(orderInfoLayout);

            //dateContainer.AddChild(eventLayout);

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
            BusinessProcess.DoAction("ViewMap");
        }

        internal void EventLayout_OnClick(object sender, EventArgs e)
        {
            DConsole.WriteLine("Go To View Event");
            BusinessProcess.DoAction("ViewEvent");
        }

        private ArrayList GetEventsFromDb()
        {
            return DBHelper.GetEvents();
        }
    }
}