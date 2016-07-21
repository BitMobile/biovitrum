using BitMobile.ClientModel3;
using BitMobile.DbEngine;
using System;
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

        public static string LastError => _db.LastError;
        public static DateTime LastSyncTime => _db.LastSyncTime;
        public static bool SuccessSync => _db.SuccessSync;

        public static void Init()
        {
            _db = new Database();
            if (_db.Exists) return;

            DConsole.WriteLine("Creating DB");
            _db.CreateFromModel();
            //DConsole.WriteLine("Filling DB with demo data");

            //string queryText;
            //using (var sql = Application.GetResourceStream("Model.main.sql"))
            //using (var reader = new StreamReader(sql))
            //{
            //    queryText = reader.ReadToEnd();
            //}
            //var query = new Query(queryText);
            //query.Execute();
            //_db.Commit();
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

        public static void DeleteByRef(DbRef @ref)
        {
            _db.Delete(@ref);
            _db.Commit();
        }

        public static object LoadEntity(string id)
        {
            return DbRef.FromString(id).GetObject();
        }

        public static void FullSyncAsync(ResultEventHandler<bool> resultEventHandler = null)
        {
            try
            {
                _db.PerformFullSyncAsync(Settings.Server, Settings.User, Settings.Password,
                    SyncHandler + resultEventHandler,
                    "Full");
            }
            catch (Exception)
            {
                SyncHandler("Full", new ResultEventArgs<bool>(false));
            }
        }

        public static void Sync(ResultEventHandler<bool> resultEventHandler = null)
        {
            try
            {
                _db.PerformSyncAsync(Settings.Server, Settings.User, Settings.Password,
                    SyncHandler + resultEventHandler,
                    "Partial");
            }
            catch (Exception)
            {
                SyncHandler("Partial", new ResultEventArgs<bool>(false));
            }
        }

        private static void SyncHandler(object state, ResultEventArgs<bool> resultEventArgs)
        {
            if (state.Equals("Full"))
                Toast.MakeToast(Translator.Translate(resultEventArgs.Result ? "sync_success" : "sync_fail"));
        }

        public static void FullSync(ResultEventHandler<bool> resultEventHandler = null)
        {
            try
            {
                _db.PerformFullSync(Settings.Server, Settings.User, Settings.Password,
                    SyncHandler + resultEventHandler,
                    "Full");
            }
            catch (Exception)
            {
                SyncHandler("Full", new ResultEventArgs<bool>(false));
            }
        }
    }
}