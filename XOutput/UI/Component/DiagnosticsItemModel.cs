using System.Collections.ObjectModel;
using XOutput.Diagnostics;

namespace XOutput.UI
{
    public class DiagnosticsItemModel : ModelBase
    {
        private string _source;

        public ObservableCollection<DiagnosticsResult> Results { get; } = new ObservableCollection<DiagnosticsResult>();

        public string Source { get => _source; set => SetProperty(ref _source, value); }
    }
}
