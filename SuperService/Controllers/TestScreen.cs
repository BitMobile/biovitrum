using BitMobile.ClientModel3.UI;
using System;
using BitMobile.ClientModel3;
using Test.Components;

namespace Test
{
    public class TestScreen : Screen
    {
        private TopInfoComponent _topInfoComponent;

        public override void OnLoading()
        {
            _topInfoComponent = new TopInfoComponent(this)
            {
                LeftButtonControl = new TextView("Отмена"),
                RightButtonControl = new TextView("Сохранить"),
                Header = "Курочка ряба",
            };

            _topInfoComponent.CommentLayout.AddChild(new TextView("Малая балканская, 17, Санкт-Петербург"));

            _topInfoComponent.ExtraLayout.AddChild(new TextView("Экстра инфо"));
            _topInfoComponent.ExtraLayout.AddChild(new TextView("Шамеймару, Марисса, Спелл Кард, Спелл Кард, Мастер Спарк, Экстра Фантазм"));
        }

        public override void OnShow()
        {
        }

        internal string GetResourceImage(string tag)
        {
            return ResourceManager.GetImage(tag);
        }

        internal void TopInfo_LeftButton_OnClick(object sender, EventArgs eventArgs)
        {
            DConsole.WriteLine("I am left");
        }

        internal void TopInfo_RightButton_OnClick(object sender, EventArgs eventArgs)
        {
            DConsole.WriteLine("I am right");
        }

        internal void TopInfo_Arrow_OnClick(object sender, EventArgs eventArgs)
        {
            _topInfoComponent.Arrow_OnClick(sender, eventArgs);
        }
    }
}