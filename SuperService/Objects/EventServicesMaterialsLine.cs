using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{

    /// <summary>
    ///     Является проекций строки табличной части услуги и материалы документа наряд
    /// </summary>
    /// <remarks>
    /// </remarks>
    public class EventServicesMaterialsLine
    {

        public string ID { get; set; }
        public int LineNumber { get; set; }
        public string Ref { get; set; }
        public string SKU { get; set; }
        // TODO: покурить с даблом, что бы не нарваться на проблему округления
        public decimal Price { get; set; }
        public decimal AmountPlan { get;  set; }
        public decimal SumPlan { get; set; }
        public decimal AmountFact { get; set; }
        public decimal SumFact { get; set; }

    }
}
