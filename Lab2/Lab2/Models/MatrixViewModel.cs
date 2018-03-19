using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab2.Models
{
    public class MatrixViewModel
    {
        public string BaseSetName { get; set; }
        public string ParamsName { get; set; }
        public List<string> BaseParams { get; set; }
        public List<int> ComparisonMarks { get; set; }
        public List<double> ColSums { get; set; }
        public List<double> IndependentFunc { get; set; }
        //inet
    }
}