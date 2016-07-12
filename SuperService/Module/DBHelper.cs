using BitMobile.ClientModel3;
using BitMobile.DbEngine;
using System.Collections;
using System.IO;
using Database = BitMobile.ClientModel3.Database;

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

            string queryText;
            using (var sql = Application.GetResourceStream("Model.main.sql"))
            using (var reader = new StreamReader(sql))
            {
                queryText = reader.ReadToEnd();
            }
            var query = new Query(queryText);
            query.Execute();
            _db.Commit();
        }

        public static void SaveEntity(DbEntity entity)
        {
            entity.Save();
            _db.Commit();
        }

        public static void SaveEntities(IEnumerable entities)
        {
            foreach (DbEntity entity in entities)
            {
                entity.Save();
            }
            _db.Commit();
        }

        public static void Commit()
        {
            _db.Commit();
        }
    }
}