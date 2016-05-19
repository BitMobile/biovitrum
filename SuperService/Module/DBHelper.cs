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
                                  "  null as TypeDeparture, " +
                                  "  event.ActualStartDate, " +
                                  "  null as Importance, " +
                                  "  client.Description, " +
                                  "  client.Address " + 
                                  "from " + 
                                  "  _Document_Event as event " +
                                  "    left join _Catalog_Client as client " +
                                  "    on event.client = client.Id ");

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

