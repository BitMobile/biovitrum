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
            BusinessProcess.DoAction("Client");
        }

        internal string GetResourceImage(string tag)
        {
            return ResourceManager.GetImage(tag);
        }

        internal string GetResourceComponent(string tag)
        {
            return ResourceManager.GetComponent(tag);
        }
    }
}