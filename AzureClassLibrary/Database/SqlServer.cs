using Microsoft.Data.SqlClient;

namespace AzureClassLibrary.Database;

public class SqlServer
{
    private readonly SqlConnectionStringBuilder _builder;
    
    public SqlServer()
    {
        _builder = new SqlConnectionStringBuilder
        {
            DataSource     = Environment.GetEnvironmentVariable("SQL_SERVER_DATASOURCE") ?? "",
            UserID         = Environment.GetEnvironmentVariable("SQL_SERVER_USERID") ?? "",
            Password       = Environment.GetEnvironmentVariable("SQL_SERVER_PASSWORD") ?? "",
            InitialCatalog = Environment.GetEnvironmentVariable("SQL_SERVER_INITIAL_CATALOG") ?? "",
            TrustServerCertificate = true,
            Encrypt = false,
            ConnectTimeout = 30,
        };
    }

    public int Demo()
    {
        using (var conn = new SqlConnection(this._builder.ConnectionString))
        {
            conn.Open();
            
            // Read rows
            const string sql = "SELECT * FROM inventory";
            var command = new SqlCommand(sql, conn);
            command.ExecuteReader();
            
            Console.WriteLine("SQL Server SQL Executed");
        }

        return 0;
    }
}