using System;
using System.Windows;
using System.Windows.Controls;

namespace XOutput.UI.Component
{
    /// <summary>
    /// Interaction logic for ControllerView.xaml
    /// </summary>
    public partial class ControllerView : UserControl, IViewBase<ControllerViewModel, ControllerModel>
    {
        public event Action<ControllerView> RemoveClicked;
        public event Action<ControllerView> DuplicateClicked;

        public ControllerViewModel ViewModel { get; }

        public ControllerView(ControllerViewModel viewModel)
        {
            this.ViewModel = viewModel;
            DataContext = viewModel;
            InitializeComponent();
        }
        private void OpenClick(object sender, RoutedEventArgs e) => ViewModel.Edit();

        private void ButtonClick(object sender, RoutedEventArgs e) => ViewModel.StartStop();

        private void RemoveClick(object sender, RoutedEventArgs e) => RemoveClicked?.Invoke(this);

        private void DuplicateClick(object sender, RoutedEventArgs e) => DuplicateClicked?.Invoke(this);
    }
}
