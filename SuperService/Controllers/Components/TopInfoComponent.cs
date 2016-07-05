using BitMobile.ClientModel3.UI;
using BitMobile.Common.Controls;
using System;

namespace Test.Components
{
    public class TopInfoComponent
    {
        private readonly Screen _parentScreen;
        private VerticalLayout _leftButton;
        private bool _minimized;
        private VerticalLayout _rightButton;
        private Image _topInfoArrowImage;
        private TextView _topInfoHeadingTextView;
        private TextView _topInfoSubHeadingTextView;
        private VerticalLayout _topInfoImageLayout;
        private bool _arrowVisible;

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
        }

        public bool Minimized
        {
            get { return _minimized; }
            set
            {
                _minimized = value;
                _topInfoArrowImage.Source = ResourceManager.GetImage(value ? "topinfo_downarrow" : "topinfo_uparrow");
                _topInfoArrowImage.Refresh();
            }
        }

        public bool ArrowVisible
        {
            get { return _arrowVisible; }
            set
            {
                _arrowVisible = value;
                _topInfoImageLayout.CssClass = value ? "TopInfoImageLayout" : "NoHeight";
                _topInfoImageLayout.Refresh();
            }
        }

        public IWrappedControl3 LeftButtonControl
        {
            get { return GetIfNotEmpty(_leftButton); }
            set { AddIfNotEmpty(_leftButton, value); }
        }

        public IWrappedControl3 RightButtonControl
        {
            get { return GetIfNotEmpty(_rightButton); }
            set { AddIfNotEmpty(_rightButton, value); }
        }

        public string Header
        {
            get { return _topInfoHeadingTextView.Text; }
            set { _topInfoHeadingTextView.Text = value; }
        }

        public string SubHeader
        {
            get { return _topInfoSubHeadingTextView.Text; }
            set
            {
                _topInfoSubHeadingTextView.Text = value;
                if (string.IsNullOrEmpty(value))
                {
                    _topInfoSubHeadingTextView.CssClass = "NoHeight";
                    _topInfoSubHeadingTextView.Refresh();
                }
                else
                {
                    _topInfoSubHeadingTextView.CssClass = "TopInfoSubHeading";
                    _topInfoSubHeadingTextView.Refresh();
                }
            }
        }

        private IWrappedControl3 GetIfNotEmpty(VerticalLayout layout)
        {
            return layout.Controls.Length == 0 ? null : (IWrappedControl3)layout.Controls[0];
        }

        private static void AddIfNotEmpty(VerticalLayout verticalLayout, IWrappedControl3 control)
        {
            if (verticalLayout.Controls.Length == 0) verticalLayout.AddChild(control);
        }

        private void OnLoading()
        {
            _leftButton = (VerticalLayout)_parentScreen.Variables["TopInfoLeftButton"];
            _rightButton = (VerticalLayout)_parentScreen.Variables["TopInfoRightButton"];
            _topInfoHeadingTextView = (TextView)_parentScreen.Variables["TopInfoHeadingTextView"];
            _topInfoSubHeadingTextView = (TextView)_parentScreen.Variables["TopInfoSubHeadingTextView"];
            _topInfoArrowImage = (Image)_parentScreen.Variables["TopInfoArrowImage"];
            _topInfoImageLayout = (VerticalLayout)_parentScreen.Variables["TopInfoImageLayout"];
        }

        internal void Arrow_OnClick(object sender, EventArgs eventArgs)
        {
            Minimized = !Minimized;
        }
    }
}