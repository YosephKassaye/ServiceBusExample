using Microsoft.Azure.ServiceBus;
using System;
using System.Data.SqlClient;
using System.Text;

namespace FromSQLtotheQueue_Sender
{
    class SenderConsole
    {
        static string ConnectionString = "";
        static string QueuePath = "";
        static string dbConnetionString = "";
        static void Main(string[] args)
        {
            var queueClient = new QueueClient(ConnectionString, QueuePath);
          
            
            using (SqlConnection myConnection = new SqlConnection(dbConnetionString))
            {
                string sqlQuery = "Select * from  DestTable";
                SqlCommand oCmd = new SqlCommand(sqlQuery, myConnection);                
                myConnection.Open();
                using (SqlDataReader oReader = oCmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        
                        var content = $"Message: {oReader["MessageDescription"].ToString()}";
                        var message = new Message(Encoding.UTF8.GetBytes(content));
                        queueClient.SendAsync(message).Wait();
                    }

                    myConnection.Close();
                }
            }
                        
            queueClient.CloseAsync().Wait();

        }
    }
}
