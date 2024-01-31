using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using XOutput.Devices;
using XOutput.Devices.Input;
using XOutput.Devices.Input.Mouse;
using XOutput.Devices.Mapper;
using XOutput.Devices.XInput;
using XOutput.Tools;

namespace XOutput.UI.Windows
{
    public class AutoConfigureViewModel : ViewModelBase<AutoConfigureModel>
    {
        private const int WaitTime = 5000;
        private const int ShortAxisWaitTime = 3000;
        private const int ShortWaitTime = 1000;
        private const int BlinkTime = 500;

        private readonly Dictionary<InputSource, double> referenceValues = new Dictionary<InputSource, double>();
        private readonly DispatcherTimer timer = new DispatcherTimer();
        private readonly IEnumerable<IInputDevice> inputDevices;
        private readonly InputMapper mapper;
        private readonly XInputTypes[] valuesToRead;
        private XInputTypes xInputType;
        private readonly InputSource[] inputTypes;
        private DateTime lastTime;

        public Func<bool> IsMouseOverButtons { get; set; }

        public AutoConfigureViewModel(AutoConfigureModel model, IEnumerable<IInputDevice> inputDevices, InputMapper mapper,
                                      XInputTypes[] valuesToRead) : base(model)
        {
            this.mapper = mapper;
            this.inputDevices = inputDevices;
            this.valuesToRead = valuesToRead;
            xInputType = valuesToRead.First();
            if (valuesToRead.Length > 1)
            {
                Model.ButtonsVisibility = Visibility.Collapsed;
                Model.TimerVisibility = Visibility.Visible;
            }
            else
            {
                Model.ButtonsVisibility = Visibility.Visible;
                Model.TimerVisibility = Visibility.Collapsed;
            }
            inputTypes = inputDevices.SelectMany(i => i.Sources).ToArray();
            timer.Interval = TimeSpan.FromMilliseconds(BlinkTime);
            timer.Tick += TimerTick;
            timer.Start();
        }

        public void Initialize()
        {
            ReadReferenceValues();

            var mappings = mapper.Mappings[xInputType].Mappers;
            if (mappings.Any())
            {
                foreach (var mapping in mappings)
                    Model.InputConfigurations.Add(new InputConfigurationModel(mapping.Source,
                                                                              mapping.MinValue * 100,
                                                                              mapping.MaxValue * 100));
            }
            else
            {
                Model.InputConfigurations.Add(new InputConfigurationModel(null, 0.0, 0.0));
            }
            Model.SelectedInputConfiguration = Model.InputConfigurations.First();

            Model.XInput = xInputType;

            SetTime(false);

            foreach (var inputDevice in inputDevices)
                inputDevice.InputChanged += ReadValues;
        }

        private void TimerTick(object sender, EventArgs e)
        {
            Model.Highlight = !Model.Highlight;
        }

        private void ReadReferenceValues()
        {
            foreach (var type in inputTypes)
            {
                foreach (var inputDevice in inputDevices)
                {
                    referenceValues[type] = inputDevice.Get(type);
                }
            }
        }

        /// <summary>
        /// Reads the current values, and if the values have changed enough saves them.
        /// </summary>
        private void ReadValues(object sender, DeviceInputChangedEventArgs e)
        {
            if (e.Device is Mouse && (IsMouseOverButtons?.Invoke() ?? false))
                return;

            var inputDevice = e.Device;
            InputSource maxType = null;
            double maxDiff = 0;
            foreach (var type in e.ChangedValues)
            {
                double oldValue = referenceValues[type];
                double newValue = inputDevice.Get(type);
                double diff = Math.Abs(newValue - oldValue);
                if (diff > maxDiff)
                {
                    maxType = type;
                    maxDiff = diff;
                }
            }

            if (maxDiff > 0.3 && maxType != Model.SelectedInputConfiguration.MaxType)
            {
                Model.SelectedInputConfiguration.MaxType = maxType;
                CalculateStartValues();
            }

            if (Model.SelectedInputConfiguration.MaxType != null &&
                Model.SelectedInputConfiguration.MaxType.InputDevice != null)
            {
                CalculateValues();
            }
        }

        public bool SaveDisableValues()
        {
            var mapperCollection = mapper.GetMapping(xInputType);
            foreach (MapperData md in mapperCollection.Mappers)
            {
                if (md.InputType == null)
                    md.Source = inputTypes.First();

                md.MinValue = Model.XInput.GetDisableValue();
                md.MaxValue = Model.XInput.GetDisableValue();
            }
            return Next();
        }

        public bool SaveValues()
        {
            bool anyValidInputValues = Model.InputConfigurations.Any(
                inputConfiguration => inputConfiguration.MaxType != null && inputConfiguration.MaxType.InputDevice != null);
            if (anyValidInputValues)
            {
                var mappers = mapper.GetMapping(xInputType).Mappers;
                mappers.Clear();

                foreach (var inputConfiguration in Model.InputConfigurations)
                {
                    if (inputConfiguration.MaxType != null && inputConfiguration.MaxType.InputDevice != null)
                    {
                        var mapperData = new MapperData
                        {
                            Source = inputConfiguration.MaxType,
                            MinValue = inputConfiguration.MinValue / 100,
                            MaxValue = inputConfiguration.MaxValue / 100
                        };
                        mappers.Add(mapperData);
                    }
                }

                return Next();
            }
            else
                return SaveDisableValues();
        }

        public bool IncreaseTime()
        {
            Model.TimerValue += DateTime.Now.Subtract(lastTime).TotalMilliseconds;
            lastTime = DateTime.Now;
            return Model.TimerValue > Model.TimerMaxValue;
        }

        public void Close()
        {
            foreach (var inputDevice in inputDevices)
            {
                inputDevice.InputChanged -= ReadValues;
            }
            timer.Stop();
        }

        public void AddInput()
        {
            Model.InputConfigurations.Add(new InputConfigurationModel(null, 0.0, 0.0));
            Model.SelectedInputConfiguration = Model.InputConfigurations.Last();
        }

        public void RemovedSelectedInput()
        {
            Model.InputConfigurations.Remove(Model.SelectedInputConfiguration);
            Model.SelectedInputConfiguration = Model.InputConfigurations.First();
        }

        protected void SetTime(bool shortTime)
        {
            Model.TimerValue = 0;
            if (shortTime)
            {
                Model.TimerMaxValue = xInputType.IsAxis() ? ShortAxisWaitTime : ShortWaitTime;
            }
            else
            {
                Model.TimerMaxValue = WaitTime;
            }
            lastTime = DateTime.Now;
        }

        protected bool Next()
        {
            Model.SelectedInputConfiguration.MaxType = null;
            int index = Array.IndexOf(valuesToRead, xInputType);
            SetTime(false);
            if (index + 1 < valuesToRead.Length)
            {
                xInputType = valuesToRead[index + 1];
                Model.XInput = xInputType;
                return true;
            }
            return false;
        }

        private void CalculateValues()
        {
            double current = Model.SelectedInputConfiguration.MaxType.InputDevice.Get(Model.SelectedInputConfiguration.MaxType);

            double min = Math.Min(current, Model.SelectedInputConfiguration.MinValue / 100);
            double minValue = Math.Round(min * 100);

            double max = Math.Max(current, Model.SelectedInputConfiguration.MaxValue / 100);
            double maxValue = Math.Round(max * 100);

            if (!Helper.DoubleEquals(minValue, Model.SelectedInputConfiguration.MinValue) || !Helper.DoubleEquals(maxValue, Model.SelectedInputConfiguration.MaxValue))
            {
                Model.SelectedInputConfiguration.MinValue = minValue;
                Model.SelectedInputConfiguration.MaxValue = maxValue;
                SetTime(true);
            }
        }

        private void CalculateStartValues()
        {
            double current = Model.SelectedInputConfiguration.MaxType.InputDevice.Get(Model.SelectedInputConfiguration.MaxType);
            double reference = referenceValues[Model.SelectedInputConfiguration.MaxType];

            double min = Math.Min(current, reference);
            Model.SelectedInputConfiguration.MinValue = Math.Round(min * 100);

            double max = Math.Max(current, reference);
            Model.SelectedInputConfiguration.MaxValue = Math.Round(max * 100);

            SetTime(true);
        }
    }
}
