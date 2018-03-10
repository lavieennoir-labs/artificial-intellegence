using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Questioning.Models.ProductionRules
{
    public class AndRuleOperation : IRuleOperation

    {
        public IList<Rule> Operands { get; set; }

        public Rule PerformOperation()
        {
            if (Operands == null || Operands.Count == 0)
                return null;

            double min = Double.MaxValue;
            foreach (var rule in Operands)
                min = Math.Min(min, rule);

            return new Rule
            {
                DefinitionCoef = min
            };
        }
    }
}