using BitMobile.ClientModel3.UI;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Test
{
    public class EquipmentScreen : Screen
    {
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
                    ["Value"] = "False"
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
    }
}