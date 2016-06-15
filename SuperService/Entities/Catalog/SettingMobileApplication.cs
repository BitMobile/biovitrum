using System;
using BitMobile.DbEngine;

namespace Test.Entities.Catalog
{
    public class SettingMobileApplication : DbEntity
    {
        public bool Predefined { get; set; }
        public DbRef Id { get; set; }
        public bool DeletionMark { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public DbRef DataType { get; set; }
        public bool LogicValue { get; set; }
        public int NumericValue { get; set; }
        
        public SettingMobileApplication(DbRef id = null)
        {
            Id = id ?? DbRef.CreateInstance("Catalog_SettingMobileApplication", Guid.NewGuid());
        }
    }
}
    