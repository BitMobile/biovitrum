using System;
using BitMobile.ClientModel3.UI;

namespace Test
{
    public class TopInfoComponent
    {
        private readonly Screen _parentScreen;
        private bool _minimized = true;

        private Image _topInfoArrowImage;
        private HorizontalLayout _topInfoExtraButtonsLayout;

        public TopInfoComponent(Screen parentScreen)
        {
            _parentScreen = parentScreen;
            OnLoading();
        }

        public Image LeftButtonImage { get; private set; }
        public Image RightButtonImage { get; private set; }
        public VerticalLayout LeftExtraLayout { get; private set; }
        public VerticalLayout RightExtraLayout { get; private set; }
        public TextView HeadingTextView { get; private set; }
        public TextView CommentTextView { get; private set; }

        private void OnLoading()
        {
            _topInfoArrowImage = (Image) _parentScreen.GetControl("TopInfoArrowImage", true);
            _topInfoExtraButtonsLayout = (HorizontalLayout) _parentScreen.GetControl("TopInfoExtraButtonsLayout", true);

            LeftButtonImage = (Image) _parentScreen.GetControl("TopInfoLeftButtonImage", true);
            RightButtonImage = (Image) _parentScreen.GetControl("TopInfoRightButtonImage", true);

            LeftExtraLayout = (VerticalLayout) _parentScreen.GetControl("TopInfoLeftExtraLayout", true);
            RightExtraLayout = (VerticalLayout) _parentScreen.GetControl("TopInfoRightExtraLayout", true);

            HeadingTextView = (TextView) _parentScreen.GetControl("TopInfoHeadingTextView", true);
            CommentTextView = (TextView) _parentScreen.GetControl("TopInfoCommentTextView", true);
        }

        internal void Arrow_OnClick(object sender, EventArgs eventArgs)
        {
            if (_minimized)
            {
                _topInfoArrowImage.Source = @"Image\up_arrow_full_width.png";
                _topInfoExtraButtonsLayout.CssClass = "TopInfoExtraButtonsLayout";
                _topInfoExtraButtonsLayout.Refresh();
                _minimized = false;
            }
            else
            {
                _topInfoArrowImage.Source = @"Image\down_arrow_full_width.png";
                _topInfoExtraButtonsLayout.CssClass = "NoHeight";
                _topInfoExtraButtonsLayout.Refresh();
                _minimized = true;
            }
        }
    }
}