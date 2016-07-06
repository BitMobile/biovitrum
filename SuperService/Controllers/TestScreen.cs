using BitMobile.ClientModel3.UI;
using System;
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
                SubHeader = "998/999 вопросов отвечено",
                Minimized = true,
                ArrowVisible = true,
                ArrowActive = true
            };
            _topInfoComponent.CommentLayout.AddChild(new TextView("17, Малая Бакланская, Санкт-Петербург, Россия, Земля, Солнце, Млечный Путь, Местное Скопление"));
            _topInfoComponent.CommentLayout.AddChild(new TextView("Итоговая сумма"));
            _topInfoComponent.CommentLayout.AddChild(new TextView("100500 ₽") {CssClass = "BigGreenTextView"});

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
        }

        internal void TopInfo_RightButton_OnClick(object sender, EventArgs eventArgs)
        {
        }

        internal void TopInfo_Arrow_OnClick(object sender, EventArgs eventArgs)
        {
            _topInfoComponent.Arrow_OnClick(sender, eventArgs);
        }
    }
}