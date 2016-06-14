using System;
using System.Collections;
using System.Globalization;
using System.Net;
using System.Text;
using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;
using Test.Components;

namespace Test
{
    public class RequestHistoryScreen : Screen
    {
        private TopInfoComponent _topInfoComponent;
        private bool _needTodayLayout = Convert.ToBoolean("True");
        private bool _needTodayBreaker = Convert.ToBoolean("True");

        public override void OnLoading()
        {
            DConsole.WriteLine("RequestHistoryScreen init");

            _topInfoComponent = new TopInfoComponent(this)
            {
                HeadingTextView = { Text = Translator.Translate("requests") },
                LeftButtonImage = { Source = ResourceManager.GetImage("topheading_back") },
                RightButtonImage = { Visible = false },
                ExtraLayoutVisible = false
            };
        }

        internal void TopInfo_LeftButton_OnClick(object sender, EventArgs e)
        {
            BusinessProcess.DoAction("BackToBag");
        }

        internal void TopInfo_RightButton_OnClick(object sender, EventArgs e)
        {
        }

        internal void TopInfo_Arrow_OnClick(object sender, EventArgs e)
        {
        }

        internal int SetTodayLayoutBoolToFalse()
        {
            _needTodayLayout = Convert.ToBoolean("False");
            return 0;
        }

        internal string GetDateNowRequestHistory()
        {
            return DateTime.Now.ToString("dddd dd MMMM");
        }

        internal string DateTimeToDate(string datetime)
        {
            return DateTime.Parse(datetime).ToString("dddd dd MMMM");
        }

        internal string DateTimeToDateWithWeekCheck(string datetime)
        {
            DateTime workDate = DateTime.Parse(datetime).Date;
            DateTime currentDate = DateTime.Now.Date;

            var workDateWeekNumber = (workDate.DayOfYear + 6)/7;
            if (workDate.DayOfWeek < DateTime.Parse("1.1." + currentDate.Year).DayOfWeek)
            {
                ++workDateWeekNumber;
            }

            var currentDateWeekNumber = (currentDate.DayOfYear + 6) / 7;
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

        internal bool IsDateChangedString(string lastdate, string nowdate)
        {
            if (DateTime.Parse(lastdate).Date > DateTime.Parse(nowdate).Date)
            {
                return true;
            }
            return false;
        }

        internal bool IsDateChanged(string lastdate, string nowdate)
        {
            if (DateTime.Parse(lastdate).Date > DateTime.Parse(nowdate).Date)
            {
                return true;
            }
            return false;
        }

        internal bool IsDateNotChanged(string lastdate, string nowdate)
        {
            if (DateTime.Parse(lastdate).Date != DateTime.Parse(nowdate).Date)
            {
                return true;
            }
            return false;
        }

        internal bool IsDateEquals(string lastdate, string nowdate)
        {
            if (DateTime.Parse(lastdate).Date == DateTime.Parse(nowdate).Date)
            {
                return true;
            }
            return false;
        }

        internal bool IsVrblTrue()
        {
            if (Convert.ToBoolean(_needTodayLayout) == Convert.ToBoolean("True"))
            {
                return _needTodayLayout;
            }
            return _needTodayLayout;
        }

        internal bool IsBreakerNeed()
        {
            if (Convert.ToBoolean(_needTodayBreaker) == Convert.ToBoolean("True"))
            {
                return _needTodayBreaker;
            }
            return _needTodayBreaker;
        }

        internal bool SetTodayLayoutBreakerToFalse()
        {
            _needTodayBreaker = Convert.ToBoolean("False");
            return _needTodayBreaker;
        }

        internal string ToHoursMinutes(string datetime)
        {
            return TimeSpan.Parse(datetime).ToString(@"hh\:mm");
        }

        internal IEnumerable GetNeedMats()
        {
            return DBHelper.GetNeedMats();
        }

        internal string GetResourceImage(string tag)
        {
            return ResourceManager.GetImage(tag);
        }
    }
}