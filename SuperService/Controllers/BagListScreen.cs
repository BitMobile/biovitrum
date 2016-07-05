using System;
using System.Collections;
using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;
using Test.Components;

namespace Test
{
    public class BagListScreen : Screen
    {
        private TabBarComponent _tabBarComponent;
        private TopInfoComponent _topInfoComponent;

        public override void OnLoading()
        {
            _topInfoComponent = new TopInfoComponent(this)
            {
                HeadingTextView = {Text = Translator.Translate("bag")},
                LeftButtonImage = {Source = ResourceManager.GetImage("baglistscreen_busket")},
                RightButtonImage = {Source = ResourceManager.GetImage("baglistscreen_plus")},
                ExtraLayoutVisible = false
            };

            _tabBarComponent = new TabBarComponent(this);
        }

        public override void OnShow()
        {
            GPS.StopTracking();
        }

        internal void TopInfo_LeftButton_OnClick(object sender, EventArgs e)
        {
            Navigation.Move("RequestHistoryScreen");
        }

        internal void TopInfo_RightButton_OnClick(object sender, EventArgs e)
        {
            Navigation.Move("MaterialsRequestScreen");
        }

        internal void TopInfo_Arrow_OnClick(object sender, EventArgs e)
        {
        }

        internal IEnumerable GetUserBag()
        {
            // TODO: сделать передачу Id юзера когда будет авторизация
            return DBHelper.GetUserBagByUserId("@ref[Catalog_User]:838443ed-a3eb-11e5-8aad-f8a963e4bf15");
        }

        internal string ConcatCountUnit(Single count, string unit)
        {
            return string.Concat(count.ToString(), unit);
        }

        internal void TabBarFirstTabButton_OnClick(object sender, EventArgs eventArgs)
        {
            _tabBarComponent.Events_OnClick(sender, eventArgs);
            DConsole.WriteLine("Bag Events");
        }

        internal void TabBarSecondTabButton_OnClick(object sender, EventArgs eventArgs)
        {
            //_tabBarComponent.Bag_OnClick(sender, eventArgs);
            DConsole.WriteLine("Bag Bag");
        }

        internal void TabBarThirdButton_OnClick(object sender, EventArgs eventArgs)
        {
            _tabBarComponent.Clients_OnClick(sender, eventArgs);
            DConsole.WriteLine("Bag Clients");
        }

        internal void TabBarFourthButton_OnClick(object sender, EventArgs eventArgs)
        {
            _tabBarComponent.Settings_OnClick(sender, eventArgs);
            DConsole.WriteLine("Bag Settings");
        }

        internal string GetResourceImage(string tag)
        {
            return ResourceManager.GetImage(tag);
        }
    }
}