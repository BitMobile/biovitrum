using System;
using System.Collections;
using System.Linq;
using System.Text;
using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;

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
            _svlOrderList = (SwipeVerticalLayout)GetControl("OrderList", true);
            _ordersList = GetOrdersFromDb();
            FillingOrderList();
        }

        private void FillingOrderList()
        {

            if (_ordersList == null)
                return;

            foreach(var item in _ordersList)
            {
                //Filling Order List
            }

        }

        internal void GoToMap_OnClick(object sender, EventArgs e)
        {
            BusinessProcess.DoAction("Map");
        }

        internal void GoToOrderScreen_OnClick(object sender, EventArgs e)
        {
            BusinessProcess.DoAction("ViewOrder");
        }

        private ArrayList GetOrdersFromDb()
        {
            //Получение данных из БД.
            return new ArrayList();
        }
    }
}
