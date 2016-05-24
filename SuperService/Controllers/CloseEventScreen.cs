using System;
using BitMobile.ClientModel3.UI;

namespace Test
{
    public class CloseEventScreen : Screen
    {
        internal void FinishButton_OnClick(object sernder, EventArgs eventArgs)
        {
            // TODO: Закрытие наряда
            BusinessProcess.DoAction("FinishEvent");
        }
    }
}