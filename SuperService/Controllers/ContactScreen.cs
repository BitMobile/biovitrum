using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;
using System;
using Test.Components;
using Test.Entities.Catalog;

namespace Test
{
    public class ContactScreen : Screen
    {

        private TopInfoComponent _topInfoComponent;
        private Contacts _contact;
        private bool _fieldsAreInitialized = false;


        public override void OnLoading()
        {
            InitClassFields();
            _topInfoComponent = new TopInfoComponent(this)
            {
                ExtraLayoutVisible = false,
                HeadingTextView = { Text = Translator.Translate("contact") },
                RightButtonImage = { Source = ResourceManager.GetImage("topheading_edit") },
                LeftButtonImage = { Source = ResourceManager.GetImage("topheading_back") },
                BigArrowActive = false
            };
        }

        public int InitClassFields()
        {
            if (_fieldsAreInitialized)
            {
                return 0;
            }

            _contact = (Contacts)Variables.GetValueOrDefault(Parameters.Contact);
            _fieldsAreInitialized = true;
            return 0;
        }

        internal string GetResourceImage(string tag)
        {
            return ResourceManager.GetImage(tag);
        }

        internal void TopInfo_LeftButton_OnClick(object sender, EventArgs e)
        {
            Navigation.Back(true);
        }

        internal void TopInfo_RightButton_OnClick(object sender, EventArgs e)
        {
            DConsole.WriteLine("Должны перейти на экран редактирования контакта");
        }

        /*internal void TopInfo_Arrow_OnClick(object sender, EventArgs e)
        {
            _topInfoComponent.Arrow_OnClick(sender, e);
        }*/



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