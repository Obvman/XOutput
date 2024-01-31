using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace XOutput.UI.Windows
{
    /// <summary>
    /// Interaction logic for AutoConfigureWindow.xaml
    /// </summary>
    public partial class AutoConfigureWindow : Window, IViewBase<AutoConfigureViewModel, AutoConfigureModel>
    {
        private readonly DispatcherTimer timer = new DispatcherTimer();
        private readonly bool timed;

        public AutoConfigureViewModel ViewModel { get; }

        public AutoConfigureWindow(AutoConfigureViewModel viewModel, bool timed)
        {
            this.ViewModel = viewModel;
            this.timed = timed;
            DataContext = viewModel;
            InitializeComponent();
        }

        private async void WindowLoaded(object sender, RoutedEventArgs e)
        {
            await Task.Delay(100);
            ViewModel.Initialize();
            ViewModel.IsMouseOverButtons =
                () => DisableButton.IsMouseOver || SaveButton.IsMouseOver || InputConfigurationsList.IsMouseOver ||
                      AddInputButton.IsMouseOver || RemoveInputButton.IsMouseOver;

            if (timed)
            {
                timer.Interval = TimeSpan.FromMilliseconds(25);
                timer.Tick += TimerTick;
                timer.Start();
            }
        }

        private void TimerTick(object sender, EventArgs e)
        {
            if (ViewModel.IncreaseTime())
            {
                bool hasNextInput = ViewModel.SaveValues();
                if (!hasNextInput)
                {
                    Close();
                }
            }
        }

        private void DisableClick(object sender, RoutedEventArgs e)
        {
            if (!ViewModel.SaveDisableValues())
            {
                Close();
            }
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
            if (!ViewModel.SaveValues())
            {
                Close();
            }
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddInputClick(object sender, RoutedEventArgs e) => ViewModel.AddInput();

        private void RemoveInputClick(object sender, RoutedEventArgs e) => ViewModel.RemovedSelectedInput();

        private void WindowClosed(object sender, EventArgs e)
        {
            timer.Tick -= TimerTick;
            timer.Stop();
            ViewModel.Close();
        }
    }
}
