using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Questioning.Models.ProductionRules
{
    public interface IRuleOperation
    {
        IList<Rule> Operands { get; set; }
        Rule PerformOperation();
    }
}