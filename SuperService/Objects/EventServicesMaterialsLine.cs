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
        public double Price { get; set; }
        public double AmountPlan { get;  set; }
        public double SumPlan { get; set; }
        public double AmountFact { get; set; }
        public double SumFact { get; set; }

    }
}
