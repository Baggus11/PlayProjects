using Common.ViewModels;
using System.ComponentModel;
namespace Common.Classes
{
    public class Pod : BindableBase   //silly name, may change later
    {
        [Description("Name")]
        public string Name { get; private set; }
        [Description("WeedCount")]
        public int WeedCount { get; private set; }
    }
}
