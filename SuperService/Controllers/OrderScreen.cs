using System;
using BitMobile.ClientModel3.UI;
using Test.Catalog;
using Test.Document;

namespace Test
{
    public class OrderScreen : Screen
    {
        public override void OnLoading()
        {
        }


        // TODO: Сделать это работать
        internal void BackButton_OnClick(object sender, EventArgs eventArgs)
        {
        }

        internal void ClientInfoButton_OnClick(object sender, EventArgs eventArgs)
        {
        }

        internal void StartButton_OnClick(object sender, EventArgs eventArgs)
        {
        }

        internal void CancelButton_OnClick(object sender, EventArgs eventArgs)
        {
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
    }
}