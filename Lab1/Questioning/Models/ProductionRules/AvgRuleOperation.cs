using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Questioning.Models.ProductionRules
{
    public class AvgRuleOperation : IRuleOperation
    {
        public IList<Rule> Operands { get; set; }

        public Rule PerformOperation()
        {
            if (Operands == null || Operands.Count == 0)
                return null;
            
            return new Rule
            {
                DefinitionCoef = Operands.Sum(r => r.DefinitionCoef) / Operands.Count
            };
        }
    }
}