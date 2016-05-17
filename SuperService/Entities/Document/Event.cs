using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitMobile.DbEngine;

namespace Test.Document
{
    public class Event : DbEntity
    {
        public DateTime StartDatePlane { get; set; }
        public DateTime ActualStartDate { get; set; }
        public string Importance { get; set; }
        public string ClientDescription { get; set; }
        public string ClientAdress { get; set; }
        public string TypeDeparture { get; set; }
    }
}
