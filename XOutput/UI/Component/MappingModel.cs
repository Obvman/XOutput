using System.Windows;
using XOutput.Devices;
using XOutput.Devices.Mapper;
using XOutput.Devices.XInput;

namespace XOutput.UI
{
    public class MappingModel : ModelBase
    {
        private XInputTypes _xInputType;
        private InputSource _selectedInput;
        private Visibility _configVisibility;
        private MapperData _mapperData;

        public XInputTypes XInputType { get => _xInputType; set => SetProperty(ref _xInputType, value); }

        public InputSource SelectedInput { get => _selectedInput; set => SetProperty(ref _selectedInput, value); }

        public decimal? Min
        {
            get => (decimal)_mapperData.MinValue * 100;
            set
            {
                if ((decimal)_mapperData.MinValue != value)
                {
                    _mapperData.MinValue = (double)(value ?? 0) / 100;
                    OnPropertyChanged(nameof(Min));
                }
            }
        }

        public decimal? Max
        {
            get => (decimal)_mapperData.MaxValue * 100;
            set
            {
                if ((decimal)_mapperData.MaxValue != value)
                {
                    _mapperData.MaxValue = (double)(value ?? 100) / 100;
                    OnPropertyChanged(nameof(Max));
                }
            }
        }

        public decimal? Deadzone
        {
            get => (decimal)_mapperData.Deadzone * 100;
            set
            {
                if ((decimal)_mapperData.Deadzone != value)
                {
                    _mapperData.Deadzone = (double)(value ?? 100) / 100;
                    OnPropertyChanged(nameof(Deadzone));
                }
            }
        }

        public Visibility ConfigVisibility { get => _configVisibility; set => SetProperty(ref _configVisibility, value); }

        public MapperData MapperData { get => _mapperData; set => SetProperty(ref _mapperData, value); }

        public void Refresh()
        {
            OnPropertyChanged(nameof(SelectedInput));
            OnPropertyChanged(nameof(Min));
            OnPropertyChanged(nameof(Max));
        }
    }
}
