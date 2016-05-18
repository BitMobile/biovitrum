using System;
using BitMobile.DbEngine;
using Test.Enum;

namespace Test.Document
{
    public class Event : DbEntity
    {
        public DateTime ActualStartDate { get; set; }
        public DbRef Id { get; set; }
        public DbRef Client { get; set; }
        public DateTime StartDatePlan { get; set; }
        public string Comment { get; set; }
    }
}