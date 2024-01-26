using System.ComponentModel;

namespace XOutput.UI
{
    public abstract class ViewModelBase<M> where M : ModelBase, INotifyPropertyChanged
    {
        public LanguageModel LanguageModel => LanguageModel.Instance;

        public M Model { get; }

        protected ViewModelBase(M model)
        {
            this.Model = model;
        }
    }
}
