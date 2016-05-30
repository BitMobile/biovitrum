using System;
using BitMobile.ClientModel3.UI;

namespace Test.Components
{
    public class TopInfoComponent
    {
        private readonly Screen _parentScreen;
        private bool _extraLayoutVisible;
        private bool _minimized = true;

        private Image _topInfoArrowImage;
        private HorizontalLayout _topInfoExtraButtonsLayout;

        private VerticalLayout _topInfoExtraLayout;
        private VerticalLayout _topInfoImageLayout;

        /// <summary>
        ///     Конструктор контроллера компонента с заголовком, двумя кнопками и доп. инфой
        /// </summary>
        /// <param name="parentScreen">
        ///     Экран, на котором находится элемент
        /// </param>
        public TopInfoComponent(Screen parentScreen)
        {
            _parentScreen = parentScreen;
            OnLoading();
            _extraLayoutVisible = true;
        }

        /// <summary>
        ///     Изображение на левой кнопке, менять свойство Source
        /// </summary>
        public Image LeftButtonImage { get; private set; }

        /// <summary>
        ///     Изображение на правой кнопке, менять свойство Source
        /// </summary>
        public Image RightButtonImage { get; private set; }

        /// <summary>
        ///     Левый Layout с дополнительной информацией. Использовать метод AddChild
        /// </summary>
        public VerticalLayout LeftExtraLayout { get; private set; }

        /// <summary>
        ///     Правый Layout с дополнительной информацией. Использовать метод AddChild
        /// </summary>
        public VerticalLayout RightExtraLayout { get; private set; }

        /// <summary>
        ///     Заголовок экрана
        /// </summary>
        public TextView HeadingTextView { get; private set; }

        /// <summary>
        ///     Комментарий, который ниже заголовка экрана
        /// </summary>
        public TextView CommentTextView { get; private set; }

        /// <summary>
        ///     Видимость контейнера с дополнительной информацией
        /// </summary>
        public bool ExtraLayoutVisible
        {
            get { return _extraLayoutVisible; }
            set
            {
                ChangeExtraVisibility(value);
                _extraLayoutVisible = value;
            }
        }

        private void ChangeExtraVisibility(bool visibility)
        {
            if (!visibility)
            {
                _topInfoExtraLayout.CssClass = "NoHeight";
                _topInfoImageLayout.CssClass = "NoHeight";
            }
            else
            {
                _topInfoExtraLayout.CssClass = "TopInfoExtraLayout";
                _topInfoImageLayout.CssClass = "TopInfoImageLayout";
            }
        }

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

            _topInfoExtraLayout = (VerticalLayout) _parentScreen.GetControl("TopInfoExtraLayout", true);
            _topInfoImageLayout = (VerticalLayout) _parentScreen.GetControl("TopInfoImageLayout", true);
        }

        internal void Arrow_OnClick(object sender, EventArgs eventArgs)
        {
            if (_minimized)
            {
//                _topInfoArrowImage.Source = @"Image\up_arrow_full_width.png";
                _topInfoArrowImage.Source = ResourceManager.GetImage("topinfo_uparrow");
                _topInfoExtraButtonsLayout.CssClass = "TopInfoExtraButtonsLayout";
                _topInfoExtraButtonsLayout.Refresh();
                _minimized = false;
            }
            else
            {
//                _topInfoArrowImage.Source = @"Image\down_arrow_full_width.png";
                _topInfoArrowImage.Source = ResourceManager.GetImage("topinfo_downarrow");
                _topInfoExtraButtonsLayout.CssClass = "NoHeight";
                _topInfoExtraButtonsLayout.Refresh();
                _minimized = true;
            }
        }
    }
}