using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Input;

namespace YGOManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class YGOManagerMainView : Window
    {
        private YGOManagerViewModel ygoManager = new YGOManagerViewModel();
        private string _defaultSaveDir = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "CardDownloads");

        public YGOManagerMainView()
        {
            InitializeComponent();
            DataContext = ygoManager;
            tbCardNames.Focus();

            Process.Start(_defaultSaveDir, string.Format("/select, \"{0}\"", _defaultSaveDir));
        }

        private void tbCardNames_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;
            else ygoManager.UpdateQueue();
        }
    }


}
