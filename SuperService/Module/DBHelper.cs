using System;
using System.Collections;
using BitMobile.ClientModel3;

namespace Test
{
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

        public static ArrayList GetEvents()
        {
            var events = new ArrayList();

            var query = new Query("select " +
                                  "  event.Id, " +
                                  "  event.StartDatePlan, " + //full date
                                  "  date(event.StartDatePlan) as startDatePlanDate, " + //date only
                                  "  ifnull(TypeDeparturesTable.description, '') as TypeDeparture, " +
                                  "  event.ActualStartDate as ActualStartDate, " + //4
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
                                  "  " +
                                  " order by " +
                                  "  event.StartDatePlan");

            var querryResult = query.Execute();

            while (querryResult.Next())
            {
                var @event = EventListElement.CreateFromRecordSet(querryResult);
                events.Add(@event);
            }

            return events;
        }

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
                            "    on  event.id = '__EVENT_ID_PARAMETER__' and event.client = client.Id  " +
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
                            "    ref = '__EVENT_ID_PARAMETER__'   " +
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
                            "   left join (select Document_Event_ServicesMaterials.Ref, sum(SumFact) as sumFact from Document_Event_ServicesMaterials where Document_Event_ServicesMaterials.Ref = '__EVENT_ID_PARAMETER__' group by Document_Event_ServicesMaterials.Ref ) as docSum  " +
                            "   on event.id = docSUm.ref " +
                            "    " +
                            "   left join (select Document_Event_CheckList.Ref, count(Document_Event_CheckList.Ref) as Total, sum(case when result is null or result = '' then 0 else 1 end) as Answered from Document_Event_CheckList where Document_Event_CheckList.Ref = '__EVENT_ID_PARAMETER__' group by Document_Event_CheckList.Ref ) as docCheckList " +
                            "   on event.id = docCheckList.ref " +
                            "    " +
                            "    left join (select Document_Event_Equipments.Ref, count(Document_Event_Equipments.Ref) as Total, sum(case when result is null or result = '' then 0 else 1 end) as Answered from Document_Event_Equipments where Document_Event_Equipments.Ref = '__EVENT_ID_PARAMETER__' group by Document_Event_Equipments.Ref ) as docEquipment " +
                            "   on event.id = docEquipment.ref " +
                            "    " +
                            "   where  " +
                            "   event.id = '__EVENT_ID_PARAMETER__'  ";

            var query = new Query(queryText.Replace("__EVENT_ID_PARAMETER__", eventID));
            var result = query.Execute();

            return result;
        }


        public static DbRecordset GetTasksByEventID(string eventID)
        {
            var query = new Query("select " +
                                  "    tasks.Id,  " +
                                  "    tasks.Ref, " +
                                  "    tasks.Terget, " +
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
                                  "   tasks.Ref = '" + eventID + "'  ");
            var result = query.Execute();

            return result;
        }

        public static void UpdateActualStartDateByEventId(DateTime dateTime, string eventId)
        {
            var query = new Query("update _Document_Event " +
                                  "    set ActualStartDate = @dateTime, " +
                                  "        Status = @inWork" +
                                  "    where Id=@id");
            DConsole.WriteLine($"{dateTime}");
            query.AddParameter("dateTime", dateTime.ToString("yyyy-MM-dd HH:mm:ss"));
            query.AddParameter("id", eventId);
            query.AddParameter("inWork", "@ref[Enum_StatusyEvents]:a00d846a-3d09-46c0-4a19-b6a10e055c9e");
            query.Execute();
            _db.Commit();
        }

        public static void UpdateActualEndDateByEnetId(DateTime dateTime, string eventId)
        {
            var query = new Query("update _Document_Event " +
                                  "    set ActualEndDate = @dateTime, " +
                                  "        Status = @done" +
                                  "    where Id=@id");
            DConsole.WriteLine($"{dateTime}");
            query.AddParameter("dateTime", dateTime.ToString("yyyy-MM-dd HH:mm:ss"));
            query.AddParameter("id", eventId);
            query.AddParameter("done", "@ref[Enum_StatusyEvents]:81998d6c-e971-8f4d-4fbb-bd4d3b61e737");
            query.Execute();
            _db.Commit();
        }

        public static void UpdateCancelEventById(string eventId)
        {
            var query = new Query("update _Document_Event " +
                                  "    set Status = @cancel" +
                                  "    where Id=@id");
            query.AddParameter("id", eventId);
            query.AddParameter("cancel", "@ref[Enum_StatusyEvents]:81ec69ec-e546-b95d-4879-1cb04ea0a1e6");
            query.Execute();
            _db.Commit();

        }
    }
}
