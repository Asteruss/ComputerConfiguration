using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerConfiguration.Models.Build.Decorators
{
    public class CableManagementDecorator : BuilderDecorator
    {
        private readonly string _level;
        private readonly double _price;

        public CableManagementDecorator(IBuild build, string level, double price) : base(build)
        {
            _level = level;
            _price = price;
        }

        public override double GetTotalPrice() => base.GetTotalPrice() + _price;
        public override string GetDescription() => base.GetDescription() + $"Кабель-менеджмент: {_level}\n";
    }
}
