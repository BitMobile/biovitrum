using System.Collections;
using System;
using BitMobile.ClientModel3;
//using Database = BitMobile.ClientModel3.Database;


namespace Test
{
    /// <summary>
    /// Обеспечивает работу с базой данных приложения</summary>
    /// <remarks>
    /// </remarks>
    public static class DBHelper
    {

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

        /// <summary>
        /// Method returns list of all events 
        /// Возвращает список всех событий </summary>
        public static ArrayList GetEvents()
        {
            return GetEvents(new DateTime());
        }

        /// <summary>
        /// Method returns list of events which plan start date biger then param
        /// Получает список событий плановая дата начала которых больше передаваемого параметра</summary>
        /// <param name="eventSinceDate"> Дата начания с которой необходимо получить события</param>
        public static ArrayList GetEvents(DateTime eventSinceDate)
        {
            var events = new ArrayList();

            var query = new Query("select " +
                                  "  event.Id, " +
                                  "  event.StartDatePlan, " + //full date
                                  "  date(event.StartDatePlan) as startDatePlanDate, " + //date only
                                  "  ifnull(TypeDeparturesTable.description, '') as TypeDeparture, " +
                                  "  event.ActualStartDate as ActualStartDate, " +//4
                                  "  Enum_StatusImportance.Description as Importance, " +
                                  "  Enum_StatusImportance.Name as ImportanceName, " +
                                  "  ifnull(client.Description, '') as Description, " +
                                  "  ifnull(client.Address, '') as Address " +
                                  "from " +
                                  "  Document_Event as event " +
                                  "    left join Catalog_Client as client " +
                                  "    on event.client = client.Id " +
                                  "      left join " +
                                  "         (select " +
                                  "             Document_Event_TypeDepartures.Ref, " +
                                  "             Catalog_TypesDepartures.description " +
                                  "          from " +
                                  "             (select " +
                                  "                 ref, " +
                                  "                 min(lineNumber) as lineNumber " +
                                  "              from " +
                                  "                 Document_Event_TypeDepartures " +
                                  "              where " +
                                  "                 active = 1 " +
                                  "              group by " +
                                  "                 ref) as t1 " +
                                  "                       left join Document_Event_TypeDepartures " +
                                  "                             on t1.ref= Document_Event_TypeDepartures.ref " +
                                  "                                     and t1.lineNumber = Document_Event_TypeDepartures.lineNumber " +
                                  "                       left join Catalog_TypesDepartures " +
                                  "                             on Document_Event_TypeDepartures.typeDeparture = Catalog_TypesDepartures.id) as TypeDeparturesTable " +
                                  "     on event.id = TypeDeparturesTable.Ref " +
                                  "          left join Enum_StatusImportance " +
                                  "               on event.Importance = Enum_StatusImportance.Id " +
                                  "  where " +
                                  "      event.StartDatePlan > @eventDate" +
                                  " order by " +
                                  "  event.StartDatePlan");

            query.AddParameter("eventDate", eventSinceDate);
            var querryResult = query.Execute();

            while (querryResult.Next())
            {
                var @event = EventListElement.CreateFromRecordSet(querryResult);
                events.Add(@event);
            }

            return events;
        }

        /// <summary>
        /// Полуает статистику по нарядам (событиям). Возвращает объект содержащий: количество нарядов на день, количество закрытых 
        /// нарядов за день, количество нарядов с начала месяца, количество закрытых нарядов с начала месяца
        /// </summary>
        public static EventsStatistic GetEventsStatistic()
        {
            var statistic = new EventsStatistic();
            var query = new Query("select " +
                                  "  SUM(CASE " +
                                  "        when StartDatePlan > date('now','start of day') then 1 " +
                                  "        else 0 " +
                                  "   End) as DayTotalAmount, " + 
                                  "    SUM(CASE " +
                                  "        when Enum_StatusyEvents.name like 'Done' and StartDatePlan > date('now','start of day') then 1 " +
                                  "        else 0 " +
                                  "   End) as DayCompleteAmout, " +
                                  "   SUM(CASE " +
                                  "        when StartDatePlan > date('now', 'start of month') and StartDatePlan < date('now', 'start of month', '+1 month') then 1 " +
                                  "        else 0 " +
                                  "   End) as MonthCompleteAmout, " +
                                  "    SUM(CASE " +
                                  "        when Enum_StatusyEvents.name like 'Done' and StartDatePlan > date('now', 'start of month') and StartDatePlan < date('now', 'start of month', '+1 month') then 1 " +
                                  "        else 0 " +
                                  "   End) as MonthTotalAmount " +
                                  "  from " +
                                  "      Document_Event as event " +
                                  "       left join Enum_StatusyEvents " +
                                  "         on event.Status = Enum_StatusyEvents.Id");
            var result = query.Execute();

            if (result.Next())
            {
                statistic.DayTotalAmount = result.GetInt32(0);
                statistic.DayCompleteAmout = result.GetInt32(1);
                statistic.MonthTotalAmount = result.GetInt32(2);
                statistic.MonthCompleteAmout = result.GetInt32(3);
            }

            return statistic;
        }

        /// <summary>
        /// Получает полную информацию по событию</summary>
        /// <param name="eventID"> Идентификатор события</param>
        public static DbRecordset GetEventByID(string eventID)
        {
            var queryText = "select " +
                            "    event.Id,  " +
                            "    event.StartDatePlan,  " +
                            "    Date(event.StartDatePlan) as StartDatePlanDate,  " +
                            "    Time(event.StartDatePlan) as StartDatePlanTime,  " +
                            "    TypeDeparturesTable.description as TypeDeparture,  " +
                            "    event.ActualStartDate,  " +
                            "    _Enum_StatusImportance.Description as Importance,  " +
                            "    event.Comment,  " +
                            "    docSUm.sumFact,  " +
                            "    docCheckList.Total as checkListTotal,  " +
                            "    docCheckList.Answered as checkListAnswered,  " +
                            "    docEquipment.Total as equipmentTotal,  " +
                            "    docEquipment.Answered as equipmentAnswered,  " +
                            "    client.id as clientId,  " +
                            "    client.Description as clientDescription,  " +
                            "    client.Address as clientAddress  " +
                            "    " +
                            " from  " +
                            "    _Document_Event as event  " +
                            "    left join  " +
                            "    _Catalog_Client as client  " +
                            "    on  event.id = @id and event.client = client.Id  " +
                            "      " +
                            "    left join  " +
                            "   (select  " +
                            "    _Document_Event_TypeDepartures.Ref,   " +
                            "    _Catalog_TypesDepartures.description  " +
                            "   from  " +
                            "    (select ref,  " +
                            "    min(lineNumber) as lineNumber  " +
                            "    from  " +
                            "    _Document_Event_TypeDepartures  " +
                            "   where   " +
                            "    ref = @id   " +
                            "    and active = 1   " +
                            "   group by " +
                            "       ref) as t1  " +
                            "    " +
                            "    left join  " +
                            "    _Document_Event_TypeDepartures on t1.ref= _Document_Event_TypeDepartures.ref " +
                            "    and t1.lineNumber = _Document_Event_TypeDepartures.lineNumber  " +
                            "    left join  " +
                            "    _Catalog_TypesDepartures on _Document_Event_TypeDepartures.typeDeparture =  _Catalog_TypesDepartures.id) as TypeDeparturesTable  " +
                            "    on event.id = TypeDeparturesTable.Ref  " +
                            "    " +
                            "   left join _Enum_StatusImportance  " +
                            "           on event.Importance = _Enum_StatusImportance.Id  " +
                            "    " +
                            "   left join (select Document_Event_ServicesMaterials.Ref, sum(SumFact) as sumFact from Document_Event_ServicesMaterials where Document_Event_ServicesMaterials.Ref = @id group by Document_Event_ServicesMaterials.Ref ) as docSum  " +
                            "   on event.id = docSUm.ref " +
                            "    " +
                            "   left join (select Document_Event_CheckList.Ref, count(Document_Event_CheckList.Ref) as Total, sum(case when result is null or result = '' then 0 else 1 end) as Answered from Document_Event_CheckList where Document_Event_CheckList.Ref = @id group by Document_Event_CheckList.Ref ) as docCheckList " +
                            "   on event.id = docCheckList.ref " +
                            "    " +
                            "    left join (select Document_Event_Equipments.Ref, count(Document_Event_Equipments.Ref) as Total, sum(case when result is null or result = '' then 0 else 1 end) as Answered from Document_Event_Equipments where Document_Event_Equipments.Ref = @id group by Document_Event_Equipments.Ref ) as docEquipment " +
                            "   on event.id = docEquipment.ref " +
                            "    " +
                            "   where  " +
                            "   event.id = @id  ";

            var query = new Query(queryText);
            query.AddParameter("id", eventID);
            var result = query.Execute();

            return result;
        }

        /// <summary>
        /// Получает список задач события</summary>
        /// <param name="eventID"> Идентификатор события</param>
        public static DbRecordset GetTasksByEventID(string eventID)
        {
            var query = new Query("select " +
                                  "    tasks.Id,  " + //ид задачи
                                  "    tasks.Ref, " + //ид документа События
                                  "    tasks.Terget, " + //цель
                                  "    equipment.Description as equipmentDescription, " +
                                  "    ResultEvent.Description as ResultEventDescription, " +
                                  "    ResultEvent.Name as ResultEventName, " +
                                  "    case  " +
                                  "        when ResultEvent.Name like 'Done' then 1 " +
                                  "        else 0 " +
                                  "    end as isDone " +
                                  "  from " +
                                  "     Document_Event_Equipments as tasks " +
                                  "      left join Catalog_Equipment as equipment " +
                                  "       on tasks.Equipment = equipment.Id " +
                                  " " +
                                  "      left join _Enum_ResultEvent as ResultEvent " +
                                  "       on tasks.Result = ResultEvent.Id " +
                                  "       " +
                                  " where " +
                                  "   tasks.Ref = @id  ");
            query.AddParameter("id", eventID);
            var result = query.Execute();

            return result;
        }

        /// <summary>
        /// Устанавливает фактическое время начала события</summary>
        /// <param name="dateTime"> Дата время начала события</param>
        /// <param name="eventId"> Дата время начала наряда (события)</param>
        public static void UpdateActualStartDateByEventId(DateTime dateTime, string eventId)
        {
            var query = new Query("update _Document_Event " +
                                  "    set ActualStartDate = @dateTime, " +
                                  "        Status = (select Enum_StatusyEvents.id from Enum_StatusyEvents where Enum_StatusyEvents.name like 'InWork')" +
                                  "    where Id=@id");
            DConsole.WriteLine($"{dateTime}");
            query.AddParameter("dateTime", dateTime.ToString("yyyy-MM-dd HH:mm:ss"));
            query.AddParameter("id", eventId);
            query.Execute();
            _db.Commit();
        }

        /// <summary>
        /// Устанавливает фактическое время завершения наряда (события)</summary>
        /// <param name="dateTime"> Дата время начала события</param>
        /// <param name="eventId"> Дата время начала события</param>
        public static void UpdateActualEndDateByEventId(DateTime dateTime, string eventId)
        {
            var query = new Query("update _Document_Event " +
                                  "    set ActualEndDate = @dateTime, " +
                                  "        Status = (select Enum_StatusyEvents.id from Enum_StatusyEvents where Enum_StatusyEvents.name like 'Done')" +
                                  "    where Id=@id");
            DConsole.WriteLine($"{dateTime}");
            query.AddParameter("dateTime", dateTime.ToString("yyyy-MM-dd HH:mm:ss"));
            query.AddParameter("id", eventId);
            query.Execute();
            _db.Commit();
        }

        /// <summary>
        /// Устанавливает признак отмены наряда (события) (события)</summary>
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
        /// Получает список вопросов чек-листов по идентификаторы события</summary>
        /// <param name="eventID"> Идентификатор события</param>
        public static DbRecordset GetCheckListByEventID(string eventID)
        {
            var query = new Query("select " + 
                                  "   checkList.Id as CheckListId, " +
                                  "   checkList.Ref as EventId, " +
                                  "   checkList.Required as Required, " + //признак обязательности
                                  "   checkList.Result as Result, " + //значение результата
                                  "   checkList.Action as ActionId, " +
                                  "   actions.Description as Description, " + //название пункта чек-листа
                                  "   typesDataParameters.Name as TypeName " +  //Тип значения чек-листа: ValList - выбор из списка значений; Snapshot - фото; остальное понятно из названий
                                  "from " +
                                  "   Document_Event_CheckList as checkList " +
                                  "   left join Catalog_Actions as actions " +
                                  "     ON checkList.Ref = @eventId " +
                                  "       AND checkList.Action = actions.Id " +
                                  "    " +
                                  "   left join Enum_TypesDataParameters as typesDataParameters " +
                                  "     ON checkList.ActionType = TypesDataParameters.Id " +
                                  "    " +
                                  "where " +
                                  "    checkList.Ref = @eventId");

            query.AddParameter("eventId", eventID);
            return query.Execute();
        }

        /// <summary>
        /// Получает список вариантов ответов для действий (вопросов)  с типом результата "выбор из списка"</summary>
        /// <param name="actionID"> Идентификатор действия</param>
        public static DbRecordset GetActionValuesList(string actionID)
        {
            var query = new Query("select " +
                                  "     Catalog_Actions_ValueList.Id, " + //идентификатор ответа
                                  "     Catalog_Actions_ValueList.Val " + //представление ответа
                                  "from " +
                                  "     Catalog_Actions_ValueList " +
                                  "where " +
                                  "     Catalog_Actions_ValueList.Ref = @actionID");
            query.AddParameter("actionID", actionID);
            return query.Execute();

        }

    }
}
