using System;
using System.Linq;
using System.Threading;
using XOutput.Devices.Input;
using XOutput.Devices.Input.DirectInput;
using XOutput.Devices.Mapper;
using XOutput.Devices.XInput;
using XOutput.Devices.XInput.SCPToolkit;
using XOutput.Devices.XInput.Vigem;
using XOutput.Logging;

namespace XOutput.Devices
{
    /// <summary>
    /// GameController is a container for input devices, mappers and output devices.
    /// </summary>
    public sealed class GameController : IDisposable
    {
        private static readonly ILogger logger = LoggerFactory.GetLogger(typeof(GameController));

        private readonly IXOutputInterface xOutputInterface;
        private Thread thread;
        private bool running;
        private Nefarius.ViGEm.Client.Targets.IXbox360Controller controller;

        /// <summary>
        /// Gets the output device.
        /// </summary>
        public XOutputDevice XInput { get; }

        /// <summary>
        /// Gets the mapping of the input device.
        /// </summary>
        public InputMapper Mapper { get; }

        /// <summary>
        /// Gets the name of the input device.
        /// </summary>
        public string DisplayName => Mapper.Name;

        /// <summary>
        /// Gets the number of the controller.
        /// </summary>
        public int ControllerCount { get; private set; } = 0;

        /// <summary>
        /// Gets if any XInput emulation is installed.
        /// </summary>
        public bool HasXOutputInstalled => xOutputInterface != null;

        /// <summary>
        /// Gets if force feedback is supported.
        /// </summary>
        public bool ForceFeedbackSupported => xOutputInterface is VigemDevice;

        /// <summary>
        /// Gets the force feedback device.
        /// </summary>
        public IInputDevice ForceFeedbackDevice { get; set; }

        public GameController(InputMapper mapper)
        {
            this.Mapper = mapper;
            xOutputInterface = CreateXOutput();
            XInput = new XOutputDevice(mapper);
            if (!string.IsNullOrEmpty(mapper.ForceFeedbackDevice))
            {
                var device = InputDevices.Instance.GetDevices().OfType<DirectDevice>().FirstOrDefault(d => d.UniqueId == mapper.ForceFeedbackDevice);
                if (device != null)
                {
                    ForceFeedbackDevice = device;
                }
            }
            running = false;
        }

        private IXOutputInterface CreateXOutput()
        {
            if (VigemDevice.IsAvailable())
            {
                logger.Info("ViGEm devices are used.");
                return new VigemDevice();
            }
            else if (ScpDevice.IsAvailable())
            {
                logger.Warning("SCP Toolkit devices are used.");
                return new ScpDevice();
            }
            else
            {
                logger.Error("Neither ViGEm nor SCP devices can be used.");
                return null;
            }
        }

        ~GameController()
        {
            Dispose();
        }

        /// <summary>
        /// Disposes all used resources
        /// </summary>
        public void Dispose()
        {
            Stop();
            XInput?.Dispose();
            xOutputInterface?.Dispose();
        }

        /// <summary>
        /// Starts the emulation of the device
        /// </summary>
        public int Start(Action onStop = null)
        {
            if (!HasXOutputInstalled)
            {
                return 0;
            }
            ControllerCount = Controllers.Instance.GetId();
            if (controller != null)
            {
                controller.FeedbackReceived -= ControllerFeedbackReceived;
            }
            if (xOutputInterface.Unplug(ControllerCount))
            {
                // Wait for unplugging
                Thread.Sleep(10);
            }
            if (xOutputInterface.Plugin(ControllerCount))
            {
                thread = new Thread(() => ReadAndReportValues(onStop));
                running = true;
                thread.Name = $"Emulated controller {ControllerCount} output refresher";
                thread.IsBackground = true;
                thread.Start();
                logger.Info($"Emulation started on {ToString()}.");
                if (ForceFeedbackSupported)
                {
                    logger.Info($"Force feedback mapping is connected on {ToString()}.");
                    controller = ((VigemDevice)xOutputInterface).GetController(ControllerCount);
                    controller.FeedbackReceived += ControllerFeedbackReceived;
                }
            }
            else
            {
                ResetId();
            }
            return ControllerCount;
        }

        /// <summary>
        /// Stops the emulation of the device
        /// </summary>
        public void Stop()
        {
            if (running)
            {
                running = false;
                XInput.InputChanged -= XInputInputChanged;
                if (ForceFeedbackSupported)
                {
                    controller.FeedbackReceived -= ControllerFeedbackReceived;
                    logger.Info($"Force feedback mapping is disconnected on {ToString()}.");
                }
                xOutputInterface?.Unplug(ControllerCount);
                logger.Info($"Emulation stopped on {ToString()}.");
                ResetId();
                thread?.Interrupt();
            }
        }

        public override string ToString() => DisplayName;

        private void ReadAndReportValues(Action onStop)
        {
            try
            {
                XInput.InputChanged += XInputInputChanged;
                while (running)
                {
                    Thread.Sleep(100);
                }
            }
            catch (ThreadInterruptedException)
            {

            }
            finally
            {
                onStop?.Invoke();
                Stop();
            }
        }

        private void XInputInputChanged(object sender, DeviceInputChangedEventArgs e)
        {
            if (!xOutputInterface.Report(ControllerCount, XInput.GetValues()))
            {
                Stop();
            }
        }

        private void ControllerFeedbackReceived(object sender, Nefarius.ViGEm.Client.Targets.Xbox360.Xbox360FeedbackReceivedEventArgs e)
        {
            ForceFeedbackDevice?.SetForceFeedback((double)e.LargeMotor / byte.MaxValue, (double)e.SmallMotor / byte.MaxValue);
        }

        private void ResetId()
        {
            if (ControllerCount != 0)
            {
                Controllers.Instance.DisposeId(ControllerCount);
                ControllerCount = 0;
            }
        }
    }
}
