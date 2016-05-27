using System;
using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;
using Test.Components;

namespace Test
{
    public class ClientScreen : Screen
    {
        

        public override void OnLoading()
        {
            
        }

        internal void Top_LeftButton_OnClick(object sender, EventArgs e)
        {
            
        }

        internal void Top_RightButton_OnClick(object sender, EventArgs e)
        {
            
            DConsole.WriteLine("Some Action");
        }

    }
}