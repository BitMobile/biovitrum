using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class TapEvents
    {
        public static void TabEventsButton_OnClick(object sender, EventArgs e)
        {
            BusinessProcess.DoAction("Auth");
        }
    }
}
