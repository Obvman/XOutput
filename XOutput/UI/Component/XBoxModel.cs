using XOutput.Devices.XInput;

namespace XOutput.UI
{
    public class XBoxModel : ModelBase
    {
        private XInputTypes _xInputType;
        private bool _highlight;

        public XInputTypes XInputType { get => _xInputType; set => SetProperty(ref _xInputType, value); }

        public bool Highlight { get => _highlight; set => SetProperty(ref _highlight, value); }
    }
}
