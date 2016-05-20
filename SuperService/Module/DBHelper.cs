using System.Collections;
using BitMobile.ClientModel3;
using Test.Document;
using Test.Objects;
using Database = BitMobile.ClientModel3.Database;

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
                                  "  TypeDeparturesTable.description as TypeDeparture, " +
                                  "  event.ActualStartDate, " +
                                  "  Enum_StatusImportance.Description as Importance, " +
                                  "  client.Description, " +
                                  "  client.Address " + 
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
                                  "order by " +
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
                statistic.DayTotalAmount = result.GetInt32(1);
                statistic.DayCompleteAmout = result.GetInt32(2);
                statistic.MonthTotalAmount = result.GetInt32(3);
                statistic.MonthCompleteAmout = result.GetInt32(4);            
            }

            return statistic;
        }
           

        public static Event GetEventByID(long EventID)
        {
            var query = new Query("select * " +
                                "from " +
                                "   _Document_Event as event " +
                                "where " + 
                                "    event.id = '" + EventID + "'");
            var result = query.Execute();
            var @event = new Event();

            if (result.Next())
            {

            }

            return @event;
        }


    }
}

