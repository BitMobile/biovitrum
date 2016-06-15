using System;
using BitMobile.DbEngine;

namespace Test.Entities.Document
{
    public class Event : DbEntity
    {
        public bool Posted { get; set; }
        public DbRef Id { get; set; }
        public bool DeletionMark { get; set; }
        public DateTime Date { get; set; }
        public string Number { get; set; }
        public string ApplicationJustification { get; set; }
        public DbRef Client { get; set; }
        public string DivisionSource { get; set; }
        public DbRef KindEvent { get; set; }
        public bool AnySale { get; set; }
        public bool AnyProblem { get; set; }
        public DateTime StartDatePlan { get; set; }
        public DateTime EndDatePlan { get; set; }
        public DateTime ActualStartDate { get; set; }
        public DateTime ActualEndDate { get; set; }
        public DbRef Author { get; set; }
        public DbRef UserMA { get; set; }
        public string Comment { get; set; }
        public string DetailedDescription { get; set; }
        public string CommentContractor { get; set; }
        public string TargInteractions { get; set; }
        public string ResultInteractions { get; set; }
        public DbRef Status { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public DateTime GPSTime { get; set; }
        public DbRef ContactVisiting { get; set; }
        public DbRef TypesDepartures { get; set; }
        public DbRef Importance { get; set; }
        
        public Event(DbRef id = null)
        {
            Id = id ?? DbRef.CreateInstance("Document_Event", Guid.NewGuid());
        }
    }
}
    