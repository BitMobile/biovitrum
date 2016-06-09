using System;
using System.Collections;
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