using System;
using System.Collections.Generic;
using BitMobile.ClientModel3.UI;

namespace Test
{
    public class TaskScreen : Screen
    {
        internal void BackButton_OnClick(object sender, EventArgs eventArgs)
        {
            BusinessProcess.DoAction("BackToTaskList");
        }

        internal void TaskFinishedButton_OnClick(object sender, EventArgs eventArgs)
        {
            // TODO: Логику прописать тут. Возможно, апдейт БД?
        }

        internal void TaskRefuseButton_OnClick(object sender, EventArgs eventArgs)
        {
            // TODO: Логику прописать тут. Возможно, апдейт БД?
        }

        internal void TopInfo_Arrow_OnClick(object sender, EventArgs eventArgs)
        {
            
        }

        internal object GetTask()
        {
            string currentTaskId = (string) BusinessProcess.GlobalVariables["currentTaskId"];
            return new Dictionary<string, object>
            {
                {"Terget", "Маршрутизатор" },
                {"Comment", "Тут много текста" },
                {"EquipmentDescription", "Asus 509-k" },
                {"TypeDepartures", "Монтаж" },
                {"resultName", "Appointed" }
            };
//            return DBHelper.GetTaskById(currentTaskId);
        }

        internal string GetResourceImage(string tag)
        {
            return ResourceManager.GetImage(tag);
        }
    }
}