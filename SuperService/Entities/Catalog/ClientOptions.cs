using System;
using BitMobile.DbEngine;

namespace Test.Entities.Catalog
{
    public class ClientOptions : DbEntity
    {
        public bool Predefined { get; set; }
        public DbRef Id { get; set; }
        public bool DeletionMark { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public DbRef DataTypeParameter { get; set; }
        public bool DisplayingBMA { get; set; }
        public bool EditingBMA { get; set; }
        
        public ClientOptions(DbRef id = null)
        {
            Id = id ?? DbRef.CreateInstance("Catalog_ClientOptions", Guid.NewGuid());
        }
    }
}
    