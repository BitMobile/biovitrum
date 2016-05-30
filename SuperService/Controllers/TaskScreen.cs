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

        internal string GetResourceImage(string tag)
        {
            return ResourceManager.GetImage(tag);
        }
    }
}