using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerConfiguration.Models.Enums
{
    public enum CaseFormFactor
    {
        MidTower,
        FullTower,
        MicroAtx,
        MiniItx
    }

    public enum CoolerType
    {
        Air,
        Liquid
    }

    public enum StorageType
    {
        HDD,
        NVMe,
        SATA_SSD,
        M2_SSD,
      
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
    public enum OptionType
    {
        SingleOption,
        MultiOption,
        ListOption
    }
    public enum ChoiceType
    {
        SingleChoice,
        MultiChoice
    }
    public enum OperationType
    {
        Earn,
        Spent
    }
    public enum ComponentStatus
    {
        NotSelected,
        Selected,
        // есть у пользователя, однако требуется использовать для проверки
        SelectedAsFake,
        // если есть выбор нескольких
        SelectedMany,
        SelectedManyAsFake,
    }
}
