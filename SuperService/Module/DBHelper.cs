using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitMobile.ClientModel3;
using Test.Document;
using Test.Objects;
using BitMobile.DbEngine;
using Database = BitMobile.ClientModel3.Database;

namespace Test.Module
{
    public static class DBHelper
    {
        private static Database _db;

        public static void init()
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
                                  "  event.StartDatePlan, " +
                                  "  TypeDeparturesTable.description as TypeDeparture, " +
                                  "  event.ActualStartDate, " +
                                  "  _Enum_StatusImportance.Description as Importance, " +
                                  "  client.Description, " +
                                  "  client.Address " + 
                                  "from " + 
                                  "  _Document_Event as event " +
                                  "    left join _Catalog_Client as client " +
                                  "    on event.client = client.Id " + 
                                  "      left join " +   
                                  "         (select " +
                                  "             _Document_Event_TypeDepartures.Ref, " + 
                                  "             _Catalog_TypesDepartures.description " +
                                  "          from " +
                                  "             (select " + 
                                  "                 ref, " +
                                  "                 min(lineNumber) as lineNumber " +
                                  "              from " +
                                  "                 _Document_Event_TypeDepartures " +
                                  "              where " + 
                                  "                 active = 1 " +
                                  "              group by " +
                                  "                 ref) as t1 " +
                                  "                       left join _Document_Event_TypeDepartures " +
                                  "                             on t1.ref= _Document_Event_TypeDepartures.ref " +
                                  "                                     and t1.lineNumber = _Document_Event_TypeDepartures.lineNumber " +
                                  "                       left join _Catalog_TypesDepartures " +
                                  "                             on _Document_Event_TypeDepartures.typeDeparture = _Catalog_TypesDepartures.id) as TypeDeparturesTable " +
                                  "     on event.id = TypeDeparturesTable.Ref " +
                                  "          left join _Enum_StatusImportance " +
                                  "               on event.Importance = _Enum_StatusImportance.Id");

            var querryResult = query.Execute();

            while (querryResult.Next())
            {
                var @event = EventListElement.CreateFromRecordSet(querryResult);
                events.Add(@event);
            }

            return events;
        }

        public static Event getEventByID(long EventID)
        {
            var query = new Query("select * from ");
            var result = query.Execute();
            var @event = new Event();

            if (result.Next())
            {

            }

            return @event;
        }


    }
}

