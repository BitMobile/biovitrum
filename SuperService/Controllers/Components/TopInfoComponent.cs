using System;
using BitMobile.ClientModel3.UI;

namespace Test.Components
{
    public class TopInfoComponent
    {
        private readonly Screen _parentScreen;
        private bool _bigArrowActive = true;
        private bool _extraLayoutVisible;
        private bool _minimized = true;

        private Image _topInfoArrowImage;
        private HorizontalLayout _topInfoExtraButtonsLayout;

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
        ///     Дополнительный контейнер ниже заголовка экрана, содержащий в себе комментарий
        /// </summary>
        public VerticalLayout ExtraLayout { get; private set; }

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

        /// <summary>
        ///     Активность большой стрелки. Если false, то меняется внешний вид.
        /// </summary>
        public bool BigArrowActive
        {
            get { return _bigArrowActive; }
            set
            {
                _bigArrowActive = value;
                _topInfoArrowImage.Source = value
                    ? ResourceManager.GetImage(_minimized ? "topinfo_downarrow" : "topinfo_uparrow")
                    : ResourceManager.GetImage(_minimized ? "topinfo_downnoarrow" : "topinfo_upnoarrow");
            }
        }

        private void ChangeExtraVisibility(bool visibility)
        {
            if (!visibility)
            {
                ExtraLayout.CssClass = "NoHeight";
                _topInfoImageLayout.CssClass = "NoHeight";
            }
            else
            {
                ExtraLayout.CssClass = "TopInfoExtraLayout";
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

            ExtraLayout = (VerticalLayout) _parentScreen.GetControl("TopInfoExtraLayout", true);
            _topInfoImageLayout = (VerticalLayout) _parentScreen.GetControl("TopInfoImageLayout", true);
        }

        internal void Arrow_OnClick(object sender, EventArgs eventArgs)
        {
            if (!BigArrowActive) return;
            if (_minimized)
            {
                _topInfoArrowImage.Source = ResourceManager.GetImage("topinfo_uparrow");
                _topInfoExtraButtonsLayout.CssClass = "TopInfoExtraButtonsLayout";
                _topInfoExtraButtonsLayout.Refresh();
                _minimized = false;
            }
            else
            {
                _topInfoArrowImage.Source = ResourceManager.GetImage("topinfo_downarrow");
                _topInfoExtraButtonsLayout.CssClass = "NoHeight";
                _topInfoExtraButtonsLayout.Refresh();
                _minimized = true;
            }
        }
    }
}