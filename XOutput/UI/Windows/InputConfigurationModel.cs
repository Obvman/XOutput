using XOutput.Devices;

namespace XOutput.UI.Windows
{
    public class InputConfigurationModel : ModelBase
    {
        private InputSource _maxType;
        private double _minValue;
        private double _maxValue;

        public InputSource MaxType { get => _maxType; set => SetProperty(ref _maxType, value); }

        public double MinValue { get => _minValue; set => SetProperty(ref _minValue, value); }

        public double MaxValue { get => _maxValue; set => SetProperty(ref _maxValue, value); }

        public InputConfigurationModel()
        {
        }

        public InputConfigurationModel(InputSource maxType, double minValue, double maxValue)
        {
            _maxType = maxType;
            _minValue = minValue;
            _maxValue = maxValue;
        }
    }
}
