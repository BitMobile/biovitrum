using System;
using BitMobile.ClientModel3.UI;

namespace Test
{
    public class TaskScreen : Screen
    {
        internal void BackButton_OnClick(object sender, EventArgs eventArgs)
        {
            BusinessProcess.DoAction("BackToTaskList");
        }
    }
}