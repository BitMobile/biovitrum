using System;
using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;
namespace Test
{
    public class EditServicesOrMaterialsScreen : Screen
    {
        public override void OnLoading()
        {
            DConsole.WriteLine("Edit Services Or Materials Screen");
            DConsole.WriteLine(BusinessProcess.GlobalVariables["currentServicesMaterialsId"].ToString());
        }

        internal void Back_OnClick(object sender, EventArgs e)
        {
            BusinessProcess.DoAction("COC");
        }
    }
}