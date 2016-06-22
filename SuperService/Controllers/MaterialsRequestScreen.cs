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
    public class MaterialsRequestScreen : Screen
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
                var newItem = Variables.GetValueOrDefault("newItem");

                if (newItem != null)
                {
                    var item = (EditServiceOrMaterialsScreenResult) newItem;

                    if (!CombineIfExist(item))
                    {
                        var itemInfo = DBHelper.GetServiceMaterialPriceByRIMID(item.RimId);
                        var dictionary = new Dictionary<string, object>
                        {
                            //SKU aka rimId
                            {"SKU", item.RimId},
                            {"Count", item.Count},
                            {"Unit", (string) itemInfo["Unit"]},
                            {"Description", (string) itemInfo["Description"]}
                        };
                        _data.Add(dictionary);
                        DConsole.WriteLine($"Element is added _data.Count = {_data.Count}");
                    }
                }
                Variables.Remove("newItem");
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
            PrintMaterialsData();
        }

        private void PrintMaterialsData()
        {
            DConsole.WriteLine($"{nameof(_data)}.{nameof(_data.Count)} = {_data.Count}");

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
            Navigation.Back();
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
            DConsole.WriteLine("GetIsEmptyList()");
            if (_data == null)
            {
                _isAdd = _isEdit = Convert.ToBoolean("False");
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
            DConsole.WriteLine($"{nameof(btn.Id)} = {btn.Id}");
            var shl = (ISwipeHorizontalLayout3) btn.Parent;
            deleteElement(btn.Id);
            PrintMaterialsData();
            shl.CssClass = "NoHeight";
            ((IVerticalLayout3) shl.Parent).Refresh();
        }

        internal ArrayList GetData()
        {
            //Данные, которые были получены у БД передаются в XML разметку.
            return _data;
        }

        private void deleteElement(string id)
        {
            var index = -1;

            for (var i = 0; i < _data.Count; i++)
            {
                var element = (Dictionary<string, object>) _data[i];
                var skuId = (string) element["SKU"];
                if (string.Compare(skuId, id, false) == 0)
                {
                    index = i;
                    DConsole.WriteLine("Index naiden" + Environment.NewLine);
                    break;
                }
            }

            if (index >= 0)
            {
                _data.RemoveAt(index);
                DConsole.WriteLine($"Element {id} with {nameof(index)} = {index} is deleted {Environment.NewLine}");
            }
            else
            {
                DConsole.WriteLine(
                    $"Element is not deleted Error in method {nameof(deleteElement)} {Environment.NewLine}");
            }
        }

        internal void AddMaterial_OnClick(object sender, EventArgs e)
        {
            //TODO: Отсюда переходим на экран добавления материалов.
            var dictionary = new Dictionary<string, object>
            {
                {"isService", false},
                {"isMaterialsRequest", true},
                {"returnKey", "newItem"},
                {"behaviour", BehaviourEditServicesOrMaterialsScreen.ReturnValue}
            };

            _isAdd = Convert.ToBoolean("True");

            Navigation.Move("RIMListScreen", dictionary);

        }

        internal void SendData_OnClick(object sender, EventArgs e)
        {
            //TODO: сохранения данных в БД.
            DBHelper.CreateNeedMatDocument(_data);
            _data = null;
            _isAdd = _isEdit = Convert.ToBoolean("False");
            DConsole.WriteLine("Data is saved");
            Navigation.Back(true);
        }

        internal void OnSwipe_Swipe(object sender, EventArgs e)
        {
        }

        internal void EditNode_OnClick(object sender, EventArgs e)
        {
            var vl = (VerticalLayout) sender;
            //TODO: Если будет починен экран редактирования то колличесто передавать отсюда.
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
            Navigation.Move("EditServicesOrMaterialsScreen", dictionary);
        }

        internal string Concat(string first, string secont)
        {
            return $"{first} {secont}";
        }
    }
}