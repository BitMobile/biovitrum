using System.Security.AccessControl;

namespace Test
{
    public static class Settings
    {
        public static string User { get; set; }
        public static string Password { get; set; }
        public static string Server { get; set; }

        public static void Init()
        {
            // TODO: Прописать нормальную инициализацию
            User = "sr";
            Password = "sr";
            Server = "";
        }
    }
}