using System;
using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;

namespace Test
{
    public class TopInfoComponent
    {
        private Screen _parentScreen;

        private Image _topInfoArrowImage;
        private HorizontalLayout _topInfoExtraButtonsLayout;
        private bool minimized = true;

        public TopInfoComponent(Screen parentScreen)
        {
            _parentScreen = parentScreen;
            OnLoading();
        }


        public void OnLoading()
        {
            _topInfoArrowImage = (Image) _parentScreen.GetControl("TopInfoArrowImage", true);
            _topInfoExtraButtonsLayout = (HorizontalLayout) _parentScreen.GetControl("TopInfoExtraButtonsLayout", true);
        }

        internal void Arrow_OnClick(object sender, EventArgs eventArgs)
        {
            if (minimized)
            {
                _topInfoArrowImage.Source = @"Image\up_arrow_full_width.png";
                _topInfoExtraButtonsLayout.CssClass = "TopInfoExtraButtonsLayout";
                _topInfoExtraButtonsLayout.Refresh();
                minimized = false;
            }
            else
            {
                _topInfoArrowImage.Source = @"Image\down_arrow_full_width.png";
                _topInfoExtraButtonsLayout.CssClass = "NoHeight";
                _topInfoExtraButtonsLayout.Refresh();
                minimized = true;
            }
        }
    }
}