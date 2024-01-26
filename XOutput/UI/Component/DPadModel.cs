using XOutput.Devices;

namespace XOutput.UI.Component
{
    public class DPadModel : ModelBase
    {
        private DPadDirection _direction;
        private int _valueX;
        private int _valueY;
        private string _label;

        public DPadDirection Direction { get => _direction; set => SetProperty(ref _direction, value); }

        public int ValueX { get => _valueX; set => SetProperty(ref _valueX, value); }

        public int ValueY { get => _valueY; set => SetProperty(ref _valueY, value); }

        public string Label { get => _label; set => SetProperty(ref _label, value); }
    }
}
