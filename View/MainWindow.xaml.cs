using Kyrsovau_Rabota.View;
using Kyrsovau_Rabota.ViewModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Kyrsovau_Rabota
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowVM();
        }



        private void EXIT(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void CLIENT(object sender, RoutedEventArgs e)
        {
            ClientFullWindow clientFullWindow = new ClientFullWindow();
            clientFullWindow.Show();
        }

        public void SOTRYDNIC(object sender, RoutedEventArgs e)
        {
            WorkerWindow workerWindow = new WorkerWindow();
            workerWindow.Show();
        }
    }
}