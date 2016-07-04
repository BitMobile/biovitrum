using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;
using System;
using Test.Catalog;
using Test.Components;

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

        /// <summary>
        ///     Проверяет строку на то, что она null, пустая
        ///     или представляет пробельный символ
        /// </summary>
        /// <param name="item">Строка для проверки</param>
        /// <returns>
        ///     True если строка пустая, null или
        ///     пробельный символ.
        /// </returns>
        internal bool IsNotEmptyString(string item)
        {
            return !(string.IsNullOrEmpty(item) && string.IsNullOrWhiteSpace(item));
        }

        internal void TopInfo_LeftButton_OnClick(object sender, EventArgs e)
        {
            Navigation.Back(true);
        }

        internal void TopInfo_RightButton_OnClick(object sender, EventArgs e)
        {
            DConsole.WriteLine("Должны перейти на экран редактирования контакта");
        }

        internal void CallButton_OnClick(object o, EventArgs e)
        {
            DConsole.WriteLine("Пытаемся позвонить");
            Phone.Call(_contact.Tel);
        }

        internal void SendMessageButton_OnClick(object o, EventArgs e)
        {
            Dialog.Message("Для отправки сообщения отправьте сообщение на номер 9441215. Стоимость смс 100500");
        }

        internal void WriteEMailButton_OnClick(object o, EventArgs e)
        {
            Dialog.Message("Для отправки мыла отправьте мыло на адрес 96115. стоимость мыла 100500");
        }

        internal void BackButton_OnClick(object o, EventArgs e)
        {
            Navigation.Back();
        }
    }
}