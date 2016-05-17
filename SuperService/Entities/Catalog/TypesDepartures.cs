using BitMobile.DbEngine;

namespace Test.Catalog
{
    public class TypesDepartures : DbEntity
    {
        public DbRef Id { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
    }
}