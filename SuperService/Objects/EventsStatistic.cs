using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class EventsStatistic
    {
        /// <summary>
        /// Общее количество нарядов за день</summary>
        public int DayTotalAmount { get; set; }
        /// <summary>
        /// Количество выполненных нарядов за день</summary>
        public int DayCompleteAmout { get; set; }
        /// <summary>
        /// Общее количество нарядов за месяц</summary>
        public int MonthCompleteAmout { get; set; }
        /// <summary>
        /// Количество выполненных нарядов за месяц</summary>
        public int MonthTotalAmount { get; set; }

    }
}
