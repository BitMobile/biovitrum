using System;
using System.Collections;
using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;
using Test.Document;

namespace Test
{
    public class OrderListScreen : Screen
    {
        private VerticalLayout _vlSlideVerticalLayout;
        private SwipeVerticalLayout _svlOrderList;
        private ArrayList _ordersList;

        public override void OnLoading()
        {
            _vlSlideVerticalLayout = (VerticalLayout)GetControl("SlideVerticalLayout", true);
            //_svlOrderList = (SwipeVerticalLayout)GetControl("OrderList", true);
            //_ordersList = GetOrdersFromDb();
            //FillingOrderList();
        }


        private void FillingOrderList()
        {

            if (_ordersList == null)
                return;

            Button btn;

            foreach (var item in _ordersList)
            {
                btn = new Button() { Text = ((Event)item).Comment };
                btn.OnClick += GoToOrderScreen_OnClick;
                _svlOrderList.AddChild(btn);
            }

        }

        internal void GoToMap_OnClick(object sender, EventArgs e)
        {
            DConsole.WriteLine("GO to map");
            BusinessProcess.DoAction("ViewMap");
        }

        internal void GoToOrderScreen_OnClick(object sender, EventArgs e)
        {
            BusinessProcess.DoAction("ViewOrder");
        }

        internal void SomeEvent_OnClick(object sender, EventArgs e)
        {
            DConsole.WriteLine("You click on me!!!!!");
        }

        private ArrayList GetOrdersFromDb()
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
