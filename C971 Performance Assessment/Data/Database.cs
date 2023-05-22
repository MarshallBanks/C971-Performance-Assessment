using System;
using System.IO;
using SQLite;

namespace C971_Performance_Assessment.Data
{
    public class Database
    {
        private static Database _instance;
        private readonly SQLiteAsyncConnection _database;

        private Database(string databasePath)
        {
            _database = new SQLiteAsyncConnection(databasePath);
            _database.CreateTableAsync<Term>().Wait();
            _database.CreateTableAsync<Course>().Wait();
            _database.CreateTableAsync<Assessment>().Wait();
        }

        public static Database GetInstance()
        {
            if (_instance == null)
            {
                string databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DegreePlannerDB.db3");
                _instance = new Database(databasePath);
            }
            return _instance;
        }

        public SQLiteAsyncConnection GetConnection()
        {
            return _database;
        }

    }
}
