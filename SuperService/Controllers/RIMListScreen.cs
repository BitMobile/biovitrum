using System;
using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;
using Test.Components;
using System.Collections;


namespace Test
{
    public class RIMListScreen : Screen
    {
        private TopInfoComponent _topInfoComponent;
        private bool _isService;

        public override void OnLoading()
        {

            DConsole.WriteLine("RIMListScreen init");

            string title = "";


            _topInfoComponent = new TopInfoComponent(this)
            {
                HeadingTextView = { Text = Translator.Translate("clients") },
                LeftButtonImage = { Visible = false },
                RightButtonImage = { Visible = false },
                ExtraLayoutVisible = false
            };

        }

        internal string GetResourceImage(string tag)
        {
            return ResourceManager.GetImage(tag);
        }

        internal void RIMLayout_OnClick(object sender, EventArgs eventArgs)
        {
            DConsole.WriteLine("RIMLayout_OnClick " + ((VerticalLayout)sender).Id);
            // TODO: Передача Id конкретной таски
            var rimID = ((VerticalLayout)sender).Id;

            object currentEventId;
            if (!BusinessProcess.GlobalVariables.TryGetValue("currentEventId", out currentEventId))
            {
                DConsole.WriteLine("Can't find current clientId, i'm crash.");
            }
            //проверяем и добавляем 
            //var line = DBHelper.GetEventServicesMaterialsLineByRIMID((string)currentEventId, rimID);


            //BusinessProcess.DoAction("AddRIM");
        }


        internal IEnumerable GetRIM()
        {
            DConsole.WriteLine("получение позиций товаров и услуг");

            object isService;
            if (!BusinessProcess.GlobalVariables.TryGetValue("isService", out isService))
            {
                DConsole.WriteLine("Can't find current clientId, i'm crash.");
            }

            _isService = (bool)isService;
            DbRecordset result = null;

            if (_isService)
            { 
                result = DBHelper.GetRIMByType(Enum.RIMType.Service);
                DConsole.WriteLine("Получили услуги " + Enum.RIMType.Material);
            }else
            {
                result = DBHelper.GetRIMByType(Enum.RIMType.Material);
                DConsole.WriteLine("Получили товары " + Enum.RIMType.Material);
            }
            
            return result;
        }
    }
}
