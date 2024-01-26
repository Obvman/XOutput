using XOutput.Devices;

namespace XOutput.UI.Component
{
    public class ButtonModel : ModelBase
    {
        private bool _value;
        private InputSource _type;

        public InputSource Type { get => _type; set => SetProperty(ref _type, value); }
        public bool Value { get => _value; set => SetProperty(ref _value, value); }
    }
}
