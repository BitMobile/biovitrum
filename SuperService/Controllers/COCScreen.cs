using System;
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

        public override void OnLoading()
        {
            DConsole.WriteLine("In to: " + nameof(OnLoading));
            _topInfoComponent = new TopInfoComponent(this)
            {
                ExtraLayoutVisible = true,
                HeadingTextView = {Text = Translator.Translate("coc")},
                RightButtonImage = {Visible = false},
                LeftButtonImage = {Source = ResourceManager.GetImage("topheading_back")},
                CommentTextView =
                {
                    Text = GetFormatStringForSums((double) _sums["Sum"])
                }
            };
            
        }

        internal string GetResourceImage(string tag)
        {
            return ResourceManager.GetImage(tag);
        }

        internal void TopInfo_LeftButton_OnClick(object sender, EventArgs e)
        {
            BusinessProcess.DoAction("ViewEvent");
        }

        internal void TopInfo_RightButton_OnClick(object sender, EventArgs e)
        {
        }

        internal void TopInfo_Arrow_OnClick(object sender, EventArgs e)
        {
        }

        internal void AddService_OnClick(object sender, EventArgs e)
        {
            BusinessProcess.GlobalVariables["isService"] = true;
            BusinessProcess.DoAction("AddServicesOrMaterials");
        }

        internal void AddMaterial_OnClick(object sender, EventArgs e)
        {
            BusinessProcess.GlobalVariables["isService"] = false;
            BusinessProcess.DoAction("AddServicesOrMaterials");
        }

        internal void EditServicesOrMaterials_OnClick(object sender, EventArgs e)
        {
            var vl = (VerticalLayout) sender;
            BusinessProcess.GlobalVariables["currentServicesMaterialsId"] = vl.Id;
            BusinessProcess.DoAction("EditServicesOrMaterials");
        }

        internal void ApplicatioMaterials_OnClick(object sender, EventArgs e)
        {
            BusinessProcess.DoAction("ApplicationMaterials");
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

        private string GetFormatStringForSums(double number)
        {
            return "\u2022" + Convert.ToDouble(number) + Translator.Translate("currency");
        }

      
        internal DbRecordset GetSums()
        {
            object eventId;
            if (!BusinessProcess.GlobalVariables.TryGetValue("currentEventId", out eventId))
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
            if (!BusinessProcess.GlobalVariables.TryGetValue("currentEventId", out eventId))
            {
                DConsole.WriteLine("Can't find current event ID, going to crash");
            }

            return DBHelper.GetServicesByEventId((string) eventId);
        }

        private string Concat(float amountFact, float price)
        {
            return Convert.ToSingle(amountFact) + " x " + Convert.ToSingle(price);
        }

        internal DbRecordset GetMaterials()
        {
            object eventId;
            if (!BusinessProcess.GlobalVariables.TryGetValue("currentEventId", out eventId))
            {
                DConsole.WriteLine("Can't find current event ID, going to crash");
            }

            return DBHelper.GetMaterialsByEventId((string) eventId);
        }
    }
}