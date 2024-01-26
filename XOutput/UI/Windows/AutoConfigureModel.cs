using System.Windows;
using XOutput.Devices;
using XOutput.Devices.XInput;

namespace XOutput.UI.Windows
{
    public class AutoConfigureModel : ModelBase
    {
        private XInputTypes _xInput;
        private bool _isAuto = true;
        private bool _highlight;
        private InputSource _maxType;
        private double _minValue;
        private double _maxValue;
        private Visibility _buttonsVisibility;
        private double _timerMaxValue;
        private double _timerValue;
        private Visibility _timerVisibility;

        public XInputTypes XInput { get => _xInput; set => SetProperty(ref _xInput, value); }

        public bool IsAuto { get => _isAuto; set => SetProperty(ref _isAuto, value); }

        public bool Highlight { get => _highlight; set => SetProperty(ref _highlight, value); }

        public InputSource MaxType { get => _maxType; set => SetProperty(ref _maxType, value); }

        public double MinValue { get => _minValue; set => SetProperty(ref _minValue, value); }

        public double MaxValue { get => _maxValue; set => SetProperty(ref _maxValue, value); }

        public Visibility ButtonsVisibility { get => _buttonsVisibility; set => SetProperty(ref _buttonsVisibility, value); }

        public double TimerMaxValue { get => _timerMaxValue; set => SetProperty(ref _timerMaxValue, value); }

        public double TimerValue { get => _timerValue; set => SetProperty(ref _timerValue, value); }

        public Visibility TimerVisibility { get => _timerVisibility; set => SetProperty(ref _timerVisibility, value); }
    }
}
