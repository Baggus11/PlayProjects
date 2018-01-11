using Common;
using HarvestHomeAPI.ViewModels;
using System.Collections.ObjectModel;
using System.Text;

namespace HarvestHomeAPI
{
    public class HarvestMainViewModel : ViewModelBase
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

        private ObservableCollection<Pod> _Pods = new ObservableCollection<Pod>();
        public ObservableCollection<Pod> Pods
        {
            get { return _Pods; }
            set { SetValue(ref _Pods, value); }
        }
    }
}
