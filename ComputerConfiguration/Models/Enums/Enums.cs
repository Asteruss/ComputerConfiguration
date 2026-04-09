using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerConfiguration.Models.Enums
{
    public enum CaseFormFactor
    {
        S,
        M,
        L,
        XL
    }

    public enum CoolerType
    {
        Air,
        Liquid
    }

    public enum StorageType
    {
        SSD,
        HDD,
        NVMe,
        SATA_SSD
    }

    public enum MemoryType
    {
        DDR4,
        DDR5
    }

    public enum EfficiencyRating
    {
        Bronze,
        Silver,
        Gold,
        Platinum,
        Titanium
    }

    public enum SocketType
    {
        LGA1700,
        LGA1200,
        AM4,
        AM5,
        sTRX4
    }

    public enum ComponentCategory
    {
        CPU,
        Motherboard,
        RAM,
        GPU,
        Storage,
        PSU,
        Case,
        Cooler
    }
}
