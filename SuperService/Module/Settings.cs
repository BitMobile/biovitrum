using System;
using System.Collections.Generic;
using BitMobile.Application;
using BitMobile.ClientModel3;

namespace Test
{
    public static class Settings
    {
        private static Dictionary<string, object> _settings;
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

        public static string AuthUrl { get; set; }

        public static bool AllowGallery { get; private set; }
        public static int PictureSize { get; private set; }
        public static bool EquipmentEnabled { get; private set; }
        public static bool CheckListEnabled { get; private set; }
        public static bool COCEnabled { get; private set; }
        public static bool BagEnabled { get; private set; }
        public static bool ShowServicePrice { get; private set; }
        public static bool ShowMaterialPrice { get; private set; }

        private static bool _initialized = false;

        public static void Init()
        {
            if (!_initialized)
                ApplicationContext.Current.Settings.ReadSettings();
            var settings = DBHelper.GetSettings();

            if (_settings == null)
            {
                _settings = new Dictionary<string, object>();
            }
            else
                _settings.Clear();

           
            while (settings.Next())
            {
                var dictionary = new Dictionary<string, object>(2)
                {
                    {"LogicValue", settings["LogicValue"]},
                    {"NumericValue", settings["NumericValue"]}
                };

                _settings[(string)settings["Description"]] = dictionary;
            }

#if DEBUG
            DConsole.WriteLine("-------------------------------------------");
            foreach (var item in _settings)
            {
                var element = (Dictionary<string, object>)item.Value;
                DConsole.WriteLine($"Description: {item.Key} LogicValue: {(bool)element["LogicValue"]}" +
                                   $" NumericValue: {(int)element["NumericValue"]}");
            }
            DConsole.WriteLine($"-------------------------------------------{Environment.NewLine}");
#endif

            //AllowGallery = 
            //PictureSize = (int)settings["PictureSize"];
            //EquipmentEnabled = (bool)settings["UsedEquipment"];
            //CheckListEnabled = (bool)settings["UsedCheckLists"];
            //COCEnabled = (bool)settings["UsedCalculate"];
            //BagEnabled = (bool)settings["UsedServiceBag"];
            //ShowServicePrice = (bool)settings["UsedCalculateService"];
            //ShowMaterialPrice = (bool)settings["UsedCalculateMaterials"];

            Server = @"http://nt0420.bt/bitmobile/testsolution/device";
            Host = @"http://nt0420.bt";
            AuthUrl = Server + @"/GetUserId";
            _initialized = true;
        }
    }
}