using System;
using BitMobile.DbEngine;

namespace Test.Entities.Document
{
    public class CheckList : DbEntity
    {
        public bool Posted { get; set; }
        public DbRef Id { get; set; }
        public bool DeletionMark { get; set; }
        public DateTime Date { get; set; }
        public string Number { get; set; }
        public string Description { get; set; }
        public string Project { get; set; }
        public DbRef Status { get; set; }
        
        public CheckList(DbRef id = null)
        {
            Id = id ?? DbRef.CreateInstance("Document_CheckList", Guid.NewGuid());
        }
    }
}
    