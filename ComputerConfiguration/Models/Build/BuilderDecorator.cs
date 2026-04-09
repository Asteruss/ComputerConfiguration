using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerConfiguration.Models.Build
{
    public abstract class BuilderDecorator : IBuild
    {
        protected readonly IBuild _build;
        protected BuilderDecorator(IBuild build) => _build = build;
        public virtual double GetTotalPrice() => _build.GetTotalPrice();
        public virtual string GetDescription() => _build.GetDescription();
        
    }
}
