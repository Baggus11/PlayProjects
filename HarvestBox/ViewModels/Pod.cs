using Common;
using System.ComponentModel;

namespace HarvestHomeAPI.ViewModels
{
    public class Pod : ViewModelBase
    {
        public Pod(string name, int weedCount)
        {
            Name = name;
            WeedCount = weedCount;
        }

        [Description("Name")]
        public string Name { get; private set; }

        [Description("WeedCount")]
        public int WeedCount { get; private set; }

    }
}