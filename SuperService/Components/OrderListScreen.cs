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



            #region Заполнения времени и начала работы.
            _vlLeftSideTemp = new VerticalLayout();
            _vlLeftSideTemp.AddChild(new TextView("Time to start"));
            _vlLeftSideTemp.AddChild(new TextView("Is start"));
            #endregion

            #region Добавление приоритета наряда.
            _vlPriority = new VerticalLayout();
            _vlPriority.AddChild(new Image());
            #endregion

            #region Добавление имени компании, адреса, типа работ
            _vlRightSideTemp = new VerticalLayout();
            _vlRightSideTemp.AddChild(new TextView("Name of company"));
            _vlRightSideTemp.AddChild(new TextView("Adress"));
            _vlRightSideTemp.AddChild(new TextView("Type"));
            #endregion

            #region Добавление элементов в Horizontal Layout
            _hlTemp = new HorizontalLayout();
            _hlTemp.AddChild(_vlLeftSideTemp);
            _hlTemp.AddChild(_vlPriority);
            _hlTemp.AddChild(_vlRightSideTemp);
            _hlTemp.OnClick += GoToOrderScreen_OnClick;
            #endregion

            _svlOrderList.AddChild(_hlTemp);
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
