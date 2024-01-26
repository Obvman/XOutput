using System.Windows.Threading;
using XOutput.Tools;
using XOutput.UI.Windows;

namespace XOutput.UI
{
    public static class UIConfiguration
    {
        [ResolverMethod(false)]
        public static MainWindowModel GetMainWindowModel() => new MainWindowModel();

        [ResolverMethod(false)]
        public static MainWindowViewModel GetMainWindowViewModel(MainWindowModel model, Dispatcher dispatcher, HidGuardianManager hidGuardianManager) => new MainWindowViewModel(model, dispatcher, hidGuardianManager);

        [ResolverMethod(false)]
        public static MainWindow GetMainWindow(MainWindowViewModel viewModel, ArgumentParser argumentParser) => new MainWindow(viewModel, argumentParser);


        [ResolverMethod(false)]
        public static SettingsModel GetSettingsModel(RegistryModifier registryModifier, Settings settings) => new SettingsModel(registryModifier, settings);

        [ResolverMethod(false)]
        public static SettingsViewModel GetSettingsViewModel(SettingsModel model) => new SettingsViewModel(model);

        [ResolverMethod(false)]
        public static SettingsWindow GetSettingsWindow(SettingsViewModel viewModel) => new SettingsWindow(viewModel);


        [ResolverMethod(false)]
        public static DiagnosticsViewModel GetDiagnosticsViewModel(DiagnosticsModel model) => new DiagnosticsViewModel(model);

        [ResolverMethod(false)]
        public static DiagnosticsWindow GetDiagnosticsWindow(DiagnosticsViewModel viewModel) => new DiagnosticsWindow(viewModel);
    }
}
