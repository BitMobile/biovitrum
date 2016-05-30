using System;
using System.Collections.Generic;
using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;
using Test.Components;

namespace Test
{
    public class TaskScreen : Screen
    {
        private TopInfoComponent _topInfoComponent;

        public override void OnLoading()
        {
            _topInfoComponent = new TopInfoComponent(this)
            {
                HeadingTextView = {Text = Translator.Translate("task")},
                LeftButtonImage = {Source = ResourceManager.GetImage("topheading_back")},
                RightButtonImage = {Visible = false},
                ExtraLayoutVisible = false
            };
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
            _topInfoComponent.Arrow_OnClick(sender, eventArgs);
        }

        internal void TopInfo_RightButton_OnClick(object sender, EventArgs eventArgs)
        {
            DConsole.WriteLine("HOW? WHY?");
        }

        internal void TopInfo_LeftButton_OnClick(object sender, EventArgs eventArgs)
        {
            BusinessProcess.DoAction("BackToTaskList");
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