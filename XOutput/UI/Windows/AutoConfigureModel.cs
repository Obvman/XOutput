using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using XOutput.Devices.XInput;

namespace XOutput.UI.Windows
{
    public class AutoConfigureModel : ModelBase
    {
        private InputConfigurationModel _selectedInputConfiguration;
        private XInputTypes _xInput;
        private bool _isAuto = true;
        private bool _highlight;
        private Visibility _buttonsVisibility;
        private double _timerMaxValue;
        private double _timerValue;
        private Visibility _timerVisibility;

        public ObservableCollection<InputConfigurationModel> InputConfigurations { get; } = new ObservableCollection<InputConfigurationModel>();

        public bool CanRemoveInput => InputConfigurations.Count > 1;

        public InputConfigurationModel SelectedInputConfiguration { get => _selectedInputConfiguration; set => SetProperty(ref _selectedInputConfiguration, value); }

        public XInputTypes XInput { get => _xInput; set => SetProperty(ref _xInput, value); }

        public bool IsAuto { get => _isAuto; set => SetProperty(ref _isAuto, value); }

        public bool Highlight { get => _highlight; set => SetProperty(ref _highlight, value); }

        public Visibility ButtonsVisibility { get => _buttonsVisibility; set => SetProperty(ref _buttonsVisibility, value); }

        public double TimerMaxValue { get => _timerMaxValue; set => SetProperty(ref _timerMaxValue, value); }

        public double TimerValue { get => _timerValue; set => SetProperty(ref _timerValue, value); }

        public Visibility TimerVisibility { get => _timerVisibility; set => SetProperty(ref _timerVisibility, value); }

        public AutoConfigureModel()
        {
            InputConfigurations.CollectionChanged += this.InputConfigurations_CollectionChanged;
        }

        ~AutoConfigureModel()
        {
            InputConfigurations.CollectionChanged -= this.InputConfigurations_CollectionChanged;
        }

        private void InputConfigurations_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) => OnPropertyChanged(nameof(CanRemoveInput));
    }
}
