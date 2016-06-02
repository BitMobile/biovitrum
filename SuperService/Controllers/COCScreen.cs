using System;
using System.Reflection;
using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;
using Test.Components;

namespace Test
{
    public class COCScreen : Screen
    {
        private TopInfoComponent _topInfoComponent;

        public override void OnLoading()
        {
            _topInfoComponent = new TopInfoComponent(this)
            {
                ExtraLayoutVisible = true,
                HeadingTextView = {Text = Translator.Translate("coc")},
                RightButtonImage = {Visible = false},
                LeftButtonImage = {Source = ResourceManager.GetImage("topheading_back")},
                CommentTextView =
                {
                    Text =
                        Translator.Translate("total") + Environment.NewLine + "123456" +
                        Translator.Translate("currency")
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

        internal void AddServiceOrMaterials_OnClick(object sender, EventArgs e)
        {
            BusinessProcess.DoAction("AddServicesOrMaterials");
        }

        internal void EditServicesOrMaterials_OnClick(object sender, EventArgs e)
        {
            BusinessProcess.DoAction("EditServicesOrMaterials");
        }

        internal void ApplicatioMaterials_OnClick(object sender, EventArgs e)
        {
            BusinessProcess.DoAction("ApplicationMaterials");
        }

        internal void OpenDeleteButton_OnClick(object sender, EventArgs e)
        {
            //TODO: сделать выдвижение кнопки удаление.
        }

        internal void DeleteButton_OnClick(object sender, EventArgs e)
        {
            
        }
    }
}