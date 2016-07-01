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
        private bool _needTodayBreaker = Convert.ToBoolean("True");
        private bool _needTodayLayout = Convert.ToBoolean("True");
        private TabBarComponent _tabBarComponent;
        private TopInfoComponent _topInfoComponent;

        public override void OnLoading()
        {
            DConsole.WriteLine("OnLoading EventList");

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
                new TextView($"{statistic.DayCompleteAmout}/{statistic.DayTotalAmount}")
                {
                    CssClass = "ExtraInfo"
                });
            _topInfoComponent.LeftExtraLayout.AddChild(new TextView(Translator.Translate("today"))
            {
                CssClass = "ButtonExtraInfo"
            });

            _topInfoComponent.RightExtraLayout.AddChild(
                new TextView($"{statistic.MonthCompleteAmout}/{statistic.MonthTotalAmount}")
                {
                    CssClass = "ExtraInfo"
                });
            _topInfoComponent.RightExtraLayout.AddChild(new TextView(Translator.Translate("per_month"))
            {
                CssClass = "ButtonExtraInfo"
            });
        }

        internal string GetStatusPicture(string importance, string status)
        {
            DConsole.WriteLine("getstatus: importance - " + importance + " status - " + status);
            var pictureTag = @"eventlistscreen_";

            if (importance == "Standart")
            {
                pictureTag += "blue";
            }
            else if (importance == "High")
            {
                pictureTag += "yellow";
            }
            else if (importance == "Critical")
            {
                pictureTag += "red";
            }

            if (status == "Appointed")
            {
                pictureTag += "border";
            }
            else if (status == "Done")
            {
                pictureTag += "done";
            }
            else if (status == "InWork")
            {
                pictureTag += "circle";
            }
            DConsole.WriteLine("pictureTag: " + pictureTag);
            return ResourceManager.GetImage(pictureTag);
        }

        internal string GetDateNowEventList()
        {
            //DConsole.WriteLine(DateTime.Now.ToString("dddd dd MMMM"));
            //return DateTime.Now.ToString("dddd dd MMMM");
            //DConsole.WriteLine(DateTime.Now.ToString("dd-MM-yyyy"));
            return DateTime.Now.ToString("dd-MM-yyyy");
        }

        internal string DateTimeToDateWithWeekCheck(string datetime)
        {
            var workDate = DateTime.Parse(datetime).Date;
            var currentDate = DateTime.Now.Date;

            var workDateWeekNumber = (workDate.DayOfYear + 6)/7;
            if (workDate.DayOfWeek < DateTime.Parse("1.1." + currentDate.Year).DayOfWeek)
            {
                ++workDateWeekNumber;
            }

            var currentDateWeekNumber = (currentDate.DayOfYear + 6)/7;
            if (currentDate.DayOfWeek < DateTime.Parse("1.1." + currentDate.Year).DayOfWeek)
            {
                ++currentDateWeekNumber;
            }

            if (workDateWeekNumber == currentDateWeekNumber)
            {
                return DateTime.Parse(datetime).ToString("dddd, dd MMMM").ToUpper();
            }
            return DateTime.Parse(datetime).ToString("dd MMMM yyyy").ToUpper();
        }

        internal string GetStartDate(string startPlan, string endPlan)
        {
            var startTime = DateTime.Parse(startPlan); //DateTime.Parse(startPlan).ToString("HH:mm:ss");
            var endTime = DateTime.Parse(endPlan); // .ToString("HH:mm");
            if (endTime - startTime > new TimeSpan(23, 59, 00))
            {
                return Translator.Translate("allday");
            }
            return startTime.ToString("HH:mm");
        }

        internal string GetTimeCounter(string actualStartDate, string statusName)
        {
            DConsole.WriteLine("actualStartDate: " + actualStartDate);
            var actualTime = DateTime.Parse(actualStartDate); // .ToString("HH:mm");

            if ((actualTime != default(DateTime)) && statusName == "Appointed")
            {
                var ans = DateTime.Now - actualTime; // .ToString(@"hh\:mm");
                DConsole.WriteLine(ans.ToString());
                return ans.Days*24 + ans.Hours + ":" + ans.Minutes; // @"hh\:mm");
            }
            return "";
        }

        internal int SetTodayLayoutToFalse()
        {
            //DConsole.WriteLine("in ToFalse entered");
            _needTodayLayout = Convert.ToBoolean("False");
            return 0;
        }

        internal int SetTodayBreakerToFalse()
        {
            //DConsole.WriteLine("SetTodayBreakerToFalse setted to false");
            _needTodayBreaker = Convert.ToBoolean("False");
            return 0;
        }

        internal bool IsDateEquals(string lastdate, string nowdate)
        {
            if (DateTime.Parse(lastdate).Date == DateTime.Parse(nowdate).Date)
            {
                return true;
            }
            return false;
        }

        internal bool IsDateEqualsOrLess(string lastdate, string nowdate)
        {
            if (DateTime.Parse(lastdate).Date >= DateTime.Parse(nowdate).Date)
            {
                DConsole.WriteLine("EqualOrLess comparing: " + lastdate + " >= " + nowdate);
                return true;
            }
            DConsole.WriteLine("EqualOrLess comparing: " + lastdate + " < " + nowdate);
            return false;
        }

        internal bool IsDateChanged(string lastdate, string nowdate)
        {
            if (DateTime.Parse(lastdate).Date < DateTime.Parse(nowdate).Date)
            {
                //DConsole.WriteLine("IsDateChanged returns " + lastdate + " < " + nowdate);
                return true;
            }
            //DConsole.WriteLine("IsDateChanged returns " + lastdate + " not < " + nowdate);
            return false;
        }

        internal bool IsTodayLayoutNeed()
        {
            //DConsole.WriteLine(_needTodayLayout.ToString());
            if (_needTodayLayout)
            {
                //DConsole.WriteLine("TodayLayoutNeed");
                return Convert.ToBoolean("True");
            }
            //DConsole.WriteLine("TodayLayoutNOTNeed");
            return Convert.ToBoolean("False");
        }

        internal bool IsTodayBreakerNeed()
        {
            if (_needTodayBreaker)
            {
                //DConsole.WriteLine("IsTodayBreakerNeed needed");
                return true;
            }
            //DConsole.WriteLine("IsTodayBreakerNeed NOTneeded");
            return false;
        }

        internal string DateTimeToDate(string datetime)
        {
            return DateTime.Parse(datetime).ToString("dddd dd MMMM");
        }

        internal IEnumerable GetEvents()
        {
            return DBHelper.GetEvents();
        }

        internal string GetResourceImage(string tag)
        {
            return ResourceManager.GetImage(tag);
        }

        // TopInfo parts
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

        internal void EventListItemHL_OnClick(object sender, EventArgs e)
        {
            DConsole.WriteLine("Go To View Event");
            var currentEvent = (HorizontalLayout) sender;
            BusinessProcess.GlobalVariables[Parameters.IdCurrentEventId] = currentEvent.Id;
            Navigation.Move("EventScreen");
        }

        // TabBar parts
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
    }

    public enum MapMarkerColor
    {
        Red,
        Green,
        Blue,
        Yellow,
        Orange
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