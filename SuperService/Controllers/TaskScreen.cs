using System;
using System.Collections.Generic;
using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;
using Test.Components;

namespace Test
{
    public class TaskScreen : Screen
    {
        private bool _taskCommentTextExpanded;
        private TextView _taskCommentTextView;
        private string _taskResult;
        private TopInfoComponent _topInfoComponent;
        private Image _wrapUnwrapImage;

        private HorizontalLayout _taskFinishedButton;
        private HorizontalLayout _taskRefuseButton;
        private TextView _taskFinishedButtonTextView;
        private TextView _taskRefuseButtonTextView;
        private Image _taskFinishedButtonImage;
        private Image _taskRefuseButtonImage;

        private MemoEdit _taskCommentEditText;

        private DockLayout _rootLayout;

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

            _taskFinishedButton = (HorizontalLayout) GetControl("TaskFinishedButton", true);
            _taskRefuseButton = (HorizontalLayout) GetControl("TaskRefuseButton", true);
            _taskFinishedButtonTextView = (TextView) GetControl("TaskFinishedButtonTextView", true);
            _taskRefuseButtonTextView = (TextView) GetControl("TaskRefuseButtonTextView", true);
            _taskFinishedButtonImage = (Image) GetControl("TaskFinishedButtonImage", true);
            _taskRefuseButtonImage = (Image) GetControl("TaskRefuseButtonImage", true);

            _taskCommentEditText = (MemoEdit) GetControl("TaskCommentEditText", true);
            _rootLayout = (DockLayout) Controls[0];
        }

        internal void TaskFinishedButton_OnClick(object sender, EventArgs eventArgs)
        {
            switch (_taskResult)
            {
                case "NotDone":
                case "New":
                    ChangeTaskToDone();
                    break;
                case "Done":
                    ChangeTaskToNew();
                    break;
                default:
                    throw new ArgumentException("Неправильный результат задания");
            }
        }

        private void ChangeTaskToNew()
        {
            _taskResult = "New";
            _taskFinishedButton.CssClass = "FinishedButtonActive";
            _taskFinishedButtonTextView.CssClass = "FinishedButtonActiveText";
            _taskFinishedButtonImage.Source = ResourceManager.GetImage("tasklist_notdone");
            _taskFinishedButton.Refresh();
            _taskRefuseButton.CssClass = "RefuseButton";
            _taskRefuseButtonTextView.CssClass = "RefuseButtonText";
            _taskRefuseButtonImage.Source = ResourceManager.GetImage("tasklist_notdone");
            _taskRefuseButton.Refresh();
        }

        private void ChangeTaskToDone()
        {
            _taskResult = "Done";
            _taskFinishedButton.CssClass = "FinishedButtonPressed";
            _taskFinishedButtonTextView.CssClass = "FinishedButtonPressedText";
            _taskFinishedButtonImage.Source = ResourceManager.GetImage("tasklist_done");
            _taskFinishedButton.Refresh();
            _taskRefuseButton.CssClass = "RefuseButton";
            _taskRefuseButtonTextView.CssClass = "RefuseButtonText";
            _taskRefuseButtonImage.Source = ResourceManager.GetImage("tasklist_notdone");
            _taskRefuseButton.Refresh();
        }

        internal void TaskRefuseButton_OnClick(object sender, EventArgs eventArgs)
        {
            switch (_taskResult)
            {
                case "Done":
                case "New":
                    ChangeTaskToNotDone();
                    break;
                case "NotDone":
                    ChangeTaskToNew();
                    break;
                default:
                    throw new ArgumentException("Неправильный результат задания");
            }
        }

        private void ChangeTaskToNotDone()
        {
            _taskResult = "NotDone";
            _taskFinishedButton.CssClass = "FinishedButtonActive";
            _taskFinishedButtonTextView.CssClass = "FinishedButtonActiveText";
            _taskFinishedButtonImage.Source = ResourceManager.GetImage("tasklist_notdone");
            _taskFinishedButton.Refresh();
            _taskRefuseButton.CssClass = "RefuseButtonPressed";
            _taskRefuseButtonTextView.CssClass = "RefuseButtonPressedText";
            _taskRefuseButtonImage.Source = ResourceManager.GetImage("tasklist_specdone");
            _taskRefuseButton.Refresh();
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
            _rootLayout.Refresh();
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
            var currentTaskId = (string) BusinessProcess.GlobalVariables["currentTaskId"];
            DConsole.WriteLine($"{_taskResult}");
            DBHelper.UpdateTaskComment(currentTaskId, _taskCommentEditText.Text);
            DBHelper.UpdateTaskResult(currentTaskId, _taskResult);
            BusinessProcess.DoAction("BackToTaskList");
        }

        internal object GetTask()
        {
//            return new Dictionary<string, object>
//            {
//                {
//                    "Target",
//                    "Lorem Ipsum - это текст-\"рыба\", часто используемый в печати и вэб-дизайне. Lorem Ipsum является стандартной \"рыбой\" для текстов на латинице с начала XVI века. В то время некий безымянный печатник создал большую коллекцию размеров и форм шрифтов, используя Lorem Ipsum для распечатки образцов. Lorem Ipsum не только успешно пережил без заметных изменений пять веков, но и перешагнул в электронный дизайн. Его популяризации в новое время послужили публикация листов Letraset с образцами Lorem Ipsum в 60-х годах и, в более недавнее время, программы электронной вёрстки типа Aldus PageMaker, в шаблонах которых используется Lorem Ipsum."
//                },
//                {"EquipmentDescription", "Asus 509-k"},
//                {"TypeDepartures", "Монтаж"},
//                {"resultName", "New"}
//            };
            string currentTaskId = (string) BusinessProcess.GlobalVariables["currentTaskId"];
            return DBHelper.GetTaskById(currentTaskId);
        }

        internal string GetResourceImage(string tag)
        {
            return ResourceManager.GetImage(tag);
        }

        internal int SetResultName(string resultName)
        {
            _taskResult = resultName;
            return 0;
        }
    }
}