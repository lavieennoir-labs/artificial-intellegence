using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Questioning.Models.ProductionRules
{
    /// <summary>
    ///     Basic class for production rules.
    ///     Used to perform unclear logic.
    /// </summary>
    public class Rule
    {
        /// <summary>
        ///     Operation for calculating Definition coefitient of Rule.
        /// </summary>
        public IRuleOperation Operation { get; set; }

        double definitionCoefitient;
        /// <summary>
        ///     Coefitient of definition of the rule.
        ///     Can strore values in range [0.0; 1.0].
        /// </summary>
        public double DefinitionCoef
        {
            get => definitionCoefitient;
            set
            {
                if (value > 1.0)
                    definitionCoefitient = 1.0;
                else if (value < 0.0)
                    definitionCoefitient = 0.0;
                else
                    definitionCoefitient = value;
            }
        }

        public static implicit operator double(Rule rule)
        {
            return rule.DefinitionCoef;
        }
        public static implicit operator Rule(double definitionCoef)
        {
            return new Rule
            {
                DefinitionCoef = definitionCoef
            };
        }
    }
}