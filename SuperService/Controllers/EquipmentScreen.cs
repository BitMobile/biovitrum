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
        private TopInfoComponent _topInfoComponent;
        private Equipment _equipment;

        public override void OnLoading()
        {
            // TODO: Получение Equipment из БД
            _equipment = new Equipment()
            {
                Description = "Роутер ASUS"
            };
            _topInfoComponent = new TopInfoComponent(this)
            {
                HeadingTextView = { Text = Translator.Translate("equipment") },
                LeftButtonImage = { Source = ResourceManager.GetImage("topheading_back") },
                RightButtonImage = { Visible = false },
                ExtraLayoutVisible = false
            };
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

        internal IEnumerable GetEquipmentInfo()
        {
            return new Dictionary<string, object>
            {
                ["Desctiption"] = "Роутер ASUS"
            };
        }

        internal IEnumerable GetParameters()
        {
            return new ArrayList
            {
                new Dictionary<string, object>
                {
                    ["Parameter"] = "Стандарты связи",
                    ["Value"] = "154i"
                },
                new Dictionary<string, object>
                {
                    ["Parameter"] = "Вид протокола",
                    ["Value"] = "Не выбрано"
                },
                new Dictionary<string, object>
                {
                    ["Parameter"] = "Есть индикаторы",
                    ["Value"] = "нет"
                },
                new Dictionary<string, object>
                {
                    ["Parameter"] = "Серийный номер",
                    ["Value"] = "17-2016gi"
                }
            };
        }

        internal IEnumerable GetHistory()
        {
            return new ArrayList
            {
                new Dictionary<string, object>
                {
                    ["Description"] = "Монтаж",
                    ["Result"] = "Выполнено",
                    ["Date"] = DateTime.Now
                },
                new Dictionary<string, object>
                {
                    ["Description"] = "Ремонт",
                    ["Result"] = "Не выполнено",
                    ["Date"] = DateTime.Now.Date
                }
            };
        }

        internal string GetResourceImage(string tag)
        {
            return ResourceManager.GetImage(tag);
        }
    }
}