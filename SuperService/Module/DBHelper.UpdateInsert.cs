using System;
using BitMobile.ClientModel3;

//using Database = BitMobile.ClientModel3.Database;

namespace Test
{
    public static partial class DBHelper
    {
        /// <summary>
        ///     Устанавливает фактическое время начала события
        /// </summary>
        /// <param name="dateTime"> Дата время начала события</param>
        /// <param name="eventId"> Дата время начала наряда (события)</param>
        public static void UpdateActualStartDateByEventId(DateTime dateTime, string eventId)
        {
            var query = new Query("update _Document_Event " +
                                  "    set ActualStartDate = @dateTime, " +
                                  "        Status = (select Enum_StatusyEvents.id from Enum_StatusyEvents where Enum_StatusyEvents.name like 'InWork')" +
                                  "    where Id=@id");
            DConsole.WriteLine($"{dateTime}");
            query.AddParameter("dateTime", dateTime.ToString(DateTimeFormat));
            query.AddParameter("id", eventId);
            query.Execute();
            _db.Commit();
        }


        /// <summary>
        ///     Устанавливает фактическое время завершения наряда (события)
        /// </summary>
        /// <param name="dateTime"> Дата время начала события</param>
        /// <param name="eventId"> Дата время начала события</param>
        public static void UpdateActualEndDateByEventId(DateTime dateTime, string eventId)
        {
            var query = new Query("update _Document_Event " +
                                  "    set ActualEndDate = @dateTime, " +
                                  "        Status = (select Enum_StatusyEvents.id from Enum_StatusyEvents where Enum_StatusyEvents.name like 'Done')" +
                                  "    where Id=@id");
            DConsole.WriteLine($"{dateTime}");
            query.AddParameter("dateTime", dateTime.ToString(DateTimeFormat));
            query.AddParameter("id", eventId);
            query.Execute();
            _db.Commit();
        }

        /// <summary>
        ///     Устанавливает признак отмены наряда (события) (события)
        /// </summary>
        /// <param name="eventId"> Дата время начала события</param>
        public static void UpdateCancelEventById(string eventId)
        {
            var query = new Query("update _Document_Event " +
                                  "    set Status = (select Enum_StatusyEvents.id from Enum_StatusyEvents where Enum_StatusyEvents.name like 'Cancel')" +
                                  "    where Id=@id");
            query.AddParameter("id", eventId);
            query.Execute();
            _db.Commit();
        }

        /// <summary>
        ///     Добавляет комментарий о желании купить в таблицу
        /// </summary>
        /// <param name="eventID">
        ///     Идентификатор события (наряда)
        /// </param>
        /// <param name="message">
        ///     Комментарий
        /// </param>
        public static void InsertClosingEventSale(string eventID, string message)
        {
            var query = new Query("insert " +
                                  "    into _Document_Reminder(Id, Date, Reminders, ViewReminder, Comment, IsTombstone, IsDirty) " +
                                  "    values(@id, " +
                                  "           @date, " +
                                  "           @eventId, " +
                                  "           (select Id from _Enum_FoReminders where Name like 'Sale'), " +
                                  "           @message, " +
                                  "           0," +
                                  "           1)");
            query.AddParameter("id", $"@ref[Document_Reminder]:{Guid.NewGuid()}");
            query.AddParameter("date", DateTime.Now.ToString(DateTimeFormat));
            query.AddParameter("eventId", eventID);
            query.AddParameter("message", message);
            query.Execute();
            _db.Commit();
        }

        /// <summary>
        ///     Добавляет комментарий о проблеме в таблицу
        /// </summary>
        /// <param name="eventID">
        ///     Идентификатор события (наряда)
        /// </param>
        /// <param name="message">
        ///     Комментарий
        /// </param>
        public static void InsertClosingEventProblem(string eventID, string message)
        {
            var query = new Query("insert " +
                                  "    into _Document_Reminder(Id, Date, Reminders, ViewReminder, Comment, IsTombstone, IsDirty) " +
                                  "    values(@id, " +
                                  "           @date, " +
                                  "           @eventId, " +
                                  "           (select Id from _Enum_FoReminders where Name like 'Problem'), " +
                                  "           @message, " +
                                  "           0," +
                                  "           1)");
            query.AddParameter("id", $"@ref[Document_Reminder]:{Guid.NewGuid()}");
            query.AddParameter("date", DateTime.Now.ToString(DateTimeFormat));
            query.AddParameter("eventId", eventID);
            query.AddParameter("message", message);
            query.Execute();
            _db.Commit();
        }

        /// <summary>
        ///     Добавляет комментарий к событию (наряду)
        /// </summary>
        /// <param name="eventID">
        ///     Идентификатор события (наряда)
        /// </param>
        /// <param name="message">
        ///     Комментарий
        /// </param>
        public static void UpdateClosingEventComment(string eventID, string message)
        {
            var query = new Query("update _Document_Event " +
                                  "    set CommentContractor = @message " +
                                  "    where Id=@eventID");
            query.AddParameter("message", message);
            query.AddParameter("eventID", eventID);
            query.Execute();
            _db.Commit();
        }

        /// <summary>
        ///     Обновляет результат у задачи
        /// </summary>
        /// <param name="taskId">
        ///     Идентификатор задачи
        /// </param>
        /// <param name="result">
        ///     Название результата. Должно быть "New", "Done" или "NotDone"
        /// </param>
        public static void UpdateTaskResult(string taskId, string result)
        {
            var query = new Query("update _Document_Event_Equipments " +
                                  "    set Result = (select Id from Enum_ResultEvent" +
                                  "                      where Name like @resultName) " +
                                  "    where Id = @taskId");
            query.AddParameter("resultName", result);
            query.AddParameter("taskId", taskId);
            query.Execute();
            _db.Commit();
        }

        public static void UpdateTaskComment(string taskId, string comment)
        {
            var query = new Query("update _Document_Event_Equipments " +
                      "    set Comment = @comment " +
                      "    where Id = @taskId");
            query.AddParameter("comment", comment);
            query.AddParameter("taskId", taskId);
            query.Execute();
            _db.Commit();
        }

        /// <summary>
        /// Пишет в поле Result переменную result и взводит IsDirty
        /// </summary>
        /// <param name="checklistitemID"> ID конкретного пункта чеклиста</param>
        /// <param name="result"> Данные, которые необходмо записать в поле Result</param>
        public static void UpdateCheckListItem(string checklistitemID, string result)
        {
            DConsole.WriteLine("Update entered");
            var query = new Query("update _Document_Event_CheckList " +
                                  "   set " +
                                  "   Result = @result , " +
                                  "   IsDirty = 1 " +
                                  "   where Id=@checklistitemID");
            query.AddParameter("checklistitemID", checklistitemID);
            query.AddParameter("result", result);
            query.Execute();
            _db.Commit();
        }
    }
}