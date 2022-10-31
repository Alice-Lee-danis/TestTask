using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Windows.Forms;

namespace Test
{
    public class TestService : ITestService
    {
        private int mouseX = 0;
        private int MouseY = 0;
        Data data = new Data();

        public List<EventsClient> eventsClients = new List<EventsClient>();

        public List<EventsClient> Recording(int cursorX, int cursorY, string eventClick, DateTime dateTime)
        {
            //var connection = new SQLiteConnection(@"Data Source = ClientEvents.db;Version=3");

            if (mouseX + 10 <= cursorX || MouseY + 10 <= cursorY || mouseX - 10 >= cursorX || MouseY - 10 >= cursorY)
            {
                mouseX = cursorX;
                MouseY = cursorY;


                using (var connections = new SQLiteConnection(@"Data Source = ClientEvents.db;Version=3"))
                {
                    try
                    {
                        SQLiteCommand command = new SQLiteCommand();
                        connections.Open();
                        command.Connection = connections;/*{data.convertDate(dateTime)}*/
                        command.CommandText = $"insert into eventsClient (id_client, eventX, eventY, eventClick, date) values ('1', '{mouseX}', '{MouseY}', '{eventClick}', '2022-10-31 20:15:00')";
                        command.ExecuteNonQuery();
                        connections.Close();
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message.ToString());

                    }
                }

                using (var connections = new SQLiteConnection(@"Data Source = ClientEvents.db;Version=3"))
                {
                    try
                    {
                        SQLiteCommand command = new SQLiteCommand();
                        connections.Open();
                        command.Connection = connections;
                        command.CommandText = $"select * from eventsClient";
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    var a = reader.GetValue(5).ToString();
                                    var c = reader.GetValue(5);

                                    eventsClients.Add(new EventsClient() { mouseClick = reader.GetValue(4).ToString(), mouseX = Convert.ToInt32(reader.GetValue(3).ToString()), mouseY = Convert.ToInt32(reader.GetValue(2).ToString()), date = Convert.ToDateTime(reader.GetValue(5)) });
                                }
                            }
                            connections.Close();
                        }
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message.ToString());
                        connections.Close();
                    }
                }
                    
                

            }
            return eventsClients;
        }

        
    }
}
