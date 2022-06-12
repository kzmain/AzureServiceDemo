using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace AzureClassLibrary.Database;

public class MySqlServer
{
    public MySqlServer()
    {
    }

    public int Demo()
    {
        string connectionString = Environment.GetEnvironmentVariable("MYSQL_CONNECTION_STRING") ?? "";

        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            // Connect to the database
            conn.Open();

            // Read rows
            String sql = "SELECT * FROM inventory";
            MySqlCommand selectCommand = new MySqlCommand(sql, conn);
            MySqlDataReader results = selectCommand.ExecuteReader();

            // Enumerate over the rows
            while (results.Read())
            {
                Console.WriteLine(JsonConvert.SerializeObject(results));
            }
        }

        return 0;
    }
}