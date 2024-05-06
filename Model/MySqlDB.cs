﻿using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Kyrsovau_Rabota.Model
{
    public class MySqlDB
    {
        MySqlConnection mySqlConnection;

        private MySqlDB()
        {
            MySqlConnectionStringBuilder stringBuilder = new();
            stringBuilder.UserID = "root";
            stringBuilder.Password = "Sal5mo5ne5lla!";
            stringBuilder.Database = "kyrsovaubd";
            stringBuilder.Server = "127.0.0.1";
            stringBuilder.CharacterSet = "utf8mb4";
            mySqlConnection = new MySqlConnection(stringBuilder.ToString());
            OpenConnection();
        }

        private bool OpenConnection()
        {
            try
            {
                mySqlConnection.Open();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public void CloseConnection()
        {
            try
            {
                mySqlConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        internal MySqlConnection GetConnection()
        {
            if (mySqlConnection.State != System.Data.ConnectionState.Open)
                if (!OpenConnection())
                    return null;

            return mySqlConnection;
        }

        static MySqlDB instance;
        public static MySqlDB Instance
        {
            get
            {
                if (instance == null)
                    instance = new MySqlDB();
                return instance;
            }
        }

        public int GetAutoID(string table)
        {
            try
            {
                string sql = "SHOW TABLE STATUS WHERE `Name` = '" + table + "'";
                using (var mc = new MySqlCommand(sql, mySqlConnection))
                using (var reader = mc.ExecuteReader())
                {
                    if (reader.Read())
                        return reader.GetInt32("Auto_increment");
                }
                return -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
        }

        //public void Test()
        //{
        //    try
        //    { 
        //        // безопасное выполнение кода, 
        //        // если произойдет ошибка, она будет перехвачена и выполнится блок catch
        //        mySqlConnection.Open();
        //        string str = "С'ироп";
        //        string sql = "INSERT INTO TagsTable VALUES (0, @tag)";
        //        using (MySqlCommand cmd = new MySqlCommand(sql, mySqlConnection))
        //        {
        //            cmd.Parameters.Add(new MySqlParameter("tag", str));
        //            cmd.ExecuteNonQuery();
        //        }

        //        mySqlConnection.Close();
        //    }
        //    catch (Exception ex) 
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}
    }
}
