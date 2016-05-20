using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Enum;
using BitMobile.ClientModel3;

namespace Test
{
    class EventListElement
    {
        public string Id { get; set; }
        public DateTime StartDatePlan { get; set; }
        public DateTime StartDatePlanDate { get; set; } //date only - begining of the day
        public string TypeDeparture { get; set; }
        public DateTime ActualStartDate { get; set; }
        public string Importance { get; set; }
        public string ClientDescription { get; set; }
        public string ClientAddress { get; set; }


        public static EventListElement CreateFromRecordSet(DbRecordset recordSet)
        {
            var eventElement = new EventListElement();
            eventElement.Id = recordSet.GetString(1);
            eventElement.StartDatePlan = recordSet.GetDateTime(2);
            eventElement.StartDatePlanDate = recordSet.GetDateTime(3);
            eventElement.TypeDeparture = recordSet.GetString(4);
            eventElement.ActualStartDate = recordSet.GetDateTime(5);
            eventElement.Importance = recordSet.GetString(6);
            eventElement.ClientDescription = recordSet.GetString(7);
            eventElement.ClientAddress = recordSet.GetString(8);

            return eventElement;
        }
    }

 
}