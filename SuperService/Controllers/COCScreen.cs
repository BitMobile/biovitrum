using System;
using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;
using Test.Components;

namespace Test
{
    public class COCScreen : Screen
    {
        private TopInfoComponent _topInfoComponent;

        public override void OnLoading()
        {
            _topInfoComponent = new TopInfoComponent(this)
            {
                ExtraLayoutVisible = true,
                HeadingTextView = {Text = Translator.Translate("coc")},
                RightButtonImage = {Visible = false},
                LeftButtonImage = {Source = ResourceManager.GetImage("topheading_back") },
                CommentTextView = {Text = "Итоговая Сумма" + Environment.NewLine + "123456"}
            };

            
        }

        internal string GetResourceImage(string tag)
        {
            return ResourceManager.GetImage(tag);
        }

        internal void TopInfo_LeftButton_OnClick(object sender, EventArgs e)
        {
            BusinessProcess.DoAction("ViewEvent");
        }

        internal void TopInfo_RightButton_OnClick(object sender, EventArgs e)
        {
            
        }

        internal void TopInfo_Arrow_OnClick(object sender, EventArgs e)
        {
            
        }

        internal void AddServiceAndMaterial_OnClick(object sender, EventArgs e)
        {
            DConsole.WriteLine("Add Service And Materials OnClick");
        }
    }
}