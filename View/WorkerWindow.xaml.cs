using Kyrsovau_Rabota.View;
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

namespace Kyrsovau_Rabota
{
    /// <summary>
    /// Логика взаимодействия для WorkerWindow.xaml
    /// </summary>
    public partial class WorkerWindow : Window
    {
        public WorkerWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowVM();


        }

        public void Enter(object sender, RoutedEventArgs e)
        {
            if (Loginbox.Text == "SNEGOK" && Passwordbox1.Password == "91141")
            {
                ClientPriem clientPriem = new ClientPriem();
                clientPriem.Show();
                Window.GetWindow(this).Close();
            }
           else
            {
                MessageBox.Show("Неверный логин или пароль", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Question);
            }
        }
    }
}
