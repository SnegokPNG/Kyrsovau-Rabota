using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kyrsovau_Rabota.Model
{
    internal class MessagesRepository
    {
        private MessagesRepository()
        {

        }
        static MessagesRepository instance;
        public static MessagesRepository Instance
        {
            get
            {
                if (instance == null)
                    instance = new MessagesRepository();
                return instance;
            }
        }

        internal ObservableCollection<Message> GetMessages()
        {
            ObservableCollection<Message> result = new ObservableCollection<Message>();
            var connect = MySqlDB.Instance.GetConnection();
            if (connect == null)
                return result;

            string sql = "SELECT * FROM priem";
            using (var mc = new MySqlCommand(sql, connect))
            using (var reader = mc.ExecuteReader())
            {
                while (reader.Read())
                {
                    var Message = new Message
                    {
                        id = reader.GetInt32("idPriem"),
                         ystroy = reader.GetString("Tag_Ystr"),
                         polomka = reader.GetString("Tag_Polomki"),
                         comment = reader.GetString("Comment")
                    };
                    result.Add(Message);
                }
            }
            return result;
        }
    }
}
