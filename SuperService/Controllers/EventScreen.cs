using System;
using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;
using Test.Catalog;
using Test.Document;

namespace Test
{
    public class EventScreen : Screen
    {
        private TextView _checkListCounterTextView;
        private TextView _cocTextView;

        private Event _currentOrder;
        private TextView _departureTypeTextView;
        private TypesDepartures _departyreType;

        private TextView _eventCommentTextView;
        private Client _orderClient;
        private DockLayout _rootLayout;
        private bool _started;

        private Button _startFinishButton;

        private TextView _startTimeTextView;

        private TextView _taskCounterTextView;

        private TopInfoComponent _topInfoComponent;
        private DbRecordset _currentEventRecordset;

        public override void OnLoading()
        {
            _topInfoComponent = new TopInfoComponent(this);

            LoadControls();
            LoadModelInfo();
            FillControls();
        }

        private void FillControls()
        {
//            _taskCounterTextView.Text = $"{GetTaskNumberDone()}/{GetTaskNumber()}";
//            _cocTextView.Text = $"{GetCertificateOfCompletion()}";
//            _checkListCounterTextView.Text = $"{GetCheckListDone()}/{GetCheckListNumber()}";
//            _startTimeTextView.Text = $"{_currentOrder.StartDatePlan.ToShortTimeString()}";
//            _departureTypeTextView.Text = $"{_departyreType.Description}";
//            _eventCommentTextView.Text = $"{_currentOrder.Comment}";

            _topInfoComponent.HeadingTextView.Text = (string) _currentEventRecordset["clientDescription"];
            _topInfoComponent.CommentTextView.Text = (string) _currentEventRecordset["clientAddress"];
            _topInfoComponent.LeftButtonImage.Source = @"Image\top_back.png";
            _topInfoComponent.RightButtonImage.Source = @"Image\top_info.png";

            _topInfoComponent.LeftExtraLayout.AddChild(new Image
            {
                CssClass = "TopInfoSideImage",
                Source = @"Image\top_map.png"
            });
            _topInfoComponent.LeftExtraLayout.AddChild(new TextView
            {
                Text = Translator.Translate("onmap"),
                CssClass = "TopInfoSideText"
            });

            _topInfoComponent.RightExtraLayout.AddChild(new Image
            {
                CssClass = "TopInfoSideImage",
                Source = @"Image\top_person.png"
            });
            _topInfoComponent.RightExtraLayout.AddChild(new TextView
            {
                Text = (string) _currentEventRecordset["clientDescription"],
                CssClass = "TopInfoSideText"
            });
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

        internal void TopInfo_LeftButton_OnClick(object sender, EventArgs eventArgs)
        {
            BusinessProcess.DoAction("EventList");
        }

        internal void TopInfo_RightButton_OnClick(object sender, EventArgs eventArgs)
        {
            DConsole.WriteLine("Nothing to see here");
        }

        internal void TopInfo_Arrow_OnClick(object sender, EventArgs eventArgs)
        {
            _topInfoComponent.Arrow_OnClick(sender, eventArgs);
            _rootLayout.Refresh();
        }


        internal DbRecordset GetCurrentEvent()
        {
            object eventId;
            if (!BusinessProcess.GlobalVariables.TryGetValue("currentEventId", out eventId))
            {
                eventId = "@ref[Document_Event]:6422e731-149a-11e6-80e3-005056011152";
            }
            _currentEventRecordset = DBHelper.GetEventByID((string) eventId);
            return _currentEventRecordset;
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

        internal string GetStringPartOfTotal(int part, int total)
        {
            return $"{part}/{total}" == "/" ? "0/0" : $"{part}/{total}";
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