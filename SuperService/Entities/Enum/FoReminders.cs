using System;
using BitMobile.DbEngine;

namespace Test.Entities.Enum
{
    public class FoReminders : DbEntity
    {
        public DbRef Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public FoRemindersEnum GetEnum() 
        {
            switch(Id.Guid.ToString())
            {
                case "a0e50e05-99b8-58c2-4a45-180c4462723a": 
                    return FoRemindersEnum.Sale;
                case "969c9dd5-db42-b32a-40c7-935be3a19251": 
                    return FoRemindersEnum.Problem;
            }
            return default(FoRemindersEnum);
        }

    }

    public enum FoRemindersEnum
    {
        Sale,
        Problem,
    } 
}
    