using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;
using BitMobile.Common.Controls;
using System;

namespace Test.Components
{
    public class TopInfoComponent
    {
        private readonly Screen _parentScreen;
        private VerticalLayout _leftButton;
        private VerticalLayout _rightButton;
        private TextView _topInfoHeadingTextView;

        public IWrappedControl3 LeftButtonControl
        {
            set
            {
                AddIfNotEmpty(_leftButton, value);
            }
        }

        public IWrappedControl3 RightButtonControl
        {
            set
            {
                AddIfNotEmpty(_rightButton, value);
            }
        }

        public string Header
        {
            get { return _topInfoHeadingTextView.Text; }
            set { _topInfoHeadingTextView.Text = value; }
        }

        private static void AddIfNotEmpty(IContainer verticalLayout, IWrappedControl3 control)
        {
            if (verticalLayout.Controls.Length == 0) verticalLayout.AddChild(control);
        }

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

        private void OnLoading()
        {
            _leftButton = (VerticalLayout)_parentScreen.Variables["TopInfoLeftButton"];
            _rightButton = (VerticalLayout)_parentScreen.Variables["TopInfoRightButton"];
            _topInfoHeadingTextView = (TextView)_parentScreen.Variables["TopInfoHeadingTextView"];
            DConsole.WriteLine("test");
            DConsole.WriteLine($"lb = {_leftButton}, rb = {_rightButton}, tihtv = {_topInfoHeadingTextView}");
        }

        internal void Arrow_OnClick(object sender, EventArgs eventArgs)
        {
        }
    }
}