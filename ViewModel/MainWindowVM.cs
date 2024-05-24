using Kyrsovau_Rabota.Model;
using Kyrsovau_Rabota.View;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Kyrsovau_Rabota.ViewModel
{
    public class MainWindowVM : BaseVM
    {
        private Message newzauvka = new();
        private Message selectedzauvka;
        private Otvet selectedotvet;
        private Otvet newotvet = new();
        private AccountClient newAccountClient = new();
        private AccountClient loginAccountClient = new();

        public AccountClient LoginAccountClient
        {
            get => loginAccountClient;
            set
            {
                loginAccountClient = value;
                Signal();
            }
        }
        public AccountClient NewAccountClient
        {
            get => newAccountClient;
            set
            {
                newAccountClient = value;
                Signal();
            }
        }
        public Otvet Selectedotvet
        {
            get => selectedotvet;
            set
            {
                selectedotvet = value;
                Signal();
            }

        }
        public Message Selectedzauvka
        {
            get => selectedzauvka;
            set
            {
                selectedzauvka = value;
                Signal();
            }
        }
        public Message Newzauvka
        {
            get => newzauvka;
            set
            {
                newzauvka = value;
                Signal();
            }
        }
        public Otvet Newotvet
        {
            get => newotvet;
            set
            {
                newotvet = value;
                Signal();
            }
        }
        public CommandVM SendZauvka { get; }
        public CommandVM SendOtvet { get; }
        public CommandVM Registration { get; }
        public CommandVM LoginClient { get; }
        public ObservableCollection<Message> Message { get; set; }
        public ObservableCollection<Otvet> Otvet { get; set; }
        public ObservableCollection<AccountClient> AccountClient { get; set; }
        public MainWindowVM()
        {
            Message = new ObservableCollection<Message>(MessagesRepository.Instance.GetMessages());

            Otvet = new ObservableCollection<Otvet>(OtvetRepository.Instance.GetOtvet());

            AccountClient = new ObservableCollection<AccountClient>(AccountsClientRepository.Instance.GetAccounts());

            SendZauvka = new CommandVM(() =>
            {
                if (!string.IsNullOrEmpty(newzauvka.polomka) && !string.IsNullOrEmpty(newzauvka.ystroy) && !string.IsNullOrEmpty(newzauvka.comment))
                {
                    var connect = MySqlDB.Instance.GetConnection();

                    string sql = "INSERT INTO priem (Tag_polomki, Tag_Ystr, Comment) values (@Tag_polomki, @Tag_Ystr, @Comment)";
                    MySqlCommand command = new MySqlCommand(sql, connect);

                    command.Parameters.AddWithValue("@Tag_polomki", Newzauvka.polomka);
                    command.Parameters.AddWithValue("@Tag_Ystr", Newzauvka.ystroy);
                    command.Parameters.AddWithValue("@Comment", Newzauvka.comment);
                    command.ExecuteNonQuery();

                    connect.Close();
                }
                else
                {
                    MessageBox.Show("Необходимо заполнить все поля для отправки заявки", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });
            SendOtvet = new CommandVM(() =>
            {
                var connect = MySqlDB.Instance.GetConnection();

                string sql = "INSERT INTO otvet (Otvet, priem_idPriem) values (@Otvet, @priem_idPriem)";
                MySqlCommand command = new MySqlCommand(sql, connect);

                command.Parameters.AddWithValue("@Otvet", Newotvet.otvet);
                command.Parameters.AddWithValue("@priem_idPriem", Selectedzauvka.id);
                command.ExecuteNonQuery();

                connect.Close();
            });
            Registration = new CommandVM(() =>
            {
                bool accountExist = false;
                if (!string.IsNullOrEmpty(newAccountClient.Login) && !string.IsNullOrEmpty(newAccountClient.Password))
                {
                    if (AccountClient.Count != 0)
                    {
                        foreach (var account in AccountClient)
                        {
                            if (account.Login == newAccountClient.Login)
                            {
                                accountExist = true;
                                break;
                            }
                        }
                    }
                    else
                    {
                        accountExist = false;
                    }
                    if (!accountExist)
                    {
                        var connect = MySqlDB.Instance.GetConnection();

                        string sql = "INSERT accountsclients (Login, Password) VALUES (@Login, @Password);";
                        MySqlCommand command = new MySqlCommand(sql, connect);

                        command.Parameters.AddWithValue("@Login", newAccountClient.Login);
                        command.Parameters.AddWithValue("@Password", newAccountClient.Password);

                        command.ExecuteNonQuery();

                        connect.Close();
                        MessageBox.Show("Аккаунт зарегестрирован", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else if (accountExist)
                    {
                        MessageBox.Show("Аккаунт существует", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else if (string.IsNullOrEmpty(newAccountClient.Login) || string.IsNullOrEmpty(newAccountClient.Password))
                {
                    MessageBox.Show("Введите логин и пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                RefreshAccountsClient();
            });
            LoginClient = new CommandVM(() =>
            {
                bool loginSuccess = false;
                foreach (var account in AccountClient)
                {
                    if (account.Login == LoginAccountClient.Login && account.Password == LoginAccountClient.Password)
                    {
                        ClientPriem clientpriem = new ClientPriem();
                        clientpriem.Show();

                        Window windowLoginClient = Application.Current.Windows.OfType<WindowLoginClient>().FirstOrDefault();
                        windowLoginClient.Close();
                        loginSuccess = true;

                        Application.Current.Properties["idAccountsClients"] = account.IDAccountClient;

                        break;
                    }
                }

                if (!loginSuccess)
                {
                    MessageBox.Show("Данного аккаунта не существует", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });
        }
        private void RefreshAccountsClient()
        {
            ObservableCollection<AccountClient> updatedAccounts = AccountsClientRepository.Instance.GetAccounts();
            foreach (var account in updatedAccounts)
            {
                AccountClient.Add(account);
            }
        }
    } //тест
}
