using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;
using BitMobile.Common.Controls;
using System;
using System.Collections.Generic;
using Test.Components;

namespace Test
{
    public class COCScreen : Screen
    {
        private DbRecordset _sums;
        private TopInfoComponent _topInfoComponent;
        private TextView _totalSumForServices;
        private TextView _totalSumForMaterials;
        private string _currentEventId;
        private bool _usedCalculateService;
        private bool _usedCalculateMaterials;

        private bool _fieldsAreInitialized = false;

        public override void OnLoading()
        {
           InitClassFields();
            var totalSum = "";

            if (!_usedCalculateMaterials && !_usedCalculateService)
            {
                totalSum = Parameters.EmptyPriceDescription;
            }
            else
            {
                totalSum =
                    $"{Math.Round((_usedCalculateService ? (double) _sums["SumServices"] : 0) + (_usedCalculateMaterials ? (double) _sums["SumMaterials"] : 0), 2)}";
            }

            _topInfoComponent = new TopInfoComponent(this)
            {
                ExtraLayoutVisible = true,
                HeadingTextView = { Text = Translator.Translate("coc") },
                RightButtonImage = { Visible = false },
                LeftButtonImage = { Source = ResourceManager.GetImage("topheading_back") },
                CommentTextView =
                {
                    Text = $"{Translator.Translate("total")}" +
                                                     $"{Environment.NewLine}" +
                                                     totalSum +
                                                     $" {Translator.Translate("currency")}"
        },
                BigArrowActive = false
            };

            DConsole.WriteLine("OnLoading() 9");

            _totalSumForServices = (TextView)GetControl("RightInfoServicesTV", true);
            _totalSumForMaterials = (TextView)GetControl("RightInfoMaterialsTV", true);
        }

        public int InitClassFields()
        {

            DConsole.WriteLine("InitClassFields()");
            //bool testBool = ?


            if (_fieldsAreInitialized)
            {
                return 0;
            }

            _currentEventId = (string)Variables.GetValueOrDefault(Parameters.IdCurrentEventId, string.Empty);
            _usedCalculateService = DBHelper.GetIsUsedCalculateService();
            _usedCalculateMaterials = DBHelper.GetIsUsedCalculateMaterials();

            GetSums();

            _fieldsAreInitialized = true;

            return 0;
        }
        
        public override void OnShow()
        {
            GPS.StopTracking();
        }

        internal string GetResourceImage(string tag)
        {
            return ResourceManager.GetImage(tag);
        }

        internal void TopInfo_LeftButton_OnClick(object sender, EventArgs e)
        {
            Navigation.Back(true);
        }

        internal void TopInfo_RightButton_OnClick(object sender, EventArgs e)
        {
        }

        internal void TopInfo_Arrow_OnClick(object sender, EventArgs e)
        {
            _topInfoComponent.Arrow_OnClick(sender, e);
        }

        internal void AddService_OnClick(object sender, EventArgs e)
        {
            var dictionary = new Dictionary<string, object>
            {
                {Parameters.IdIsService, true},
                {Parameters.IdCurrentEventId, _currentEventId}
            };
            Navigation.Move("RIMListScreen", dictionary);
        }

        internal void AddMaterial_OnClick(object sender, EventArgs e)
        {
            var dictionary = new Dictionary<string, object>
            {
                {Parameters.IdIsService, false},
                {Parameters.IdCurrentEventId, _currentEventId}
            };
            Navigation.Move("RIMListScreen", dictionary);
        }

        internal void EditServicesOrMaterials_OnClick(object sender, EventArgs e)
        {
            /*TODO: Внимание не редактировать элементы до тех пор, пока не починят экран редактирования, а то
             * приложение упадет
             */
            var vl = (VerticalLayout)sender;
            var dictionary = new Dictionary<string, object>
            {
                {Parameters.IdBehaviour, BehaviourEditServicesOrMaterialsScreen.UpdateDB},
                {Parameters.IdLineId, vl.Id}
            };

            Navigation.Move("EditServicesOrMaterialsScreen", dictionary);
        }

        internal void ApplicatioMaterials_OnClick(object sender, EventArgs e)
        {
            Navigation.Move("MaterialsRequestScreen");
        }

        internal void OpenDeleteButton_OnClick(object sender, EventArgs e)
        {
            //TODO: Обходной путь получения парента. Внимание!!!!! .
            var vl = (VerticalLayout)sender;
            var hl = (IHorizontalLayout3)vl.Parent;
            var shl = (ISwipeHorizontalLayout3)hl.Parent;
            ++shl.Index;
        }

        internal void DeleteButton_OnClick(object sender, EventArgs e)
        {
            //TODO: Обходной путь получения парента. Внимание!!!!!.
            var vl = (VerticalLayout)sender;
            DBHelper.DeleteServiceOrMaterialById(vl.Id);
            var shl = (ISwipeHorizontalLayout3)vl.Parent;
            shl.CssClass = "NoHeight";
            var sums = GetSums();
            _totalSumForServices.Text = GetFormatStringForServiceSums();
            _totalSumForMaterials.Text = GetFormatStringForMaterialSums();
            _topInfoComponent.CommentTextView.Text = $"{Translator.Translate("total")}" +
                                                     $"{Environment.NewLine}" +
                                                     $"{Math.Round((double)sums["Sum"], 2)} " +
                                                     $"{Translator.Translate("currency")}";
            shl.Refresh();
        }

        internal string GetFormatStringForServiceSums()
        {
            var totalSum = Convert.ToDouble(_sums["SumServices"]).ToString();
            return $"\u2022 {(_usedCalculateService?totalSum:Parameters.EmptyPriceDescription)} {Translator.Translate("currency")}";
        }

        internal string GetFormatStringForMaterialSums()
        {
            var totalSum = Convert.ToDouble(_sums["SumMaterials"]).ToString();
            return $"\u2022 {(_usedCalculateMaterials ? totalSum : Parameters.EmptyPriceDescription)} {Translator.Translate("currency")}";
        }

        internal string GetServicePriceDescription(DbRecordset service)
        {
            return _usedCalculateService ? service["Price"].ToString() : Parameters.EmptyPriceDescription;
        }

        internal string GetMaterialPriceDescription(DbRecordset material)
        {
            return _usedCalculateMaterials ? material["Price"].ToString() : Parameters.EmptyPriceDescription;
        }

        internal DbRecordset GetSums()
        {
            object eventId;
            if (!BusinessProcess.GlobalVariables.TryGetValue(Parameters.IdCurrentEventId, out eventId))
            {
                DConsole.WriteLine("Can't find current event ID, going to crash");
            }
            DConsole.WriteLine("In to: " + nameof(GetSums));
            _sums = DBHelper.GetCocSumsByEventId((string)eventId);

            return _sums;
        }

        internal DbRecordset GetServices()
        {
            object eventId;
            if (!BusinessProcess.GlobalVariables.TryGetValue(Parameters.IdCurrentEventId, out eventId))
            {
                DConsole.WriteLine("Can't find current event ID, going to crash");
            }

            return DBHelper.GetServicesByEventId((string)eventId);
        }

        internal string Concat(float amountFact, string price, string unit)
        {
            DConsole.WriteLine("Concat - amountFact=" + amountFact + " type=" + amountFact.GetType() + " price=" + price + " type=" + price.GetType());
            return $"{amountFact} {unit} x {price} {Translator.Translate("currency")}";
        }

        internal DbRecordset GetMaterials()
        {
            object eventId;
            if (!BusinessProcess.GlobalVariables.TryGetValue(Parameters.IdCurrentEventId, out eventId))
            {
                DConsole.WriteLine("Can't find current event ID, going to crash");
            }

            return DBHelper.GetMaterialsByEventId((string)eventId);
        }
    }
}