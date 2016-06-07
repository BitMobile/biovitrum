using System;
using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;
namespace Test
{
    // TODO: Экран Заявка на материалы
    public class ApplicationMaterialsScreen : Screen
    {
        public override void OnLoading()
        {
            DConsole.WriteLine("Application Materials Screen");
        }

        internal void Back_OnClick(object sender, EventArgs e)
        {
            BusinessProcess.DoAction("COC");
        }
    }
}