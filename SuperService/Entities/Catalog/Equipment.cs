using System;
using BitMobile.DbEngine;

namespace Test.Entities.Catalog
{
    public class Equipment : DbEntity
    {
        public bool Predefined { get; set; }
        public DbRef Id { get; set; }
        public bool DeletionMark { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public DbRef SKU { get; set; }
        
        public Equipment(DbRef id = null)
        {
            Id = id ?? DbRef.CreateInstance("Catalog_Equipment", Guid.NewGuid());
        }
    }
}
    