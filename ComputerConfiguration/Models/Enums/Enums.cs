using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [Description("Процессор")]
        CPU,
        [Description("Материнская плата")]
        Motherboard,
        [Description("Оперативная память")]
        RAM,
        [Description("Видеокарта")]
        GPU,
        [Description("Накопитель")]
        Storage,
        [Description("Блок питания")]
        PSU,
        [Description("Корпус")]
        Case,
        [Description("Охлаждение")]
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
    public enum CompatibilityRuleEnum
    {
        Error,
        Warning,
        Good,
        ComponentNotFound
    }

    public enum OrderStatus
    {
        WaitForPayment,
        Accepted,
        InProgress,
        InDelivery,
        Delivered,
        Done
    }
}
