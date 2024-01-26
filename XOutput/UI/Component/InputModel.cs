using System.Windows.Media;
using XOutput.Devices.Input;

namespace XOutput.UI.Component
{
    public class InputModel : ModelBase
    {
        private IInputDevice _device;
        private Brush _background;

        public IInputDevice Device { get => _device; set => SetProperty(ref _device, value); }

        public Brush Background { get => _background; set => SetProperty(ref _background, value); }

        public string DisplayName => string.Format("{0} ({1})", _device.DisplayName, _device.UniqueId);
    }
}
