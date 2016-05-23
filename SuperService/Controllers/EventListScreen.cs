using System;
using System.Collections;
using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;
using Test.Document;

namespace Test
{
    public class EventListScreen : Screen
    {
        //private VerticalLayout _vlSlideVerticalLayout;
        private ScrollView _svlEventList;
        private ArrayList _eventsList;
        private TopInfoComponent _topInfoComponent ;

        public override void OnLoading()
        {
            //_vlSlideVerticalLayout = (VerticalLayout)GetControl("SlideVerticalLayout", true);
            _svlEventList = (ScrollView)GetControl("EventList", true);
            _eventsList = GetEventsFromDb();

            _topInfoComponent = new TopInfoComponent(this)
            {
                LeftButtonImage = {Source = @"Image\top_eventlist_filtr_button.png"},
                RightButtonImage = {Source = @"Image\top_eventlist_map_button.png"},
                HeadingTextView = {Text = Translator.Translate("orders")},
                LeftExtraLayout = {CssClass = "ExtraLeftLayoutCss" },
                RightExtraLayout = {CssClass = "ExtraRightLayoutCss" }
            };

            _topInfoComponent.LeftExtraLayout.AddChild(new TextView(@"7/9") {CssClass = "ExtraInfo"});
            _topInfoComponent.LeftExtraLayout.AddChild(new TextView(Translator.Translate("today")) {CssClass = "BottonExtraInfo"});
            _topInfoComponent.RightExtraLayout.AddChild(new TextView(@"14/29") { CssClass = "ExtraInfo" });
            _topInfoComponent.LeftExtraLayout.AddChild(new TextView(Translator.Translate("per_month")) { CssClass = "BottonExtraInfo" });
            FillingOrderList();
        }


        private void FillingOrderList()
        {

            if (_eventsList == null)
                return;

        }

        internal void TopInfo_LeftButton_OnClick(object sender, EventArgs e)
        {
            
        }



        internal void TopInfo_Arrow_OnClick(object sender, EventArgs e)
        {
            _topInfoComponent.Arrow_OnClick(sender,e);
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
            //Получение данных из БД.
            ArrayList data = new ArrayList();
            //for (int i = 0; i < 10; i++)
            //{
            //    data.Add(new Event { Comment = i.ToString() });
            //}

            return data;
            //return DBHelper.GetEvents(); 

        }
    }
}
