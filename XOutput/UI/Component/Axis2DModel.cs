using XOutput.Devices;

namespace XOutput.UI.Component
{
    public class Axis2DModel : ModelBase
    {
        private int _valueX;
        private int _valueY;
        private int _maxX;
        private int _maxY;

        public InputSource TypeX { get; set; }
        public InputSource TypeY { get; set; }

        public int ValueX { get => _valueX; set => SetProperty(ref _valueX, value); }

        public int ValueY { get => _valueY; set => SetProperty(ref _valueY, value); }

        public int MaxX { get => _maxX; set => SetProperty(ref _maxX, value); }

        public int MaxY { get => _maxY; set => SetProperty(ref _maxY, value); }
    }
}
