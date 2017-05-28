using Common.ViewModels;
using System.Diagnostics;
using System.Text;
using System.Windows;
/// <summary>
/// todo: Write a UI for setting up and monitoring gardening units, implements and accessories.
/// This program will be written to support 10x10 ft gardens.
/// </sumary>
namespace UIMockup
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static GrowerMainViewModel Data = new GrowerMainViewModel();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = Data;
        }
        public static void UpdateStatusBox(string statusMsg = "")
        {
            Data.Status = statusMsg;
            Debug.WriteLine(statusMsg);
        }
        private void OnMainLoaded(object sender, RoutedEventArgs e)
        {
            UpdateStatusBox("Ready.");
        }
    }
}
