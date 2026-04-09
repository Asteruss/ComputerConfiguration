using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerConfiguration.Models.Build.Decorators
{
    public class BulkDiscountDecorator : BuilderDecorator
    {
        private readonly double _threshold;
        private readonly double _discountPercent;

        public BulkDiscountDecorator(IBuild build, double threshold, double discountPercent) : base(build)
        {
            _threshold = threshold;
            _discountPercent = discountPercent;
        }

        public override double GetTotalPrice()
        {
            double basePrice = base.GetTotalPrice();
            return basePrice >= _threshold
                ? basePrice * (1 - _discountPercent / 100)
                : basePrice;
        }

        public override string GetDescription()
        {
            double basePrice = base.GetTotalPrice();
            if (basePrice >= _threshold)
                return base.GetDescription() + $"Скидка {_discountPercent}% при заказе от {_threshold}₽\n";
            return base.GetDescription();
        }
    }
}
