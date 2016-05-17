using System;
using System.Collections.Generic;
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

        public override void OnLoading()
        {
            _vlSlideVerticalLayout = (VerticalLayout)GetControl("SlideVerticalLayout", true);
            _svlOrderList = (SwipeVerticalLayout)GetControl("OrderList", true);
            GetOrdersList();
        }

        private void GetOrdersList()
        {
            HorizontalLayout _hlTemp;
            VerticalLayout _vlLeftSideTemp;
            VerticalLayout _vlPriority;
            VerticalLayout _vlRightSideTemp;

            _vlLeftSideTemp = FillLeftSideTemp();

            _vlPriority = new VerticalLayout();
            _vlPriority.AddChild(new Image());


            _vlRightSideTemp = new VerticalLayout();
            FillRIghtSideTemp(_vlRightSideTemp);

            _hlTemp = new HorizontalLayout();
            FillHorizontalLayout(_hlTemp, _vlLeftSideTemp, _vlPriority, _vlRightSideTemp);

            _svlOrderList.AddChild(_hlTemp);
        }

        private void FillHorizontalLayout(HorizontalLayout _hlTemp, VerticalLayout _vlLeftSideTemp, VerticalLayout _vlPriority, VerticalLayout _vlRightSideTemp)
        {
            _hlTemp.AddChild(_vlLeftSideTemp);
            _hlTemp.AddChild(_vlPriority);
            _hlTemp.AddChild(_vlRightSideTemp);
            _hlTemp.OnClick += GoToOrderScreen_OnClick;
        }

        private static void FillRIghtSideTemp(VerticalLayout _vlRightSideTemp)
        {
            _vlRightSideTemp.AddChild(new TextView("Name of company"));
            _vlRightSideTemp.AddChild(new TextView("Adress"));
            _vlRightSideTemp.AddChild(new TextView("Type"));
        }

        private static VerticalLayout FillLeftSideTemp()
        {
            VerticalLayout _vlLeftSideTemp = new VerticalLayout();
            _vlLeftSideTemp.AddChild(new TextView("Time to start"));
            _vlLeftSideTemp.AddChild(new TextView("Is start?"));
            return _vlLeftSideTemp;
        }

        internal void GoToMap_OnClick(object sender, EventArgs e)
        {
            BusinessProcess.DoAction("Map");
        }

        internal void GoToOrderScreen_OnClick(object sender, EventArgs e)
        {
            BusinessProcess.DoAction("ViewOrder");
        }

    }
}
