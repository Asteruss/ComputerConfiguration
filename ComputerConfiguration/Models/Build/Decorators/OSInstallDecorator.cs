using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerConfiguration.Models.Build.Decorators
{
    public class OSInstallDecorator : BuilderDecorator
    {
        private readonly AdditionalService _service;
        private readonly AdditionalServiceOption _option;

        public OSInstallDecorator(IBuild build, AdditionalService service, AdditionalServiceOption option) : base(build)
        {
            _service = service;
            _option = option;
        }

        public override double GetTotalPrice()
        {
            return base.GetTotalPrice() + _option.AdditionalPrice;
        }

        public override string GetDescription()
        {
            string desc = _service.Name;
            if (_option != null)
                desc += $" ({_option.Option})";
            return base.GetDescription() + $"{desc}\n";
        }
    }
}
