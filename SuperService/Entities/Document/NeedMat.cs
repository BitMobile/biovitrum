using System;
using BitMobile.DbEngine;

namespace Test.Entities.Document
{
    public class NeedMat : DbEntity
    {
        public bool Posted { get; set; }
        public DbRef Id { get; set; }
        public bool DeletionMark { get; set; }
        public DateTime Date { get; set; }
        public string Number { get; set; }
        public DbRef DocIn { get; set; }
        public DbRef StatsNeed { get; set; }
        public DbRef SR { get; set; }
        public bool FillFull { get; set; }
        public string SRMComment { get; set; }
        public string SRComment { get; set; }
        
        public NeedMat(DbRef id = null)
        {
            Id = id ?? DbRef.CreateInstance("Document_NeedMat", Guid.NewGuid());
        }
    }
}
    