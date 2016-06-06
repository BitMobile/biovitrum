using System;
using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;
using Test.Components;

namespace Test
{
    // TODO: Экран Заявка на материалы
    public class ApplicationMaterialsScreen : Screen
    {
        private TopInfoComponent _topInfoComponent;
        private bool isEmptyApplicationData;

        public override void OnLoading()
        {
            _topInfoComponent = new TopInfoComponent(this)
            {
                ExtraLayoutVisible = false,
                HeadingTextView = {Text = Translator.Translate("order")},
                RightButtonImage = {Visible = false},
                //Времмено срелка назад
                LeftButtonImage = {Source = ResourceManager.GetImage("topheading_back")}
            };
        }


        internal void TopInfo_LeftButton_OnClick(object sender, EventArgs e)
        {
            DConsole.WriteLine("Back to screen .....");
            BusinessProcess.DoAction("COC");
        }

        internal void TopInfo_RightButton_OnClick(object sender, EventArgs e)
        {
        }

        internal void TopInfo_Arrow_OnClick(object sender, EventArgs e)
        {
        }

        internal string GetResourceImage(string tag)
        {
            return ResourceManager.GetImage(tag);
        }
    }
}