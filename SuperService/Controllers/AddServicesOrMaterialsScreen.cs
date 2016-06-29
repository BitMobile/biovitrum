using System;
using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;
namespace Test
{
    class AddServicesOrMaterialsScreen : Screen
    {
        public override void OnLoading()
        {
            DConsole.WriteLine("Add Services Or Materials Screen");
            DConsole.WriteLine(BusinessProcess.GlobalVariables[Parameters.IdIsService].ToString());
        }

        internal void Back_OnClick(object sender, EventArgs e)
        {
            Navigation.Back();
        }
    }
}
