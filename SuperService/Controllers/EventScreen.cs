using System;
using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;
using Test.Catalog;
using Test.Document;

namespace Test
{
    public class EventScreen : Screen
    {
        private DockLayout _rootLayout;

        private TextView _startTimeTextView;
        private TextView _departureTypeTextView;

        private TextView _eventCommentTextView;

        private TextView _taskCounterTextView;
        private TextView _checkListCounterTextView;
        private TextView _cocTextView;

        private Button _startFinishButton;

        private Event _currentOrder;
        private Client _orderClient;
        private TypesDepartures _departyreType;

        private TextView _topInfoCommenTextView;

        private TextView _topInfoHeadingTextView;

        private TopInfoComponent _topInfoComponent;
        private bool _started = false;

        public override void OnLoading()
        {
            _topInfoComponent = new TopInfoComponent(this);
            DConsole.WriteLine("Loading controls");
            LoadControls();

            DConsole.WriteLine("Loading model info");
            LoadModelInfo();

            DConsole.WriteLine("Writing info to controls");
            FillControlsWithInfo();
        }

        private void FillControlsWithInfo()
        {
            _taskCounterTextView.Text = $"{GetTaskNumberDone()}/{GetTaskNumber()}";
            _cocTextView.Text = $"{GetCertificateOfCompletion()}";
            _checkListCounterTextView.Text = $"{GetCheckListDone()}/{GetCheckListNumber()}";
            _startTimeTextView.Text = $"{_currentOrder.StartDatePlan}";
            _departureTypeTextView.Text = $"{_departyreType.Description}";
            _eventCommentTextView.Text = $"{_currentOrder.Comment}";
            _topInfoHeadingTextView.Text = _orderClient.Description;
            _topInfoCommenTextView.Text = _orderClient.Address;
        }

        private void LoadModelInfo()
        {
            _currentOrder = GetCurrentOrder();
            _orderClient = GetOrderClient();
            _departyreType = GetDepartureType();
        }

        private void LoadControls()
        {
            _rootLayout = (DockLayout) GetControl("RootLayout");

            _taskCounterTextView = (TextView) GetControl("TaskCounterTextView", true);
            _cocTextView = (TextView) GetControl("CertificateOfCompletionTextView", true);
            _checkListCounterTextView = (TextView) GetControl("CheckListCounterTextView", true);
            _startTimeTextView = (TextView) GetControl("StartTimeTextView", true);
            _departureTypeTextView = (TextView) GetControl("DepartureTypeTextView", true);
            _eventCommentTextView = (TextView) GetControl("EventCommentTextView", true);

            _startFinishButton = (Button) GetControl("StartFinishButton", true);

            _topInfoHeadingTextView = (TextView) GetControl("TopInfoHeadingTextView", true);
            _topInfoCommenTextView = (TextView) GetControl("TopInfoCommentTextView", true);
        }


        // TODO: Сделать это работать
        internal void BackButton_OnClick(object sender, EventArgs eventArgs)
        {
            BusinessProcess.DoAction("EventList");
        }

        internal void ClientInfoButton_OnClick(object sender, EventArgs eventArgs)
        {
            BusinessProcess.DoAction("Client");
        }

        internal void StartFinishButton_OnClick(object sender, EventArgs eventArgs)
        {
            if (!_started)
            {
                _startFinishButton.CssClass = "FinishButton";
                _startFinishButton.Refresh();
                _startFinishButton.Text = Translator.Translate("finish");
                _started = true;
            }
            else
            {
                _startFinishButton.CssClass = "StartButton";
                _startFinishButton.Refresh();
                _startFinishButton.Text = Translator.Translate("start");
                _started = false;
            }
        }

        internal void CancelButton_OnClick(object sender, EventArgs eventArgs)
        {
            BusinessProcess.DoAction("EventList");
        }

        internal void Test(object sender, EventArgs eventArgs)
        {
            DConsole.WriteLine("TEST TEST TEST");
        }

        internal void TopInfo_Arrow_OnClick(object sender, EventArgs eventArgs)
        {
            _topInfoComponent.Arrow_OnClick(sender, eventArgs);
            _rootLayout.Refresh();
        }

        internal void Test2(object sender, EventArgs eventArgs)
        {
            DConsole.WriteLine("TEST 2 TEST 2 TEST 2");
        }

        // TODO: Работа с базой данных
        private Event GetCurrentOrder()
        {
            return new Event
            {
                Comment = "Плановое подключение к третьей сети, при монтаже быть аккуратным с текущей коммуникацией.",
                StartDatePlan = new DateTime(2017, 05, 17, 10, 00, 00)
            };
        }

        private TypesDepartures GetDepartureType()
        {
            return new TypesDepartures
            {
                Description = "Монтаж"
            };
        }

        private int GetTaskNumber()
        {
            return 9;
        }

        private int GetTaskNumberDone()
        {
            return 4;
        }

        private decimal GetCertificateOfCompletion()
        {
            return new decimal(1440.00);
        }

        private int GetCheckListNumber()
        {
            return 9;
        }

        private int GetCheckListDone()
        {
            return 5;
        }

        private bool GetCheckListRequired()
        {
            return true;
        }

        private Client GetOrderClient()
        {
            return new Client
            {
                Description = "Газпром нефть",
                Address = "Малая Балканская ул., 17, Санкт-Петербург"
            };
        }
    }
}