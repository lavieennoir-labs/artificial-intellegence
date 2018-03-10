using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Questioning.Models.ProductionRules
{
    /// <summary>
    ///     Represents a tree structure for calculating 
    ///     definition coefitionts in production rules.
    /// </summary>
    public class RuleTree
    {
        public Rule RootRule { get; set; }

        /// <summary>
        ///     Update value of RootRule by performing operations in tree
        ///     from leafs to root.
        /// </summary>
        public void CalculateNodeCoefitients()
        {
            if (RootRule == null)
                return;
            UpdateNode(RootRule);
        }

        void UpdateNode(Rule node)
        {
            if (node.Operation == null)
                return;
            foreach (var rule in node.Operation.Operands)
                UpdateNode(rule);
            node.DefinitionCoef = node.Operation.PerformOperation();
        }
    }
}