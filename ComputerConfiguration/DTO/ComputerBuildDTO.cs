using ComputerConfiguration.Commands;
using ComputerConfiguration.Models.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerConfiguration.DTO
{
    public class ComputerBuildDTO : NotifyPropertyChanged
    {
        private Cpu _cpu;
        public Cpu Cpu
        {
            get => _cpu; set
            {
                _cpu = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsSelectedCpu));
                OnPropertyChanged(nameof(IsSelectedAnything));

            }
        }
        private Gpu _gpu;
        public Gpu Gpu
        {
            get => _gpu; set
            {
                _gpu = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsSelectedGpu));
                OnPropertyChanged(nameof(IsSelectedAnything));

            }
        }
        private Motherboard _motherboard;
        public Motherboard Motherboard
        {
            get => _motherboard; set
            {
                _motherboard = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsSelectedMotherboard));
                OnPropertyChanged(nameof(IsSelectedAnything));

            }
        }
        private List<Ram> _rams = new();
        public List<Ram> Rams
        {
            get => _rams; set
            {
                _rams = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsSelectedRam));
                OnPropertyChanged(nameof(IsSelectedAnything));

            }
        }
        public bool IsSelectedAnything =>  IsSelectedCpu || IsSelectedGpu || IsSelectedMotherboard || IsSelectedRam;
        public bool IsSelectedCpu => _cpu != null;
        public bool IsSelectedGpu => _gpu != null;
        public bool IsSelectedMotherboard => _motherboard != null;
        public bool IsSelectedRam => _rams.Count != 0;
    }
}
