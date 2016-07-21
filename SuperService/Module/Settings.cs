using BitMobile.Application;

namespace Test
{
    public static class Settings
    {
        //TODO: неочень хорошо так хранить пользователя и пароль.
        public static string User
        {
            get { return ApplicationContext.Current.Settings.UserName; }
            set
            {
                ApplicationContext.Current.Settings.UserName = value;
                ApplicationContext.Current.Settings.WriteSettings();
            }
        }

        public static string Password
        {
            get { return ApplicationContext.Current.Settings.Password; }
            set
            {
                ApplicationContext.Current.Settings.Password = value;
                ApplicationContext.Current.Settings.WriteSettings();
            }
        }

        // TODO: Хранить сервер
        public static string Server { get; set; }

        public static string Host { get; set; }

        public static string UserId { get; set; }

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
            ApplicationContext.Current.Settings.ReadSettings();
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