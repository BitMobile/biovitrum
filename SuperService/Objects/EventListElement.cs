using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Enum;
using BitMobile.ClientModel3;

namespace Test.Objects
{
    class EventListElement
    {
        public string Id { get; set; }
        public DateTime StartDatePlan { get; set; }
        public string TypeDeparture { get; set; }
        public DateTime ActualStartDate { get; set; }
        public StatusImportance Importance { get; set; }
        public string ClientDescription { get; set; }
        public string ClientAddress { get; set; }


        public static EventListElement CreateFromRecordSet(DbRecordset recordSet)
        {
            var eventElement = new EventListElement();
            eventElement.Id = recordSet.GetString(1);
            eventElement.StartDatePlan = recordSet.GetDateTime(2);
            eventElement.TypeDeparture = recordSet.GetString(3);
            eventElement.ActualStartDate = recordSet.GetDateTime(4);
            //eventElement.Importance = recordSet.GetString(5);
            eventElement.ClientDescription = recordSet.GetString(6);
            eventElement.ClientAddress = recordSet.GetString(7);

            return eventElement;
        }
    }

 
}