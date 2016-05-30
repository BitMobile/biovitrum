using System;
using System.Collections;
using BitMobile.ClientModel3;

//using Database = BitMobile.ClientModel3.Database;

namespace Test
{
    /// <summary>
    ///     Обеспечивает работу с базой данных приложения
    /// </summary>
    /// <remarks>
    /// </remarks>
    public static partial class DBHelper
    {
        private const string DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

        private static Database _db;

        public static void Init()
        {
            _db = new Database();
            if (!_db.Exists)
            {
                DConsole.WriteLine("creating db");
                _db.CreateFromModel();
                DConsole.WriteLine("db has been created");
            }
        }

    }
}
