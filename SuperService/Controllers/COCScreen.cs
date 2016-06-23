using System;
using System.Collections.Generic;
using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;
using BitMobile.Common.Controls;
using Test.Components;

namespace Test
{
    public class COCScreen : Screen
    {
        private DbRecordset _sums;
        private TopInfoComponent _topInfoComponent;
        private string _currentEventId;

        private bool _fieldsAreInitialized = false;

        public override void OnLoading()
        {
            InitClassFields();

            _topInfoComponent = new TopInfoComponent(this)
            {
                ExtraLayoutVisible = true,
                HeadingTextView = {Text = Translator.Translate("coc")},
                RightButtonImage = {Visible = false},
                LeftButtonImage = {Source = ResourceManager.GetImage("topheading_back")},
                CommentTextView =
                {
                    Text = Translator.Translate("total") + Environment.NewLine + Convert.ToDouble((double) _sums["Sum"])
                           + Translator.Translate("currency")
                },
                BigArrowActive = false
            };
        }


        public int InitClassFields()
        {
            if (_fieldsAreInitialized)
            {
                return 0;
            }

            _currentEventId = (string)Variables.GetValueOrDefault(Parameters.IdCurrentEventId, string.Empty);

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
            var vl = (VerticalLayout) sender;
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
            var vl = (VerticalLayout) sender;
            var hl = (IHorizontalLayout3) vl.Parent;
            var shl = (ISwipeHorizontalLayout3) hl.Parent;
            ++shl.Index;
        }

        internal void DeleteButton_OnClick(object sender, EventArgs e)
        {
            //TODO: Обходной путь получения парента. Внимание!!!!!.
            var vl = (VerticalLayout) sender;
            DBHelper.DeleteServiceOrMaterialById(vl.Id);
            var shl = (ISwipeHorizontalLayout3) vl.Parent;
            shl.CssClass = "NoHeight";
            shl.Refresh();
        }

        internal string GetFormatStringForSums(double number)
        {
            return "\u2022" + Convert.ToDouble(number) + Translator.Translate("currency");
        }


        internal DbRecordset GetSums()
        {
 
            object eventId;
            if (!BusinessProcess.GlobalVariables.TryGetValue(Parameters.IdCurrentEventId, out eventId))
            {
                DConsole.WriteLine("Can't find current event ID, going to crash");
            }
            DConsole.WriteLine("In to: " + nameof(GetSums));
            _sums = DBHelper.GetCocSumsByEventId((string) eventId);

            return _sums;
        }

        internal DbRecordset GetServices()
        {
            object eventId;
            if (!BusinessProcess.GlobalVariables.TryGetValue(Parameters.IdCurrentEventId, out eventId))
            {
                DConsole.WriteLine("Can't find current event ID, going to crash");
            }

            return DBHelper.GetServicesByEventId((string) eventId);
        }

        internal string Concat(float amountFact, float price)
        {

            DConsole.WriteLine("Concat - amountFact=" + amountFact + " type=" + amountFact.GetType() + " price=" + price + " type="+ price.GetType());
           // return Convert.ToSingle(amountFact) + " x " + Convert.ToSingle(price);
            return "" + amountFact + " x " + price;
        }

        internal DbRecordset GetMaterials()
        {
            object eventId;
            if (!BusinessProcess.GlobalVariables.TryGetValue(Parameters.IdCurrentEventId, out eventId))
            {
                DConsole.WriteLine("Can't find current event ID, going to crash");
            }

            return DBHelper.GetMaterialsByEventId((string) eventId);
        }
    }
}
 