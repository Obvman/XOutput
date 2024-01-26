using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace XOutput.UI
{
    public abstract class ModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Invokes the property changed event
        /// </summary>
        /// <param name="name">Name of the property that changed</param>
        protected void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        /// <summary>
        /// Used in property setters to set the value and raise a notification to the GUI so it can update the
        /// property display.
        /// </summary>
        /// <typeparam name="T">The type of the property being set</typeparam>
        /// <param name="storage">Reference to the property that's being set.</param>
        /// <param name="value">The value the property is being set to.</param>
        /// <param name="propertyName">
        /// The name of the property variable that will be updated.
        /// Defaults to the name of the property setter that's calling this method.
        /// </param>
        /// <returns>
        /// true if the property changed and the UI was notified.
        /// false if the property didn't change.
        /// </returns>
        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (object.Equals(storage, value))
                return false;

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
