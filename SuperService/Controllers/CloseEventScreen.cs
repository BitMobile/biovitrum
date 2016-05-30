using System;
using BitMobile.ClientModel3.UI;

namespace Test
{
    public class CloseEventScreen : Screen
    {
        private bool _problem;
        private HorizontalLayout _problemButton;
        private VerticalLayout _problemCommentLayout;
        private Image _problemImage;
        private bool _wantToBuy;
        private HorizontalLayout _wantToBuyButton;
        private VerticalLayout _wantToBuyCommentLayout;
        private Image _wantToBuyImage;

        private MemoEdit _wantToBuyCommentMemoEdit;
        private MemoEdit _problemCommentMemoEdit;
        private MemoEdit _commentaryMemoEdit;

        public override void OnLoading()
        {
            _wantToBuyButton = (HorizontalLayout) GetControl("WantToBuyButton", true);
            _wantToBuyCommentLayout = (VerticalLayout) GetControl("WantToBuyCommentLayout", true);
            _wantToBuyImage = (Image) GetControl("WantToBuyImage", true);

            _problemButton = (HorizontalLayout) GetControl("ProblemButton", true);
            _problemCommentLayout = (VerticalLayout) GetControl("ProblemCommentLayout", true);
            _problemImage = (Image) GetControl("ProblemImage", true);

            _wantToBuyCommentMemoEdit = (MemoEdit) GetControl("WantToBuyCommentMemoEdit", true);
            _problemCommentMemoEdit = (MemoEdit) GetControl("ProblemCommentMemoEdit", true);
            _commentaryMemoEdit = (MemoEdit) GetControl("CommentaryMemoEdit", true);
        }

        internal void WantToBuyButton_OnClick(object sender, EventArgs eventArgs)
        {
            if (!_wantToBuy)
            {
                UpdateButtonCSS(_wantToBuyButton, _wantToBuyCommentLayout, _wantToBuyImage, "BigButtonPressed",
                    "CommentLayout", "closeevent_wtb_selected");
                _wantToBuy = true;
            }
            else
            {
                UpdateButtonCSS(_wantToBuyButton, _wantToBuyCommentLayout, _wantToBuyImage, "BigButton", "NoHeight",
                    "closeevent_wtb");
                _wantToBuy = false;
            }
        }

        private void UpdateButtonCSS(HorizontalLayout buttonLayout, VerticalLayout commentLayout, Image image,
            string buttonCSS,
            string commentCSS, string name)
        {
            buttonLayout.CssClass = buttonCSS;
            buttonLayout.Refresh();
            commentLayout.CssClass = commentCSS;
            commentLayout.Refresh();
            image.Source = ResourceManager.GetImage(name);
            image.Refresh();
        }


        internal void ProblemButton_OnClick(object sender, EventArgs eventArgs)
        {
            if (!_problem)
            {
                UpdateButtonCSS(_problemButton, _problemCommentLayout, _problemImage, "BigButtonPressed",
                    "CommentLayout", "closeevent_problem_selected");
                _problem = true;
            }
            else
            {
                UpdateButtonCSS(_problemButton, _problemCommentLayout, _problemImage, "BigButton", "NoHeight",
                    "closeevent_problem");
                _problem = false;
            }
        }

        internal void FinishButton_OnClick(object sender, EventArgs eventArgs)
        {
            // TODO: Закрытие наряда
            string eventId = (string) BusinessProcess.GlobalVariables["currentEventId"];
            if (_wantToBuy)
                DBHelper.InsertClosingEventSale(eventId, _wantToBuyCommentMemoEdit.Text);
            if (_problem)
                DBHelper.InsertClosingEventProblem(eventId, _problemCommentMemoEdit.Text);

            if (!string.IsNullOrEmpty(_commentaryMemoEdit.Text))
                DBHelper.UpdateClosingEventComment(eventId, _commentaryMemoEdit.Text);

            BusinessProcess.DoAction("FinishEvent");
        }

        internal string GetResourceImage(string tag)
        {
            return ResourceManager.GetImage(tag);
        }
    }
}