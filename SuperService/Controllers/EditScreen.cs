using System;
using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;

namespace Test
{
    internal class EditContactScreen : Screen
    {
        public override void OnLoading()
        {
            DConsole.WriteLine("Edit Screen");
        }

        internal void Back_OnClick(object sender, EventArgs e)
        {
            BusinessProcess.DoAction("Client");
        }
    }
}