using Microsoft.Data.Sqlite;
using StiVisita.Interfaces;
using System.Collections;
using StiVisita.Common;

namespace StiVisita.Data
{
    public class DatabaseManager: IDbContext
    {
        public SqliteConnection? _connection;
        private SqliteDataReader? rdr;
        private SqliteCommand? command;
        private SqliteTransaction? transaction;

        private readonly string? _dbPath;

        public DatabaseManager() 
        {
            Console.WriteLine("###### DatabaseManager Receiving Database infos....!!! ######");
            _dbPath = GlobalVariables.DatabasePath;

            Console.WriteLine("###### DatabaseManager Starting a connnection....!!! ######");
            CreateConnection();

            Console.WriteLine("###### DatabaseManager Running db migrations!!! ######");
            RunMigrations();

            Console.WriteLine("###### DatabaseManager ready for Execute Queries....!!! ######");
        }

        // Destroy, Update, Create
        public void ExecuteVoidQuery(string query)
        {
            //await Task.Run(() => {});
            try
            {
                _connection?.Open();
                transaction = _connection?.BeginTransaction();
                command = _connection?.CreateCommand();
                command.CommandText = query;
                command.ExecuteNonQuery();
                transaction.Commit();
                _connection?.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());  
            }           
        }
        // FindAll, FindOne
        public List<Hashtable> ExecuteCallBackQuery(string query)
        {
            //await Task.Run(() => {});
            List<Hashtable> TableData = new List<Hashtable>();

            try
            {
                _connection?.Open();
                transaction = _connection?.BeginTransaction();
                command = _connection?.CreateCommand();
                command.CommandText = query;
                command.ExecuteNonQuery();
                rdr = command.ExecuteReader();
                while (rdr.Read())
                {
                    Hashtable row = new Hashtable();

                    for (int i = 0; i < rdr.FieldCount; i++)
                    {
                        row.Add(rdr.GetName(i), rdr.GetValue(i));
                    }
                    TableData.Add(row);
                }
                transaction.Commit();
                _connection?.Close();
                }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return TableData;
        }
        private SqliteConnection CreateConnection()
        {
            if (_connection == null)
            {
                _connection = new SqliteConnection("" + new SqliteConnectionStringBuilder { DataSource = _dbPath });
            }

            Console.WriteLine("****** DatabaseManager connection Started....!!! ******");
            return _connection;
        }
        private void RunMigrations()
        {
            foreach (var queryString in Schema.Migrations())
            {
                this.ExecuteVoidQuery(queryString.ToString());
            }
            Console.WriteLine("****** DatabaseManager migrations Up to Date!!! ******");
        }
    }
}