using System;
using System.Collections;
using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;
using Test.Components;

namespace Test
{
    public class RIMListScreen : Screen
    {
        private bool _isService;
        private TopInfoComponent _topInfoComponent;

        public override void OnLoading()
        {
            DConsole.WriteLine("RIMListScreen init");

            var title = "";


            _topInfoComponent = new TopInfoComponent(this)
            {
                HeadingTextView =
                {
                    Text = _isService ? Translator.Translate("services") : Translator.Translate("materials")
                },
                LeftButtonImage = {Source = ResourceManager.GetImage("topheading_back")},
                RightButtonImage = {Visible = false},
                ExtraLayoutVisible = false
            };
        }

        internal string GetResourceImage(string tag)
        {
            return ResourceManager.GetImage(tag);
        }

        internal void TopInfo_LeftButton_OnClick(object sender, EventArgs e)
        {
            BusinessProcess.DoBack();
        }

        internal void RIMLayout_OnClick(object sender, EventArgs eventArgs)
        {
            var rimID = ((VerticalLayout) sender).Id;
            var price = decimal.Parse(((TextView) ((VerticalLayout) sender).Controls[1]).Text);

            object currentEventId;
            if (!BusinessProcess.GlobalVariables.TryGetValue("currentEventId", out currentEventId))
            {
                DConsole.WriteLine("Can't find current clientId, i'm crash.");
            }

            DConsole.WriteLine("Пытаемся найти номенклатуру в документе " + (string) currentEventId + " по гуиду " +
                               rimID);
            var line = DBHelper.GetEventServicesMaterialsLineByRIMID((string) currentEventId, rimID);
            if (line == null)
            {
                DConsole.WriteLine("Позиция не найдена, просто добавлеям новую");
                line = new EventServicesMaterialsLine();
                line.Ref = (string) currentEventId;
                line.SKU = rimID;
                line.Price = price;
                line.AmountPlan = 0;
                line.SumPlan = 0;
                line.AmountFact = 1;
                line.SumFact = line.AmountFact*line.Price;

                DBHelper.InsertEventServicesMaterialsLine(line);

                DConsole.WriteLine("Добавили");
            }
            else
            {
                DConsole.WriteLine("Позиция найдена, увеличим количество и обновим БД amountFact " + line.AmountFact);
                line.AmountFact += 1;
                line.SumFact = line.Price*line.AmountFact;
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

            _isService = (bool) isService;
            DbRecordset result = null;

            if (_isService)
            {
                result = DBHelper.GetRIMByType(RIMType.Service);
                DConsole.WriteLine("Получили услуги " + RIMType.Material);
            }
            else
            {
                result = DBHelper.GetRIMByType(RIMType.Material);
                DConsole.WriteLine("Получили товары " + RIMType.Material);
            }

            return result;
        }
    }
}