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
        private bool _isMaterialRequest; //признак того, что запрос пришел из рюкзака монтажника
        private bool _isNotEmptyData;
        private bool _isService; //отображать услуги в противном случае материалы
        private bool _isUseServiceBag; //признак того, что используется рюкзак монтажника
        private bool _usedCalculateService;
        private bool _usedCalculateMaterials;

        private TopInfoComponent _topInfoComponent;

        public override void OnLoading()
        {
            InitClassFields();

            _topInfoComponent = new TopInfoComponent(this)
            {
                Header = _isService ? Translator.Translate("services") : Translator.Translate("materials"),
                LeftButtonControl = new Image() { Source = ResourceManager.GetImage("topheading_back") }
            };
        }

        public int InitClassFields()
        {
            if (_fieldsAreInitialized)
            {
                return 0;
            }

            _isMaterialRequest = Convert.ToBoolean(Variables.GetValueOrDefault(Parameters.IdIsMaterialsRequest, Convert.ToBoolean("False")));
            _isService = Convert.ToBoolean(Variables.GetValueOrDefault(Parameters.IdIsService, Convert.ToBoolean("False")));
            _currentEventID = (string)Variables.GetValueOrDefault(Parameters.IdCurrentEventId, string.Empty);
            _isUseServiceBag = DBHelper.GetIsUseServiceBag();
            _usedCalculateService = DBHelper.GetIsUsedCalculateService();
            _usedCalculateMaterials = DBHelper.GetIsUsedCalculateMaterials();

            DConsole.WriteLine("InitClassFields() _isMaterialRequest=" + _isMaterialRequest + " _isService=" + _isService);
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

        internal void TopInfo_RightButton_OnClick(object sender, EventArgs e)
        {
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
                    {Parameters.IdIsService, _isService},
                    {Parameters.IdIsMaterialsRequest, _isMaterialRequest }
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
                        {"rimId"    , rimID},
                        {Parameters.IdIsService, _isService},
                        {Parameters.IdIsMaterialsRequest, _isMaterialRequest }
                    };

                    Navigation.ModalMove("EditServicesOrMaterialsScreen", dictionary);
                }
                else
                {
                    DConsole.WriteLine("Позиция найдена, открываем окно редактирования количества ");
                    var dictionary = new Dictionary<string, object>
                    {
                        {Parameters.IdBehaviour, BehaviourEditServicesOrMaterialsScreen.UpdateDB},
                        {Parameters.IdLineId   , line.ID},
                        {Parameters.IdIsService, _isService},
                        {Parameters.IdIsMaterialsRequest, _isMaterialRequest }
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

            if (_isMaterialRequest)
            {
                //при запросе из рюкзака отображаем все материалы
                result = DBHelper.GetRIMByType(RIMType.Material);
            }
            else
            {
                //запрос из АВР Наряда
                if (_isService)
                {
                    //услуги всегда отображаем все
                    result = DBHelper.GetRIMByType(RIMType.Service);
                }
                else if(_isUseServiceBag)
                {
                    //Если используется рюкзак монтажника, то отображаются только те материалы, которые есть в рюкзаке
                    //TODO: заменить на метод DBHelper.GetUserBagByUserId когда будет реализована работа с пользователями и ИД. Пока выводим все номенклатуры
                    //DBHelper.GetUserBagByUserId("@ref[Catalog_User]:838443ed-a3eb-11e5-8aad-f8a963e4bf15");

                    result = DBHelper.GetRIMFromBag();
                }
                else
                {
                    //если рюкзак не используется - получаем все материалы
                    result = DBHelper.GetRIMByType(RIMType.Material);
                }
            }
            
            return result;
        }

        internal string GetPriceDescription(DbRecordset rimLine)
        {
            var result = Parameters.EmptyPriceDescription;
            if (_isMaterialRequest)
            {
                //при запросе материалов в рюкзак цену не отображаем
                result = "";
            }
            else if ((_usedCalculateService && Convert.ToBoolean(rimLine["service"])) || (_usedCalculateMaterials && !Convert.ToBoolean(rimLine["service"])))
            {
                result = rimLine["Price"].ToString();
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