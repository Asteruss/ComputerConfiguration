using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerConfiguration.Models.Build.Decorators
{
    public class WarrantyDecorator : BuilderDecorator
    {
        private readonly int _years;
        private readonly double _pricePerYear;

        public WarrantyDecorator(IBuild build, int years, double pricePerYear) : base(build)
        {
            _years = years;
            _pricePerYear = pricePerYear;
        }

        public override double GetTotalPrice() => base.GetTotalPrice() + _years * _pricePerYear;
        public override string GetDescription() => base.GetDescription() + $"Гарантия {_years} год{((_years == 1) ? "" : "а")}\n";
    }
}
