using System;
using System.Collections.Generic;
using System.Linq;

namespace XOutput.Devices
{
    /// <summary>
    /// Event delegate for DeviceInputChanged event.
    /// </summary>
    /// <param name="sender">the <see cref="IDevice"/> with changed input</param>
    /// <param name="e">event arguments</param>
    public delegate void DeviceInputChangedHandler(object sender, DeviceInputChangedEventArgs e);

    /// <summary>
    /// Event argument class for DeviceInputChanged event.
    /// </summary>
    public class DeviceInputChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the changed device.
        /// </summary>
        public IDevice Device { get; }
        /// <summary>
        /// Gets the changed values.
        /// </summary>
        public IEnumerable<InputSource> ChangedValues { get; }
        /// <summary>
        /// Gets the changed DPad values.
        /// </summary>
        public IEnumerable<int> ChangedDPads { get; }

        public DeviceInputChangedEventArgs(IDevice device, IEnumerable<InputSource> changedValues)
        {
            Device = device;
            ChangedValues = changedValues;
            ChangedDPads = new int[0];
        }

        public DeviceInputChangedEventArgs(IDevice device, IEnumerable<InputSource> changedValues, IEnumerable<int> changedDPads)
        {
            Device = device;
            ChangedDPads = changedDPads;
            ChangedValues = changedValues;
        }

        /// <summary>
        /// Gets if the value of the type has changed.
        /// </summary>
        /// <param name="type">input type</param>
        /// <returns></returns>
        public bool HasValueChanged(InputSource type) => ChangedValues.Contains(type);

        /// <summary>
        /// Gets if the value of the DPad has changed.
        /// </summary>
        /// <param name="type">input type</param>
        /// <returns></returns>
        public bool HasDPadChanged(int dPadIndex) => ChangedDPads.Contains(dPadIndex);
    }
}
