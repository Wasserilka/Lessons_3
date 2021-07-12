using System.Data.SQLite;

namespace MetricsAgent.DAL
{
    interface IConnectionManager
    {
        const string ConnectionString = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";
        static SQLiteConnection connection;

        static public SQLiteConnection GetOpenedConnection()
        {
            connection = new SQLiteConnection(ConnectionString);
            connection.Open();
            return connection;
        }
    }
}
