using System;
using BitMobile.DbEngine;
using Test.Enum;

namespace Test.Document
{
    public class Event : DbEntity
    {
        public DateTime ActualStartDate { get; set; }
        public StatusImportance Importance { get; set; }
        public string TypeDeparture { get; set; }
    }
}
