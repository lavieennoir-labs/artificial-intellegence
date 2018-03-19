using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab2.Models
{
    public class DefalutViewModels
    {
        
        public MatrixViewModel GetDefaultMatrix()
        {
            var vm = new MatrixViewModel
            {
                ParamsName = "Колір",
                BaseSetName = "Теплий колір",
                BaseParams = new List<string>
                {
                    "Червоний",
                    "Оранжевий",
                    "Жовтий",
                    "Зелений",
                    "Блакитний",
                    "Синій",
                    "Фіолетовий"
                },
                ComparisonMarks = new List<int>
                {
                    9, 7, 5, 3, 1, 1, 1
                },
                ColSums = new List<double>(),
                IndependentFunc = new List<double>()
            };

            for (int idx = 0; idx < vm.ComparisonMarks.Count(); idx++)
            {
                double sum = 0;
                for (int i = 0; i < vm.BaseParams.Count(); i++)
                    sum += vm.ComparisonMarks[idx] / (double)vm.ComparisonMarks[i];
                vm.ColSums.Add(sum);
            }
            var max = vm.ColSums.Max(i => 1 / i);
            for (int idx = 0; idx < vm.ComparisonMarks.Count(); idx++)
                vm.IndependentFunc.Add(1 / vm.ColSums[idx] / max);
            return vm;
        }
    }
}