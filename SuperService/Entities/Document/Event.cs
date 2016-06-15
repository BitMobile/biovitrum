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
        public class Files : DbEntity
        {
            public DbRef Id { get; set; }
            public DbRef Ref { get; set; }
            public int LineNumber { get; set; }
            public string FullFileName { get; set; }
            public DbRef FileName { get; set; }

            public Files (DbRef id = null)
            {
                Id = id ?? DbRef.CreateInstance("Document_Event_Files", Guid.NewGuid());					
            }
        }
        public class Equipments : DbEntity
        {
            public DbRef Id { get; set; }
            public int LineNumber { get; set; }
            public DbRef Ref { get; set; }
            public DbRef Equipment { get; set; }
            public string Terget { get; set; }
            public DbRef Result { get; set; }
            public string Comment { get; set; }
            public DbRef SID { get; set; }

            public Equipments (DbRef id = null)
            {
                Id = id ?? DbRef.CreateInstance("Document_Event_Equipments", Guid.NewGuid());					
            }
        }
        public class Photos : DbEntity
        {
            public DbRef Id { get; set; }
            public int LineNumber { get; set; }
            public DbRef Ref { get; set; }
            public DbRef UIDPhoto { get; set; }
            public DbRef Equipment { get; set; }

            public Photos (DbRef id = null)
            {
                Id = id ?? DbRef.CreateInstance("Document_Event_Photos", Guid.NewGuid());					
            }
        }
        public class Parameters : DbEntity
        {
            public DbRef Id { get; set; }
            public int LineNumber { get; set; }
            public DbRef Ref { get; set; }
            public DbRef Parameter { get; set; }
            public string Val { get; set; }

            public Parameters (DbRef id = null)
            {
                Id = id ?? DbRef.CreateInstance("Document_Event_Parameters", Guid.NewGuid());					
            }
        }
        public class CheckList : DbEntity
        {
            public DbRef Id { get; set; }
            public int LineNumber { get; set; }
            public DbRef Ref { get; set; }
            public DbRef Action { get; set; }
            public DbRef CheckListRef { get; set; }
            public string Result { get; set; }
            public DbRef ActionType { get; set; }
            public bool Required { get; set; }

            public CheckList (DbRef id = null)
            {
                Id = id ?? DbRef.CreateInstance("Document_Event_CheckList", Guid.NewGuid());					
            }
        }
        public class TypeDepartures : DbEntity
        {
            public DbRef Id { get; set; }
            public int LineNumber { get; set; }
            public DbRef Ref { get; set; }
            public DbRef TypeDeparture { get; set; }
            public bool Active { get; set; }

            public TypeDepartures (DbRef id = null)
            {
                Id = id ?? DbRef.CreateInstance("Document_Event_TypeDepartures", Guid.NewGuid());					
            }
        }
        public class ServicesMaterials : DbEntity
        {
            public DbRef Id { get; set; }
            public int LineNumber { get; set; }
            public DbRef Ref { get; set; }
            public DbRef SKU { get; set; }
            public decimal Price { get; set; }
            public decimal AmountPlan { get; set; }
            public decimal SumPlan { get; set; }
            public decimal AmountFact { get; set; }
            public decimal SumFact { get; set; }

            public ServicesMaterials (DbRef id = null)
            {
                Id = id ?? DbRef.CreateInstance("Document_Event_ServicesMaterials", Guid.NewGuid());					
            }
        }

    }
}
    