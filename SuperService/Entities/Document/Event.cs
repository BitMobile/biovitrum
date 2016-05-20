using System;
using BitMobile.DbEngine;
using Test.Enum;
using Test.Catalog;

namespace Test.Document
{
    public class Event : DbEntity
    {
        public DateTime ActualStartDate { get; set; }
        public DbRef Id { get; set; }
        public DbRef Client { get; set; }
        public DateTime StartDatePlan { get; set; }
        public string Comment { get; set; }
        public StatusImportance Importance { get; set; }
        public TypesDepartures TypesDepartures { get; set; }
    }
}