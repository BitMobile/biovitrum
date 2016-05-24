using System;
using System.Collections;
using System.Collections.Generic;
using BitMobile.ClientModel3.UI;

namespace Test
{
    public class TaskListScreen : Screen
    {
        internal void BackButton_OnClick(object sender, EventArgs eventArgs)
        {
            BusinessProcess.DoAction("BackToEvent");
        }

        internal IEnumerable GetTasks()
        {
            return new ArrayList
            {
                new Dictionary<string, object>
                {
                    {"Id", "1"},
                    {"Name", "Ремонт"},
                    {"Comment", "Маршрутизатор"},
                    {"Done", "False"}
                },
                new Dictionary<string, object>
                {
                    {"Id", "2"},
                    {"Name", "Монтаж"},
                    {"Comment", "Сервер"},
                    {"Done", "False"}
                },
                new Dictionary<string, object>
                {
                    {"Id", "3"},
                    {"Name", "Настройка"},
                    {"Comment", "Роутер"},
                    {"Done", "False"}
                },
                new Dictionary<string, object>
                {
                    {"Id", "4"},
                    {"Name", "Монтаж"},
                    {"Comment", "Сервер"},
                    {"Done", "False"}
                }
            };
        }
    }
}