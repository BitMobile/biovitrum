using System;
using BitMobile.DbEngine;

namespace Test.Document
{
    public class Event : DbEntity
    {
        public DbRef Id { get; set; }
        public DbRef Client { get; set; }
        public DateTime StartDatePlan { get; set; }
        public string Comment { get; set; }
    }
}