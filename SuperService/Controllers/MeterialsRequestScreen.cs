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
        private static ArrayList _data;
        private static bool _isAdd = Convert.ToBoolean("False");
        private static bool _isEdit = Convert.ToBoolean("False");
        private bool _isEmptyList;
        private TopInfoComponent _topInfoComponent;

        public override void OnLoading()
        {
            _topInfoComponent = new TopInfoComponent(this)
            {
                ExtraLayoutVisible = false,
                HeadingTextView = {Text = Translator.Translate("request")},
                RightButtonImage = {Visible = false},
                LeftButtonImage = {Source = ResourceManager.GetImage("close")}
            };
        }

        public override void OnShow()
        {
            DConsole.WriteLine($"{nameof(_isAdd)} = {_isAdd} {nameof(_isEdit)} {_isEdit}");
            if (_isAdd)
            {
                var newItem = BusinessProcess.GlobalVariables.GetValueOrDefault("newItem");

                if (newItem != null)
                {
                    var item = (EditServiceOrMaterialsScreenResult) newItem;

                    if (!CombineIfExist(item))
                    {
                        var dictionary = new Dictionary<string, object>
                        {
                            //SKU aka rimId
                            {"SKU", item.RimId},
                            {"Count", item.Count},
                            {"Unit", "unit"},
                            {"Description", "Description"}
                        };
                        _data.Add(dictionary);
                        DConsole.WriteLine($"Element is added _data.Count = {_data.Count}");
                    }
                }
                BusinessProcess.GlobalVariables.Remove("newItem");
                _isAdd = Convert.ToBoolean("False");
            }
            else if (_isEdit)
            {
                var editItem = BusinessProcess.GlobalVariables.GetValueOrDefault("editItem");

                if (editItem != null)
                {
                    var item = (EditServiceOrMaterialsScreenResult) editItem;

                    foreach (var element in _data)
                    {
                        var dictionary = (Dictionary<string, object>) element;
                        var elementRimId = (string) dictionary["SKU"];

                        if (string.Compare(elementRimId, item.RimId, false) == 0)
                        {
                            dictionary["Count"] = item.Count;
                            break;
                        }
                    }
                    DConsole.WriteLine("element is changed!");
                }
                BusinessProcess.GlobalVariables.Remove("editItem");
                _isEdit = Convert.ToBoolean("False");
            }

            DConsole.WriteLine($"{Environment.NewLine}{Environment.NewLine}");
            printMaterialsData();
        }

        private void printMaterialsData()
        {
            foreach (var item in _data)
            {
                var element = (Dictionary<string, object>) item;

                foreach (var i in element)
                {
                    DConsole.WriteLine($"{i.Key} = {i.Value}");
                }

                DConsole.WriteLine($"{Environment.NewLine}{Environment.NewLine}");
            }
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

        /// <summary>
        ///     Метод осуществляет проверку, существует ли
        ///     элемент в коллекции _data. Если существует
        ///     тогда объединяет элементы с одинаковым rimId
        ///     и суммирует их колличество.
        /// </summary>
        /// <param name="item"> Элемент, который нужно проверить на существование в коллекции</param>
        /// <returns> true, если элемент существует в коллекции и объединился. false если не существует </returns>
        private bool CombineIfExist(EditServiceOrMaterialsScreenResult item)
        {
            foreach (var element in _data)
            {
                var dictionary = (Dictionary<string, object>) element;
                var elementRimId = (string) dictionary["SKU"];


                if (string.Compare(elementRimId, item.RimId, false) == 0)
                {
                    DConsole.WriteLine($"Count before {(int) dictionary["Count"]} SKU = {(string) dictionary["SKU"]}");
                    dictionary["Count"] = (int) dictionary["Count"] + item.Count;
                    DConsole.WriteLine($"Element is Exist and changed count = {(int) dictionary["Count"]}");
                    return true;
                }
            }
            return false;
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
            if (_data == null)
            {
                _data = new ArrayList();
            }

            DConsole.WriteLine($"{nameof(_data)} {nameof(_data.Count)} = {_data.Count}");
            if (_data.Count > 0)
            {
                _isEmptyList = Convert.ToBoolean("False");
            }
            else
            {
                _isEmptyList = Convert.ToBoolean("True");
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
                {"returnKey", "editItem"},
                {"rimId", vl.Id},
                {"priceVisible", Convert.ToBoolean("False")},
                {"behaviour", BehaviourEditServicesOrMaterialsScreen.ReturnValue},
                {"lineId", null}
            };
            BusinessProcess.GlobalVariables["isService"] = false;
            BusinessProcess.GlobalVariables["isMaterialsRequest"] = true;
            _isEdit = Convert.ToBoolean("True");
            BusinessProcess.DoAction("EditServicesOrMaterials", dictionary);
        }

        internal string Concat(string first, string secont)
        {
            return $"{first} {secont}";
        }
    }
}