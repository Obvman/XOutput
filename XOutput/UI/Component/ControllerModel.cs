using System.Windows.Media;
using XOutput.Devices;

namespace XOutput.UI.Component
{
    public class ControllerModel : ModelBase
    {
        private GameController _controller;
        private string _buttonText;
        private bool _started;
        private bool _canStart;
        private Brush _background;

        public GameController Controller { get => _controller; set => SetProperty(ref _controller, value); }

        public string ButtonText { get => _buttonText; set => SetProperty(ref _buttonText, value); }

        public bool Started { get => _started; set => SetProperty(ref _started, value); }

        public bool CanStart { get => _canStart; set => SetProperty(ref _canStart, value); }

        public Brush Background { get => _background; set => SetProperty(ref _background, value); }

        public string DisplayName { get => Controller.ToString(); }

        public void RefreshName()
        {
            OnPropertyChanged(nameof(DisplayName));
        }
    }
}
