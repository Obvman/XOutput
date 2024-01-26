using System.Collections.ObjectModel;
using System.Windows.Controls;
using XOutput.UI.Component;

namespace XOutput.UI.Windows
{
    public class ControllerSettingsModel : ModelBase
    {
        private ComboBoxItem forceFeedback;
        private string title;
        private bool startWhenConnected;

        public ObservableCollection<MappingView> MapperAxisViews { get; } = new ObservableCollection<MappingView>();
        public ObservableCollection<MappingView> MapperDPadViews { get; } = new ObservableCollection<MappingView>();
        public ObservableCollection<MappingView> MapperButtonViews { get; } = new ObservableCollection<MappingView>();
        public ObservableCollection<IUpdatableView> XInputAxisViews { get; } = new ObservableCollection<IUpdatableView>();
        public ObservableCollection<IUpdatableView> XInputDPadViews { get; } = new ObservableCollection<IUpdatableView>();
        public ObservableCollection<IUpdatableView> XInputButtonViews { get; } = new ObservableCollection<IUpdatableView>();
        public ObservableCollection<ComboBoxItem> ForceFeedbacks { get; } = new ObservableCollection<ComboBoxItem>();

        public ComboBoxItem ForceFeedback
        {
            get => forceFeedback;
            set
            {
                if (forceFeedback != value)
                {
                    forceFeedback = value;
                    OnPropertyChanged(nameof(ForceFeedback));
                }
            }
        }

        public string Title
        {
            get => title;
            set
            {
                if (title != value)
                {
                    title = value;
                    OnPropertyChanged(nameof(Title));
                }
            }
        }

        public bool StartWhenConnected
        {
            get => startWhenConnected;
            set
            {
                if (startWhenConnected != value)
                {
                    startWhenConnected = value;
                    OnPropertyChanged(nameof(StartWhenConnected));
                }
            }
        }
    }
}
