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
    public class Equipment_Equiements : DbEntity
    {
        public DbRef Id { get; set; }
        public int LineNumber { get; set; }
        public DateTime Period { get; set; }
        public DbRef Clients { get; set; }
        public DbRef Ref { get; set; }
        public DbRef StatusEquiement { get; set; }
        public DbRef ContractSale { get; set; }
        public DbRef CantractService { get; set; }
        public string ContactForEquiemnt { get; set; }
        public string Info { get; set; }
        public DbRef Equiement { get; set; }

        public Equipment_Equiements (DbRef id = null)
        {
            Id = id ?? DbRef.CreateInstance("Catalog_Equipment_Equiements", Guid.NewGuid());					
        }
   }
    public class Equipment_EquiementsHistory : DbEntity
    {
        public DbRef Id { get; set; }
        public int LineNumber { get; set; }
        public DateTime Period { get; set; }
        public DbRef Client { get; set; }
        public DbRef Ref { get; set; }
        public DbRef Equiements { get; set; }
        public string Target { get; set; }
        public DbRef Result { get; set; }
        public string ObjectGet { get; set; }
        public string Comment { get; set; }
        public DbRef Executor { get; set; }

        public Equipment_EquiementsHistory (DbRef id = null)
        {
            Id = id ?? DbRef.CreateInstance("Catalog_Equipment_EquiementsHistory", Guid.NewGuid());					
        }
   }
    public class Equipment_Files : DbEntity
    {
        public DbRef Id { get; set; }
        public DbRef Ref { get; set; }
        public int LineNumber { get; set; }
        public string FullFileName { get; set; }
        public DbRef FileName { get; set; }

        public Equipment_Files (DbRef id = null)
        {
            Id = id ?? DbRef.CreateInstance("Catalog_Equipment_Files", Guid.NewGuid());					
        }
   }
    public class Equipment_Parameters : DbEntity
    {
        public DbRef Id { get; set; }
        public int LineNumber { get; set; }
        public DbRef Ref { get; set; }
        public DbRef Parameter { get; set; }
        public string Val { get; set; }

        public Equipment_Parameters (DbRef id = null)
        {
            Id = id ?? DbRef.CreateInstance("Catalog_Equipment_Parameters", Guid.NewGuid());					
        }
   }


}
    