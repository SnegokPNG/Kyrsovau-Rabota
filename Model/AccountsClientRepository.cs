using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kyrsovau_Rabota.Model
{
    internal class AccountsClientRepository
    {
        private AccountsClientRepository()
        {

        }
        static AccountsClientRepository instance;
        public static AccountsClientRepository Instance
        {
            get
            {
                if (instance == null)
                    instance = new AccountsClientRepository();
                return instance;
            }
        }

        internal ObservableCollection<AccountClient> GetAccounts()
        {
            ObservableCollection<AccountClient> result = new ObservableCollection<AccountClient>();
            var connect = MySqlDB.Instance.GetConnection();
            if (connect == null)
                return result;

            string sql = "SELECT * FROM accountsclients";
            using (var mc = new MySqlCommand(sql, connect))
            using (var reader = mc.ExecuteReader())
            {
                while (reader.Read())
                {
                    var account = new AccountClient
                    {
                        IDAccountClient = reader.GetInt32("idAccountsClients"),
                        Login = reader.GetString("Login"),
                        Password = reader.GetString("Password")
                    };
                    result.Add(account);
                }
            }
            return result;
        }
    }
}
