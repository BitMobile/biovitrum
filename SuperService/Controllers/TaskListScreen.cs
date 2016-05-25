using System;
using System.Collections;
using BitMobile.ClientModel3.UI;

namespace Test
{
    public class TaskListScreen : Screen
    {
        internal void BackButton_OnClick(object sender, EventArgs eventArgs)
        {
            BusinessProcess.DoAction("BackToEvent");
        }

        internal void TaskLayout_OnClick(object sender, EventArgs eventArgs)
        {
            // TODO: Передача Id конкретной таски
            BusinessProcess.GlobalVariables["currentTaskId"] = ((HorizontalLayout) sender).Id;
            BusinessProcess.DoAction("ViewTask");
        }

        internal object GetEvent()
        {
            return DBHelper.GetEventByID((string) BusinessProcess.GlobalVariables["currentEventId"]);
        }

        internal IEnumerable GetTasks()
        {
//            return new ArrayList
//            {
//                new Dictionary<string, object>
//                {
//                    {"Id", "1"},
//                    {"Name", "Ремонт"},
//                    {"Comment", "Маршрутизатор"},
//                    {"Done", false}
//                },
//                new Dictionary<string, object>
//                {
//                    {"Id", "2"},
//                    {"Name", "Монтаж"},
//                    {"Comment", "Сервер"},
//                    {"Done", false}
//                },
//                new Dictionary<string, object>
//                {
//                    {"Id", "3"},
//                    {"Name", "Настройка"},
//                    {"Comment", "Роутер"},
//                    {"Done", true}
//                },
//                new Dictionary<string, object>
//                {
//                    {"Id", "4"},
//                    {"Name", "Монтаж"},
//                    {"Comment", "Сервер"},
//                    {"Done", false}
//                }
//            };
            return DBHelper.GetTasksByEventID((string) BusinessProcess.GlobalVariables["currentEventId"]);
        }
    }
}