using Kyrsovau_Rabota.Model;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kyrsovau_Rabota.ViewModel
{
    public class MainWindowVM : BaseVM
    {
        private Message newzauvka = new();
        

        public Message Newzauvka
        {
            get => newzauvka;
            set
            {
                newzauvka = value;
                Signal();
            }
        }
        public CommandVM SendZauvka { get; }
        public ObservableCollection<Message> Message { get; set; }
        public MainWindowVM()
        {
            Message = new ObservableCollection<Message>(MessagesRepository.Instance.GetMessages());

            SendZauvka = new CommandVM(() =>
            {
                var connect = MySqlDB.Instance.GetConnection();

                string sql = "INSERT INTO priem (Tag_polomki, Tag_Ystr, Comment) values (@Tag_polomki, @Tag_Ystr, @Comment)";
                MySqlCommand command = new MySqlCommand(sql, connect);

                command.Parameters.AddWithValue("@Tag_polomki", newzauvka.polomka);
                command.Parameters.AddWithValue("@Tag_Ystr", newzauvka.ystroy);
                command.Parameters.AddWithValue("@Comment", newzauvka.comment);
                command.ExecuteNonQuery();

                connect.Close();
            });
        }
    }
}
