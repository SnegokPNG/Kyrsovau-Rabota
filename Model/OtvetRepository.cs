using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;

namespace Kyrsovau_Rabota.Model
{
    internal class OtvetRepository
    {
        private OtvetRepository()
        {

        }
        static OtvetRepository instance;
        public static OtvetRepository Instance
        {
            get
            {
                if (instance == null)
                    instance = new OtvetRepository();
                return instance;
            }
        }

        internal ObservableCollection<Otvet> GetOtvet()
        {
            ObservableCollection<Otvet> result = new ObservableCollection<Otvet>();
            var connect = MySqlDB.Instance.GetConnection();
            if (connect == null)
                return result;

            string sql = "SELECT * FROM otvet";
            using (var mc = new MySqlCommand(sql, connect))
            using (var reader = mc.ExecuteReader())
            {
                while (reader.Read())
                {
                    var Otvet = new Otvet
                    {
                        id = reader.GetInt32("idOtvet"),
                        otvet = reader.GetString("Otvet"),
                        idpriem = reader.GetInt32("priem_idPriem"),
                        iduser = reader.GetInt32("IdUser")
                    };
                    result.Add(Otvet);
                }
            }
            return result;
        }
    }
}
