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
                HeadingTextView = { Text = _isService?Translator.Translate("services"): Translator.Translate("materials") },
                LeftButtonImage = { Source = ResourceManager.GetImage("topheading_back") },
                RightButtonImage = { Visible = false },
                ExtraLayoutVisible = false
            };

        }

        internal string GetResourceImage(string tag)
        {
            return ResourceManager.GetImage(tag);
        }

        internal void TopInfo_LeftButton_OnClick(object sender, EventArgs e)
        {
            BusinessProcess.DoAction("ViewCOC");
        }

        internal void RIMLayout_OnClick(object sender, EventArgs eventArgs)
        {
            DConsole.WriteLine("RIMLayout_OnClick " + ((VerticalLayout)sender).Id);
            // TODO: Передача Id конкретной таски
            var rimID = ((VerticalLayout)sender).Id;
            double price = Double.Parse(((TextView)((VerticalLayout)sender).Controls[1]).Text);
            DConsole.WriteLine("цена " + price);

            object currentEventId;
            if (!BusinessProcess.GlobalVariables.TryGetValue("currentEventId", out currentEventId))
            {
                DConsole.WriteLine("Can't find current clientId, i'm crash.");
            }
            var line = DBHelper.GetEventServicesMaterialsLineByRIMID((string)currentEventId, rimID);
            if (line == null)
            {
                DConsole.WriteLine("Позиция не найдена, просто добавлеям новую");
                line = new EventServicesMaterialsLine();
                line.Ref = (string)currentEventId;
                line.SKU = rimID;
                // TODO: Добавить получение цены
                line.Price = price;
                line.AmountPlan = 0;
                line.SumPlan = 0;
                line.AmountPlan = 1;
                line.SumFact = line.AmountPlan * line.Price;

                DBHelper.InsertEventServicesMaterialsLine(line);

                DConsole.WriteLine("Добавили");
            }
            else
            {
                DConsole.WriteLine("Позиция найдена, увеличим количество и обновим БД");
                line.AmountFact += 1;
                line.SumFact = line.Price * line.AmountFact;
                DBHelper.UpdateEventServicesMaterialsLine(line);
                DConsole.WriteLine("Обновили");
            }

            DConsole.WriteLine("Пытаемся перейти на экран АВР");
            BusinessProcess.DoAction("RIMAdded");

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
