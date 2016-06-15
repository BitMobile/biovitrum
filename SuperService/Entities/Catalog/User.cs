using System;
using BitMobile.DbEngine;

namespace Test.Entities.Catalog
{
    public class User : DbEntity
    {
        public bool Predefined { get; set; }
        public DbRef Id { get; set; }
        public bool DeletionMark { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UserDB { get; set; }
        public string EMail { get; set; }
        public DbRef UserID { get; set; }
        public string Phone { get; set; }
        public string Role { get; set; }
        
        public User(DbRef id = null)
        {
            Id = id ?? DbRef.CreateInstance("Catalog_User", Guid.NewGuid());
        }
    }
}
    