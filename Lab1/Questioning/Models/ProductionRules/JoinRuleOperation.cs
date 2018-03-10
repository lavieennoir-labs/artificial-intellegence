using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Questioning.Models.ProductionRules
{
    public class JoinRuleOperation : IRuleOperation

    {
        public IList<Rule> Operands { get; set; }

        public Rule PerformOperation()
        {
            if (Operands == null || Operands.Count < 2)
                return null;

            double val = Operands[0] + Operands[1]
                    - Operands[0] * Operands[1];

            for (int i = 2; i < Operands.Count; i++)
                val = val + Operands[i]
                    - val * Operands[i];

            return new Rule
            {
                DefinitionCoef = val
            };
        }
    }
}