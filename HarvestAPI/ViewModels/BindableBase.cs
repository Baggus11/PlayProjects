using System.ComponentModel;
using System.Runtime.CompilerServices;
namespace HarvestAPI.ViewModels
{
    /// <summary>
    /// BindableBase
    /// </summary>
    public abstract class BindableBase : INotifyPropertyChanged
    {
        // This is the same BindableBase as from Prism, apparently
        // This is here for fun. Install Prism if you want to use Prism.
        public event PropertyChangedEventHandler PropertyChanged;
        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value))
            {
                return false;
            }
            storage = value;
            this.OnPropertyChanged(propertyName);
            return true;
        }
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
