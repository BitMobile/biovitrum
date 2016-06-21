using System;
using System.Collections;
using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;
using Test.Components;

namespace Test
{
    public class TaskListScreen : Screen
    {
        private TopInfoComponent _topInfoComponent;

        public override void OnLoading()
        {
            _topInfoComponent = new TopInfoComponent(this)
            {
                HeadingTextView = {Text = Translator.Translate("tasks")},
                LeftButtonImage = {Source = ResourceManager.GetImage("topheading_back") },
                RightButtonImage = {Visible = false},
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

        internal void TaskLayout_OnClick(object sender, EventArgs eventArgs)
        {
            BusinessProcess.GlobalVariables["currentTaskId"] = ((HorizontalLayout) sender).Id;
            Navigation.Move("TaskScreen");
        }

        internal void TopInfo_Arrow_OnClick(object sernder, EventArgs eventArgs)
        {
            _topInfoComponent.Arrow_OnClick(sernder, eventArgs);
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

        internal string GetResourceImage(string tag)
        {
            return ResourceManager.GetImage(tag);
        }
    }
}