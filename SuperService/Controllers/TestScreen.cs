using System;
using System.Collections.Generic;
using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;

namespace Test
{
    public class TestScreen : Screen
    {
        private VerticalLayout _rootLayout;
        private TextView _testTextView;

        public override void OnLoading()
        {
            _rootLayout = (VerticalLayout) Controls[0];
            _testTextView = (TextView) _rootLayout.Controls[1];
        }

        public override void OnShow()
        {
            DConsole.WriteLine("OnShow?");

            if (BusinessProcess.GlobalVariables.ContainsKey("serviceMaterialNumber"))
            {
                var result =
                    (EditServiceOrMaterialsScreenResult) BusinessProcess.GlobalVariables["serviceMaterialNumber"];
                _testTextView.Text = $"Price = {result.Price}, Count = {result.Count}, Full = {result.FullPrice}";
            }
        }

        internal string GetResourceImage(string tag)
        {
            return ResourceManager.GetImage(tag);
        }

        internal void Button_OnClick(object sender, EventArgs eventArgs)
        {
            Navigation.Move("EditServicesOrMaterialsScreen", new Dictionary<string, object>
            {
                {"priceVisible", true},
                {"priceEditable", true},
                {"minimum", 0},
                {"behaviour", BehaviourEditServicesOrMaterialsScreen.ReturnValue},
                {"returnKey", "serviceMaterialNumber"}
            });
        }
    }
}