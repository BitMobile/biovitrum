using System;
using BitMobile.ClientModel3.UI;

namespace Test
{
    public class CloseEventScreen : Screen
    {
        private bool _problem;
        private HorizontalLayout _problemButton;
        private VerticalLayout _problemCommentLayout;
        private bool _wantToBuy;
        private HorizontalLayout _wantToBuyButton;
        private VerticalLayout _wantToBuyCommentLayout;

        public override void OnLoading()
        {
            _wantToBuyButton = (HorizontalLayout) GetControl("WantToBuyButton", true);
            _wantToBuyCommentLayout = (VerticalLayout) GetControl("WantToBuyCommentLayout", true);
            _problemButton = (HorizontalLayout) GetControl("ProblemButton", true);
            _problemCommentLayout = (VerticalLayout) GetControl("ProblemCommentLayout", true);
        }

        internal void WantToBuyButton_OnClick(object sender, EventArgs eventArgs)
        {
            if (!_wantToBuy)
            {
                UpdateButtonCSS(_wantToBuyButton, _wantToBuyCommentLayout, "BigButtonPressed", "CommentLayout");
                _wantToBuy = true;
            }
            else
            {
                UpdateButtonCSS(_wantToBuyButton, _wantToBuyCommentLayout, "BigButton", "NoHeight");
                _wantToBuy = false;
            }
        }

        private void UpdateButtonCSS(HorizontalLayout buttonLayout, VerticalLayout commentLayout, string buttonCSS,
            string commentCSS)
        {
            buttonLayout.CssClass = buttonCSS;
            buttonLayout.Refresh();
            commentLayout.CssClass = commentCSS;
            commentLayout.Refresh();
        }


        internal void ProblemButton_OnClick(object sender, EventArgs eventArgs)
        {
            if (!_problem)
            {
                UpdateButtonCSS(_problemButton, _problemCommentLayout, "BigButtonPressed", "CommentLayout");
                _problem = true;
            }
            else
            {
                UpdateButtonCSS(_problemButton, _problemCommentLayout, "BigButton", "NoHeight");
                _problem = false;
            }
        }

        internal void FinishButton_OnClick(object sender, EventArgs eventArgs)
        {
            // TODO: Закрытие наряда
            BusinessProcess.DoAction("FinishEvent");
        }
    }
}