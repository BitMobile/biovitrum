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
        private TextView _taskCommentTextView;
        private bool _taskCommentTextExpanded = false;
        private Image _wrapUnwrapImage;

        public override void OnLoading()
        {
            _topInfoComponent = new TopInfoComponent(this)
            {
                HeadingTextView = {Text = Translator.Translate("task")},
                LeftButtonImage = {Source = ResourceManager.GetImage("topheading_back")},
                RightButtonImage = {Visible = false},
                ExtraLayoutVisible = false
            };

            _taskCommentTextView = (TextView) GetControl("TaskCommentTextView", true);
            _wrapUnwrapImage = (Image) GetControl("WrapUnwrapImage", true);
        }

        internal void TaskFinishedButton_OnClick(object sender, EventArgs eventArgs)
        {
            // TODO: Логику прописать тут. Возможно, апдейт БД?
        }

        internal void TaskRefuseButton_OnClick(object sender, EventArgs eventArgs)
        {
            // TODO: Логику прописать тут. Возможно, апдейт БД?
        }

        internal void WrapUnwrapButton_OnClick(object sender, EventArgs eventArgs)
        {
            if (_taskCommentTextExpanded)
            {
                _taskCommentTextView.CssClass = "SubComment";
                _wrapUnwrapImage.Source = ResourceManager.GetImage("longtext_expand");
                _taskCommentTextExpanded = false;
            }
            else
            {
                _taskCommentTextView.CssClass = "SubCommentExpanded";
                _wrapUnwrapImage.Source = ResourceManager.GetImage("longtext_close");
                _taskCommentTextExpanded = true;
            }
            _taskCommentTextView.Refresh();
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
            return new Dictionary<string, object>
            {
                {"Target", "Маршрутизатор" },
                {"Comment", "Lorem Ipsum - это текст-\"рыба\", часто используемый в печати и вэб-дизайне. Lorem Ipsum является стандартной \"рыбой\" для текстов на латинице с начала XVI века. В то время некий безымянный печатник создал большую коллекцию размеров и форм шрифтов, используя Lorem Ipsum для распечатки образцов. Lorem Ipsum не только успешно пережил без заметных изменений пять веков, но и перешагнул в электронный дизайн. Его популяризации в новое время послужили публикация листов Letraset с образцами Lorem Ipsum в 60-х годах и, в более недавнее время, программы электронной вёрстки типа Aldus PageMaker, в шаблонах которых используется Lorem Ipsum." },
                {"EquipmentDescription", "Asus 509-k" },
                {"TypeDepartures", "Монтаж" },
                {"resultName", "Appointed" }
            };
//            string currentTaskId = (string) BusinessProcess.GlobalVariables["currentTaskId"];
//            return DBHelper.GetTaskById(currentTaskId);
        }

        internal string GetResourceImage(string tag)
        {
            return ResourceManager.GetImage(tag);
        }
    }
}