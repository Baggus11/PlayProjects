using Common;
using HarvestAPI.ViewModels;
using System.Collections.ObjectModel;
using System.Text;
namespace HarvestAPI
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
            set { SetPropertyValue(ref _Pods, value); }
        }
    }
}
