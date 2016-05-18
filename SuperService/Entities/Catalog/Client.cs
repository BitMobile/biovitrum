using BitMobile.DbEngine;

namespace Test.Catalog
{
    public class Client : DbEntity
    {
        public DbRef Id { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
    }
}
