using BitMobile.ClientModel3;
using BitMobile.ClientModel3.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using Test.Components;

namespace Test
{
    public class RIMListScreen : Screen
    {
        private string _currentEventID;

        private bool _fieldsAreInitialized;
        private bool _isMaterialRequest;
        private bool _isNotEmptyData;
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
                LeftButtonImage = { Source = ResourceManager.GetImage("topheading_back") },
                RightButtonImage = { Visible = false },
                ExtraLayoutVisible = false
            };
        }

        public int InitClassFields()
        {
            if (_fieldsAreInitialized)
            {
                return 0;
            }

            _isMaterialRequest = (bool)Variables.GetValueOrDefault("isMaterialsRequest", Convert.ToBoolean("False"));
            _isService = (bool)Variables.GetValueOrDefault(Parameters.IdIsService, Convert.ToBoolean("False"));
            _currentEventID = (string)Variables.GetValueOrDefault(Parameters.IdCurrentEventId, string.Empty);
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
            var rimID = ((VerticalLayout)sender).Id;
            if (_isMaterialRequest)
            {
                //пришли из экрана заявки на материалы
                var key = Variables.GetValueOrDefault("returnKey", "newItem");
                var dictionary = new Dictionary<string, object>
                {
                    {"rimId", rimID},
                    {"priceVisible", Convert.ToBoolean("False")},
                    {Parameters.IdBehaviour, BehaviourEditServicesOrMaterialsScreen.ReturnValue},
                    {"returnKey", key},
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
                        {Parameters.IdBehaviour, BehaviourEditServicesOrMaterialsScreen.InsertIntoDB},
                        {"rimId"    , rimID}
                    };

                    Navigation.ModalMove("EditServicesOrMaterialsScreen", dictionary);
                }
                else
                {
                    DConsole.WriteLine("Позиция найдена, открываем окно редактирования количества ");
                    var dictionary = new Dictionary<string, object>
                    {
                        {Parameters.IdBehaviour, BehaviourEditServicesOrMaterialsScreen.UpdateDB},
                        {Parameters.IdLineId   , line.ID}
                    };

                    Navigation.ModalMove("EditServicesOrMaterialsScreen", dictionary);
                }
            }
        }

        internal IEnumerable GetRIM()
        {
            DConsole.WriteLine("получение позиций товаров и услуг");

            var result = GetDataFromDb();

            return result;
        }

        private DbRecordset GetDataFromDb()
        {
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

        internal bool GetIsNotEmpty()
        {
            var result = GetDataFromDb();

            while (result.Next())
            {
                _isNotEmptyData = Convert.ToBoolean("True");
                break;
            }

            return _isNotEmptyData;
        }
    }
}