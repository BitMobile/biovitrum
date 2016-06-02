using System;
using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;
using BitMobile.Common.Controls;
using Test.Components;

namespace Test
{
    public class COCScreen : Screen
    {
        private DbRecordset _sums;
        private TopInfoComponent _topInfoComponent;

        public override void OnLoading()
        {
            GetSums();
            _topInfoComponent = new TopInfoComponent(this)
            {
                ExtraLayoutVisible = true,
                HeadingTextView = {Text = Translator.Translate("coc")},
                RightButtonImage = {Visible = false},
                LeftButtonImage = {Source = ResourceManager.GetImage("topheading_back")},
                CommentTextView =
                {
                    Text = GetFormatString((double) _sums["Sum"])
                }
            };
            DConsole.WriteLine(_topInfoComponent.CommentTextView.Text);
        }

        internal string GetResourceImage(string tag)
        {
            return ResourceManager.GetImage(tag);
        }

        internal void TopInfo_LeftButton_OnClick(object sender, EventArgs e)
        {
            BusinessProcess.DoAction("ViewEvent");
        }

        internal void TopInfo_RightButton_OnClick(object sender, EventArgs e)
        {
        }

        internal void TopInfo_Arrow_OnClick(object sender, EventArgs e)
        {
        }

        internal void AddServiceOrMaterials_OnClick(object sender, EventArgs e)
        {
            BusinessProcess.DoAction("AddServicesOrMaterials");
        }

        internal void EditServicesOrMaterials_OnClick(object sender, EventArgs e)
        {
            BusinessProcess.DoAction("EditServicesOrMaterials");
        }

        internal void ApplicatioMaterials_OnClick(object sender, EventArgs e)
        {
            BusinessProcess.DoAction("ApplicationMaterials");
        }

        internal void OpenDeleteButton_OnClick(object sender, EventArgs e)
        {
            //TODO: сделать выдвижение кнопки удаление.
            var vl = (VerticalLayout) sender;
            var hl = (IHorizontalLayout3) vl.Parent;
            var shl = (ISwipeHorizontalLayout3) hl.Parent;
            ++shl.Index;
        }

        internal void DeleteButton_OnClick(object sender, EventArgs e)
        {
            var vl = (VerticalLayout) sender;
            var shl = (ISwipeHorizontalLayout3) vl.Parent;
            shl.CssClass = "NoHeight";
            shl.Refresh();
        }

        private string GetFormatString(double number)
        {
            return number + Translator.Translate("currency");
        }

        internal DbRecordset GetSums()
        {
            object eventId;
            if (!BusinessProcess.GlobalVariables.TryGetValue("currentEventId", out eventId))
            {
                DConsole.WriteLine("Can't find current event ID, going to crash");
            }

            DConsole.WriteLine((string) eventId);
            _sums = DBHelper.GetCocSumsByEventId((string) eventId);
            return _sums;
        }
    }
}