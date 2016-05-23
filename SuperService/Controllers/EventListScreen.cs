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

        public override void OnLoading()
        {
            DConsole.WriteLine("1");
            _vlSlideVerticalLayout = (VerticalLayout)GetControl("SlideVerticalLayout", true);
            _svlEventList = (SwipeVerticalLayout)GetControl("EventList", true);
            _eventsList = GetEventsFromDb();

            FillingOrderList();
        }


        private void FillingOrderList()
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
            DConsole.WriteLine("2");

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
            //ArrayList data = new ArrayList();
            //for (int i = 0; i < 10; i++)
            //{
            //    data.Add(new Event { Comment = i.ToString() });
            //}

            
            return DBHelper.GetEvents(); 

        }
    }
}
