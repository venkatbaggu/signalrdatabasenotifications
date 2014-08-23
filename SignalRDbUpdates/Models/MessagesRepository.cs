using SignalRDbUpdates.Hubs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SignalRDbUpdates.Models
{
    public class MessagesRepository
    {
        string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public IEnumerable<Messages> GetAllMessages()
        {
            List<Messages> messages = new List<Messages>();
            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(@"SELECT [MessageID], [Message], [Date] FROM [dbo].[Messages]", connection))
                {
                    command.Notification = null;

                    SqlDependency dependency = new SqlDependency(command);
                    dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);

                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        messages.Add(new Messages { MessageID = (int)reader["MessageID"], Message = (string)reader["Message"], MessageDate = Convert.ToDateTime(reader["Date"]) });
                    }
                }
              
            }
            return messages;
           
            
        }

        private void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Type == SqlNotificationType.Change)
            {
                MessagesHub.SendMessages();
            }
        }
    }
}