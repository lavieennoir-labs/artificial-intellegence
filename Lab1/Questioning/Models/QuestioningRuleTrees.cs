using Questioning.Models.ProductionRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Questioning.Models
{
    public class QuestioningRuleTrees
    {
        /// <summary>
        ///     Generates a rule tree with leafs diven in parameters.
        /// </summary>
        public RuleTree GetNoviceTree(Rule q1, Rule q2, Rule q3, Rule q4) {
            return new RuleTree {
                RootRule = new Rule {
                    Operation = new AvgRuleOperation {
                        Operands = new List<Rule> {
                            q1,
                            q2,
                            q3,
                            q4
                        }
                    }
                }
            };
        }
    }
}