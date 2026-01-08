using System;
using System.Data.SQLite;
using System.IO;

namespace VibeMap.Utils
{
    public static class DbConnection
    {
        public static SQLiteConnection GetConnection(string dbName = "UserStore.db")
        {
            string dbFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Database");
            if (!Directory.Exists(dbFolder)) Directory.CreateDirectory(dbFolder);

            string dbPath = Path.Combine(dbFolder, dbName);
            string connectionString = $"Data Source={dbPath};Version=3;Busy Timeout=5000;Journal Mode=WAL;";

            return new SQLiteConnection(connectionString);
        }

        public static SQLiteConnection GetUserConnection() => GetConnection("UserStore.db");
        public static SQLiteConnection GetCatalogConnection() => GetConnection("CatalogStore.db");
    }
}
