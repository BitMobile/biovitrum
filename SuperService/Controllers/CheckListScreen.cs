using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;

namespace Test
{
    public class CheckListScreen : Screen
    {
        internal void BackButton_OnClick(object sender, EventArgs eventArgs)
        {
            BusinessProcess.DoAction("BackToEvent");
        }

        private void Camera_OnClick(object sender, EventArgs e)
        {
            //Camera.MakeSnapshot("//private//test.jpg", 5,);
        }
    }
}
