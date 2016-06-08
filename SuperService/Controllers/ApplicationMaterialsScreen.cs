using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Mail;
using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;
using BitMobile.Common.Controls;
using Test.Components;

namespace Test
{
    // TODO: Переименовать файл в MeterialsReuestScreen
    public class ApplicationMaterialsScreen : Screen
    {
        private bool _isEmptyList = false;
        private TopInfoComponent _topInfoComponent;
        private VerticalLayout _rootVerticalLayout;
        //TODO: Заменить на RecordSet
        private ArrayList _data;

        public override void OnLoading()
        {
            _rootVerticalLayout = (VerticalLayout) this.GetControl("Root");
            _topInfoComponent = new TopInfoComponent(this)
            {
                ExtraLayoutVisible = false,
                HeadingTextView = {Text = Translator.Translate("request")},
                RightButtonImage = {Visible = false},
                LeftButtonImage = {Source = ResourceManager.GetImage("close")}
            };
        }


        internal void TopInfo_LeftButton_OnClick(object sender, EventArgs e)
        {
            DConsole.WriteLine("Back to screen .....");
            BusinessProcess.DoAction("COC");
        }

        internal void TopInfo_RightButton_OnClick(object sender, EventArgs e)
        {

        }

        internal void TopInfo_Arrow_OnClick(object sender, EventArgs e)
        {
        }

        internal string GetResourceImage(string tag)
        {
            return ResourceManager.GetImage(tag);
        }

        internal bool GetIsEmptyList()
        {
            FillData();
            if (_data.Count > 0)
            {
                _isEmptyList = false;
                }
            else
            {
                _isEmptyList = true;
            }
            return _isEmptyList;
        }

        internal void OpenDeleteButton_OnClick(object sender, EventArgs e)
        {
            var vl = (VerticalLayout)sender;
            var hl = (IHorizontalLayout3)vl.Parent;
            var shl = (ISwipeHorizontalLayout3)hl.Parent;
            ++shl.Index;
            DConsole.WriteLine(nameof(shl.Index) + "=" + shl.Index.ToString());
        }

        internal void DeleteButton_OnClick(object sender, EventArgs e)
        {
            var btn = (Button) sender;
            var shl = (ISwipeHorizontalLayout3) btn.Parent;
            shl.CssClass = "NoHeight";
            shl.Refresh();
        }

        private void FillData()
        {
            _data = new ArrayList();

            for (int i = 0; i < 0; i++)
            {
                    Dictionary<string,object> dic = new Dictionary<string, object>();
                dic["first"] = "Test " + i.ToString();
                dic["second"] = "Test " + i.ToString();
                _data.Add(dic);
            }
        }

        internal ArrayList GetData()
        {
            //Получение данных из БД в разментку XML
            return _data;
        }
    }
}