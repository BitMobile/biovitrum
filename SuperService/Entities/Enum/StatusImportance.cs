using System;
using BitMobile.DbEngine;

namespace Test.Model.Enum
{
    public class StatusImportance : DbEntity
    {
        public DbRef Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
        public StatusImportance(DbRef Id = null)
        {
            this.Id = Id ?? DbRef.CreateInstance("Enum_StatusImportance", Guid.NewGuid());
        }
    }
}
    