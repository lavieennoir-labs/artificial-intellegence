using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.Model
{
    public interface IProductionRule
    { 
        double Execute(List<Node> children);
    }
}
