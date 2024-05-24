using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;

namespace Kyrsovau_Rabota.Model
{
    internal class AccountsUsersRepository
    {
        private AccountsUsersRepository()
        {

        }
        static AccountsUsersRepository instance;
        public static AccountsUsersRepository Instance
        {
            get
            {
                if (instance == null)
                    instance = new AccountsUsersRepository();
                return instance;
            }
        }

        internal ObservableCollection<AccountUser> GetAccountsUser()
        {
            ObservableCollection<AccountUser> result = new ObservableCollection<AccountUser>();
            var connect = MySqlDB.Instance.GetConnection();
            if (connect == null)
                return result;

            string sql = "SELECT * FROM accountssotrydnik";
            using (var mc = new MySqlCommand(sql, connect))
            using (var reader = mc.ExecuteReader())
            {
                while (reader.Read())
                {
                    var account = new AccountUser
                    {
                        IDAccountUser = reader.GetInt32("idAccountsSotrydnik"),
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
