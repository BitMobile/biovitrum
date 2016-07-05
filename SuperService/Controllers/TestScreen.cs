using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;
using System;
using System.Collections.Generic;

namespace Test
{
    public class TestScreen : Screen
    {
        private VerticalLayout _rootLayout;
        private TextView _testTextView;

        public override void OnLoading()
        {
            _rootLayout = (VerticalLayout)Controls[0];
            _testTextView = (TextView)_rootLayout.Controls[1];
        }

        public override void OnShow()
        {
            DConsole.WriteLine("OnShow?");
        }

        internal string GetResourceImage(string tag)
        {
            return ResourceManager.GetImage(tag);
        }

        internal void Button_OnClick(object sender, EventArgs eventArgs)
        {
        }

        internal void TopInfo_LeftButton_OnClick(object sender, EventArgs eventArgs)
        {
        }

        internal void TopInfo_RightButton_OnClick(object sender, EventArgs eventArgs)
        {
        }

        internal void TopInfo_Arrow_OnClick(object sender, EventArgs eventArgs)
        {
        }
    }
}