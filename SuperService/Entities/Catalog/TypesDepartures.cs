using System;
using BitMobile.DbEngine;

namespace Test.Model.Catalog
{
    public class TypesDepartures : DbEntity
    {
        public bool Predefined { get; set; }
        public DbRef Id { get; set; }
        public bool DeletionMark { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        
        public TypesDepartures(DbRef Id = null)
        {
            this.Id = Id ?? DbRef.CreateInstance("Catalog_TypesDepartures", Guid.NewGuid());
        }
    }
}
    