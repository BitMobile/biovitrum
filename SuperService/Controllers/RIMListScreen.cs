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
        private string _currentEventID;

        private bool _fieldsAreInitialized;
        private bool _isMaterialRequest;
        private bool _isService;

        private TopInfoComponent _topInfoComponent;

        public override void OnLoading()
        {
            InitClassFields();

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


        public int InitClassFields()
        {
            if (_fieldsAreInitialized)
            {
                return 0;
            }

            _isMaterialRequest = (bool) Variables.GetValueOrDefault("isMaterialsRequest", Convert.ToBoolean("False"));
            _isService = (bool) Variables.GetValueOrDefault("isService", Convert.ToBoolean("False"));
            _currentEventID = (string) Variables.GetValueOrDefault("currentEventId", string.Empty);
            _fieldsAreInitialized = true;

            return 0;
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
            if (_isMaterialRequest)
            {
                //пришли из экрана заявки на материалы
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
                Navigation.ModalMove("EditServicesOrMaterialsScreen", dictionary);
            }
            else
            {
                DConsole.WriteLine("Пытаемся найти номенклатуру в документе " + _currentEventID + " по гуиду " + rimID);
                var line = DBHelper.GetEventServicesMaterialsLineByRIMID(_currentEventID, rimID);

                if (line == null)
                {
                    DConsole.WriteLine("Позиция не найдена, просто добавлеям новую");

                    var dictionary = new Dictionary<string, object>
                    {
                        {"behaviour", BehaviourEditServicesOrMaterialsScreen.InsertIntoDB},
                        {"rimId", rimID}
                    };

                    Navigation.ModalMove("EditServicesOrMaterialsScreen", dictionary);
                }
                else
                {
                    DConsole.WriteLine("Позиция найдена, открываем окно редактирования количества ");
                    var dictionary = new Dictionary<string, object>
                    {
                        {"behaviour", BehaviourEditServicesOrMaterialsScreen.UpdateDB},
                        {"lineId", line.ID}
                    };

                    Navigation.ModalMove("EditServicesOrMaterialsScreen", dictionary);
                }
            }
        }


        internal IEnumerable GetRIM()
        {
            DConsole.WriteLine("получение позиций товаров и услуг");


            DbRecordset result;

            if (_isService)
            {
                result = DBHelper.GetRIMByType(RIMType.Service);
                DConsole.WriteLine("Получили услуги " + RIMType.Material);
            }
            else
            {
                var isBag = DBHelper.GetIsBag();

                if (!isBag)
                {
                    result = DBHelper.GetRIMByType(RIMType.Material);
                    DConsole.WriteLine("Получили товары " + RIMType.Material);
                }
                else
                {
                    result = DBHelper.GetRIMFromBag();
                    DConsole.WriteLine($"Получаем материалы из рюкзака ");
                }
            }

            return result;
        }
    }
}