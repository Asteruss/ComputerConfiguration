using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerConfiguration.Models.Build
{
    public interface IBuild
    {
        double GetTotalPrice();
        string GetDescription();
    }
}
