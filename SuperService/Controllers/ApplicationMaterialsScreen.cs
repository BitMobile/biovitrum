using System;
using System.ComponentModel;
using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;
using Test.Components;

namespace Test
{
    // TODO: Переименовать файл в MeterialsReuestScreen
    public class ApplicationMaterialsScreen : Screen
    {
        private bool _isEmptyList = false;
        private TopInfoComponent _topInfoComponent;
        private VerticalLayout _rootVerticalLayout;

        public override void OnLoading()
        {
            _rootVerticalLayout = (VerticalLayout) this.GetControl("Root");
            _topInfoComponent = new TopInfoComponent(this)
            {
                ExtraLayoutVisible = false,
                HeadingTextView = {Text = Translator.Translate("request")},
                RightButtonImage = {Visible = false},
                LeftButtonImage = {Source = ResourceManager.GetImage("close")}
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

        internal bool GetIsEmptyList()
        {
            return _isEmptyList;
        }
    }
}