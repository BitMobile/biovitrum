using System;
using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;

namespace Test
{
    internal class AddContactScreen : Screen
    {
        public override void OnLoading()
        {
            DConsole.WriteLine("Add Contackt Screen");
        }

        internal void Back_OnClick(object sender, EventArgs e)
        {
            //BusinessProcess.DoAction("Client");
            Navigation.Back();
        }

        internal void AddContactButton_OnClick(object sender, EventArgs e)
        {

        }


        internal void TopInfo_LeftButton_OnClick(object sender, EventArgs e)
        {
            //BusinessProcess.DoAction("BackToEvent");
            Navigation.Back();
        }
    }
}