using Kyrsovau_Rabota.Model;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Kyrsovau_Rabota.ViewModel
{
    public class MainWindowVM : BaseVM
    {
        private Message newzauvka = new();
        private Message selectedzauvka;
        private Otvet selectedotvet;
        private Otvet newotvet = new();

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
        public ObservableCollection<Message> Message { get; set; }
        public ObservableCollection<Otvet> Otvet { get; set; }
        public MainWindowVM()
        {
            Message = new ObservableCollection<Message>(MessagesRepository.Instance.GetMessages());

            Otvet = new ObservableCollection<Otvet>(OtvetRepository.Instance.GetOtvet());

            SendZauvka = new CommandVM(() =>
            {
                var connect = MySqlDB.Instance.GetConnection();

                string sql = "INSERT INTO priem (Tag_polomki, Tag_Ystr, Comment) values (@Tag_polomki, @Tag_Ystr, @Comment)";
                MySqlCommand command = new MySqlCommand(sql, connect);

                command.Parameters.AddWithValue("@Tag_polomki", Newzauvka.polomka);
                command.Parameters.AddWithValue("@Tag_Ystr", Newzauvka.ystroy);
                command.Parameters.AddWithValue("@Comment", Newzauvka.comment);
                command.ExecuteNonQuery();

                connect.Close();
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
        }
    }
}
