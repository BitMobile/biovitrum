using System;
using BitMobile.DbEngine;

namespace Test.Entities.Enum
{
    public class StatsNeedNum : DbEntity
    {
        public DbRef Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public StatsNeedNumEnum GetEnum() 
        {
            switch(Id.Guid.ToString())
            {
                case "aba20521-d3cb-0a3f-42c2-eee2ce12a07a": 
                    return StatsNeedNumEnum.New;
                case "bdd2411f-c0e4-2a47-4486-a87fa2ad1dfb": 
                    return StatsNeedNumEnum.Done;
                case "b1c2efd2-66a9-e59e-4211-09077ec95dc7": 
                    return StatsNeedNumEnum.Confirmed;
                case "b7b121a8-56ab-5a80-4a81-096647744ebd": 
                    return StatsNeedNumEnum.Cancel;
            }
            return default(StatsNeedNumEnum);
        }

    }

    public enum StatsNeedNumEnum
    {
        New,
        Done,
        Confirmed,
        Cancel,
    } 
}
    