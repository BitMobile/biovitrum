using BitMobile.ClientModel3;

namespace Test
{
    public class Solution : Application
    {
        public override void OnCreate()
        {
            DConsole.WriteLine("DB init...");
            DBHelper.Init();
            DConsole.WriteLine("Loading first screen");
            BusinessProcess.GlobalVariables[Parameters.IdCurrentEventId] =
                "@ref[Document_Event]:c2e83f87-218a-11e6-80e3-005056011152";
            Navigation.Move("CheckListScreen");
            //            Navigation.Move("EquipmentScreen");
        }
    }
}