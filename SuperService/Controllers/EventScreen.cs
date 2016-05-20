using System;
using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;
using Test.Catalog;
using Test.Document;

namespace Test
{
    public class EventScreen : Screen
    {
        private TextView _contactDescriptionTextView;
        private TextView _contactAddressTextView;
        private TextView _taskCounterTextView;
        private TextView _cocTextView;
        private TextView _checkListCounterTextView;
        private TextView _startTimeTextView;
        private TextView _departureTypeTextView;
        private Button _startButton;

        private Event _currentOrder;
        private Client _orderClient;
        private TypesDepartures _departyreType;

        public override void OnLoading()
        {
            DConsole.WriteLine("Loading controls");
            _contactDescriptionTextView = (TextView) GetControl("ContactDescriptionTextView", true);
            _contactAddressTextView = (TextView) GetControl("ContactAddressTextView", true);
            _taskCounterTextView = (TextView) GetControl("TaskCounterTextView", true);
            _cocTextView = (TextView) GetControl("CertificateOfCompletionTextView", true);
            _checkListCounterTextView = (TextView) GetControl("CheckListCounterTextView", true);
            _startTimeTextView = (TextView) GetControl("StartTimeTextView", true);
            _departureTypeTextView = (TextView) GetControl("DepartureTypeTextView", true);
            _startButton = (Button) GetControl("StartButton", true);
 
            DConsole.WriteLine("Loading model info");
            _currentOrder = GetCurrentOrder();
            _orderClient = GetOrderClient();
            _departyreType = GetDepartureType();

            DConsole.WriteLine("Writing info to controls");
            _contactDescriptionTextView.Text = _orderClient.Description;
            _contactAddressTextView.Text = _orderClient.Address;
            _taskCounterTextView.Text = $"{GetTaskNumberDone()}/{GetTaskNumber()}";
            _cocTextView.Text = $"{GetCertificateOfCompletion()}";
            _checkListCounterTextView.Text = $"{GetCheckListDone()}/{GetCheckListNumber()}";
            _startTimeTextView.Text = $"{_currentOrder.StartDatePlan}";
            _departureTypeTextView.Text = $"{_departyreType.Description}";
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

        internal void StartButton_OnClick(object sender, EventArgs eventArgs)
        {
            _startButton.Text = Translator.Translate("finish");
        }

        internal void CancelButton_OnClick(object sender, EventArgs eventArgs)
        {
            BusinessProcess.DoAction("EventList");
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
            return new TypesDepartures()
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