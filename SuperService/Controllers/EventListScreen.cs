using System;
using System.Collections;
using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;
using Test.Document;

namespace Test
{
    public class EventListScreen : Screen
    {
        private VerticalLayout _vlSlideVerticalLayout;
        private SwipeVerticalLayout _svlEventList;
        private ArrayList _eventsList;
        private TabEventsComponent _tabEventsComponent;

        public override void OnLoading()
        {
            _tabEventsComponent = new TabEventsComponent(this);

            _vlSlideVerticalLayout = (VerticalLayout)GetControl("SlideVerticalLayout", true);
            _svlEventList = (SwipeVerticalLayout)GetControl("EventList", true);

            _eventsList = GetEventsFromDb();
            FillingEventList();
        }

        internal void TabEventsButton_OnClick(object sender, EventArgs eventArgs)
        {
            //_tabEventsComponent.Events_OnClick(sender, eventArgs);
            DConsole.WriteLine("Events Events");
        }

        internal void TabBagButton_OnClick(object sender, EventArgs eventArgs)
        {
            _tabEventsComponent.Bag_OnClick(sender, eventArgs);
            DConsole.WriteLine("Events Bag");
        }

        internal void TabClientsButton_OnClick(object sender, EventArgs eventArgs)
        {
            _tabEventsComponent.Clients_OnClick(sender, eventArgs);
            DConsole.WriteLine("Events Clients");
        }

        internal void TabSettingsButton_OnClick(object sender, EventArgs eventArgs)
        {
            _tabEventsComponent.Settings_OnClick(sender, eventArgs);
            DConsole.WriteLine("Events Settings");
        }


        private void FillingEventList()
        {

            if (_eventsList == null)
                return;

            Button btn;

            foreach (var item in _eventsList)
            {
                btn = new Button() { Text = ((Event)item).Comment };
                btn.OnClick += GoToEventScreen_OnClick;
                _svlEventList.AddChild(btn);
            }

        }

        internal void GoToMap_OnClick(object sender, EventArgs e)
        {
            DConsole.WriteLine("GO to map");
            BusinessProcess.DoAction("ViewMap");
        }

        internal void GoToEventScreen_OnClick(object sender, EventArgs e)
        {
            BusinessProcess.DoAction("ViewEvent");
        }

        private ArrayList GetEventsFromDb()
        {
            //Получение данных из БД.
            ArrayList data = new ArrayList();
            for (int i = 0; i < 10; i++)
            {
                data.Add(new Event { Comment = i.ToString() });
            }

            return data;

        }
    }
}
