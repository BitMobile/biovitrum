using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;

namespace Test
{
    public class TabEventsComponent
    {
        private Screen _parentScreen;

        //private Image _topInfoArrowImage;
        //private HorizontalLayout _topInfoExtraButtonsLayout;
        //private bool minimized = true;

        public TabEventsComponent(Screen parentScreen)
        {
            _parentScreen = parentScreen;
        }


        internal void Events_OnClick(object sender, EventArgs e)
        {
            BusinessProcess.DoAction("OrderList");
        }

        internal void Bag_OnClick(object sender, EventArgs e)
        {
            BusinessProcess.DoAction("Bag");
        }

        internal void Clients_OnClick(object sender, EventArgs e)
        {
            BusinessProcess.DoAction("ClientList");
        }

        internal void Settings_OnClick(object sender, EventArgs e)
        {
            BusinessProcess.DoAction("Settings");
        }
    }
}
