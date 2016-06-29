using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;
using System;
using Test.Entities.Catalog;

namespace Test
{
    public class ContactScreen : Screen
    {
        private Contacts _contact;

        public ContactScreen()
        {
            Variables[Parameters.Contact] = new Contacts
            {
                Description = "Сергеев Алексей",
                Position = "Старший инженер",
                Tel = "+7 (921) 859-93-29",
                EMail = "nsergeev@comp.ru"
            };
        }

        public override void OnLoading()
        {
            _contact = (Contacts)Variables.GetValueOrDefault(Parameters.Contact);
        }

        internal void CallButton_OnClick(object o, EventArgs e)
        {
            Phone.Call(_contact.Tel);
        }

        internal void SendMessageButton_OnClick(object o, EventArgs e)
        {
            Dialog.Message("Мы не можем отправлять сообщения");
        }

        internal void WriteLetterButton_OnClick(object o, EventArgs e)
        {
            Dialog.Message("Мы не можем отправлять EMail");
        }

        internal void BackButton_OnClick(object o, EventArgs e)
        {
            Navigation.Back();
        }
    }
}