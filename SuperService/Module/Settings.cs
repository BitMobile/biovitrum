namespace Test
{
    public static class Settings
    {
        public static string User { get; set; }
        public static string Password { get; set; }
        public static string Server { get; private set; }
        public static bool AllowGallery { get; private set; }
        public static int PictureSize { get; private set; }
        public static bool EquipmentEnabled { get; private set; }
        public static bool CheckListEnabled { get; private set; }
        public static bool COCEnabled { get; private set; }
        public static bool BagEnabled { get; private set; }
        public static bool ShowServicePrice { get; private set; }
        public static bool ShowMaterialPrice { get; private set; }

        public static void Init()
        {
            Server = "";

            var settings = DBHelper.GetSettings();
            AllowGallery = (bool)settings["AllowGalery"];
            PictureSize = (int)settings["PictureSize"];
            EquipmentEnabled = (bool)settings["UsedEquipment"];
            CheckListEnabled = (bool)settings["UsedCheckLists"];
            COCEnabled = (bool)settings["UsedCalculate"];
            BagEnabled = (bool)settings["UsedServiceBag"];
            ShowServicePrice = (bool)settings["UsedCalculateService"];
            ShowMaterialPrice = (bool)settings["UsedCalculateMaterials"];
        }
    }
}