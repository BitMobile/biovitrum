using BitMobile.DbEngine;

namespace Test.Catalog
{
    public class TypesDepartures : DbEntity
    {
        public bool DeletionMark { get; set; }
        public DbRef Id { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
    }
}