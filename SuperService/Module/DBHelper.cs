using System.Collections;
using System.IO;
using BitMobile.ClientModel3;
using Test.Model.Catalog;


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

        private const string EventStatusDoneName = "Done";
        private const string EventStatusCancelName = "Cancel";

        private static Database _db;

        public static void Init()
        {
            _db = new Database();
            if (_db.Exists) return;

            DConsole.WriteLine("Creating DB");
            _db.CreateFromModel();
            DConsole.WriteLine("Filling DB with demo data");

            var sql = Application.GetResourceStream("Model.main.sql");
            var reader = new StreamReader(sql);
            var queryText = reader.ReadToEnd();
            DConsole.WriteLine(queryText.Substring(0, 15));
            var query = new Query(queryText);
            query.Execute();
            _db.Commit();
        }
    }
}