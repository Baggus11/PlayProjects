using HarvestHomeAPI.ViewModels;
using System.Windows;
/// <summary>
/// todo: Write a UI for setting up and monitoring gardening units, implements and accessories.
/// This program will be written to support 10x10 ft gardens.
/// </sumary>
namespace HarvestHomeAPI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static HarvestMainViewModel ViewModel = new HarvestMainViewModel();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = ViewModel;
            ViewModel.Pods.Add(new Pod("Box 1", 12)); //todo - fix binding
        }

        private void UpdateStatusBox(string statusMsg = "")
        {
            ViewModel.Status = statusMsg;
        }

        private void OnMainLoaded(object sender, RoutedEventArgs e)
        {
            UpdateStatusBox("Ready.");
        }
    }
}
