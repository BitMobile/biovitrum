using System;
using BitMobile.DbEngine;

namespace Test.Entities.Document
{
    public class Reminder : DbEntity
    {
        public bool Posted { get; set; }
        public DbRef Id { get; set; }
        public bool DeletionMark { get; set; }
        public DateTime Date { get; set; }
        public string Number { get; set; }
        public DbRef Reminders { get; set; }
        public DbRef ViewReminder { get; set; }
        public string Comment { get; set; }
        
        public Reminder(DbRef id = null)
        {
            Id = id ?? DbRef.CreateInstance("Document_Reminder", Guid.NewGuid());
        }
    }
}
    