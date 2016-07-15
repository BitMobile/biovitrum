using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;
using System;
using System.Collections.Generic;
using Test.Components;

namespace Test
{
    public class EventScreen : Screen
    {
        private DbRecordset _currentEventRecordset;
        private Button _refuseButton;
        private DockLayout _rootLayout;
        private Button _startButton;
        private TextView _taskCommentTextView;
        private bool _taskCommentTextExpanded;
        private Image _wrapUnwrapImage;

        private Button _startFinishButton;

        private TopInfoComponent _topInfoComponent;

        public override void OnLoading()
        {
            _topInfoComponent = new TopInfoComponent(this);

            LoadControls();
            FillControls();

            IsEmptyDateTime((string)_currentEventRecordset["ActualStartDate"]);
        }

        private void FillControls()
        {
            try
            {
                _topInfoComponent.Header =
                    ((string)_currentEventRecordset["clientDescription"]).CutForUIOutput(13, 2);
                _topInfoComponent.CommentLayout.AddChild(new TextView(
                    ((string)_currentEventRecordset["clientAddress"]).CutForUIOutput(17, 2)));
            }
            catch (ArgumentException ex)
            {
                DConsole.WriteLine("First Try");
                DConsole.WriteLine($"{ex.Message}{Environment.NewLine}{ex.StackTrace}");
            }

            _topInfoComponent.LeftButtonControl = new Image { Source = ResourceManager.GetImage("topheading_back") };
            _topInfoComponent.RightButtonControl = new Image { Source = ResourceManager.GetImage("topheading_info") };

            _taskCommentTextView = (TextView)GetControl("EventCommentTextView", true);
            _wrapUnwrapImage = (Image)GetControl("WrapUnwrapImage", true);

            var extraHorizontalLayout = new HorizontalLayout { CssClass = "ExtraHorizontalLayout" };
            var leftExtraLayout = new VerticalLayout { CssClass = "ExtraVerticalLayout" };
            var rightExtraLayout = new VerticalLayout { CssClass = "ExtraVerticalLayout" };

            _topInfoComponent.ExtraLayout.AddChild(extraHorizontalLayout);
            extraHorizontalLayout.AddChild(leftExtraLayout);
            extraHorizontalLayout.AddChild(rightExtraLayout);

            leftExtraLayout.AddChild(new Image
            {
                CssClass = "TopInfoSideImage",
                Source = ResourceManager.GetImage("topinfo_extra_map")
            });
            leftExtraLayout.AddChild(new TextView
            {
                Text = Translator.Translate("onmap"),
                CssClass = "TopInfoSideText"
            });

            rightExtraLayout.AddChild(new Image
            {
                CssClass = "TopInfoSideImage",
                Source = ResourceManager.GetImage("topinfo_extra_person")
            });
            var text = (string)_currentEventRecordset["ContactVisitingDescription"];
            DConsole.WriteLine("text: " + text);
            rightExtraLayout.AddChild(new TextView
            {
                Text = ((string)_currentEventRecordset["ContactVisitingDescription"]).CutForUIOutput(12, 2),
                CssClass = "TopInfoSideText"
            });

            leftExtraLayout.OnClick += GoToMapScreen_OnClick;
        }

        public override void OnShow()
        {
            GPS.StartTracking();
        }

        private void LoadControls()
        {
            _rootLayout = (DockLayout)GetControl("RootLayout");
            _startFinishButton = (Button)GetControl("StartFinishButton", true);
            _startButton = (Button)GetControl("StartButton", true);
            _refuseButton = (Button)GetControl("RefuseButton", true);
        }

        internal void ClientInfoButton_OnClick(object sender, EventArgs eventArgs)
        {
            Navigation.Move("ClientScreen");
        }

        internal void RefuseButton_OnClick(object sender, EventArgs eventArgs)
        {
            Navigation.Move("CancelEventScreen");
        }

        internal string FormatEventStartDatePlanTime(string eventStartDatePlanTime)
        {
            return eventStartDatePlanTime.Substring(0, 5);
        }

        internal void StartButton_OnClick(object sender, EventArgs eventArgs)
        {
            Dialog.Ask(Translator.Translate("areYouSure"), (o, args) =>
            {
                if (args.Result == Dialog.Result.Yes)
                {
                    ChangeLayoutToStartedEvent();
                }
            });
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

        private void ChangeLayoutToStartedEvent()
        {
            _startButton.CssClass = "NoHeight";
            _startButton.Visible = false;
            _startButton.Refresh();
            _refuseButton.CssClass = "NoHeight";
            _refuseButton.Visible = false;
            _refuseButton.Refresh();
            _startFinishButton.CssClass = "FinishButton";
            _startFinishButton?.Refresh();
            _startFinishButton.Text = $"{Translator.Translate("finish")}\n{DateTime.Now.Date.ToString("HH:mm")}";
            _rootLayout.Refresh();
            Event_OnStart();
        }

        internal void StartFinishButton_OnClick(object sender, EventArgs eventArgs)
        {
            var result = DBHelper.GetTotalFinishedRequireQuestionByEventId(
                (string)BusinessProcess.GlobalVariables[Parameters.IdCurrentEventId]);

            var isActiveEvent = result.Next()
                ? ((long)result["count"]) == 0
                : Convert.ToBoolean("True");

            if (isActiveEvent)
            {
                Dialog.Alert(Translator.Translate("closeeventquestion"), (o, args) =>
                {
                    if (CheckEventBeforeClosing() && args.Result == 0)
                    {
                        DBHelper.UpdateActualEndDateByEventId(DateTime.Now,
                            (string)BusinessProcess.GlobalVariables[Parameters.IdCurrentEventId]);
                        Navigation.Move("CloseEventScreen");
                    }
                }, null,
                    Translator.Translate("yes"), Translator.Translate("no"));
            }
        }

        private bool CheckEventBeforeClosing()
        {
            // TODO: Здесь будет проверка наряда перед закрытием
            return true;
        }

        private void Event_OnStart()
        {
            DBHelper.UpdateActualStartDateByEventId(DateTime.Now,
                (string)BusinessProcess.GlobalVariables[Parameters.IdCurrentEventId]);
        }

        internal void TopInfo_LeftButton_OnClick(object sender, EventArgs eventArgs)
        {
            Navigation.Back();
        }

        internal void TopInfo_RightButton_OnClick(object sender, EventArgs eventArgs)
        {
            BusinessProcess.GlobalVariables[Parameters.IdClientId] =
                _currentEventRecordset[Parameters.IdClientId].ToString();
            Navigation.Move("ClientScreen");
        }

        internal void TopInfo_Arrow_OnClick(object sender, EventArgs eventArgs)
        {
            _topInfoComponent.Arrow_OnClick(sender, eventArgs);
            _rootLayout.Refresh();
        }

        internal void TaskCounterLayout_OnClick(object sender, EventArgs eventArgs)
        {
            if (CheckBigButtonActive(sender))
                Navigation.Move("TaskListScreen");
        }

        private bool CheckBigButtonActive(object sender)
        {
            // TODO: Сделать проверку более аккуратной?
            var layout = (HorizontalLayout)sender;
            return ((TextView)layout.Controls[2]).Text != "0";
        }

        internal void GoToCOCScreen_OnClick(object sender, EventArgs e)
        {
            object eventId;
            if (!BusinessProcess.GlobalVariables.TryGetValue(Parameters.IdCurrentEventId, out eventId))
            {
                DConsole.WriteLine("Can't find current event ID, going to crash");
            }

            var dictinory = new Dictionary<string, object>
            {
                {Parameters.IdCurrentEventId, (string) eventId}
            };
            Navigation.Move("COCScreen", dictinory);
        }

        internal void CheckListCounterLayout_OnClick(object sender, EventArgs eventArgs)
        {
            Navigation.Move("CheckListScreen");
        }

        internal DbRecordset GetCurrentEvent()
        {
            object eventId;
            if (!BusinessProcess.GlobalVariables.TryGetValue(Parameters.IdCurrentEventId, out eventId))
            {
                DConsole.WriteLine("Can't find current event ID, going to crash");
            }
            _currentEventRecordset = DBHelper.GetEventByID((string)eventId);
            return _currentEventRecordset;
        }

        internal string GetStringPartOfTotal(long part, long total)
        {
            return Converter.ToDecimal(part) != 0 ? $"{part}/{total}" : $"{total}";
        }

        internal bool IsEmptyDateTime(string dateTime)
        {
            return dateTime == "0001-01-01 00:00:00";
        }

        internal bool IsNotEmptyDateTime(string dateTime)
        {
            return dateTime != "0001-01-01 00:00:00";
        }

        internal string GetResourceImage(string tag)
        {
            return ResourceManager.GetImage(tag);
        }

        internal void GoToMapScreen_OnClick(object sender, EventArgs e)
        {
            var clientId = (string)_currentEventRecordset[Parameters.IdClientId];
            var dictionary = new Dictionary<string, object>
            {
                {Parameters.IdScreenStateId, MapScreenStates.EventScreen},
                {Parameters.IdClientId, clientId}
            };

            BusinessProcess.GlobalVariables.Remove(Parameters.IdScreenStateId);
            BusinessProcess.GlobalVariables.Remove(Parameters.IdClientId);
            BusinessProcess.GlobalVariables[Parameters.IdScreenStateId] = MapScreenStates.EventScreen;
            BusinessProcess.GlobalVariables[Parameters.IdClientId] = clientId;

            Navigation.Move("MapScreen", dictionary);
        }

        internal bool IsNotZero(long count)
        {
            return Convert.ToInt64(count) != Convert.ToInt64(0L);
        }

        internal string GetFormatString(string translate, object date)
        {
            DateTime startActualDate;
            var isOk = DateTime.TryParse((string)date, out startActualDate);

            if (isOk)
            {
                var result = DateTime.Now - startActualDate;
                var ours = (int)result.TotalHours;
                var totalHours = ours < Convert.ToInt32(10) ? $"0{ours}" : $"{ours}";
                DConsole.WriteLine($"Времени с начала наряда {result}");
                return $"{Translator.Translate(translate)}\n{totalHours}:{result.ToString("mm")}";
            }
            throw new Exception("Parsing error");
        }
    }
}