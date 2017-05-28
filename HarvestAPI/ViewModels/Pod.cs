using Common.ViewModels;
using System.ComponentModel;
namespace HarvestAPI
{
    public class Pod : BindableBase
    {
        [Description("Name")]
        public string Name { get; private set; }
        [Description("WeedCount")]
        public int WeedCount { get; private set; }
    }
}