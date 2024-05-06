using Kyrsovau_Rabota.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Kyrsovau_Rabota.View
{
    /// <summary>
    /// Логика взаимодействия для ClientFullWindow.xaml
    /// </summary>
    public partial class ClientFullWindow : Window
    {
        public ClientFullWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowVM();
        }

        private void zauvka(object sender, RoutedEventArgs e)
        {
            ClientWindow clientWindow = new ClientWindow();
            clientWindow.Show();
        }
    }
}
