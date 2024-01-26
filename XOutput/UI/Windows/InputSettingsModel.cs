using System.Collections.ObjectModel;
using XOutput.UI.Component;

namespace XOutput.UI.Windows
{
    public class InputSettingsModel : ModelBase
    {
        private string _title;
        private string _forceFeedbackText;
        private string _testButtonText;
        private bool _forceFeedbackEnabled;
        private bool _forceFeedbackAvailable;
        private bool _isAdmin;
        private bool _hidGuardianAdded;

        public ObservableCollection<IUpdatableView> InputAxisViews { get; } = new ObservableCollection<IUpdatableView>();
        public ObservableCollection<IUpdatableView> InputDPadViews { get; } = new ObservableCollection<IUpdatableView>();
        public ObservableCollection<IUpdatableView> InputButtonViews { get; } = new ObservableCollection<IUpdatableView>();

        public string Title { get => _title; set => SetProperty(ref _title, value); }

        public string ForceFeedbackText { get => _forceFeedbackText; set => SetProperty(ref _forceFeedbackText, value); }

        public string TestButtonText { get => _testButtonText; set => SetProperty(ref _testButtonText, value); }

        public bool ForceFeedbackEnabled { get => _forceFeedbackEnabled; set => SetProperty(ref _forceFeedbackEnabled, value); }

        public bool ForceFeedbackAvailable { get => _forceFeedbackAvailable; set => SetProperty(ref _forceFeedbackAvailable, value); }

        public bool IsAdmin { get => _isAdmin; set => SetProperty(ref _isAdmin, value); }

        public bool HidGuardianAdded { get => _hidGuardianAdded; set => SetProperty(ref _hidGuardianAdded, value); }
    }
}
