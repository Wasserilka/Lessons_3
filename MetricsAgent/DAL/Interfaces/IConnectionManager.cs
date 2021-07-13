using System.Data.SQLite;

namespace MetricsAgent.DAL
{
    interface IConnectionManager
    {
        public string ConnectionString { get; }
        public SQLiteConnection connection { get; set; }

        public SQLiteConnection GetOpenedConnection();
    }

    class ConnectionManager : IConnectionManager
    {
        public string ConnectionString { get; }
        public SQLiteConnection connection { get; set; }

        public ConnectionManager()
        {
            ConnectionString = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";
        }

        public SQLiteConnection GetOpenedConnection()
        {
            connection = new SQLiteConnection(ConnectionString);
            connection.Open();
            return connection;
        }
    }
}
