using System;
using BitMobile.ClientModel3.UI;
using BitMobile.ClientModel3;
using Test;

namespace Test.Components
{
    class TopComponent
    {
        private readonly Screen _parentScreen;
       
        public Image TopLeftButtonImage { get; private set; }
        public Image TopRightButtonImage { get; private set; }
        public VerticalLayout TopLeftButton { get; private set; }
        public VerticalLayout TopRightButton { get; private set; }
        public TextView TopHeadingTextView { get; private set; }
        

        public TopComponent(Screen parentScreen)
        {
            _parentScreen = parentScreen;
            OnLoading();
        }

        private void OnLoading()
        {
            TopLeftButtonImage = (Image) _parentScreen.GetControl("TopLeftButtonImage", true);
            TopRightButtonImage = (Image) _parentScreen.GetControl("TopRightButtonImage", true);

            TopLeftButton = (VerticalLayout) _parentScreen.GetControl("TopLeftButton", true);
            TopRightButton = (VerticalLayout) _parentScreen.GetControl("TopRightButton", true);

            TopHeadingTextView = (TextView) _parentScreen.GetControl("TopHeadingTextView", true);

        }

        internal void LeftButton_OnClick(string nextAction)
        {
            BusinessProcess.DoAction(nextAction);
        }

        internal void RightButton_OnClick(string nextAction)
        {
             BusinessProcess.DoAction(nextAction);
        }
    }
}
