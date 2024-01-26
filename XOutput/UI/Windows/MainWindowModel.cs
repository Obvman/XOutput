using System.Collections.ObjectModel;
using XOutput.UI.Component;

namespace XOutput.UI.Windows
{
    public class MainWindowModel : ModelBase
    {
        private bool _isAdmin;
        private Tools.Settings settings;

        public Tools.Settings Settings
        {
            get => settings;
            set
            {
                if (settings != value)
                {
                    settings = value;
                    OnPropertyChanged(nameof(AllDevices));
                }
            }
        }

        public ObservableCollection<InputView> Inputs { get; } = new ObservableCollection<InputView>();

        public bool AllDevices
        {
            get => settings?.ShowAll ?? false;
            set
            {
                if (settings != null && settings.ShowAll != value)
                {
                    settings.ShowAll = value;
                    OnPropertyChanged(nameof(AllDevices));
                }
            }
        }

        public bool IsAdmin { get => _isAdmin; set => SetProperty(ref _isAdmin, value); }

        public ObservableCollection<ControllerView> Controllers { get; } = new ObservableCollection<ControllerView>();
    }
}
