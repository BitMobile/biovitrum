using System;
using System.Collections;
using System.Collections.Generic;
using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;
using BitMobile.Common.Controls;
using Test.Components;

namespace Test
{
    // TODO: Сделать задвигающие SwipeHorizontalLayout
    public class MeterialsRequestScreen : Screen
    {
        private ArrayList _data;
        private Dictionary<string, object> _dictionaryData;
        private string _editId;
        private bool _isAdd = Convert.ToBoolean("False");
        private bool _isEdit = Convert.ToBoolean("False");
        private bool _isEmptyList;
        private TopInfoComponent _topInfoComponent;

        public override void OnLoading()
        {
            _dictionaryData = new Dictionary<string, object>();
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
            BusinessProcess.DoBack();
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

        /// <summary>
        ///     Проверяет данные, которые получили в поле
        ///     _data.
        /// </summary>
        /// <returns>true - если БД вернула 0 записей, иначе false</returns>
        internal bool GetIsEmptyList()
        {
            //TODO: проверка данных на их наличие, true если БД возращает 0 записей. Отредактировать если _data типа RecordSet
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
            var vl = (VerticalLayout) sender;
            var hl = (IHorizontalLayout3) vl.Parent;
            var shl = (ISwipeHorizontalLayout3) hl.Parent;
            ++shl.Index;
        }

        internal void DeleteButton_OnClick(object sender, EventArgs e)
        {
            var btn = (Button) sender;
            var shl = (ISwipeHorizontalLayout3) btn.Parent;
            shl.CssClass = "NoHeight";
            ((IVerticalLayout3) shl.Parent).Refresh();
        }

        private void FillData()
        {
            _data = new ArrayList();
            //TODO: Заменить цикл на запрос из БД
            for (var i = 0; i < 0; i++)
            {
                var dic = new Dictionary<string, object>();
                dic["first"] = "Test " + i;
                dic["second"] = "Test " + i;
                dic["Id"] = Guid.NewGuid().ToString();
                _data.Add(dic);
            }
        }

        internal ArrayList GetData()
        {
            //Данные, которые были получены у БД передаются в XML разметку.
            return _data;
        }

        internal void AddMaterial_OnClick(object sender, EventArgs e)
        {
            //TODO: Отсюда переходим на экран добавления материалов.
            var dictionary = new Dictionary<string, object>
            {
                {"isService", false},
                {"isMaterialsRequest", true},
                {"returnKey", "newItem"}
            };
            BusinessProcess.GlobalVariables["isService"] = false;
            BusinessProcess.GlobalVariables["isMaterialsRequest"] = true;
            _isAdd = Convert.ToBoolean("True");
            BusinessProcess.DoAction("AddServicesOrMaterials", dictionary, false);
        }

        internal void SendData_OnClick(object sender, EventArgs e)
        {
            //TODO: сохранения данных в БД.
            DConsole.WriteLine("Data is saved");
        }

        internal void OnSwipe_Swipe(object sender, EventArgs e)
        {
        }

        internal void EditNode_OnClick(object sender, EventArgs e)
        {
            var vl = (VerticalLayout) sender;

            var dictionary = new Dictionary<string, object>
            {
                {"isService", false},
                {"isMaterialsRequest", true},
                {"returnKey", vl.Id}
            };
            BusinessProcess.GlobalVariables["isService"] = false;
            BusinessProcess.GlobalVariables["isMaterialsRequest"] = true;
            _isEdit = Convert.ToBoolean("True");
            _editId = vl.Id;
        }
    }
}