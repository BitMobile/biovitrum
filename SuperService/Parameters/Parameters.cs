namespace Test
{
    internal static class Parameters
    {
        //Идентификаторы передаваемых между экранами параметров
        public const string IdCurrentEventId = "currentEventId";

        public const string IdScreenStateId = "screenState";
        public const string IdClientId = "clientId";
        public const string IdIsService = "isService";
        public const string IdBehaviour = "behaviour";
        public const string IdLineId = "lineId";
        public const string IdEquipmentId = "idEquipmentId";
        public const string IdIsMaterialsRequest = "isMaterialsRequest";
        public const string IdIsReadonly = "isReadonly";
        public const string Contact = "contact";

        //Представление пустой суммы для отобажения на экранах с отключенной настройкой отображения цен
        public const string EmptyPriceDescription = " -- ";

        //Параметры для получения настроек.
        public const string AllowGallery = "AllowGalery";

        public const string PictureSize = "PictureSize";
        public const string EquipmentEnabled = "UsedEquipment";
        public const string CheckListEnabled = "UsedCheckLists";
        public const string COCEnabled = "UsedCalculate";
        public const string BagEnabled = "UsedServiceBag";
        public const string ShowServicePrice = "UsedCalculateService";
        public const string ShowMaterialPrice = "UsedCalculateMaterials";
        public const string LogicValue = "LogicValue";
        public const string NumericValue = "NumericValue";

#if DEBUG

        /// <summary>
        ///     Представляет разделитель строк для режима DEBUG
        /// </summary>
        public const string Splitter = "-----------------------------------------------";

#endif
    }
}