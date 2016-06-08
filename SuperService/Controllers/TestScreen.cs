using System;
using BitMobile.ClientModel3.UI;

namespace Test
{
    public class TestScreen : Screen
    {
        private DockLayout _rootLayout;

        public override void OnLoading()
        {
            _rootLayout = (DockLayout) Controls[0];
        }

        internal string GetResourceImage(string tag)
        {
            return ResourceManager.GetImage(tag);
        }

        internal void TabEventsButton_OnClick(object o, EventArgs eventArgs)
        {
            ((TextView) GetControl("TextViewBag", true)).CssClass = "TabTextViewNotActive";
            ((Image) GetControl("ImgBag", true)).Source = ResourceManager.GetImage("tabbar_bag");
            ((Image) GetControl("ImgEvents", true)).Source = ResourceManager.GetImage("tabbar_events_active");
            ((TextView) GetControl("TextViewEvents", true)).CssClass = "TabTextViewActive";
            ((SwipeVerticalLayout) GetControl("TextViewsLayout", true)).CssClass = "RootLayout";
            ((SwipeVerticalLayout) GetControl("ButtonsLayout", true)).CssClass = "NoHeight";
            _rootLayout.Refresh();
        }

        internal void TabBagButton_OnClick(object o, EventArgs eventArgs)
        {
            ((TextView) GetControl("TextViewBag", true)).CssClass = "TabTextViewActive";
            ((Image) GetControl("ImgBag", true)).Source = ResourceManager.GetImage("tabbar_bag_active");
            ((Image) GetControl("ImgEvents", true)).Source = ResourceManager.GetImage("tabbar_events");
            ((TextView) GetControl("TextViewEvents", true)).CssClass = "TabTextViewNotActive";
            ((SwipeVerticalLayout) GetControl("TextViewsLayout", true)).CssClass = "NoHeight";
            ((SwipeVerticalLayout) GetControl("ButtonsLayout", true)).CssClass = "RootLayout";
            _rootLayout.Refresh();
        }

        internal void TabClientsButton_OnClick(object o, EventArgs eventArgs)
        {
        }

        internal void TabSettingsButton_OnClick(object o, EventArgs eventArgs)
        {
        }
    }
}