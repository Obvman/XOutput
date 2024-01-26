using Nefarius.ViGEm.Client;
using Nefarius.ViGEm.Client.Exceptions;
using Nefarius.ViGEm.Client.Targets;
using Nefarius.ViGEm.Client.Targets.Xbox360;
using System;
using System.Collections.Generic;

namespace XOutput.Devices.XInput.Vigem
{
    /// <summary>
    /// ViGEm XOutput implementation.
    /// </summary>
    public sealed class VigemDevice : IXOutputInterface
    {
        private readonly ViGEmClient client;
        private readonly Dictionary<int, IXbox360Controller> controllers = new Dictionary<int, IXbox360Controller>();
        private readonly Dictionary<XInputTypes, VigemXbox360ButtonMapping> buttonMappings;
        private readonly Dictionary<XInputTypes, VigemXbox360AxisMapping> axisMappings;
        private readonly Dictionary<XInputTypes, VigemXbox360SliderMapping> sliderMappings;

        public VigemDevice()
        {
            buttonMappings = new Dictionary<XInputTypes, VigemXbox360ButtonMapping>
            {
                { XInputTypes.A, new VigemXbox360ButtonMapping(Xbox360Button.A) },
                { XInputTypes.B, new VigemXbox360ButtonMapping(Xbox360Button.B) },
                { XInputTypes.X, new VigemXbox360ButtonMapping(Xbox360Button.X) },
                { XInputTypes.Y, new VigemXbox360ButtonMapping(Xbox360Button.Y) },
                { XInputTypes.L1, new VigemXbox360ButtonMapping(Xbox360Button.LeftShoulder) },
                { XInputTypes.R1, new VigemXbox360ButtonMapping(Xbox360Button.RightShoulder) },
                { XInputTypes.Back, new VigemXbox360ButtonMapping(Xbox360Button.Back) },
                { XInputTypes.Start, new VigemXbox360ButtonMapping(Xbox360Button.Start) },
                { XInputTypes.Home, new VigemXbox360ButtonMapping(Xbox360Button.Guide) },
                { XInputTypes.R3, new VigemXbox360ButtonMapping(Xbox360Button.RightThumb) },
                { XInputTypes.L3, new VigemXbox360ButtonMapping(Xbox360Button.LeftThumb) },
                { XInputTypes.UP, new VigemXbox360ButtonMapping(Xbox360Button.Up) },
                { XInputTypes.DOWN, new VigemXbox360ButtonMapping(Xbox360Button.Down) },
                { XInputTypes.LEFT, new VigemXbox360ButtonMapping(Xbox360Button.Left) },
                { XInputTypes.RIGHT, new VigemXbox360ButtonMapping(Xbox360Button.Right) }
            };

            axisMappings = new Dictionary<XInputTypes, VigemXbox360AxisMapping>
            {
                { XInputTypes.LX, new VigemXbox360AxisMapping(Xbox360Axis.LeftThumbX) },
                { XInputTypes.LY, new VigemXbox360AxisMapping(Xbox360Axis.LeftThumbY) },
                { XInputTypes.RX, new VigemXbox360AxisMapping(Xbox360Axis.RightThumbX) },
                { XInputTypes.RY, new VigemXbox360AxisMapping(Xbox360Axis.RightThumbY) }
            };

            sliderMappings = new Dictionary<XInputTypes, VigemXbox360SliderMapping>
            {
                { XInputTypes.L2, new VigemXbox360SliderMapping(Xbox360Slider.LeftTrigger) },
                { XInputTypes.R2, new VigemXbox360SliderMapping(Xbox360Slider.RightTrigger) }
            };

            client = new ViGEmClient();
        }

        /// <summary>
        /// Gets if <see cref="VigemDevice"/> is available.
        /// </summary>
        /// <returns></returns>
        public static bool IsAvailable()
        {
            try
            {
                new ViGEmClient().Dispose();
                return true;
            }
            catch (VigemBusNotFoundException)
            {
                return false;
            }
            catch (DllNotFoundException)
            {
                return false;
            }
        }

        /// <summary>
        /// Implements <see cref="IXOutputInterface.Plugin(int)"/>
        /// </summary>
        /// <param name="controllerCount">number of controller</param>
        /// <returns>If it was successful</returns>
        public bool Plugin(int controllerCount)
        {
            var controller = client.CreateXbox360Controller();
            controller.Connect();
            controllers.Add(controllerCount, controller);
            return true;
        }

        /// <summary>
        /// Implements <see cref="IXOutputInterface.Unplug(int)"/>
        /// </summary>
        /// <param name="controllerCount">number of controller</param>
        /// <returns>If it was successful</returns>
        public bool Unplug(int controllerCount)
        {
            if (controllers.ContainsKey(controllerCount))
            {
                var controller = controllers[controllerCount];
                controllers.Remove(controllerCount);
                controller.Disconnect();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Implements <see cref="IXOutputInterface.Report(int, Dictionary{XInputTypes, double})"/>
        /// </summary>
        /// <param name="controllerCount">Number of controller</param>
        /// <param name="values">values for each XInput</param>
        /// <returns>If it was successful</returns>
        public bool Report(int controllerCount, Dictionary<XInputTypes, double> values)
        {
            if (controllers.ContainsKey(controllerCount))
            {
                var controller = controllers[controllerCount];
                foreach (var value in values)
                {
                    if (value.Key.IsAxis())
                    {
                        var mapping = axisMappings[value.Key];
                        controller.SetAxisValue(mapping.Type, mapping.GetValue(value.Value));
                    }
                    else if (value.Key.IsSlider())
                    {
                        var mapping = sliderMappings[value.Key];
                        controller.SetSliderValue(mapping.Type, mapping.GetValue(value.Value));
                    }
                    else
                    {
                        var mapping = buttonMappings[value.Key];
                        controller.SetButtonState(mapping.Type, mapping.GetValue(value.Value));
                    }
                }
                return true;
            }
            return false;
        }

        public void Dispose()
        {
            foreach (var controller in controllers.Values)
            {
                controller.Disconnect();
            }
            client.Dispose();
        }

        public IXbox360Controller GetController(int controllerCount) => controllers[controllerCount];
    }
}
