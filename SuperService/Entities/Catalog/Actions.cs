using System;
using BitMobile.DbEngine;

namespace Test.Entities.Catalog
{
    public class Actions : DbEntity
    {
        public bool Predefined { get; set; }
        public DbRef Id { get; set; }
        public bool DeletionMark { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public DbRef ActionType { get; set; }
        
        public Actions(DbRef id = null)
        {
            Id = id ?? DbRef.CreateInstance("Catalog_Actions", Guid.NewGuid());
        }
        public class ValueList : DbEntity
        {
            public DbRef Id { get; set; }
            public int LineNumber { get; set; }
            public DbRef Ref { get; set; }
            public string Val { get; set; }

            public ValueList (DbRef id = null)
            {
                Id = id ?? DbRef.CreateInstance("Catalog_Actions_ValueList", Guid.NewGuid());					
            }
        }

    }
}
    