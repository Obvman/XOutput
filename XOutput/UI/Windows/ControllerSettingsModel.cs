using System.Collections.ObjectModel;
using System.Windows.Controls;
using XOutput.UI.Component;

namespace XOutput.UI.Windows
{
    public class ControllerSettingsModel : ModelBase
    {
        private ComboBoxItem _forceFeedback;
        private string _title;
        private bool _startWhenConnected;

        public ObservableCollection<MappingView> MapperAxisViews { get; } = new ObservableCollection<MappingView>();
        public ObservableCollection<MappingView> MapperDPadViews { get; } = new ObservableCollection<MappingView>();
        public ObservableCollection<MappingView> MapperButtonViews { get; } = new ObservableCollection<MappingView>();
        public ObservableCollection<IUpdatableView> XInputAxisViews { get; } = new ObservableCollection<IUpdatableView>();
        public ObservableCollection<IUpdatableView> XInputDPadViews { get; } = new ObservableCollection<IUpdatableView>();
        public ObservableCollection<IUpdatableView> XInputButtonViews { get; } = new ObservableCollection<IUpdatableView>();
        public ObservableCollection<ComboBoxItem> ForceFeedbacks { get; } = new ObservableCollection<ComboBoxItem>();

        public ComboBoxItem ForceFeedback { get => _forceFeedback; set => SetProperty(ref _forceFeedback, value); }

        public string Title { get => _title; set => SetProperty(ref _title, value); }

        public bool StartWhenConnected { get => _startWhenConnected; set => SetProperty(ref _startWhenConnected, value); }
    }
}
