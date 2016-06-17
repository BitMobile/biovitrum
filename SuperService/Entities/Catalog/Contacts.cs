using System;
using BitMobile.DbEngine;

namespace Test.Entities.Catalog
{
    public class Contacts : DbEntity
    {
        public bool Predefined { get; set; }
        public DbRef Id { get; set; }
        public bool DeletionMark { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public string Position { get; set; }
        public string Tel { get; set; }
        public string EMail { get; set; }
        
        public Contacts(DbRef id = null)
        {
            Id = id ?? DbRef.CreateInstance("Catalog_Contacts", Guid.NewGuid());
        }
}


}
    