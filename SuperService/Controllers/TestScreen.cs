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
                Header = "Чек-лист",
                SubHeader = "998/999 вопросов отвечено",
                Minimized = false,
                ArrowVisible = true
            };
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