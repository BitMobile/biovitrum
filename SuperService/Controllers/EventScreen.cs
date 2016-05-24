using System;
using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;
using Test.Components;

namespace Test
{
    public class EventScreen : Screen
    {
        private DbRecordset _currentEventRecordset;
        private DockLayout _rootLayout;
        private bool _started;

        private Button _startFinishButton;

        private TopInfoComponent _topInfoComponent;

        public override void OnLoading()
        {
            _topInfoComponent = new TopInfoComponent(this);

            LoadControls();
            FillControls();
        }

        private void FillControls()
        {
            _topInfoComponent.HeadingTextView.Text = (string) _currentEventRecordset["clientDescription"];
            _topInfoComponent.CommentTextView.Text = (string) _currentEventRecordset["clientAddress"];
            _topInfoComponent.LeftButtonImage.Source = @"Image\top_back.png";
            _topInfoComponent.RightButtonImage.Source = @"Image\top_info.png";

            _topInfoComponent.LeftExtraLayout.AddChild(new Image
            {
                CssClass = "TopInfoSideImage",
                Source = @"Image\top_map.png"
            });
            _topInfoComponent.LeftExtraLayout.AddChild(new TextView
            {
                Text = Translator.Translate("onmap"),
                CssClass = "TopInfoSideText"
            });

            _topInfoComponent.RightExtraLayout.AddChild(new Image
            {
                CssClass = "TopInfoSideImage",
                Source = @"Image\top_person.png"
            });
            _topInfoComponent.RightExtraLayout.AddChild(new TextView
            {
                Text = (string) _currentEventRecordset["clientDescription"],
                CssClass = "TopInfoSideText"
            });
        }

        private void LoadControls()
        {
            _rootLayout = (DockLayout) GetControl("RootLayout");
            _startFinishButton = (Button) GetControl("StartFinishButton", true);
        }

        internal void ClientInfoButton_OnClick(object sender, EventArgs eventArgs)
        {
            BusinessProcess.DoAction("Client");
        }

        internal void StartFinishButton_OnClick(object sender, EventArgs eventArgs)
        {
            if (!_started)
            {
                _startFinishButton.CssClass = "FinishButton";
                _startFinishButton.Refresh();
                _startFinishButton.Text = Translator.Translate("finish");
                _started = true;
                Event_OnStart();
            }
            else
            {
                Dialog.Alert(Translator.Translate("closeeventquestion"), (o, args) =>
                {
                    if (CheckEventBeforeClosing() && args.Result == 0)
                        BusinessProcess.DoAction("CloseEvent");
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
            // TODO: Логика на старт наряда
        }

        internal void TopInfo_LeftButton_OnClick(object sender, EventArgs eventArgs)
        {
            BusinessProcess.DoAction("EventList");
        }

        internal void TopInfo_RightButton_OnClick(object sender, EventArgs eventArgs)
        {
            DConsole.WriteLine("Nothing to see here");
        }

        internal void TopInfo_Arrow_OnClick(object sender, EventArgs eventArgs)
        {
            _topInfoComponent.Arrow_OnClick(sender, eventArgs);
            _rootLayout.Refresh();
        }

        internal void TaskCounterLayout_OnClick(object sender, EventArgs eventArgs)
        {
            BusinessProcess.DoAction("ViewTasks");
        }

        internal DbRecordset GetCurrentEvent()
        {
            object eventId;
            if (!BusinessProcess.GlobalVariables.TryGetValue("currentEventId", out eventId))
            {
                eventId = "@ref[Document_Event]:6422e731-149a-11e6-80e3-005056011152";
            }
            _currentEventRecordset = DBHelper.GetEventByID((string) eventId);
            return _currentEventRecordset;
        }

        internal string GetStringPartOfTotal(int part, int total)
        {
            return $"{part}/{total}" == "/" ? "0/0" : $"{part}/{total}";
        }
    }
}