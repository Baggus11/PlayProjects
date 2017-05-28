using Common.Classes;
using System.Collections.ObjectModel;
using System.Text;
namespace Common.ViewModels
{
    public class GrowerMainViewModel : ViewModelBase
    {
        private StringBuilder _Status = new StringBuilder();
        public string Status
        {
            get { return _Status.ToString(); }
            set
            {
                _Status.AppendLine(value);
                RaisePropertyChanged(nameof(Status));
            }
        }
        /// <summary>
        /// INotifyable Property _Pods
        /// </summary>
        private ObservableCollection<Pod> _Pods = new ObservableCollection<Pod>();
        public ObservableCollection<Pod> Pods
        {
            get { return _Pods; }
            set
            {
                if (Pods != value)
                {
                    _Pods = value;
                    RaisePropertyChanged(nameof(Pods));
                }
            }
        }
    }
}
