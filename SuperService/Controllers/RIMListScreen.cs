using System;
using System.Collections;
using System.Collections.Generic;
using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;
using Test.Components;

namespace Test
{
    public class RIMListScreen : Screen
    {
        private bool _isMaterialRequest;
        private bool _isService;
        private TopInfoComponent _topInfoComponent;

        public override void OnLoading()
        {
            DConsole.WriteLine("RIMListScreen init");

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

            var isMaterialRequest = Variables.GetValueOrDefault("isMaterialsRequest", Convert.ToBoolean("False"));
            _isMaterialRequest = (bool) isMaterialRequest;
        }

        internal string GetResourceImage(string tag)
        {
            return ResourceManager.GetImage(tag);
        }

        internal void TopInfo_LeftButton_OnClick(object sender, EventArgs e)
        {
            Navigation.Back();
        }

        internal void TopInfo_Arrow_OnClick(object sender, EventArgs e)
        {
            _topInfoComponent.Arrow_OnClick(sender, e);
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
                line = new EventServicesMaterialsLine
                {
                    Ref = (string) currentEventId,
                    SKU = rimID,
                    Price = price,
                    AmountPlan = 0,
                    SumPlan = 0,
                    AmountFact = 1
                };
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

            if (_isMaterialRequest)
            {
                var key = Variables.GetValueOrDefault("returnKey", "newItem");
                var dictionary = new Dictionary<string, object>
                {
                    {"rimId", rimID},
                    {"priceVisible", Convert.ToBoolean("False")},
                    {"behaviour", BehaviourEditServicesOrMaterialsScreen.ReturnValue},
                    {"returnKey", key},
                    {"lineId", null}
                };
                DConsole.WriteLine("Go to EditServicesOrMaterials is Material Request true");
                Navigation.Move("EditServicesOrMaterialsScreen", dictionary);
            }
            else
            {
                var dictionary = new Dictionary<string, object>
                {
                    {"rimId", rimID},
                    {"behaviour", BehaviourEditServicesOrMaterialsScreen.InsertIntoDB}
                };

                DConsole.WriteLine("Go to EditServicesOrMaterials is Material Request false");
                Navigation.Move("EditServicesOrMaterialsScreen", dictionary);
            }
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
            DbRecordset result;

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