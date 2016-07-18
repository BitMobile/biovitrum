using System.Security.AccessControl;

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
        }
    }
}