using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.Model
{
    public class AndProductionRule : IProductionRule
    {
        public double Execute(List<Node> children)
        {
            return children.Where(c => c.Selected).Count() > 0 ?
                children.Where(c => c.Selected)?.
                    Min(c => c.DefinitionCoef) ?? 0 : 0;
        }
    }

    public class OrProductionRule : IProductionRule
    {
        public double Execute(List<Node> children)
        {
            return children.Where(c => c.Selected).Count() > 0 ? 
                children.Where(c => c.Selected)?.
                    Max(c => c.DefinitionCoef) ?? 0 : 0;
        }
    }

    public class DivideProductionRule : IProductionRule
    {
        public double Execute(List<Node> children)
        {
            var c = children.Where(ch => ch.Selected);
            if (c.Count() < 2)
                return 0;
            double result = c.First().DefinitionCoef;
            for (int i = 1; i < c.Count(); i++)
            {
                var currentCoef = c.First().DefinitionCoef;
                result = result + currentCoef - result * currentCoef / 100.0;
            }
            return result;
        }
    }
}
