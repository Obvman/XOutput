using XOutput.Devices;

namespace XOutput.UI.Component
{
    public class AxisModel : ModelBase
    {
        private InputSource _type;
        private int _value;
        private int _max;

        public InputSource Type { get => _type; set => SetProperty(ref _type, value); }
        public int Value { get => _value; set => SetProperty(ref _value, value); }
        public int Max { get => _max; set => SetProperty(ref _max, value); }
    }
}
