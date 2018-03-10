using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Questioning.Models.ProductionRules
{
    public class OrRuleOperation : IRuleOperation

    {
        public IList<Rule> Operands { get; set; }

        public Rule PerformOperation()
        {
            if (Operands == null || Operands.Count == 0)
                return null;

            double max = Double.MinValue;
            foreach (var rule in Operands)
                max = Math.Max(max, rule);

            return new Rule
            {
                DefinitionCoef = max
            };
        }
    }
}