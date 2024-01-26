using System.Collections.Generic;
using System.Linq;

namespace XOutput.Devices
{
    /// <summary>
    /// Holds a current state of the device.
    /// </summary>
    public class DeviceState
    {
        private readonly object syncLock = new object();

        private readonly DPadDirection[] dPads;
        private readonly List<InputSource> changedSources;
        private readonly List<int> changedDpad;
        private readonly List<int> allDpads;

        /// <summary>
        /// Gets the current values.
        /// </summary>
        public IEnumerable<InputSource> Values { get; }

        /// <summary>
        /// Gets the current DPad values.
        /// </summary>
        public IEnumerable<DPadDirection> DPads => dPads;

        public DeviceState(IEnumerable<InputSource> types, int dPadCount)
        {
            Values = types.ToArray();
            changedSources = new List<InputSource>(types.Count());
            dPads = new DPadDirection[dPadCount];
            allDpads = Enumerable.Range(0, dPadCount).ToList();
            changedDpad = new List<int>();
        }

        /// <summary>
        /// Sets new DPad values.
        /// </summary>
        /// <param name="newDPads">new values</param>
        /// <returns>changed DPad indices</returns>
        public bool SetDPad(int i, DPadDirection newValue)
        {
            lock (syncLock)
            {
                var oldValue = dPads[i];
                if (newValue != oldValue)
                {
                    dPads[i] = newValue;
                    changedDpad.Add(i);
                    return true;
                }
            }
            return false;
        }

        public void ResetChanges()
        {
            lock (syncLock)
            {
                changedSources.Clear();
                changedDpad.Clear();
            }
        }

        public void MarkChanged(InputSource source)
        {
            lock (syncLock)
            {
                changedSources.Add(source);
            }
        }

        public bool AnyChanges(bool force = false)
        {
            lock (syncLock)
            {
                return force ? Values.Any() : changedSources.Any();
            }
        }

        public bool AnyChangedDpads(bool force = false)
        {
            lock (syncLock)
            {
                return force ? allDpads.Any() : changedDpad.Any();
            }
        }

        public IEnumerable<InputSource> GetChanges(bool force = false) => force ? Values : changedSources;

        public IEnumerable<int> GetChangedDpads(bool force = false) => force ? allDpads : changedDpad;
    }
}
