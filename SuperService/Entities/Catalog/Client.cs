using System;
using BitMobile.DbEngine;

namespace Test.Entities.Catalog
{
    public class Client : DbEntity
    {
        public bool Predefined { get; set; }
        public DbRef Id { get; set; }
        public bool DeletionMark { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string Address { get; set; }
        public DbRef Contractor { get; set; }
        
        public Client(DbRef id = null)
        {
            Id = id ?? DbRef.CreateInstance("Catalog_Client", Guid.NewGuid());
        }
    }
}
    