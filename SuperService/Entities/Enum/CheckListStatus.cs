using System;
using BitMobile.DbEngine;

namespace Test.Entities.Enum
{
    public class CheckListStatus : DbEntity
    {
        public DbRef Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public CheckListStatusEnum GetEnum() 
        {
            switch(Id.Guid.ToString())
            {
                case "854946f6-fc1d-bec2-4968-1f7e3c8d3c61": 
                    return CheckListStatusEnum.Blank;
                case "ba4d325a-f1b7-072d-4c3e-fd4bf9f33901": 
                    return CheckListStatusEnum.Active;
                case "ab0acbab-556c-7058-4ba9-e4cd72c2958d": 
                    return CheckListStatusEnum.Disactive;
            }
            return default(CheckListStatusEnum);
        }
    }

    public enum CheckListStatusEnum
    {
        Blank,
        Active,
        Disactive,
    } 
}
    