using BitMobile.DbEngine;

namespace Test.Catalog
{
    public class Client : DbEntity
    {
        public string Description { get; set; }
        public string Address { get; set; }
    }
}