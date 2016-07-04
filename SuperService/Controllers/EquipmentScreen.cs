using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using Test.Components;
using Test.Entities.Catalog;

namespace Test
{
    public class EquipmentScreen : Screen
    {
        private bool _fieldsAreInitialized = false;

        private TopInfoComponent _topInfoComponent;

        //TODO: Когда починят метод getObject сделать чтение в этот объект по Гуиду, пока пересозаем его в initClassFields
        private Equipment _equipment;

        private string _equipmentId;
        private string _equipmentDescription;

        public override void OnLoading()
        {
            InitClassFields();

            _topInfoComponent = new TopInfoComponent(this)
            {
                HeadingTextView = { Text = Translator.Translate("equipment") },
                LeftButtonImage = { Source = ResourceManager.GetImage("topheading_back") },
                RightButtonImage = { Visible = false },
                ExtraLayoutVisible = false
            };
        }

        public int InitClassFields()
        {
            if (_fieldsAreInitialized)
            {
                return 0;
            }

            //TODO: сделать получение по гуиду через getObject когда его починят

            _equipmentId = (string)Variables.GetValueOrDefault(Parameters.IdEquipmentId, "");
            var equipmentRS = DBHelper.GetEquipmentById(_equipmentId);
            if (equipmentRS.Next())
            {
                _equipmentDescription = equipmentRS.GetString(0);
            }

            _fieldsAreInitialized = true;

            return 0;
        }

        internal void TopInfo_LeftButton_OnClick(object sender, EventArgs eventArgs)
        {
            Navigation.Back(true);
        }

        internal void TopInfo_RightButton_OnClick(object sender, EventArgs eventArgs)
        {
        }

        internal void TopInfo_Arrow_OnClick(object sender, EventArgs eventArgs)
        {
            _topInfoComponent.Arrow_OnClick(sender, eventArgs);
        }

        internal void BackButton_OnClick(object sender, EventArgs eventArgs)
        {
            Navigation.Back(true);
        }

        internal string GetEquipmentDescription()
        {
            return _equipmentDescription;
        }

        internal DbRecordset GetParameters()
        {
            return DBHelper.GetEquipmentParametersById(_equipmentId);
        }

        internal DbRecordset GetHistory()
        {
            var res = DBHelper.GetEquipmentHistoryById(_equipmentId, new DateTime());
            return res;
        }

        internal string FormatDateString(string dateTime)
        {
            DateTime date;

            if (DateTime.TryParse(dateTime, out date))
            {
                return date.ToString("d MMMM");
            }

            throw new FormatException("Date format uncorrect");
        }

        internal string GetResourceImage(string tag)
        {
            return ResourceManager.GetImage(tag);
        }
    }
}