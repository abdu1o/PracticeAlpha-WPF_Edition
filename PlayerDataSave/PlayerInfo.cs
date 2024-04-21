using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace PracticeAlpha_WPF_Edition.PlayerDataSave
{
    internal class PlayerInfo
    {
        public string Name {  get; set; }

        static public void PushInfo(string name, string points, string time)
        {
            int PID = 1;
            string connectionString = "Data Source=D:\\TEST\\PA\\PracticeAlpha-WPF_Edition\\Resources\\DataBase\\Player.db;Version=3;";
            // string connectionString = "Data Source=C:\\Users\\akapa\\source\\repos\\PracticeAlpha-WPF_Edition\\Resources\\DataBase\\Player.db;Version=3;";
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = $"SELECT ID FROM Login WHERE Name = '{name}'";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PID = reader.GetInt32(0);
                        }
                    }
                    command.CommandText = $"UPDATE Score SET Points = '{points}', Time = '{time}' WHERE Player_ID = {PID}";
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }
    }
}
